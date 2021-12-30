using AutoMapper;
using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Application.Services.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Concrete
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        public async Task<IdentityResult> Register(RegisterDTO model)
        {
            var user = _mapper.Map<AppUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
            }

            return result;
        }

        public async Task<SignInResult> Login(LoginDTO model)
        {
            var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, false, false);
            return result;
        }
        public async Task LogOut()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UpdateProfileDTO> GetById(string id)
        {
            var user = await _unitOfWork.AppUserRepository.GetFilteredFirstOrDefault(
                selector: x => new GetAppUserVM
                {
                    Id = x.Id,
                    FullName = x.FullName,
                    UserName = x.UserName,
                    Password = x.PasswordHash,
                    ConfirmPassword = x.PasswordHash,
                    Email = x.Email
                },
                expression: x => x.Status != Domain.Enums.Status.Passive);

            var updatedUser = _mapper.Map<UpdateProfileDTO>(user);

            return updatedUser;
        }

        public async Task UpdateUser(UpdateProfileDTO model)
        {
            var user = await _unitOfWork.AppUserRepository.GetDefault(x => x.Id == model.Id);

            if (user != null)
            {
                if (model.Image != null)
                {
                    using var image = Image.Load(model.Image.OpenReadStream());
                    image.Mutate(x => x.Resize(256, 256));
                    image.Save("wwwroot/images/users/" + user.UserName + ".jpg");
                    user.Imagepath = ("/images/users/" + user.UserName + ".jpg");

                    if (model.FullName != null)
                    {
                        user.FullName = model.UserName;
                        _unitOfWork.AppUserRepository.Update(user);
                        await _unitOfWork.Commit();
                    }

                }

            }
            if (model.Password != null)
            {
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.Password);
                await _userManager.UpdateAsync(user);
            }

            if (model.UserName != null)
            {
                await _userManager.SetUserNameAsync(user, model.UserName);
            }
            if (model.Email != null)
            {
                await _userManager.SetEmailAsync(user, user.Email);
            }
        }  
    }
}

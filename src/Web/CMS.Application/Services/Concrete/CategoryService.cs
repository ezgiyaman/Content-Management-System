using AutoMapper;
using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Application.Services.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.Enums;
using CMS.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {

            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public async Task Create(CreateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            await _unitOfWork.CategoryRepository.Add(category);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.Id == id);
            category.Status = Status.Passive;
            category.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();
        }

        public async Task<UpdateCategoryDTO> GetById(int id)
        {

            var category = await _unitOfWork.CategoryRepository.GetFilteredFirstOrDefault(
                selector: x => new GetCategoryVM
                {

                    Id = x.Id,
                    Name = x.Name

                },
                expression: x => x.Id == id && x.Status != Status.Passive);

            var updatedCategory = _mapper.Map<UpdateCategoryDTO>(category);

            return updatedCategory;

        }

        public async Task Update(UpdateCategoryDTO model)
        {
            var category = _mapper.Map<Category>(model);

            _unitOfWork.CategoryRepository.Update(category);

            await _unitOfWork.Commit();
        }

        public async Task<List<GetCategoryVM>> GetCategories()
        {
            var categoryList = await _unitOfWork.CategoryRepository.GetFilteredList(
                 selector: x => new GetCategoryVM
                 {

                     Id = x.Id,
                     Name = x.Name,
                     Slug = x.Slug

                 },
                expression: x => x.Status != Status.Passive);

            return categoryList;
        }

        public async Task<Category> GetBySlug(string slug)
        {
            var category = await _unitOfWork.CategoryRepository.GetDefault(x => x.Slug == slug);
            return category;
        }




    }
}

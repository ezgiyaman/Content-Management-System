using AutoMapper;
using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Application.Services.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.Enums;
using CMS.Domain.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Concrete
{
    public class PageService : IPageService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        public PageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task Create(CreatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);

            await _unitOfWork.PageRepository.Add(page);

            await _unitOfWork.Commit();
        }

        public async Task<List<GetPageVM>> GetPages()
        {
            var pageList = await _unitOfWork.PageRepository.GetFilteredList(
                selector: x => new GetPageVM
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title,
                    Slug = x.Slug
                },

                expression: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Title));

            return pageList;
        }

        public async Task<UpdatePageDTO> GetById(int id)
        {
            var page = await _unitOfWork.PageRepository.GetFilteredFirstOrDefault(
                selector: x => new GetPageVM
                {
                    Id = x.Id,
                    Content = x.Content,
                    Title = x.Title
                },

                expression: x => x.Status != Status.Passive && x.Id == id);

            var updatePage = _mapper.Map<UpdatePageDTO>(page);

            return updatePage;

        }

        public async Task Update(UpdatePageDTO model)
        {
            var page = _mapper.Map<Page>(model);

            _unitOfWork.PageRepository.Update(page);

            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var page = await _unitOfWork.PageRepository.GetDefault(x => x.Id == id);

            page.Status = Status.Passive;
            page.DeleteDate = DateTime.Now;

            await _unitOfWork.Commit();
        }


        public async Task<Page> GetBySlug(string slug)
        {
            var page = await _unitOfWork.PageRepository.GetDefault(x => x.Slug == slug);
            return page;
        }


    }
}

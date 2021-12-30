using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Interface
{
    public interface IPageService
    {
        Task Create(CreatePageDTO model);
        Task Update(UpdatePageDTO model);
        Task Delete(int id);
        Task<UpdatePageDTO> GetById(int id);
        Task<List<GetPageVM>> GetPages();

        Task<Page> GetBySlug(string slug);


    }
}

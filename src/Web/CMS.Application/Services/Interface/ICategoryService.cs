using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Domain.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Interface
{
    public interface ICategoryService
    {
        Task Create(CreateCategoryDTO model);

        Task Update(UpdateCategoryDTO model);

        Task Delete(int id);

        Task<UpdateCategoryDTO> GetById(int id);

        Task<List<GetCategoryVM>> GetCategories();

        //Slug'a göre Category getirme
        Task<Category> GetBySlug(string slug);


    }
}

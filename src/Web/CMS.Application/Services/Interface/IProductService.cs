using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Interface
{
    public interface IProductService
    {
        Task Create(CreateProductDTO model);

        Task Update(UpdateProductDTO model);

        Task Delete(int id);
        Task<UpdateProductDTO> GetById(int id);

        Task<List<GetProductVM>> GetProducts();

        Task<List<GetProductVM>> GetProductsByCategory(int categoryId);

    }
}

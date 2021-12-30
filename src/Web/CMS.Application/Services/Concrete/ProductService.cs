using AutoMapper;
using CMS.Application.Models.DTOs;
using CMS.Application.Models.VMs;
using CMS.Application.Services.Interface;
using CMS.Domain.Entities.Concrete;
using CMS.Domain.Enums;
using CMS.Domain.UnitOfWork;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Application.Services.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;
        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
        public async Task Create(CreateProductDTO model)
        {
            var product = _mapper.Map<Product>(model);

            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                image.Save("wwwroot/images/products/" + product.Name + ".jpg");
                product.ImagePath = ("/images/products/" + product.Name + ".jpg");
            }

            await _unitOfWork.ProductRepository.Add(product);

            await _unitOfWork.Commit();

        }
        public async Task<List<GetProductVM>> GetProducts()
        {
            var productList = await _unitOfWork.ProductRepository.GetFilteredList(
                selector: x => new GetProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    ImagePath = x.ImagePath,
                    Price = x.Price,
                    CategoryName = x.Category.Name
                },

                expression: x => x.Status != Status.Passive);

            return productList;

        }

        //Listeleme yaparken Category'leri Id'sinden yakaladım.

        public async Task<List<GetProductVM>> GetProductsByCategory(int categoryId)
        {
            var productByCategoryList = await _unitOfWork.ProductRepository.GetFilteredList(
                selector: x => new GetProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    CategoryName = x.Category.Name,
                    Price = x.Price,
                    ImagePath = x.ImagePath
                },
            expression: x => x.CategoryId == categoryId && x.Status != Status.Passive,

            orderBy: x => x.OrderBy(z => z.Name));

            return productByCategoryList;
        }

        public async Task<UpdateProductDTO> GetById(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetFilteredFirstOrDefault(
                selector: x => new GetProductVM
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price
                },
               expression: x => x.Status != Status.Passive);

            var updateProduct = _mapper.Map<UpdateProductDTO>(product);

            updateProduct.Categories = await _unitOfWork.CategoryRepository.GetFilteredList(selector: x => new GetCategoryVM
            {
                Id = x.Id,
                Name = x.Name
            },

             expression: x => x.Status != Status.Passive,
             orderBy: x => x.OrderBy(x => x.Name));

            return updateProduct;
        }


        public async Task Update(UpdateProductDTO model)
        {

            var product = _mapper.Map<Product>(model);
            if (model.Image != null)
            {
                using var image = Image.Load(model.Image.OpenReadStream());
                image.Mutate(x => x.Resize(256, 256));
                image.Save("wwwroot/images/products/" + product.Name + ".jpg");
                product.ImagePath = ("/images/products/" + product.Name + ".jpg");
            }

            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.Commit();
        }

        public async Task Delete(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetDefault(x => x.Id == id);
            product.DeleteDate = DateTime.Now;
            product.Status = Status.Passive;
            await _unitOfWork.Commit();

        }
 
        
    }
}

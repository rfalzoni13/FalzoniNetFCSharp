using FalzoniNetFCSharp.Domain.DTO.Stock;
using FalzoniNetFCSharp.Domain.Interfaces.Base;
using FalzoniNetFCSharp.Domain.Interfaces.Stock;
using FalzoniNetFCSharp.Service.Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FalzoniNetFCSharp.Service.Stock
{
    public class ProductService : ServiceBase<ProductDTO>
    {
        private readonly IProductRepository _productRepository;
        //private readonly IProductCategoryRepository _productCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IProductRepository productRepository, 
            //IProductCategoryRepository productCategoryRepository,
            IUnitOfWork unitOfWork)
        {
            _productRepository = productRepository;
            //_productCategoryRepository = productCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public override ProductDTO Get(Guid Id)
        {
            var product = _productRepository.Get(Id);

            return new ProductDTO(product);
        }

        public override IEnumerable<ProductDTO> GetAll()
        {
            var products = _productRepository.GetAll();

            return products.ToList().ConvertAll(c => new ProductDTO(c));
        }

        public override void Add(ProductDTO productDTO)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    productDTO.ConfigureNewEntity();

                    var product = productDTO.ConvertToEntity();

                    _productRepository.Add(product);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public override void Update(ProductDTO productDTO)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    var product = _productRepository.Get(productDTO.Id);

                    // Update principal data
                    product.Name = productDTO.Name;
                    product.Description = productDTO.Description;
                    product.Price = productDTO.Price;

                    // Update modified entity data
                    product.Modified = DateTime.Now;

                    _productRepository.Update(product);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        public override void Delete(Guid Id)
        {
            using (var transaction = _unitOfWork.BeginTransaction())
            {
                try
                {
                    _productRepository.Delete(Id);

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }
    }
}

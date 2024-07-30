using FalzoniNetFCSharp.Service.Stock;
using FalzoniNetFCSharp.Domain.DTO.Stock;
using System.Collections.Generic;
using System;

namespace FalzoniNetFCSharp.Application.ServiceApplication.Stock
{
    public class ProductServiceApplication
    {
        private readonly ProductService _productService;

        public ProductServiceApplication(ProductService productService)
        {
            _productService = productService;
        }

        public ProductDTO Get(Guid Id)
        {
            if (Id == Guid.Empty)
                throw new ApplicationException("Erro ao buscar usuário!");

            return _productService.Get(Id);
        }

        public List<ProductDTO> GetAll() => _productService.GetAll();


        public void Add(ProductDTO productDTO)
        {
            Validate(productDTO);

            _productService.Add(productDTO);
        }

        public void Update(ProductDTO productDTO)
        {
            Validate(productDTO);

            _productService.Update(productDTO);
        }

        public void Delete(ProductDTO productDTO)
        {
            if (productDTO.Id == Guid.Empty)
                throw new ApplicationException("Erro ao buscar usuário!");

            _productService.Delete(productDTO);
        }

        // Private METHODS
        private void Validate(ProductDTO productDTO)
        {
            if (string.IsNullOrEmpty(productDTO.Name)) throw new ApplicationException("Necessário informar o nome do Produto");

            if (productDTO.Code <= 0) throw new ApplicationException("Necessário informar o código do produto");

            if (string.IsNullOrEmpty(productDTO.Description)) throw new ApplicationException("Necessário informar a desrição do produto");
            
            if (productDTO.Price <= 0) throw new ApplicationException("Necessário estabelecer o preço do produto");
            
            if (productDTO.ProductCategoryId == Guid.Empty) throw new ApplicationException("Necessário informar a categoria do produto");
        }
    }
}

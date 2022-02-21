using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Errors;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Infrastructre.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace API.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productsRepo;
        private readonly IGenericRepository<ProductType> _productsType;
        private readonly IGenericRepository<ProductBrand> _productsBrand;
        private readonly IMapper _mapper;
     
        public ProductsController(IGenericRepository<Product> productsRepo, IGenericRepository<ProductType> productsType,
         IGenericRepository<ProductBrand> productsBrand, IMapper mapper)
        {
            _mapper = mapper;
            _productsBrand = productsBrand;
            _productsType = productsType;
            _productsRepo = productsRepo;
           
        }
        
        [HttpGet]
        public async Task <ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
           
            var spec = new ProductswithTypesAndBrandsSpecifications();                         
            var products = await _productsRepo.ListAsync(spec);

            return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
            
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task <ActionResult<ProductToReturnDto>> GetProductsById(int id)
        {
            var spec = new ProductswithTypesAndBrandsSpecifications(id);  
            var product = await _productsRepo.GetEntityWithSpec(spec);
            if(product==null) return NotFound(new ApiResponse(404));
            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]

        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrand()
        {
            var result = await _productsBrand.ListAllAsync();
            return Ok(result);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductType()
        {
            var result = await _productsType.ListAllAsync();
            return Ok(result);
        }



    }
}
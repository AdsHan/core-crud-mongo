using CMO.Product.API.Application.DTO;
using CMO.Product.Domain.Entities;
using CMO.Product.Domain.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace CMO.Products.API.Controllers

{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        // GET: api/products
        /// <summary>
        /// Obtêm os produtos
        /// </summary>
        /// <returns>Coleção de objetos da classe Produto</returns>                
        /// <response code="200">Lista dos produtos</response>        
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">Nenhum produto foi localizado</response>         
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get()
        {
            var products = await _productRepository.GetAllAsync();

            if (products == null)
            {
                return NotFound();
            }

            return Ok(products);
        }

        // GET: api/products/5
        /// <summary>
        /// Obtêm as informações do produto pelo seu Id
        /// </summary>
        /// <param name="id">Código do produto</param>
        /// <returns>Objetos da classe Produto</returns>                
        /// <response code="200">Informações do Produto</response>
        /// <response code="400">Falha na requisição</response>            
        /// <response code="404">O produto não foi localizado</response>         
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        // POST api/products/
        /// <summary>
        /// Grava o produto
        /// </summary>   
        /// <remarks>
        /// Exemplo request:
        ///
        ///     POST / Produto
        ///     {
        ///         "title": "Sandalia",
        ///         "description": "Sandália Preta Couro Salto Fino",
        ///         "price": 249.50,
        ///         "quantity": 100       
        ///     }
        /// </remarks>        
        /// <returns>Retorna objeto criado da classe Produto</returns>                
        /// <response code="201">O produto foi incluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ActionName("NewProduct")]
        public async Task<IActionResult> PostAsync([FromBody] ProductDTO productDTO)
        {
            var product = new ProductModel()
            {
                Title = productDTO.Title,
                Description = productDTO.Description,
                Price = productDTO.Price,
                Quantity = productDTO.Quantity
            };

            await _productRepository.AddAsync(product);

            return CreatedAtAction("NewProduct", new { id = product.Id }, product);
        }

        // PUT: api/products/5
        /// <summary>
        /// Altera o produto
        /// </summary>        
        /// <param name="id">Código do produto</param>        
        /// <response code="204">O produto foi alterado corretamente</response>                
        /// <response code="400">Falha na requisição</response>       
        /// <response code="404">O produto não foi localizado</response>         
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] ProductDTO productDTO)
        {
            if (id != productDTO.Id)
            {
                return BadRequest();
            }

            var product = await _productRepository.GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            product.Update(productDTO.Title, productDTO.Description, productDTO.Price, productDTO.Quantity);

            try
            {
                var result = await _productRepository.UpdateAsync(product);
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                var isProductExists = await ProductModelExistsAsync(id);

                if (!isProductExists)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest();
                }
            }
        }

        // DELETE: api/products/5
        /// <summary>
        /// Deleta o produto pelo seu Id
        /// </summary>        
        /// <param name="id">Código do produto</param>        
        /// <response code="204">O produto foi excluído corretamente</response>                
        /// <response code="400">Falha na requisição</response>         
        /// <response code="404">O produto não foi localizado</response>         
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var isProductExists = await ProductModelExistsAsync(id);

            if (!isProductExists)
            {
                return NotFound();
            }

            var result = await _productRepository.DeleteAsync(id);

            if (result.DeletedCount == 0)
            {
                return BadRequest();
            }
            else
            {
                return NoContent();
            }
        }

        private async Task<bool> ProductModelExistsAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return product != null ? true : false;
        }
    }
}

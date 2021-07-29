using CMO.Product.Domain.Entities;
using CMO.Product.Domain.Repositories;
using System.Threading.Tasks;

namespace CMO.Product.API.Service
{
    public class ProductPopulateService
    {
        private readonly IProductRepository _productRepository;

        public ProductPopulateService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public void Initialize()
        {

            CreateUserAsync(new ProductModel()
            {
                Title = "Sandalia",
                Description = "Sandália Preta Couro Salto Fino",
                Price = 249.50,
                Quantity = 100
            });

            CreateUserAsync(new ProductModel()
            {
                Title = "Sapatilha",
                Description = "Sapatilha Tecido Platino ",
                Price = 142.50,
                Quantity = 25
            });

            CreateUserAsync(new ProductModel()
            {
                Title = "Chinelo",
                Description = "Chinelo Tradicional Adulto-Unissex",
                Price = 60.50,
                Quantity = 50
            });

        }

        private async Task CreateUserAsync(ProductModel product)
        {

            if (_productRepository.GetByIdAsync(product.Id).Result == null)
            {
                var resultado = _productRepository.AddAsync(product);
            }

        }
    }
}
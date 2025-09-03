using Marten.Schema;

namespace Catalog.API.Data
{
    public class CatalogInitialData : IInitialData
    {
        public async Task Populate(IDocumentStore store, CancellationToken cancellation)
        {
            using var session = store.LightweightSession();
            // Add initial data seeding logic here if needed
            if (await session.Query<Product>().AnyAsync())
                return;

            // Marten upsert will cater for existing records
            session.Store<Product>(GetPreConfiguredProducts());
            await session.SaveChangesAsync(cancellation);
        }

        private IEnumerable<Product> GetPreConfiguredProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = new Guid("63d490f4-3f5e-4d6e-8b8c-1f2b3c4d5e6f"),
                    Name = "IPhone X",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Price = 950.00M,
                    Category = new List<string> { "Smart Phone" },
                    ImageFile = "product-1.png"
                },
                new Product()
                {
                    Id = new Guid("73d490f4-3f5e-4d6e-8b8c-1f2b3c4d5e6f"),
                    Name = "Samsung 10",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Price = 840.00M,
                    Category = new List<string> { "Smart Phone" },

                    ImageFile = "product-2.png"
                },
                new Product()
                {
                    Id = new Guid("83d490f4-3f5e-4d6e-8b8c-1f2b3c4d5e6f"),
                    Name = "Huawei Plus",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Price = 650.00M,
                    Category = new List<string> { "Smart Phone" },

                    ImageFile = "product-3.png"
                },
                new Product()
                {
                    Id = new Guid("93d490f4-3f5e-4d6e-8b8c-1f2b3c4d5e6f"),
                    Name = "Xiaomi Mi 9",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Price = 470.00M,
                    Category = new List<string> { "Smart Phone" },

                    ImageFile = "product-4.png"
                },
                new Product()
                {
                    Id = new Guid("a3d490f4-3f5e-4d6e-8b8c-1f2b3c4d5e6f"),
                    Name = "HTC U11+ Plus",
                    Description = "This phone is the company's biggest change to its flagship smartphone in years. It includes a borderless.",
                    Price = 380,
                    Category = new List<string> { "Smart Phone" },
                    ImageFile = "product-5.png"
                }
            };
        }

    }

}

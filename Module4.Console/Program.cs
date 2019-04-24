using Module4.Client.Console.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace Module4.Console
{
    class Program
    {
        const string HostIP = "127.0.0.1";
        const string ApiHost = "http://127.0.0.1:5000/";
        static HttpClient client = new HttpClient();

        static Program()
        {
            InitializeHttpClient();
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to console client!");

            //Wait Till Host Gets Ready
            Thread.Sleep(5000);

            PrintCategories();
            PrintProducts();

            System.Console.WriteLine("Press any key to quit!");
            System.Console.ReadLine();
        }

        private static void InitializeHttpClient()
        {
            client.BaseAddress = new Uri(ApiHost);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        private static void PrintCategories()
        {
            var categories = GetCategoryAsync().GetAwaiter().GetResult();
            System.Console.WriteLine("\nCategories");
            foreach (var category in categories)
            {
                ShowCategory(category);
            }
        }

        private static void PrintProducts()
        {
            var products = GetProductAsync().GetAwaiter().GetResult();
            System.Console.WriteLine("\nProducts");
            foreach (var product in products)
            {
                ShowProduct(product);
            }
        }

        private static async Task<List<Category>> GetCategoryAsync()
        {
            List<Category> product = null;
            HttpResponseMessage response = await client.GetAsync("api/category");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<Category>>();
            }

            return product;
        }

        private static async Task<List<Product>> GetProductAsync()
        {
            List<Product> product = null;
            HttpResponseMessage response = await client.GetAsync("api/product");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<Product>>();
            }

            return product;
        }
        private static void ShowCategory(Category category)
        {
            System.Console.WriteLine($"{nameof(category.CategoryId)}: {category.CategoryId}\t{nameof(category.CategoryName)}: {category.CategoryName}\t{nameof(category.Description)}: {category.Description}");
        }

        private static void ShowProduct(Product product)
        {
            System.Console.WriteLine($"{nameof(product.ProductName)}: {product.ProductName}\t{nameof(product.UnitPrice)}: {product.UnitPrice}\t{nameof(product.Category.CategoryName)}: {product.Category.CategoryName}");
        }
    }
}

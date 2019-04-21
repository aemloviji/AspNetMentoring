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
        static HttpClient client = new HttpClient();
        static Program()
        {
            InitializeHttpClient();
        }

        static void Main(string[] args)
        {
            System.Console.WriteLine("Welcome to console client!");

            //wait for api server to start
            Thread.Sleep(5000);

            var res = GetProductAsync().GetAwaiter().GetResult();

            System.Console.WriteLine("Press any key to quit!");
            System.Console.ReadLine();
        }
        private static void InitializeHttpClient()
        {
            client.BaseAddress = new Uri("http://localhost:44306/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        private static async Task<List<Catergory>> GetCategoryAsync<Catergory>()
        {
            List<Catergory> product = null;
            HttpResponseMessage response = await client.GetAsync("api/category");
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<List<Catergory>>();
            }

            return product;
        }

        private static void ShowProduct(Product product)
        {
            System.Console.WriteLine($"Name: {product.Name}\tPrice: {product.Price}\tCategory: {product.Category}");
        }

        private static void ShowCategory(Category category)
        {
            // System.Console.WriteLine($"Name: {category.Name}\tPrice: {category.Price}\tCategory: {category.Category}");
        }
    }
}

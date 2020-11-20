using ERPSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ERPSystem.Services
{
    public class InventoryService
    {
        private HttpClient _client;
        private const string _productsUri = "api/Products/";
        private const string _categoriesUri = "api/Categories/";
        private const string _productsByCategory = "api/Products/category/";


        public InventoryService(HttpClient httpClient)
        {
            _client = httpClient;
        }

        public async Task<Product> GetProductAsync(int id)
        {
            Product product = null;
            HttpResponseMessage response = await _client.GetAsync(_productsUri+id.ToString());
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public async Task<Category> GetCategoryAsync(int id)
        {
            Category category = null;
            HttpResponseMessage response = await _client.GetAsync(_categoriesUri + id.ToString());
            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }
            return category;
        }



        public async Task<Product> CreateProductAsync(Product product)
        {
            Product newProduct = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync(_productsUri, product);
            if (response.IsSuccessStatusCode)
            {
                newProduct = await response.Content.ReadAsAsync<Product>();
            }
            return newProduct;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            Category newCategory = null;
            HttpResponseMessage response = await _client.PostAsJsonAsync(_categoriesUri, category);
            if (response.IsSuccessStatusCode)
            {
                newCategory = await response.Content.ReadAsAsync<Category>();
            }
            return newCategory;
        }

        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            List<Product> products = null;
            HttpResponseMessage response = await _client.GetAsync(_productsUri);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<Product>>();
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(int categoryId)
        {
            List<Product> products = null;
            HttpResponseMessage response = await _client.GetAsync(_productsByCategory+categoryId);
            if (response.IsSuccessStatusCode)
            {
                products = await response.Content.ReadAsAsync<List<Product>>();
            }
            return products;
        }



        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            List<Category> categories = null;
            HttpResponseMessage response = await _client.GetAsync(_categoriesUri);
            if (response.IsSuccessStatusCode)
            {
                categories = await response.Content.ReadAsAsync<List<Category>>();
            }
            return categories;
        }

        public async Task<bool> UpdateProductAsync(int id, Product product)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(_productsUri+id, product);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateCategoryAsync(int id, Category category)
        {
            HttpResponseMessage response = await _client.PutAsJsonAsync(_categoriesUri + id, category);
            //response.EnsureSuccessStatusCode();
            //// Deserialize the updated product from the response body.
            //category = await response.Content.ReadAsAsync<Category>();
            //return category;
            return response.IsSuccessStatusCode;
        }
    

        public async Task<Product> DeleteProductAsync(int id)
        {
            Product product = null;
            HttpResponseMessage response = await _client.DeleteAsync(_productsUri + id);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Product>();
            }
            return product;
        }

        public async Task<Category> DeleteCategoryAsync(int id)
        {
            //HttpResponseMessage response = await _client.DeleteAsync(_categoriesUri + id);
            //return response.StatusCode;
            Category category = null;
            HttpResponseMessage response = await _client.DeleteAsync(_categoriesUri + id);
            if (response.IsSuccessStatusCode)
            {
                category = await response.Content.ReadAsAsync<Category>();
            }
            return category;
        }
    }
}

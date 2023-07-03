using System.Text;
using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/products/";
    private ProductViewModel productViewModel;
    private IEnumerable<ProductViewModel> productsViewModel;
    private const string HTTP_CLIENT_PRODUCT_API = "ProductApi";

    public ProductService(IHttpClientFactory httpClient)
    {
        _httpClient = httpClient;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            productsViewModel = await JsonSerializer.DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _options);
        }

        return productsViewModel;
    }

    public async Task<ProductViewModel> FindProductById(int id)
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
        }

        return productViewModel;
    }
    public async Task<ProductViewModel> CreateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        StringContent content = new StringContent(JsonSerializer.Serialize(productViewModel), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            productViewModel = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
        }

        return productViewModel;
    }

    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productViewModel)
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        ProductViewModel productUpdated = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, productViewModel))
        {
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var apiResponse = await response.Content.ReadAsStreamAsync();
            productUpdated = await JsonSerializer.DeserializeAsync<ProductViewModel>(apiResponse, _options);
        }

        return productUpdated;
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }
    }
}

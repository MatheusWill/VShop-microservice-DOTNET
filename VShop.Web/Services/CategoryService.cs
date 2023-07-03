using System.Text.Json;
using VShop.Web.Models;
using VShop.Web.Services.Interfaces;

namespace VShop.Web.Services;

public class CategoryService : ICategoryService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly JsonSerializerOptions _options;
    private const string apiEndpoint = "/api/categories/";
    private const string HTTP_CLIENT_PRODUCT_API = "ProductApi";

    public CategoryService(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory;
        _options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }
    public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
    {
        var client = _httpClient.CreateClient(HTTP_CLIENT_PRODUCT_API);

        IEnumerable<CategoryViewModel> categories;

        var response = await client.GetAsync(apiEndpoint);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        var apiResponse = await response.Content.ReadAsStreamAsync();
        categories = await JsonSerializer
            .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);

        return categories;
    }
}

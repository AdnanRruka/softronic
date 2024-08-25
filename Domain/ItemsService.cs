using Api.Domain;
using Api.Models;
using System.Text.Json;

namespace ItemService.Domain
{
    public class ItemsService : IItemsService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl;
        private readonly string _apiKey;

        public ItemsService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _apiUrl = configuration["ApiSettings:Url"];
            _apiKey = configuration["ApiSettings:Key"];
        }

        public async Task<IEnumerable<ItemDto>> GetItemsAsync()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_apiUrl}/api/fetch");
            request.Headers.Add("X-Functions-Key", _apiKey);

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Failed to fetch items: {response.StatusCode}");
            }

            var content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<IEnumerable<ItemDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? null;
        }

        public async Task<IEnumerable<ItemDto>> GetSelectedItemsAsync(List<string> itemIds)
        {
            var items = await GetItemsAsync();
            var selectedItems = items.Where(item => itemIds.Contains(item.Id));

            return selectedItems;
        }
    }
}

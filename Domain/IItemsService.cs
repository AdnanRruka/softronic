using Api.Models;

namespace Api.Domain
{
    public interface IItemsService
    {
        public Task<IEnumerable<ItemDto>> GetItemsAsync();
        public Task<IEnumerable<ItemDto>> GetSelectedItemsAsync(List<string> ids);
    }
}

using Api.Domain;
using Api.Models;
using Microsoft.AspNetCore.Mvc;
using ItemService;
using ItemService.Domain;

namespace Api.Controllers
{
    [ApiController]
    [Route("items")]
    public class ItemsController : ControllerBase
    {
        private readonly IItemsService _itemsService;
        public ItemsController(IItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Get()
        {
            var items = await _itemsService.GetItemsAsync();
            return Ok(items);
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<ItemDto>>> Post([FromForm]List<string> items)
        {
            var selectedItems = await _itemsService.GetSelectedItemsAsync(items);
            return Ok(selectedItems);
        }
    }
}
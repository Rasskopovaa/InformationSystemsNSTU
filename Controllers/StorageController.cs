using Microsoft.AspNetCore.Mvc;
using lab1.Storage;

namespace lab1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly StorageService _storageService;

        public StorageController(StorageService storageService)
        {
            _storageService = storageService;
        }

        [HttpGet]
        [Route("storageType")]
        public ActionResult<string> GetStorageType()
        {
            return Ok(_storageService.GetStorageType());
        }

        [HttpGet]
        [Route("itemsCount")]
        public ActionResult<string> GetItemsCount()
        {
            return Ok(_storageService.GetNumberOfItems());
        }
    }
}
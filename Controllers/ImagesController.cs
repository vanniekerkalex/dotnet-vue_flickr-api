using Microsoft.AspNetCore.Mvc;
using planday.Services.ImagesService;

namespace planday.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class ImagesController : ControllerBase
  {
    private readonly ImagesService _imagesService;

    public ImagesController(ImagesService imagesService)
    {
      _imagesService = imagesService;
    }

    [HttpGet]
    public IActionResult Get(string tags = "")
    {
      return Ok(_imagesService.Search(tags).Result);
    }

    [HttpPost]
    public IActionResult Post(string tags = "")
    {
      return Ok(_imagesService.Search(tags).Result);
    }

  }
}

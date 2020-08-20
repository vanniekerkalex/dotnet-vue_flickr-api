﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public IActionResult Get()
    {

      return Ok(_imagesService.GetImages().Result);
    }

    [HttpPost]
    public IActionResult Post(string tags = "")
    {
      return Ok(_imagesService.SearchImages(tags).Result);
    }

  }
}
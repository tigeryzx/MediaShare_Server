using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Drawing;
using Abp.Web.Models;
using Abp.Domain.Repositories;
using MediaShare.Media;
using MediaShare.Media.Manager;

namespace MediaShare.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ImageController : MediaShareControllerBase
    {
        private readonly IImageService _imageService;

        public ImageController(
            IImageService imageService
            )
        {
            this._imageService = imageService;
        }

        [DontWrapResult]
        [HttpGet]
        public ActionResult GetImage(int imageId,int? maxWidth)
        {
            var imageByte = this._imageService.GetVideoImageByte(imageId, maxWidth);
            if (imageByte != null)
                return File(imageByte, "image/jpg");

            return NotFound();
        }
    }
}

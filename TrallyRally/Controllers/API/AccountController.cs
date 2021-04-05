using System;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using TrallyRally.Data;
using TrallyRally.Services;
using TrallyRally.Dtos;
using TrallyRally.Helpers;

namespace TrallyRally.Controllers.API
{
    public class UpdateAccountRequest
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Photo { get; set; }
    }

    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IUserService _userService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(DatabaseContext context, IUserService userService, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _userService = userService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public ActionResult<PlayerDto> Get()
        {
            var user = _userService.GetUserFromClaims(User);
            var player = _context.Players.Find(user.ID);

            return Ok(player.ConvertToDto());
        }

        [HttpPost]
        public void Post([FromBody] UpdateAccountRequest request)
        {
            var user = _userService.GetUserFromClaims(User);
            var player = _context.Players.Find(user.ID);

            if (!String.IsNullOrEmpty(request.Name))
            {
                player.Name = request.Name;
            }

            if (!String.IsNullOrEmpty(request.Phone))
            {
                player.Phone = request.Phone;
            }

            if (!String.IsNullOrEmpty(request.Photo))
            {
                player.Avatar = uploadPhoto(request.Photo);
            }

            _context.SaveChanges();
        }

        private string uploadPhoto(string base64Photo)
        {
            var relativePath = Path.Combine("uploads/avatars", ImageUploader.RandomJpegName());
            return ImageUploader.UploadJpeg(base64Photo, _webHostEnvironment.WebRootPath, relativePath);
        }
    }
}

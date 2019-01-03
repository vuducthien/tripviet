using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using TripViet.Commons;
using TripViet.Commons.Extensions;
using TripViet.Domains;

namespace TripViet.Controllers
{
    [Authorize]
    public class ImageController : Controller
    {
        ITripVietContext _context;
        IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public ImageController(ITripVietContext context, UserManager<User> userManager, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        [HttpPost]
        public FileStreamResult Index(IList<IFormFile> files)
        {
            using (Image img = Image.FromStream(files[0].OpenReadStream()))
            {
                Stream ms = new MemoryStream(img.Resize(100, 100).ToByteArray());

                return new FileStreamResult(ms, "image/jpg");
            }
        }

    }
}

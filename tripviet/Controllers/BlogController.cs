using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TripViet.Commons;
using TripViet.Data;
using TripViet.Domains;
using TripViet.Models;
using TripViet.Models.BlogViewModels;

namespace TripViet.Controllers
{
    [Authorize]
    [Route("[controller]/[action]")]
    public class BlogController : Controller
    {
        ITripVietContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        ApplicationDbContext _identityContext;
        IMapper _mapper;

        public BlogController(ITripVietContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext identityContext, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _identityContext = identityContext;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{blogType}")]
        public IActionResult WriteBlog(int blogType)
        {
            return View(new BlogViewModel() { BlogType = (BlogType)blogType });
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public JsonResult WriteBlog([FromBody] BlogViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Guid.Parse(_userManager.GetUserId(HttpContext.User));
                var current = DateTime.Now;
                var blog = _mapper.Map<Blog>(model);
                blog.CreatedById = blog.UpdatedById = userId;
                blog.CreatedDate = blog.UpdatedDate = current;
                if (model.Places.Any())
                {
                    foreach (var item in blog.Places)
                    {
                        item.CreatedById = item.UpdatedById = userId;
                        item.CreatedDate = item.UpdatedDate = current;
                    }
                }
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                return Json(blog.Id);
            }
            return Json(null);
        }

        [HttpPut]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> WriteBlog(BlogViewModel model, string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(HttpContext.User);
                var blog = _mapper.Map<Blog>(model);
                blog.CreatedById = Guid.Parse(user.Id);
                blog.UpdatedById = Guid.Parse(user.Id);
                blog.CreatedDate = DateTime.Now;
                blog.UpdatedDate = DateTime.Now;
                _context.Blogs.Add(blog);
                _context.SaveChanges();
                model.Author = _identityContext.Users.Where(x => x.Id == user.Id).Select(x => x.UserName).FirstOrDefault();
                model.Time = blog.CreatedDate.ToString("dd/MM/yyyy");
                return View("Detail", model);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        [Route("{id}")]
        public IActionResult Detail(Guid id)
        {
            var blog = _context.Blogs.Include(x => x.Places).FirstOrDefault(x => x.Id == id);
            var model = _mapper.Map<BlogViewModel>(blog);
            model.Author = _identityContext.Users.Where(x => Guid.Parse(x.Id) == blog.CreatedById).Select(x => x.UserName).FirstOrDefault();
            model.Time = blog.CreatedDate.ToString("dd/MM/yyyy");
            return View(model);
        }

        public async Task<IActionResult> GetInspiration()
        {
            var users = _identityContext.Users.ToList();
            var blogs = _context.Blogs.Where(x=>x.BlogType == BlogType.Story).OrderByDescending(c=>c.CreatedDate).Take(5).ToList();
            List<BlogViewModel> model = _mapper.Map<List<Blog>, List<BlogViewModel>>(blogs);
            return View(model);
        }

        public async Task<IActionResult> GetConnection()
        {
            var users = _identityContext.Users.ToList();
            var blogs = _context.Blogs.Where(x => x.BlogType == BlogType.Schedule).OrderByDescending(c => c.CreatedDate).Take(5).ToList();
            List<BlogViewModel> model = _mapper.Map<List<Blog>, List<BlogViewModel>>(blogs);
            return View(model);
        }
    }
}

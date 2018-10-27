using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TripViet.Commons;
using TripViet.Data;
using TripViet.Domains;
using TripViet.Models;
using TripViet.Models.BlogViewModels;

namespace TripViet.Controllers
{
    public class HomeController : Controller
    {
        ITripVietContext _context;
        ApplicationDbContext _identityContext;
        IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public HomeController(ITripVietContext context, UserManager<ApplicationUser> userManager, ApplicationDbContext identityContext, IMapper mapper)
        {
            _context = context;
            _identityContext = identityContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var users = _identityContext.Users.ToList();
            var stories = _context.Blogs.Where(x => x.BlogType == BlogType.Story).Include(x => x.Places).OrderByDescending(c => c.CreatedDate).Take(5).ToList();
            var schedules = _context.Blogs.Where(x => x.BlogType == BlogType.Schedule).Include(x => x.Places).OrderByDescending(c => c.CreatedDate).Take(5).ToList();
            var model = new List<BlogViewModel>();
            List<BlogViewModel> listStories = _mapper.Map<List<Blog>, List<BlogViewModel>>(stories);
            List<BlogViewModel> listSchedules = _mapper.Map<List<Blog>, List<BlogViewModel>>(schedules);
            model.AddRange(listStories);
            model.AddRange(listSchedules);
            return View(model);
        }

        [HttpPost]
        public JsonResult SearchPlace([FromBody] SearchViewModel model)
        {
            var query = from b in _context.Blogs
                        join p in _context.Places on b.Id equals p.BlogId
                        where p.NonHtmlAddress.Contains(model.Text)
                        select b;
            var blogs = query.Include(x => x.Places).Distinct().OrderBy(x => x.BlogType).ToList();
            var authorIds = blogs.Select(x => x.CreatedById.ToString()).Distinct();
            var authors = _identityContext.Users.Where(x => authorIds.Contains(x.Id)).Select(x => new { x.Id, x.UserName }).ToList();

            var result = _mapper.Map<List<Blog>, List<BlogViewModel>>(blogs);
            result.ForEach(x =>
            {
                x.Content = x.Content.Length > 50 ? x.Content.Substring(0, 50) + "..." : x.Content;
                x.Author = authors.Where(u => u.Id == x.CreatedById.ToString()).Select(u => u.UserName).FirstOrDefault();
            });
            return Json(result);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error() => View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

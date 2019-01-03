using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TripViet.Commons;
using TripViet.Domains;
using TripViet.Models;
using TripViet.Models.BlogViewModels;

namespace TripViet.Controllers
{
    public class HomeController : Controller
    {
        ITripVietContext _context;
        ITripVietContext _tripVietContext;
        IMapper _mapper;
        private readonly UserManager<User> _userManager;
        public HomeController(ITripVietContext context, UserManager<User> userManager, ITripVietContext tripVietContext, IMapper mapper)
        {
            _context = context;
            _tripVietContext = tripVietContext;
            _mapper = mapper;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var users = _tripVietContext.Users.ToList();
            var stories = _context.Blogs.Where(x => x.BlogType == BlogType.Story).Include(x => x.Places).OrderByDescending(c => c.CreatedDate).Take(5).ToList();
            var schedules = _context.Blogs.Where(x => x.BlogType == BlogType.Schedule).Include(x => x.Places).OrderByDescending(c => c.CreatedDate).Take(5).ToList();
            var model = new List<BlogViewModel>();
            List<BlogViewModel> listStories = _mapper.Map<List<Blog>, List<BlogViewModel>>(stories);
            List<BlogViewModel> listSchedules = _mapper.Map<List<Blog>, List<BlogViewModel>>(schedules);
            model.AddRange(listStories);
            model.AddRange(listSchedules);
            return View(model);
        }

        public IActionResult Profile(Guid id)
        {
            return View();
        }

        [HttpPost]
        public JsonResult SearchPlace([FromBody] SearchViewModel model)
        {
            var query = from b in _context.Blogs
                        join p in _context.Places on b.Id equals p.BlogId
                        where p.NonHtmlAddress.Contains(model.Text)
                        select b;
            var blogs = query.Include(x => x.Places).Distinct().OrderBy(x => x.BlogType).ToList();
            var authorIds = blogs.Select(x => x.CreatedById).Distinct();
            var authors = _tripVietContext.Users.Where(x => authorIds.Contains(x.Id)).Select(x => new { x.Id, x.UserName }).ToList();

            var result = _mapper.Map<List<Blog>, List<BlogViewModel>>(blogs);
            result.ForEach(x =>
            {
                x.Content = x.Content.Length > 50 ? x.Content.Substring(0, 50) + "..." : x.Content;
                x.Author = authors.Where(u => u.Id == x.CreatedById).Select(u => u.UserName).FirstOrDefault();
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

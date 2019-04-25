using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FizzBuzzApp.Models;
using Microsoft.AspNetCore.Http.Features;

namespace FizzBuzzApp.Controllers
{
    public class UsersController : Controller
    {
        private readonly UserContext _context;

        public UsersController(UserContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Details(int userID)
        {
            var user = _context.Users.Find(userID);
            return View(user);
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Value")] User user)
        {
            if (ModelState.IsValid)
            {
                user.SetServerAnswer();
                string remoteIpAddress = HttpContext.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress.ToString();
                user.IP = remoteIpAddress;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Details), new { userID = user.ID });
                //return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}

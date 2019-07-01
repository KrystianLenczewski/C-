using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MemeGenerator.Controllers
{
    


    [Authorize(Roles = "Admin")]
    public class AdminServiceController : Controller
    {
        private readonly memyContext _context;

        public AdminServiceController(memyContext context)
        {
            _context = context;
        }

        public IActionResult adminPanel()
        {
            
            return View();
        }

        // GET: MemeReports
        public async Task<IActionResult> ReportsList()
        {
            return View(await _context.MemeReports.ToListAsync());
        }
        public IActionResult CreateCategory()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory([Bind("IdCategory,NameCategory")] Categories categories)
        {

            if (ModelState.IsValid)
            {
                _context.Add(categories);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(adminPanel));
            }
            return View(categories);
        }





    }
}
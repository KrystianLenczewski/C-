using MemeGenerator.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MemeGenerator.Helpers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Collections.Generic;

namespace EFGetStarted.AspNetCore.ExistingDb.Controllers
{
    public class helpers
    {
        memyContext db = new memyContext();
        public  IEnumerable<SelectListItem> GetCategories()
        {
            // your context
            return new SelectList(db.Categories.OrderBy(x => x.NameCategory),
                                                          "IdCategory ", "NameCategory ");
        }
    }
    public  class MemyController : Controller
    {

        private readonly UserManager<IdentityUser> _userManager;

        //1 MB
        const long fileMaxSize = 1 * 1024 * 1024;

        memyContext db = new memyContext();

       
      
        // GET: /<controller>/

        public async Task<IActionResult> Index(string memyCategory,
                  string searchString, DateTime? searchDate,
                  int? like, int? dislike)
        {
            // Use LINQ to get list of genres.
            IQueryable<string> genreQuery = from m in db.Memy
                                            orderby m.Category
                                            select m.Category;

            var memyy = from m in db.Memy
                         select m;

            if (!string.IsNullOrEmpty(searchString))
            {
                memyy = memyy.Where(s => s.Title.Contains(searchString));
            }

            if (like!=null)
            {
                memyy = memyy.Where(s => s.Like==like);
            }
            //if (dislike != null)
            //{
            //    memyy = memyy.Where(s => s.Dislike == dislike);
            //}
            if (searchDate != null)
            {
                memyy = memyy.Where(s => s.releaseDate.Value.Date == searchDate.Value.Date);



            }


            if (!string.IsNullOrEmpty(memyCategory))
            {
                memyy = memyy.Where(x => x.Category == memyCategory);
            }

            var memyCategoryyy = new MemyCategoryViewModel
            {
                Categorys = new SelectList(await genreQuery.Distinct().ToListAsync()),
                Memys = await memyy.ToListAsync()

            };

            return View(memyCategoryyy);
        }
        public string Welcome(string name, string id)
        {
            return $"Hello, Your name's {name}. Your ID is {id}.";
        }




        public IActionResult Create()
        {

            return View();
        }
        public IActionResult Miniature()
        {
            var model = db.Memy.ToList();
            return View(model);
        }

        public async Task<IActionResult> Show(int? id, Marking user)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            if (id == null)
            {
                return NotFound();
            }

            var memy = await db.Memy
                .FirstOrDefaultAsync(m => m.Id_mema == id);


            var memyy = (from a in db.Marking
                         where a.Authorr == currentUser.Identity.Name && a.IdMema == id
                         select a).FirstOrDefault();


            if (memyy == null)
            {
                user.CountLike = 0;
                user.CountDislike = 0;

                user.Authorr = currentUser.Identity.Name;
                user.IdMema = memy.Id_mema;

                db.Marking.Add(user);
                await db.SaveChangesAsync();
            }
            if (memy == null)
            {
                return NotFound();
            }



            return View(memy);
        }

         IQueryable<string> kategorie;

        [HttpPost]
        public async Task<IActionResult> Create(Memy model, IFormFile fileUpload, Color backgroundColor)
        {
            kategorie = from m in db.Categories orderby m.NameCategory select m.NameCategory;

            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            if (fileUpload == null)
            {
                ModelState.AddModelError("errFileUpload", "The file upload field is required.");
                return View();
            }
            if (fileUpload.Length > fileMaxSize)
            {
                ModelState.AddModelError("errFileUpload", "File size can not be larger than " + (fileMaxSize / 1048576).ToString() + " MB");
                return View();
            }
            if (!Helper.IsValidImageFile(fileUpload))
            {
                ModelState.AddModelError("errFileUpload", "The uploaded file is not an image.");
                return View();
            }

            
         
            if (ModelState.IsValid)
            {

                model.Like = 0;
                model.Dislike = 0;
                model.Autor = currentUser.Identity.Name;
                string pathImgMemy = "/images/memy/";
                string pathSave = $"wwwroot{pathImgMemy}";
                if (!Directory.Exists(pathSave))
                {
                    Directory.CreateDirectory(pathSave);
                }
                string extFile = Path.GetExtension(fileUpload.FileName);
                string fileName = DateTime.Now.ToString("dd-MM-yyyy-hh-mm-ss") + extFile;
                var path = Path.Combine(Directory.GetCurrentDirectory(), pathSave, fileName);

                int width = 300, height = 300;
                int borderWidth = 50, borderHeight = 50;

                using (var memoryStream = new MemoryStream())
                {

                    await fileUpload.CopyToAsync(memoryStream);
                    using (var image = Image.FromStream(memoryStream))
                    {
                        //Resized image
                        var resized = (Image)(new Bitmap(image, new Size(width, height)));
                        //Edit here
                        Bitmap blank = new Bitmap(width + 2*borderWidth, height + 2*borderHeight);
                        using (Graphics g = Graphics.FromImage(blank))
                        {
                            g.FillRectangle(new SolidBrush(backgroundColor), new Rectangle(0, 0, width+2*borderWidth, height+2*borderHeight));
                            g.DrawImage(resized, new Point(borderWidth, borderHeight));
                            memoryStream.Position = 0;
                            blank.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                        }
                        ////Write
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            memoryStream.Position = 0;
                            await memoryStream.CopyToAsync(stream);
                        }
                    }
                }

                DateTime dateNow = DateTime.Now;
                model.coverImg = pathImgMemy + fileName;
                model.releaseDate = dateNow;
                model.modifyDate = dateNow;
                db.Memy.Add(model);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            var errors = ModelState.SelectMany(x => x.Value.Errors.Select(w => w.ErrorMessage));

            foreach (var error in errors)
            {
                Console.WriteLine(error);
            }

            return View();
        }

        public async Task<IActionResult> Delete(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var memy = await db.Memy
                .FirstOrDefaultAsync(m => m.Id_mema == id);
            if (!(memy.Autor == currentUser.Identity.Name))
            {
                return RedirectToAction(nameof(Index));
            }
            return View(memy);
        }

        // POST: Memies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memy = await db.Memy.FindAsync(id);
            db.Memy.Remove(memy);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        public async Task<IActionResult> Edit(int? id)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;

            if (id == null)
            {
                return NotFound();
            }

            var memy = await db.Memy.FindAsync(id);

            if (memy == null)
            {
                return NotFound();
            }

          if(!(memy.Autor==currentUser.Identity.Name))
            {
                return RedirectToAction(nameof(Index));
            }
           return View(memy);
        }

        // POST: Memies/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Model memyy,int id, [Bind("Id_mema,Autor,Title,coverImg,Description,Category,releaseDate,modifyDate,Like,Dislike")] Memy memy)
        {
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            DateTime dateNow = DateTime.Now;


            //  memy.Id_mema = id;
            if (id != memy.Id_mema)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(memy);
                    memy.Autor = currentUser.Identity.Name;
                    memy.modifyDate = dateNow;
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemyExists(memy.Id_mema))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
             return View(memy);

        }
        private bool MemyExists(int id)
        {
            return db.Memy.Any(e => e.Id_mema == id);
        }

        //lajkowanie
        public async Task<IActionResult> Like(int id, Marking user)
        {
            if (!User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Account", "Identity", new { id = "Login" });
            }
            var memy = db.Memy.SingleOrDefault(s => s.Id_mema == id);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var memyyy = (from a in db.Marking
                          where a.Authorr == currentUser.Identity.Name && a.IdMema == id
                          select a).FirstOrDefault();

            memy.Like++;
            if (id != memy.Id_mema)
            {
                return NotFound();
            }


            if (ModelState.IsValid && memyyy.IdMema == id && memyyy.CountLike == 0
            && memyyy.CountDislike == 0)
            {

                memyyy.CountLike = 1;
                memyyy.CountDislike = 1;

                db.Update(memy);
                await db.SaveChangesAsync();

                return RedirectToAction("Show", new { id = id });
            }

            return RedirectToAction("Show", new { id = id });
        }




        public async Task<IActionResult> Dislike(int id)
        {
            if (!User.Identity.IsAuthenticated)
            {

                return RedirectToAction("Account", "Identity", new { id = "Login" });
            }
            var memy = db.Memy.SingleOrDefault(s => s.Id_mema == id);
            System.Security.Claims.ClaimsPrincipal currentUser = this.User;
            var memyyy = (from a in db.Marking
                          where a.Authorr == currentUser.Identity.Name && a.IdMema == id
                          select a).FirstOrDefault();

            memy.Dislike = memy.Dislike + 1;
            if (id != memy.Id_mema)
            {
                return NotFound();
            }

            if (ModelState.IsValid && memyyy.IdMema == id && memyyy.CountLike == 0 && memyyy.CountDislike == 0)
            {

                memyyy.CountLike = 1;
                memyyy.CountDislike = 1;


                db.Update(memy);
                await db.SaveChangesAsync();

                return RedirectToAction("Show", new { id = id });
            }
            return RedirectToAction("Show", new { id = id });
        }


        public IActionResult CreateReport(int id)
        {
            return View();
        }

        // POST: MemeReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(int id,[Bind("Id_report,Id_user,Id_meme,Description")] MemeReports memeReports)
        {
            var userName = User.FindFirst(ClaimTypes.Name).Value;

            memeReports.Id_meme = id;
            memeReports.login = userName;
            if (ModelState.IsValid)
            {
                db.Add(memeReports);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memeReports);
        }


        private bool MemeReportsExists(int id)
        {
            return db.MemeReports.Any(e => e.Id_report == id);
        }



    }



}

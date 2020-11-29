using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebshopRestAPI.Controllers
{
    public class TokenController : Controller
    {
        // GET: TokenController
        public ActionResult Index()
        {
            return View();
        }

        // GET: TokenController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TokenController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TokenController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TokenController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TokenController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TokenController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TokenController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

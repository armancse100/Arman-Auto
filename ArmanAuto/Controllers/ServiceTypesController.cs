using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArmanAuto.Data;
using ArmanAuto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArmanAuto.Controllers
{
    public class ServiceTypesController : Controller
    {
        private readonly ApplicationDbContext _db;
        public ServiceTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ServiceTypes.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServiceType serviceType)
        {
            if (ModelState.IsValid)
            {
                _db.Add(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(serviceType);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if(serviceType == null)
            {
                return NotFound();
            }
            else
            {
                return View(serviceType);
            }
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            else
            {
                return View(serviceType);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, ServiceType serviceType)
        {
            if(id != serviceType.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _db.ServiceTypes.Update(serviceType);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View();
    

        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveServiceType(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            _db.Remove(serviceType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceType = await _db.ServiceTypes.SingleOrDefaultAsync(m => m.Id == id);
            if (serviceType == null)
            {
                return NotFound();
            }
            else
            {
                return View(serviceType);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
        }

        
    }
}
using Bikemvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Bikemvc.Controllers
{
    [Authorize]
    public class BikeController : Controller
    {
        BikeDbContext db = new BikeDbContext();


        public ActionResult Bikes()
        {
            return View(db.BikeInfos.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(BikeInfo BikeInfo)
        {
            if (ModelState.IsValid)
            {
                db.BikeInfos.Add(BikeInfo);
                db.SaveChanges();
                return RedirectToAction("Bikes");
            }

            return View(BikeInfo);
        }
        public ActionResult Edit(int? BikeInfoId)
        {
            if (BikeInfoId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BikeInfo BikeInfo = db.BikeInfos.Find(BikeInfoId);
            if (BikeInfo == null)
            {
                return HttpNotFound();
            }
            return View(BikeInfo);
        }
        [HttpPost]
        public ActionResult Edit(BikeInfo BikeInfo)
        {
            if (ModelState.IsValid)
            {
                var BikeIn = db.BikeInfos.Where(w => w.BikeInfoId == BikeInfo.BikeInfoId).FirstOrDefault();
                BikeIn.BikeName = BikeInfo.BikeName;
                db.SaveChanges();
                return RedirectToAction("Bikes");
            }
            return View(BikeInfo);
        }
        public ActionResult Delete(int? BikeInfoId)
        {
            try
            {
                var firstEntity = db.BikeInfos.Where(c => c.BikeInfoId == BikeInfoId).FirstOrDefault();
                db.BikeInfos.Remove(firstEntity);
                db.SaveChanges();
            }
            finally
            {

            }
            return RedirectToAction("Bikes");
        }


    }
}
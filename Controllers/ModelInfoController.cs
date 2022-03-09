using Bikemvc.Models;
using Bikemvc.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bikemvc.Controllers
{
    [Authorize]
    public class ModelInfoController : Controller
    {
        BikeDbContext db = new BikeDbContext();

        public ActionResult ModelShow()
        {
            ModelInfoViewModel uivm = new ModelInfoViewModel();

            ViewBag.BikeInfoId = new SelectList(db.BikeInfos.ToList(), "BikeInfoId", "BikeName");

            return View(uivm);
        }
        public ActionResult Delete(int ModelInfoId)
        {
            try
            {
                var firstEntity = db.ModelInfos.Where(s => s.ModelInfoId == ModelInfoId).FirstOrDefault();
                db.ModelInfos.Remove(firstEntity);
                db.SaveChanges();
            }
            finally
            {

            }

            return RedirectToAction("ModelShow");

        }
        public ActionResult Insert()
        {
            List<SelectListItem> BikeInfoList = new List<SelectListItem>();

            foreach (var u in db.BikeInfos.ToList())
            {
                BikeInfoList.Add(new SelectListItem()
                {
                    Text = u.BikeName,
                    Value = u.BikeInfoId.ToString()
                });

            }

            ViewBag.BikeInfoId = BikeInfoList;

            HttpContext.Session.Add("ActionToken", ActionMode.Insert);

            return RedirectToAction("ModelShow");
        }
        [HttpPost]

        public ActionResult Insert(ModelInfo ModelInfoWorkEntity)
        {

            try
            {
                string fileName = System.IO.Path.GetFileName(ModelInfoWorkEntity.PicturePFB.FileName);

                ModelInfoWorkEntity.PicturePFB.SaveAs(Server.MapPath("~/Images/") + fileName);
                ModelInfoWorkEntity.Image = "Images/" + fileName;

                db.ModelInfos.Add(ModelInfoWorkEntity);
                //db.SaveChanges();
            }
            finally
            {

            }

            if (ModelState.IsValid)
            {
                db.ModelInfos.Add(ModelInfoWorkEntity);
                db.SaveChanges();
                return RedirectToAction("ModelShow");
            }

            ViewBag.BikeInfoId = new SelectList(db.BikeInfos, "BikeInfoId", "BikeName");
            return View(ModelInfoWorkEntity);
        }
        public ActionResult Edit(int ModelInfoid)
        {

            HttpContext.Session.Add("ActionToken", ActionMode.Edit);
            HttpContext.Session.Add("ModelInfoId", ModelInfoid);
            ViewBag.BikeInfoId = new SelectList(db.BikeInfos, "BikeInfoId", "BikeName");
            return RedirectToAction("ModelShow");
        }
        [HttpPost]

        public ActionResult Edit(ModelInfo ModelInfoWorkEntity)
        {

            try
            {
                string fileName = System.IO.Path.GetFileName(ModelInfoWorkEntity.PicturePFB.FileName);

                ModelInfoWorkEntity.PicturePFB.SaveAs(Server.MapPath("~/Images/") + fileName);
                var updateModelInfo = db.ModelInfos.Where(w => w.ModelInfoId == ModelInfoWorkEntity.ModelInfoId).FirstOrDefault();

                updateModelInfo.ModelInfoId = ModelInfoWorkEntity.ModelInfoId;
                updateModelInfo.ModelName = ModelInfoWorkEntity.ModelName;
                updateModelInfo.IsAvailable = ModelInfoWorkEntity.IsAvailable;
                updateModelInfo.ReleaseDate = ModelInfoWorkEntity.ReleaseDate;
                updateModelInfo.ModelPrice = ModelInfoWorkEntity.ModelPrice;
                updateModelInfo.BikeInfoId = ModelInfoWorkEntity.BikeInfoId;
                updateModelInfo.Image = "Images/" + fileName;

                db.SaveChanges();
            }
            finally
            {

            }
            ViewBag.BikeInfoId = new SelectList(db.BikeInfos, "BikeInfoId", "BikeName", ModelInfoWorkEntity.BikeInfoId);
            return RedirectToAction("ModelShow");
        }


    }
}
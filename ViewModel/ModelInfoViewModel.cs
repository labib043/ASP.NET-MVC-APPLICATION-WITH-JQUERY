using Bikemvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bikemvc.ViewModel
{
    public class ModelInfoViewModel
    {
        
       public IList<ModelInfo> ModelInfoes { get; set; }
            BikeDbContext db = new BikeDbContext();
            public ModelInfoViewModel()
            {
                ModelInfoes = new List<ModelInfo>();
                ModelInfoes = db.ModelInfos.ToList();
                if (HttpContext.Current.Session["ActionToken"] != null)
                {
                    if ((ActionMode)HttpContext.Current.Session["ActionToken"] == ActionMode.Insert)
                    {
                        ModelInfoWorkEntity = new ModelInfo()
                        {

                        };
                    }
                    else if ((ActionMode)HttpContext.Current.Session["ActionToken"] == ActionMode.Edit)
                    {
                        int sessionId = (int)HttpContext.Current.Session["ModelInfoId"];
                        ModelInfoWorkEntity = db.ModelInfos.Where(w => w.ModelInfoId == sessionId).FirstOrDefault();

                    }
                }

            }

            public ModelInfo ModelInfoWorkEntity { get; set; }
        }


        public enum ActionMode
        {
            Select, Edit, Insert, Delete
        }
    }


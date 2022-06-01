using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Models;
using BAL;

namespace Sky_Rushabh.Controllers
{
    public class EventManageController : Controller
    {
        EventBal objEventBal = new EventBal();
        // GET: EventManage        

        [HttpPost]
        public ActionResult Event(EventModel objEventModel)
        {
            if (ModelState.IsValid)
            {
                if (objEventModel.EndDate >= objEventModel.StartDate)
                {
                    int iReturn = objEventBal.InsertEventData(objEventModel);
                    if (iReturn > 0)
                    {
                        TempData["SuccessMessage"] = "Record save successfully";
                        //ViewBag.SuccessMessage = ValidationMessages.Save;
                        return RedirectToAction("Event", "EventManage", new { id = 0 });
                    }
                    else
                        ViewBag.ErrorMessage = "Record not save successfully";
                }
                else
                {
                    ViewBag.ErrorMessage = "Enddate greater then startdate!";
                }
                ModelState.Clear();
            }
            return View(objEventModel);
        }

        public ActionResult EventDataList()
        {
            return View(objEventBal.GetEventData());
        }

        public ActionResult EventDataListPassed()
        {
            return View(objEventBal.EventDataListPassed());
        }

        public ActionResult Event(int? Id = null)
        {
            EventModel objEventModel = new EventModel();
            if (Id > 0 && Id != null)
            {
                objEventModel = objEventBal.GetEventById(Convert.ToInt32(Id));
            }
            else
            {
                objEventModel.StartDate = DateTime.Now;
                objEventModel.EndDate = DateTime.Now;
            }
            return View(objEventModel);
        }

        [HttpPost]
        public ActionResult DeleteEvent(int Id)
        {
            int result = objEventBal.DeleteEvent(Id);
            if (result > 0)
            {
                TempData["Message"] = "Data Delete successfully";
            }
            return RedirectToAction("Event");
        }
    }
}
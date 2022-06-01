using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using Models;

namespace BAL
{
    public class EventBal
    {
        EventDal objEventDal = new EventDal();
        public List<EventModel> GetEventData()
        {
            return objEventDal.GetEventData();
        }

        public List<EventModel> EventDataListPassed()
        {
            return objEventDal.EventDataListPassed();
        }

        public int InsertEventData(EventModel objEventModel)
        {
            return objEventDal.InsertEventData(objEventModel);
        }

        public EventModel GetEventById(int Id)
        {
            return objEventDal.GetEventById(Id);
        }

        public int DeleteEvent(int Id)
        {
            return objEventDal.DeleteEvent(Id);
        }
    }
}
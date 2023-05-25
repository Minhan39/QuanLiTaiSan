using DevExpress.Web.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QLTS.Controllers
{
    public class BaseController : Controller
    {
        protected void SafeExecute(Action method)
        {
            try
            {
                method();
            }
            catch (Exception e)
            {
                SetErrorText(e.Message);
            }
        }
        protected object SafeExecute(Func<object> method)
        {
            try
            {
                return method();
            }
            catch (Exception e)
            {
                SetErrorText(e.Message);
            }
            return null;
        }
        protected void SetErrorText(string message)
        {
            ViewBag.GeneralError = message;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TNW.Interfaces;

namespace TNW.Controllers
{
    public class ControllerBase : Controller
    {
        protected IUnitOfWork _unitOfWork;
        protected string UserId => User.Identity.GetUserId();

        public ControllerBase(IUnitOfWork uow)
        {
            _unitOfWork = uow;
        }
    }
}

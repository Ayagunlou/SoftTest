using SoftTest.Models;
using Microsoft.AspNetCore.Mvc;
using Data_Access_Layer.Repository.Models;
using Microsoft.AspNetCore.Authorization;

namespace SoftTest.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Bussiness_logic_layer.OrderBLL _BLL;
        public HomeController()
        {
            _BLL = new Bussiness_logic_layer.OrderBLL();
        }

        [HttpGet,AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Swagger()
        {
            return Redirect("http://localhost:5208/swagger");
        }

        [HttpGet]
        [Route("getall")]
        public ActionResult GetAll()
        {
            var result = _BLL.GetAll();

            return View(result);
        }

        [HttpGet]
        [Route("add")]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("add")]
        public ActionResult Add(PreOrderModel order)
        {
            var result = _BLL.Add(order);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        [Route("update")]
        public ActionResult Update(int id)
        {
            return View(_BLL.FindById(id));
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(PreOrderModel order)
        {
            var result = _BLL.Update(order);
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public ActionResult ChangeToOpen(int id, string terminate)
        {
            char[] Conchar = terminate.ToCharArray();
            var result = _BLL.ChangeIdentityToOpen(id, Conchar[0]);
            if (result == false)
            {
                TempData["Message"] = $"Is Terminate  Already ID : {id}";
                TempData["Value"] = $"{terminate}";
            }
            return RedirectToAction(nameof(GetAll));
        }


        [HttpGet]
        public ActionResult ChangeToApproval(int id, string terminate)
        {
            char[] Conchar = terminate.ToCharArray();
            var result = _BLL.ChangeIdentityToApproval(id, Conchar[0]);
            if (result == false)
            {
                TempData["Message"] = $"Is Terminate  Already ID : {id}";
            }
            return RedirectToAction(nameof(GetAll));
        }

        [HttpGet]
        public ActionResult ChangeToCancel(int id, string terminate)
        {
            char[] Conchar = terminate.ToCharArray();
            var result = _BLL.ChangeIdentityToCancel(id, Conchar[0]);
            if (result == false)
            {
                TempData["Message"] = $"Is Terminate  Already ID : {id}";
            }
            return RedirectToAction(nameof(GetAll));
        }


        //[HttpGet]
        //[Route("regis")]
        //public ActionResult Regis()
        //{
        //    return View();
        //}
        //[HttpPost]
        //[Route("regis")]
        //public ActionResult Regis (UserModel user)
        //{
        //    if(user == null)
        //    {
        //    user.UserName = user.UserName.ToLower();
        //    }
        //    var result = _BLL.Register(user);
        //    if (result == false)
        //    {
        //        return View();
        //    }
        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //public ActionResult Login (UserModel user)
        //{
        //    var result = _BLL.Login(user);
        //    if (result == null)
        //    {
        //        return View();
        //    }
        //    return RedirectToAction(nameof(GetAll));
        //}

        [HttpGet]
        [Route("from")]
        public ActionResult from()
        {
            TempData["numorder"] = DateTime.Now.ToString($"yyyyMMddHHmmss");
            var result = _BLL.GetPara().GroupBy(c=>c.Vender,c=>c.Vender).ToList();
            if (result == null)
            {
                return View();
            }
            var ew = result.Select(x => x.Key).ToList();
            foreach(var e in ew)
            {
                var final = _BLL.GetPara().Where(c => c.Vender == e).ToList();
                //var er = _BLL.ChangeIdentityToGen(final.Select(c => c.Id).ToList(), 'g');
                return View(final);
            }
            //var er=_BLL.ChangeIdentityToGen(result.Select(c=>c.Id).ToList(),'g');
            return RedirectToAction(nameof(GetAll));
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using WebApplication4.Models;
using WebApplication4.Interface;
using WebApplication4.Services;
/*
 Microsoft.Data.Sqlite.Core 
SQLitePCLRaw.bundle_e_sqlite3
 */
namespace WebApplication4.Controllers
{
    public class CryptoController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICrypto _Cryptoq;
        private readonly KeyIVServices _keyIvServices;

        public CryptoController(ILogger<HomeController> logger, ICrypto Cryptoq, KeyIVServices keyedService)
        {
            _logger = logger;
            _Cryptoq = Cryptoq;
            _keyIvServices = keyedService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            CryptoModel cryptoResult = new CryptoModel();
            return View(cryptoResult);
        }
        [HttpPost]
        public IActionResult Index(CryptoModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model = _Cryptoq.DoCrypto(model);
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = ex.Message;
                }

                return View(model);
            }

            return View(model);
        }
        [HttpGet("Crypto/KeyIv/{id}")]
        public IActionResult KeyIv(int id)
        {
            var cryptoResult = _keyIvServices.SelectKeyIv(id);
            return Json(cryptoResult);
        }
        [HttpGet]
        public IActionResult SetKeyIv()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SetKeyIv(SetKeyIv request)
        {
            if (!ModelState.IsValid)
            {
                //if (request.IsExist)
                //    var result = _keyIvServices.AddKeyIv(request);
                //else
                //    var result = _keyIvServices.EditKeyIv(request);
            }

            return Json(request);
        }

        [HttpDelete]
        public IActionResult KeyIv(long id)
        {
            return View();
        }


        //public IActionResult Encode(CryptoModel request)
        //{

        //    return View();
        //}

        //public IActionResult Decode(CryptoModel request)
        //{

        //    return View();

        //}

        //public IActionResult PwdHash(CryptoModel request)
        //{

        //    return View();
        //}

    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication4.Models;
using Microsoft.Data.Sqlite;
using System.Xml.Linq;
using System;
/*
 Microsoft.Data.Sqlite.Core 
SQLitePCLRaw.bundle_e_sqlite3
 */
namespace WebApplication4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        /// <summary>
        /// 第一題正常顯示畫面
        /// </summary>
        /// <returns></returns>
        public IActionResult First_Blood()
        {
            string show = "<a href='/Double_Kill'>請讓第二題顯示出來 之後的View自己建！！</a>";

            //return View(show);
            return View(nameof(First_Blood),show);
        }

        /// <summary>
        /// 第二題： 請讓第三題顯示出來
        /// </summary>
        /// <returns></returns>
        public IActionResult Double_Kill()
        {

            return View();
        }
        /// <summary>
        /// 第三題 ：使用Service檔案的sqlite 在畫面顯示查詢結果
        /// </summary>
        /// <returns></returns>
        public IActionResult Triple_Kill()
        {
            return View();
        }
        // 第四題 ：在Service檔案的SqliteDbTest 加入方法在畫面顯示table: EncryptionKeys 有哪些欄位

        public IActionResult Quadra_Kill()
        {
            return View();
        }

        //建立model 對EncryptionKeys 新增一筆資料並且顯示在此畫面
        public IActionResult Pentakill()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

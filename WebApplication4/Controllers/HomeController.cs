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
        /// �Ĥ@�D���`��ܵe��
        /// </summary>
        /// <returns></returns>
        public IActionResult First_Blood()
        {
            string show = "<a href='/Double_Kill'>�����ĤG�D��ܥX�� ���᪺View�ۤv�ءI�I</a>";

            //return View(show);
            return View(nameof(First_Blood),show);
        }

        /// <summary>
        /// �ĤG�D�G �����ĤT�D��ܥX��
        /// </summary>
        /// <returns></returns>
        public IActionResult Double_Kill()
        {

            return View();
        }
        /// <summary>
        /// �ĤT�D �G�ϥ�Service�ɮת�sqlite �b�e����ܬd�ߵ��G
        /// </summary>
        /// <returns></returns>
        public IActionResult Triple_Kill()
        {
            return View();
        }
        // �ĥ|�D �G�bService�ɮת�SqliteDbTest �[�J��k�b�e�����table: EncryptionKeys ���������

        public IActionResult Quadra_Kill()
        {
            return View();
        }

        //�إ�model ��EncryptionKeys �s�W�@����ƨåB��ܦb���e��
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

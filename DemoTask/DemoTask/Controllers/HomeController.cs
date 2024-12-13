using DemoTask.IRepository;
using DemoTask.Models;
using DemoTask.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DemoTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        static readonly IDemoTaskRepository demoRepository = new DemoTaskRepository();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public ActionResult<Res_Item> GetAllItems()
        {
            Res_Item res_Item = new Res_Item();
            try
            {
                var material =  demoRepository.GetAllItems();
                return Json(material);

            }
            catch (Exception ex)
            {
                res_Item.ec = -1;
                res_Item.em = "Unhandled Exception: " + ex.Message;
                return res_Item;
            }
        }
        [HttpPost]
        public ActionResult<Response> AddItems([FromBody] AddItem addItem)
        {
            Response res = new Response();
            try
            {
                res = demoRepository.AddItems(addItem);
                return res;
            }
            catch (Exception ex)
            {
                res.ec = -1;
                res.em = "Unhandled Exception: " + ex.Message;
                return res;
            }
        }

        [HttpGet]
        public ActionResult GetDdlData()
        {
            try
            {
                DdlItems ObjClient = new DdlItems();
                var list = demoRepository.GetDdlData();
                return Json(list);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
        }

        public IActionResult PriceChange()
        {
            return View();
        }
        [HttpPost]
        public ActionResult<Res_PriceChange> PriceChanges([FromBody] Req_PriceChange req_Price  )
        {
            //Res_PriceChange pc = new Res_PriceChange();
            try
            {

                _logger.LogInformation("PriceChanges action hit");
                var priceChanges = demoRepository.PriceChanges(req_Price);
                _logger.LogInformation("Data returned from repository: {Count}", priceChanges.Data.Count);
                return Json(priceChanges);

            }
            catch (Exception ex)
            {
                //return BadRequest(ex.Message.ToString());
                return BadRequest(ex.Message);
            }
        }

    }
}


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Dojodachi.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            if(HttpContext.Session.GetObjectFromJson<Dachi>("DachiData") == null)
            {
                HttpContext.Session.SetObjectAsJson("DachiData", new Dachi());
            }            
            ViewBag.DachiData = HttpContext.Session.GetObjectFromJson<Dachi>("DachiData");
                if(ViewBag.DachiData.Fullness < 1 || ViewBag.DachiData.Happiness < 1 ){
                    ViewBag.DachiData.Message = "Oh dear, your Dachi died..r.i.p little fella";
                } 
                if(ViewBag.DachiData.Fullness >= 100 && ViewBag.DachiData.Happiness >= 100 && ViewBag.DachiData.energy >= 100){
                    ViewBag.DachiData.Message = "Your Dachi has reached max happiness with a full belly! Way to go bro!";
                }

            return View();
        }

        [HttpGet]
        [Route("feed")]
        public IActionResult Feed()
        {
            Dachi CurrDachiData = HttpContext.Session.GetObjectFromJson<Dachi>("DachiData");
            if(CurrDachiData.Meals > 0)
            {
                CurrDachiData.Feed();
                CurrDachiData.Message = "Now eating! Yum Yum!";
            } 
            else 
            {
                CurrDachiData.Message = "No more meals? Guess it's time to work!";
            }
            HttpContext.Session.SetObjectAsJson("DachiData",CurrDachiData);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("play")]
        public IActionResult Play()
        {
            Dachi CurrDachiData = HttpContext.Session.GetObjectFromJson<Dachi>("DachiData");
            if(CurrDachiData.Energy <= 0)
            {
                CurrDachiData.Message = ("No more energy...Time to sleep!");
            }
            else
            {
                CurrDachiData.Play();
                CurrDachiData.Message = "Play ball!";
            }
            
            HttpContext.Session.SetObjectAsJson("DachiData",CurrDachiData);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("work")]
        public IActionResult Work()
        {
            Dachi CurrDachiData = HttpContext.Session.GetObjectFromJson<Dachi>("DachiData");
            if(CurrDachiData.Energy <= 0)
            {
                CurrDachiData.Message = ("Can't work now, not enough energy. Time to sleep!");
            }
            else
            {
                CurrDachiData.Work();
                CurrDachiData.Message = "All work and no play..";
            }
            HttpContext.Session.SetObjectAsJson("DachiData",CurrDachiData);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("sleep")]
        public IActionResult Sleep()
        {
            Dachi CurrDachiData = HttpContext.Session.GetObjectFromJson<Dachi>("DachiData");
            if(CurrDachiData.Fullness <= 0 || CurrDachiData.Happiness <= 0)
            {
                CurrDachiData.Message = ("Too hungry and sad to sleep!");
            }
            else
            {
                CurrDachiData.Sleep();
                CurrDachiData.Message = "Zzzz..";
            }
            HttpContext.Session.SetObjectAsJson("DachiData",CurrDachiData);
            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("reset")]
        public IActionResult Reset()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
}
    }


    public static class SessionExtensions
    {
        public static void SetObjectAsJson(this ISession session, string key, object value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }
        
        public static T GetObjectFromJson<T>(this ISession session, string key)
        {
            string value = session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }

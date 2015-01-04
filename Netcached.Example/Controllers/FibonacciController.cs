using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Netcached.Client;
using Netcached.Example.Models;

namespace Netcached.Example.Controllers
{
    [RequireHttps]
    public class FibonacciController : Controller
    {
        // GET: /Fibonacci/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Fibonacci/Calculate/
        public ActionResult Calculate(int position)
        {
            ViewBag.ReturnUrl = Url.Action("Index");
            NetcachedClient client = new NetcachedClient();
            long? result = client.Get<long?>(position.ToString());
            if (!result.HasValue)
            {
                result = CalculateFibonacci(position);
                client.Set(position.ToString(), result);
            }

            return View(new FibonacciViewModel() { Position = position, Value = result.Value });
        }

        /// <summary>
        /// An intentionaly slow calculation of Fibonacci numbers so as to illustrate the caching
        /// </summary>
        /// <param name="position">The position of the number in the Fibonacci sequence, starting from 0</param>
        /// <returns></returns>
        private static Int64 CalculateFibonacci(Int32 position)
        {
            if (position == 0 || position == 1)
            {
                return position;
            }
            return CalculateFibonacci(position - 2) + CalculateFibonacci(position - 1);
        }
    }
}
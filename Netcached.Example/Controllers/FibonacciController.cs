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
        // 
        // GET: /Fibonacci/
        public ActionResult Index()
        {
            return View();
        }

        // GET: /Fibonacci/position
        public ActionResult Index(int position)
        {

            NetcachedClient client = new NetcachedClient();
            int? value = client.Get<int?>(position.ToString());
            if (value != null)
            {
                return View(new FibonacciViewModel(){Position = position, Value = (int)value});
            }

            return View(new FibonacciViewModel(){Position = position, Value = Calculate(position)});
        }

        /// <summary>
        /// An intentionaly slow calculation of Fibonacci numbers so as to illustrate the caching
        /// </summary>
        /// <param name="position">The position of the number in the Fibonacci sequence, starting from 0</param>
        /// <returns></returns>
        private static Int64 Calculate(Int32 position)
        {
            if (position == 0 || position == 1)
            {
                return position;
            }
            return Calculate(position - 2) + Calculate(position - 1);
        }
    }
}
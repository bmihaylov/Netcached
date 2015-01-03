using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Netcached.Example.Models
{
    public class FibonacciViewModel
    {
        [Display(Name = "Position")]
        [DataType(DataType.Text)]
        public Int32 Position { get; set; }

        [Display(Name = "Value")]
        [DataType(DataType.Text)]
        public Int64 Value { get; set; }
    }
}
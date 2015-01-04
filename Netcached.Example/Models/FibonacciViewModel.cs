using System;
using System.ComponentModel.DataAnnotations;

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
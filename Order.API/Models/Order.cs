using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.API.Models
{
    public class Order
    {

        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public int ProductID { get; set; }     
        public int Count { get; set; }
    }
}

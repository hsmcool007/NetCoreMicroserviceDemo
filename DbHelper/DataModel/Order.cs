using System;
using System.Collections.Generic;
using System.Text;

namespace DbHelper.DataModel
{
    public class Order
    {
        public int ID { get; set; }
        public DateTime CreateTime { get; set; }
        public int ProductID { get; set; }
        public int Count { get; set; }

    }
}

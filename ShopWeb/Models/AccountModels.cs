using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopWeb.Models
{
    public class AccountModels
    {
        public string mem_phone { set; get; }
        public string goods_id { set; get; }
        public string goods_name { set; get; }
        public float unit_price { set; get; }
        public int goods_num { set; get; }
        public float total_price { set; get; }
        public float all_price { set; get; }
        public float score { set; get; }
        public List<AccountModels> accountModeList { set; get; }
    }
}
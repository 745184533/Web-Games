using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class PurchaseListController : Controller
    {
        // GET: PurchaseList
        public ActionResult Index()
        {
            if (Session["has_login"] == null)
            {
                if (Session["ReturnToPurchaseList"] == null) Session["ReturnToPurchaseList"] = "true";
                else Session["ReturnToPurchaseList"] = null;
                return Redirect("/Login");
            }
            else
            {
                Session.Remove("ReturnToPurchaseList");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var p_list = memberPurchase.getPurchaseLists(Session["mem_phone"].ToString()).Select(p_info => new MemberPurchaseListViewModel()
                {
                    goods_id = p_info.goods_id,
                    goods_name = p_info.goods_name,
                    goods_img_path=memberPurchase.getGoods(p_info.goods_id).goods_img_path,
                    goods_num = p_info.goods_num,
                    date=p_info.date,     
                    unit_price = p_info.unit_price,
                    total_price = p_info.total_price,
                }).ToList();
                var resView = new MemberPurchaseListViewModel()
                {
                    mem_phone = Session["mem_phone"].ToString(),
                    purchase_lists = p_list,
                };
                return View(resView);
            }
        }

        [HttpPost]
        public JsonResult SelectGoodsToBuy(PurchaseHomeTotalInfo purchaseHomeTotalInfo)
        {
            Session.Remove("ReturnToPurchaseCar");
            string mem_phone = Session["mem_phone"].ToString();
            var goods_items = purchaseHomeTotalInfo.selected_goods_list;
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            DateTime now_time = DateTime.Now;
            foreach(var goods in goods_items)
            {
                memberPurchase.addPurchaseLists(mem_phone, goods.goods_id, goods.goods_num,now_time);
                memberPurchase.deletePurchaseCar(mem_phone, goods.goods_id);
            }
            return Json("购买商品成功");
        }
    }
}
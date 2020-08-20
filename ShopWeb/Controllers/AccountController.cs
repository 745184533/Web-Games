using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShopWeb.Models;

namespace ShopWeb.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult pay2048(AccountModels accountModels)
        {

            if (Session["has_pay"] != null)
            {
                Session.Remove("has_pay");
                accountModels.score = 0;
                for (int i = 0; i < accountModels.accountModeList.Count; ++i)
                {
                    accountModels.all_price += accountModels.accountModeList[i].total_price;
                }
                accountModels.all_price = accountModels.all_price / 100 + accountModels.all_price;
                return View(accountModels);
            }
            else return Redirect("/PurchaseList");
        }

        [HttpPost]
        public ActionResult judge(AccountModels accountModels)
        {
            if (accountModels.score >= accountModels.all_price)
            {
                Session.Remove("has_pay");
                ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
                var account_list = accountModels.accountModeList;
                string mem_phone = Session["mem_phone"].ToString();
                DateTime now_time = DateTime.Now;
                for (int i = 0; i < account_list.Count; ++i)
                {
                    memberPurchase.addPurchaseLists(mem_phone, account_list[i].goods_id, account_list[i].goods_num, now_time);
                    memberPurchase.deletePurchaseCar(mem_phone, account_list[i].goods_id);
                }
                return RedirectToAction("order_success");
            }
            else return Redirect("/PurchaseCar");
        }

        [HttpPost]
        public ActionResult Index(MemberPurchaseCarViewModel memberPurchaseCarViewModels, string[] selected)
        {
            ShopBusinessLogic.MemberPurchase memberPurchase = new ShopBusinessLogic.MemberPurchase();
            var submit_list = memberPurchaseCarViewModels.select_list;
            var select_list = new List<AccountModels>();
            float all_price = 0;
            for (int i = 0; i < submit_list.Count; ++i)
            {
                if (selected[i] != null && selected[i] == "on")
                {
                    var item = new AccountModels()
                    {
                        goods_id = submit_list[i].goods_id,
                        goods_name = memberPurchase.getGoods(submit_list[i].goods_id).goods_name,
                        goods_num = submit_list[i].goods_num,
                        unit_price = memberPurchase.getGoods(submit_list[i].goods_id).goods_price,
                        total_price = memberPurchase.getGoods(submit_list[i].goods_id).goods_price * submit_list[i].goods_num,
                    };
                    if (item.goods_num > 0)
                    {
                        all_price += item.total_price;
                        select_list.Add(item);
                    }
                }
            }
            var resView = new AccountModels()
            {
                mem_phone = Session["mem_phone"].ToString(),
                accountModeList = select_list,
                all_price = all_price,
            };
            Session["has_pay"] = "true";
            return View(resView);
        }

        public ActionResult order_success()
        {
            return View();
        }
    }
}
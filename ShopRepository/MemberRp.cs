using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ShopModel;


namespace ShopRepository
{
    public class MemberRp
    {
        private static List<Member> members = new List<Member>()
        {
            new Member()
            {
                id="mjxlkl123",
                pwd="lklklklk",
                nname="mjx",
            },
            new Member()
            {
                id="ylqylq666",
                pwd="qlqlql",
                nname="ylq",
            }
        };
        public List<Member> GetAll()
        {
            return members;
        }

        public Member GetById(string id)
        {
            return members.Where(now => now.id == id).FirstOrDefault();
        }
    }
}

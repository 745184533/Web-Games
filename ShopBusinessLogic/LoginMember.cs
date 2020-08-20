using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;

namespace ShopBusinessLogic
{
    public class LoginMember
    {
        private ShopRepository.MySQL.Login_ShopRp rp = new ShopRepository.MySQL.Login_ShopRp();

        public Member GetMemberByPhone(string phone)
        {
            return rp.Login_GetMemberByPhone(phone);
        }

        public bool SignUpMemberByPhone(string phone,string pwd,string name,string address)
        {
            return rp.SignUp_Member(phone, pwd, name,address);
        }

        public void ModifyMemberName(string phone,string new_name)
        {
           rp.Modify_MemberName(phone, new_name);
        }

        public void ModifyMemberPwd(string phone,string new_pwd)
        {
            rp.Modify_MemberPwd(phone, new_pwd);
        }

        public void ModifyMemberAddress(string phone, string new_address)
        {
            rp.Modify_MemAddress(phone, new_address);
        }
    }
}

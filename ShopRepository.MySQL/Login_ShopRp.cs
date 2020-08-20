using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopModel;
using MySql.Data.MySqlClient;

namespace ShopRepository.MySQL
{
    public class Login_ShopRp
    {
        private string connectionstring = "server=localhost;database=webdb;username=root;pwd=123;Old Guids=true;";
        //Old Guids=true;必须要加

        public Member Login_GetMemberByPhone(string phone)
        {
            string queryString = "select * from members where mem_phone=@phone";
            var result = new Member();
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                try
                {
                    conn.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        result = new Member()
                        {
                            mem_phone = reader.GetString("mem_phone"),
                            mem_pwd = reader.GetString("mem_pwd"),
                            mem_name = reader.GetString("mem_name"),
                            mem_address=reader.GetString("mem_address"),
                        };
                    }
                    reader.Close();
                }
                catch (Exception ex) { }
                finally
                {
                    conn.Close();
                }
            }
            return result;
        }

        public bool SignUp_Member(string phone,string pwd,string name,string address)
        {
            bool flag = true;
            string queryString = "insert into members set mem_phone=@phone , mem_pwd=@pwd , mem_name=@name ,mem_address=@address";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@pwd", pwd));
                cmd.Parameters.Add(new MySqlParameter("@name", name));
                cmd.Parameters.Add(new MySqlParameter("@address", address));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex) {
                    flag = false;
                }
                finally
                {
                    conn.Close();
                }
            }
            return flag;
        }

        public void Modify_MemberName(string phone,string new_name)
        {
            string queryString = "update members set mem_name=@new_name where mem_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_name", new_name));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Modify_MemberPwd(string phone, string new_pwd)
        {
            string queryString = "update members set mem_pwd=@new_pwd where mem_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_pwd", new_pwd));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }

        public void Modify_MemAddress(string phone, string new_address)
        {
            string queryString = "update members set mem_address=@new_address where mem_phone=@phone ";
            using (MySqlConnection conn = new MySqlConnection(connectionstring))
            {
                MySqlCommand cmd = new MySqlCommand(queryString, conn);
                cmd.Parameters.Add(new MySqlParameter("@phone", phone));
                cmd.Parameters.Add(new MySqlParameter("@new_address", new_address));
                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}

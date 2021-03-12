using AssetDeprciation.DatabaseConnection;
using AssetDeprciation.Models;
using AssetDeprciation.Security;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AssetDeprciation.Controllers
{
    public class AuthenticateController : Controller
    {
        //
        // GET: /Authenticate/
        public ActionResult Login()
        {
            AuthenticateModel model = new AuthenticateModel();
            
            model.Book = getbookList();
            return View(model);
        }

     public List<SelectListItem> getbookList()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            SqlConnection con = DBConnection.getConnection();
            //string sql = "select * from Jct_Asset_Dep_Book_AccountingYear where Closed<>1 or Manual<>1";
            string sql = "select BookCode,AccYear from Jct_Asset_Dep_Book_AccountingYear where Closed<>1 or Manual<>1 UNION  SELECT BookCode,FinYear FROM Jct_Asset_Dep_FinancialYear WHERE Closed<>1 OR Manual <> 1 ";     
            SqlCommand cmd = new SqlCommand(sql, con);
            SqlDataReader read = cmd.ExecuteReader();
            if (read.HasRows)
            {
                while (read.Read())
                {
                    string book = read["BookCode"].ToString() + "(" + read["AccYear"].ToString() + ")";
                    list.Add(new SelectListItem { Text = book, Value = book });
                }
               
            }
            return list; 

        }
        [HttpPost]
        public ActionResult Login(AuthenticateModel model)
        {



                string username = null;

                SqlConnection con = DBConnection.getConnection();

                string sql = "select empcode  from jctdev4.dbo.jct_login_emp where empcode='" + model.Username + "' and active = 'Y' and ((convert(varchar(8),dateofbirth,112)='" + model.Password + " ' and  new_pass is null)  or (new_pass is not null and new_pass=convert(varchar(30),convert(varbinary,'" + model.Password + "')))) ";
                SqlCommand cmd = new SqlCommand(sql, con);
                SqlDataReader read = cmd.ExecuteReader();
                while (read.Read())
                {
                    username = read["empcode"].ToString();
                }
                read.Close();
                if (username != null)
                {
                    ArrayList li = new ArrayList();
                    string Dept = null;
                    string query = "select Rolename,Dept from jct_asset_dep_role where empcode=@1";
                    SqlCommand cmd1 = new SqlCommand(query, con);
                    cmd1.Parameters.AddWithValue("@1", username);
                    SqlDataReader reader = cmd1.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            li.Add(reader["Rolename"].ToString());
                            Dept = reader["Dept"].ToString();
                        }
                        reader.Close();
                        con.Close();

                        //foreach (var item in li)
                        //{
                        //    roles = (string[])item;
                        //}
                        //-----------------Security Initialization--------------------//
                        var roles = li.Cast<string>().ToArray();
                        CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                        serializeModel.UserId = username;
                        serializeModel.roles = roles;
                        serializeModel.book = model.BookName;
                        serializeModel.dept = Dept;
                        string userData = JsonConvert.SerializeObject(serializeModel);
                        FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                             1,
                            username,
                             DateTime.Now,
                             DateTime.Now.AddMinutes(15),
                             false,
                             userData);
                        string encTicket = FormsAuthentication.Encrypt(authTicket);
                        HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                        Response.Cookies.Add(faCookie);
                        return RedirectToAction("Index", "Home");
                        //------------------------------------------------------------//
                    }
                    else
                    {
                        ModelState.AddModelError("", "Incorrect Username or Password");
                    }
                }

                else
                {
                    ModelState.AddModelError("", "Incorrect Username or Password");

                }
                model.Book = getbookList();
               return View(model);
            }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Authenticate", null);
        }
        }
	}

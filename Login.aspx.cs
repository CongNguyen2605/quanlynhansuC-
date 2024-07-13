using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BTL_QLNS
{
    public partial class Login : System.Web.UI.Page
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\LTWeb Nâng Cao\Database\QLNS1.mdb";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM Tai_Khoan WHERE TenDangNhap = @Username AND MatKhau = @Password";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Username", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        string tenDangNhap = reader["TenDangNhap"].ToString();
                        string maVaiTro = reader["MaVaiTro"].ToString();


                        Session["TenDangNhap"] = tenDangNhap;
                        Session["MaVaiTro"] = maVaiTro;

                        Response.Redirect("Menu.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tên đăng nhập hoặc mật khẩu không đúng.');", true);
                    }
                }
            }
        }






        protected void btnRegister_Click(object sender, EventArgs e)
        {
            string newUsername = txtNewUsername.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (newPassword != confirmPassword)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Xác nhận mật khẩu không khớp. Vui lòng kiểm tra lại.');", true);
                return;
            }

 
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string queryCheckExist = "SELECT COUNT(*) FROM Tai_Khoan WHERE TenDangNhap = @Username";
                using (OleDbCommand cmdCheckExist = new OleDbCommand(queryCheckExist, conn))
                {
                    cmdCheckExist.Parameters.AddWithValue("@Username", newUsername);

                    conn.Open();
                    int existingUserCount = (int)cmdCheckExist.ExecuteScalar();

                    if (existingUserCount > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Tên đăng nhập đã tồn tại. Vui lòng chọn tên khác.');", true);
                        return;
                    }
                }
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string queryInsert = "INSERT INTO Tai_Khoan (TenDangNhap, MatKhau, MaVaiTro) VALUES (@Username, @Password, 2)";
                using (OleDbCommand cmdInsert = new OleDbCommand(queryInsert, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@Username", newUsername);
                    cmdInsert.Parameters.AddWithValue("@Password", newPassword);

                    conn.Open();
                    int rowsAffected = cmdInsert.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
            
                        Session["TenDangNhap"] = newUsername;
                        Session["MaVaiTro"] = "2"; 

                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đăng ký thành công.'); window.location='Menu.aspx';", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đã xảy ra lỗi khi đăng ký. Vui lòng thử lại.');", true);
                    }
                }
            }
        }



    }
}
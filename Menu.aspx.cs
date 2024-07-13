using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Globalization;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace BTL_QLNS
{
    public partial class Menu : System.Web.UI.Page
    {
        string connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\LTWeb Nâng Cao\Database\QLNS1.mdb";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               




                hdnSelectedTab.Value = "#employees-content";
                LoadNhanSuGrid();
                LoadPhongBanGrid();
                LoadChamCongGrid();
                LoadKhenThuongGrid();
                LoadBaoHiemGrid();
                LoadKieuNVGrid();
                LoadLuongGrid();
                LoadMonthYearFromChamCong();



                LoadSearchNhanSuGrid();
                LoadSearchChamCongGrid();
                LoadSearchKhenThuongGrid();
                LoadSearchBaoHiemGrid();

                loadmanv();
                Loadcbo();
                LoadDepartments();




                if (Session["TenDangNhap"] != null && Session["MaVaiTro"] != null)   // TenDangNhap khi Dang Nhap
                {
                    txtUsername.Text = Session["TenDangNhap"].ToString();
                    txtRole.Text = Session["MaVaiTro"].ToString();
                }
                else
                {
                    Response.Redirect("Login.aspx");
                }






                ddlDay.Items.Clear();
                ddlDay.Items.Add(new ListItem("-- Chọn ngày --", ""));
                for (int i = 1; i <= 31; i++)
                {
                    ddlDay.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }


                ddlDayReward.Items.Clear();
                ddlDayReward.Items.Add(new ListItem("-- Chọn ngày --", ""));
                for (int i = 1; i <= 31; i++)
                {
                    ddlDayReward.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }

                



            }
            else
            {
                // Đọc giá trị của HiddenField để khôi phục tab
                string selectedTabId = hdnSelectedTab.Value;
                // Sử dụng giá trị này để khôi phục trạng thái tab (nếu cần)
            }
        }






        private void LoadNhanSuGrid()
        {

            string query = "SELECT MaNhanVien, HoDem, Ten, NgaySinh, DiaChi, DienThoai, ChucVu, MaPhongBan, KieuNhanVien, IIF(LyLich IS NOT NULL, 'Có', 'Không') AS CoLyLich FROM Nhan_Vien";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        DataTable dt = new DataTable();
                        dt.Load(reader);
                        gvEmployees.DataSource = dt;
                        gvEmployees.DataBind();
                    }
                }
            }
        }
        private void LoadPhongBanGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM Phong_Ban";
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvDepartments.DataSource = dt; 
                gvDepartments.DataBind();
            }
        }

        private void LoadKhenThuongGrid()
        {
            string query = "SELECT Thuong_Phat.MaThuongPhat,Thuong_Phat.MaNhanVien, Nhan_Vien.HoDem + ' ' + Nhan_Vien.Ten as HoTen, Thuong_Phat.NgayThang, Thuong_Phat.ThuongPhat, Thuong_Phat.LyDo, Thuong_Phat.GiaTIen FROM Thuong_Phat inner join Nhan_Vien on Thuong_Phat.MaNhanVien = Nhan_Vien.MaNhanVien";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                gvRewards.DataSource = dt;
                gvRewards.DataBind();
            }
        }


        private void LoadBaoHiemGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM Bao_Hiem";
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvInsurance.DataSource = dt;
                gvInsurance.DataBind();
            }
        }


        private void LoadKieuNVGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = @"SELECT Nhan_Vien.MaNhanVien, Nhan_Vien.HoDem, Nhan_Vien.Ten, Kieu_NV.KieuNhanVien, Kieu_NV.HeSoLuong, Kieu_NV.LuongCung, Kieu_NV.LuongThoiVu
                 FROM Nhan_Vien
                 INNER JOIN Kieu_NV ON Nhan_Vien.MaNhanVien = Kieu_NV.MaNhanVien";

                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                
                gvEmployeeTypes.DataSource = dt;
                gvEmployeeTypes.DataBind();
            }
        }


        private void LoadChamCongGrid()
        {  
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT MaNhanVien, HoDem, Ten FROM Nhan_Vien";
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAttendance.DataSource = dt;
                gvAttendance.DataBind();
            }
        }

        private void LoadLuongGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT MaNhanVien, HoDem, Ten FROM Nhan_Vien";
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvAttendance.DataSource = dt;
                gvAttendance.DataBind();
            }
        }



        private void LoadSearchNhanSuGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT MaNhanVien, HoDem, Ten, DiaChi, DienThoai, ChucVu, MaPhongBan FROM Nhan_Vien";
                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSearchEmployeeResults.DataSource = dt;
                gvSearchEmployeeResults.DataBind();
            }
        }

        private void LoadSearchChamCongGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT C.MaNhanVien, NV.HoDem, NV.Ten, C.Ngay " +
                               "FROM Cham_Cong C INNER JOIN Nhan_Vien NV ON C.MaNhanVien = NV.MaNhanVien";

                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSearchAttendanceResults.DataSource = dt;
                gvSearchAttendanceResults.DataBind();
            }
        }

        private void LoadSearchKhenThuongGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT TP.MaNhanVien, NV.HoDem, NV.Ten, TP.NgayThang, TP.LyDo, TP.GiaTien " +
                               "FROM Thuong_Phat TP INNER JOIN Nhan_Vien NV ON TP.MaNhanVien = NV.MaNhanVien";

                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSearchRewardResults.DataSource = dt;
                gvSearchRewardResults.DataBind();
            }
        }

        private void LoadSearchBaoHiemGrid()
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT BH.MaNhanVien, NV.HoDem, NV.Ten, BH.NgayCap, BH.NgayHetHan " +
                               "FROM Bao_Hiem BH INNER JOIN Nhan_Vien NV ON BH.MaNhanVien = NV.MaNhanVien";

                OleDbDataAdapter da = new OleDbDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvSearchInsuranceResults.DataSource = dt;
                gvSearchInsuranceResults.DataBind();
            }
        }






        // Nhân viên 


        private void LoadDepartments()
        {

            string query = "SELECT MaPhongBan, TenPhongBan FROM Phong_Ban";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        txtDepartment.DataSource = reader;
                        txtDepartment.DataTextField = "TenPhongBan";
                        txtDepartment.DataValueField = "MaPhongBan";
                        txtDepartment.DataBind();
                    }
                    conn.Close();
                }
            }
        }

        protected void btnAddEmployee_Click(object sender, EventArgs e)
        {
            btnSaveEmployee.Style["display"] = "none";
            btnAddNV.Style["display"] = "block";
            ClearEmployeeFields();
            LoadDepartments(); // Load lại danh sách phòng ban
            ScriptManager.RegisterStartupScript(this, this.GetType(), "employeePopup", "$('#employeePopup').modal('show');", true);
        }





        private void ClearEmployeeFields()
        {
            txtEmployeeFirstName.Text = "";
            txtEmployeeLastName.Text = "";
            txtDate.Text = "";
            txtDC.Text = "";
            txtDT.Text = "";
            txtPosition.Text = "";
            txtDateTD.Text = "";
            txtMaNV.Text = "";
        }
        protected void btnDeleteEmployee_Click(object sender, EventArgs e)
        {
            Button btnDeleteEmployee = (Button)sender;
            string maNhanVien = btnDeleteEmployee.CommandArgument;

            ConfirmDeleteEmployee(maNhanVien);
        }

        protected void ConfirmDeleteEmployee(string maNhanVien)
        {
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();

                // Xóa dữ liệu từ bảng Luong
                string queryLuong = "DELETE FROM Luong WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdLuong = new OleDbCommand(queryLuong, conn))
                {
                    cmdLuong.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdLuong.ExecuteNonQuery();
                }

                // Xóa dữ liệu từ bảng Cham_Cong
                string queryChamCong = "DELETE FROM Cham_Cong WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdChamCong = new OleDbCommand(queryChamCong, conn))
                {
                    cmdChamCong.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdChamCong.ExecuteNonQuery();
                }

                // Xóa dữ liệu từ bảng Kieu_NV
                string queryKieuNV = "DELETE FROM Kieu_NV WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdKieuNV = new OleDbCommand(queryKieuNV, conn))
                {
                    cmdKieuNV.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdKieuNV.ExecuteNonQuery();
                }

                // Xóa dữ liệu từ bảng Bao_Hiem
                string queryBaoHiem = "DELETE FROM Bao_Hiem WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdBaoHiem = new OleDbCommand(queryBaoHiem, conn))
                {
                    cmdBaoHiem.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdBaoHiem.ExecuteNonQuery();
                }

                // Xóa dữ liệu từ bảng Khen_Thuong
                string queryKhenThuong = "DELETE FROM Thuong_Phat WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdKhenThuong = new OleDbCommand(queryKhenThuong, conn))
                {
                    cmdKhenThuong.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmdKhenThuong.ExecuteNonQuery();
                }

                // Xóa dữ liệu từ bảng Nhan_Vien
                string queryNhanVien = "DELETE FROM Nhan_Vien WHERE MaNhanVien = @MaNhanVien";
                using (OleDbCommand cmdNhanVien = new OleDbCommand(queryNhanVien, conn))
                {
                    cmdNhanVien.Parameters.AddWithValue("@MaNhanVien", maNhanVien);

                    try
                    {
                        int rowsAffected = cmdNhanVien.ExecuteNonQuery();

                        if (rowsAffected > 0)
                        {
                            ShowSuccessMessage("Xóa nhân viên thành công.");
                        }
                        else
                        {
                            ShowErrorMessage("Xóa nhân viên thất bại.");
                        }
                    }
                    catch (Exception ex)
                    {
                        ShowErrorMessage("Lỗi: " + ex.Message);
                    }
                }

                conn.Close();
                LoadNhanSuGrid();
                loadmanv();
                Loadcbo();
                LoadChamCongGrid();
                LoadKieuNVGrid();
                LoadLuongGrid();
                LoadKhenThuongGrid();
                LoadBaoHiemGrid();
            }
        }



        protected void btnAddNV_Click(object sender, EventArgs e)
        {
            string queryNV = "INSERT INTO Nhan_Vien (MaNhanVien, HoDem, Ten, NgaySinh, GioiTinh, DiaChi, DienThoai, ChucVu, KieuNhanVien, NgayTuyenDung, MaPhongBan, TrinhDo, LyLich) " +
                             "VALUES (?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?, ?)";
            string queryKieuNV = "INSERT INTO Kieu_NV (MaNhanVien, KieuNhanVien) VALUES (?, ?)";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbTransaction transaction = conn.BeginTransaction();

                try
                {
                    using (OleDbCommand cmdNV = new OleDbCommand(queryNV, conn, transaction))
                    {
                        cmdNV.Parameters.AddWithValue("?", txtMaNV.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", txtEmployeeFirstName.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", txtEmployeeLastName.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", DateTime.ParseExact(txtDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        cmdNV.Parameters.AddWithValue("?", ddlGender.SelectedValue);
                        cmdNV.Parameters.AddWithValue("?", txtDC.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", txtDT.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", txtPosition.Text.Trim());
                        cmdNV.Parameters.AddWithValue("?", txtType.SelectedValue);
                        cmdNV.Parameters.AddWithValue("?", DateTime.ParseExact(txtDateTD.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        cmdNV.Parameters.AddWithValue("?", txtDepartment.SelectedValue);
                        cmdNV.Parameters.AddWithValue("?", ddlTrinhDo.SelectedValue);

                        if (fileLyLich.HasFile)
                        {
                            byte[] fileData;
                            using (BinaryReader br = new BinaryReader(fileLyLich.PostedFile.InputStream))
                            {
                                fileData = br.ReadBytes(fileLyLich.PostedFile.ContentLength);
                            }
                            cmdNV.Parameters.AddWithValue("?", fileData);
                        }
                        else
                        {
                            cmdNV.Parameters.AddWithValue("?", DBNull.Value);
                        }

                        int rowsAffectedNV = cmdNV.ExecuteNonQuery();

                        if (rowsAffectedNV > 0)
                        {
                            using (OleDbCommand cmdKieuNV = new OleDbCommand(queryKieuNV, conn, transaction))
                            {
                                cmdKieuNV.Parameters.AddWithValue("?", txtMaNV.Text.Trim());
                                cmdKieuNV.Parameters.AddWithValue("?", txtType.SelectedValue);

                                int rowsAffectedKieuNV = cmdKieuNV.ExecuteNonQuery();

                                if (rowsAffectedKieuNV > 0)
                                {
                                    transaction.Commit();
                                    ShowSuccessMessage("Thêm nhân viên thành công.");
                                    ClearEmployeeFields();
                                    LoadNhanSuGrid();
                                    loadmanv();
                                    Loadcbo();
                                    LoadChamCongGrid();
                                    LoadKieuNVGrid();
                                }
                                else
                                {
                                    transaction.Rollback();
                                    ShowErrorMessage("Thêm KieuNhanVien không thành công.");
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback();
                            ShowErrorMessage("Thêm nhân viên không thành công.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    ShowErrorMessage("Lỗi: " + ex.Message);
                }
            }
        }


        protected void btnSaveEmployee_Click(object sender, EventArgs e)
        {

            string updateNhanVienQuery = "UPDATE Nhan_Vien SET HoDem = ?, Ten = ?, NgaySinh = ?, GioiTinh = ?, DiaChi = ?, DienThoai = ?, ChucVu = ?, KieuNhanVien = ?, NgayTuyenDung = ?, MaPhongBan = ?,  TrinhDo = ?, LyLich = ? WHERE MaNhanVien = ?";

   
            string updateKieuNVQuery = "UPDATE Kieu_NV SET KieuNhanVien = ? WHERE MaNhanVien = ?";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbTransaction transaction = conn.BeginTransaction(); 

                try
                {
                 
                    using (OleDbCommand cmdNhanVien = new OleDbCommand(updateNhanVienQuery, conn, transaction))
                    {
                        cmdNhanVien.Parameters.AddWithValue("?", txtEmployeeFirstName.Text.Trim());
                        cmdNhanVien.Parameters.AddWithValue("?", txtEmployeeLastName.Text.Trim());
                        cmdNhanVien.Parameters.AddWithValue("?", DateTime.ParseExact(txtDate.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        cmdNhanVien.Parameters.AddWithValue("?", ddlGender.SelectedValue);
                        cmdNhanVien.Parameters.AddWithValue("?", txtDC.Text.Trim());
                        cmdNhanVien.Parameters.AddWithValue("?", txtDT.Text.Trim());
                        cmdNhanVien.Parameters.AddWithValue("?", txtPosition.Text.Trim());
                        cmdNhanVien.Parameters.AddWithValue("?", txtType.SelectedValue);
                        cmdNhanVien.Parameters.AddWithValue("?", DateTime.ParseExact(txtDateTD.Text.Trim(), "dd/MM/yyyy", CultureInfo.InvariantCulture));
                        cmdNhanVien.Parameters.AddWithValue("?", txtDepartment.SelectedValue);
                        cmdNhanVien.Parameters.AddWithValue("?", ddlTrinhDo.SelectedValue);

                    
                        if (fileLyLich.HasFile)
                        {
                            byte[] fileData;
                            using (BinaryReader br = new BinaryReader(fileLyLich.PostedFile.InputStream))
                            {
                                fileData = br.ReadBytes(fileLyLich.PostedFile.ContentLength);
                            }
                            cmdNhanVien.Parameters.AddWithValue("?", fileData);
                        }
                        else
                        {
                            cmdNhanVien.Parameters.AddWithValue("?", DBNull.Value);
                        }

                        cmdNhanVien.Parameters.AddWithValue("?", hdnEmployeeID.Value);

                        int rowsAffectedNhanVien = cmdNhanVien.ExecuteNonQuery();

          
                        if (rowsAffectedNhanVien > 0)
                        {
                            using (OleDbCommand cmdKieuNV = new OleDbCommand(updateKieuNVQuery, conn, transaction))
                            {
                                cmdKieuNV.Parameters.AddWithValue("?", txtType.SelectedValue); 
                                cmdKieuNV.Parameters.AddWithValue("?", hdnEmployeeID.Value);

                                int rowsAffectedKieuNV = cmdKieuNV.ExecuteNonQuery();

                                if (rowsAffectedKieuNV > 0)
                                {
                                    transaction.Commit(); 
                                    ShowSuccessMessage("Cập nhật thông tin nhân viên và loại nhân viên thành công.");
                                    ClearEmployeeFields();
                                    LoadNhanSuGrid(); 
                                    LoadKieuNVGrid(); 
                                }
                                else
                                {
                                    transaction.Rollback(); 
                                    ShowErrorMessage("Cập nhật loại nhân viên không thành công.");
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback(); 
                            ShowErrorMessage("Cập nhật thông tin nhân viên không thành công.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    ShowErrorMessage("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }



        protected void btnEditEmployee_Click(object sender, EventArgs e)
        {
            Button btnEditEmployee = (Button)sender;
            string employeeID = btnEditEmployee.CommandArgument;
            btnSaveEmployee.Style["display"] = "block";
            btnAddNV.Style["display"] = "none";

            string query = "SELECT * FROM Nhan_Vien WHERE MaNhanVien = @MaNhanVien";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", employeeID);
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            txtMaNV.Text = reader["MaNhanVien"].ToString();
                            txtEmployeeFirstName.Text = reader["HoDem"].ToString();
                            txtEmployeeLastName.Text = reader["Ten"].ToString();
                            txtDate.Text = DateTime.Parse(reader["NgaySinh"].ToString()).ToString("dd/MM/yyyy");
                            ddlGender.SelectedValue = reader["GioiTinh"].ToString();
                            txtDC.Text = reader["DiaChi"].ToString();
                            txtDT.Text = reader["DienThoai"].ToString();
                            txtPosition.Text = reader["ChucVu"].ToString();
                            txtType.SelectedValue = reader["KieuNhanVien"].ToString();
                            txtDateTD.Text = DateTime.Parse(reader["NgayTuyenDung"].ToString()).ToString("dd/MM/yyyy");
                            txtDepartment.SelectedValue = reader["MaPhongBan"].ToString();
                            ddlTrinhDo.SelectedValue = reader["TrinhDo"].ToString();
                            hdnEmployeeID.Value = employeeID;

                            // Hiển thị lý lịch nếu có
                            string lyLich = reader["LyLich"].ToString();
                            if (!string.IsNullOrEmpty(lyLich))
                            {
                                fileLyLich.Visible = true;
                            }
                        }
                    }
                }
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "showPopup", "$('#employeePopup').modal('show');", true);
        }

        protected void DownloadLyLich_Click(object sender, EventArgs e)
        {
            LinkButton lnkDownloadLyLich = (LinkButton)sender;
            string employeeID = lnkDownloadLyLich.CommandArgument;

            string query = "SELECT LyLich, HoDem, Ten FROM Nhan_Vien WHERE MaNhanVien = @MaNhanVien";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", employeeID);
                    conn.Open();
                    using (OleDbDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            byte[] fileData = reader["LyLich"] as byte[];
                            string hoDem = reader["HoDem"].ToString();
                            string ten = reader["Ten"].ToString();

                            if (fileData != null)
                            {
                                string fileName = $"{hoDem}_{ten}.pdf";

                                Response.Clear();
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Content-Disposition", $"attachment; filename={fileName}");
                                Response.BinaryWrite(fileData);
                                Response.End();
                            }
                            else
                            {
                                ShowErrorMessage("Không tìm thấy lý lịch cho nhân viên này.");
                            }
                        }
                    }
                }
            }
        }




        private void ShowErrorMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "errorAlert", $"alert('{message}');", true);
        }

        private void ShowSuccessMessage(string message)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "successAlert", $"alert('{message}');", true);
        }





        // Phòng ban



        private void ClearDepartmentFields()
        {
            txtDepartmentName.Text = "";
            txtDepartmentID.Text = "";
        }

        protected void btnAddDepartment_Click(object sender, EventArgs e)  
        {
            btnSaveDepartment.Style["display"] = "none";
            btnAddPB.Style["display"] = "block";

        
            ClearDepartmentFields();

      
            hdnDepartmentID.Value = "";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "departmentPopup", "$('#departmentPopup').modal('show');", true);
        }


        protected void btnEditDepartment_Click(object sender, EventArgs e)
        {
            Button btnEditDepartment = (Button)sender;
            string departmentID = btnEditDepartment.CommandArgument;
            btnSaveDepartment.Style["display"] = "block";
            btnAddPB.Style["display"] = "none";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM Phong_Ban WHERE MaPhongBan = @MaPhongBan";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaPhongBan", departmentID);

                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtDepartmentName.Text = reader["TenPhongBan"].ToString();
                    txtDepartmentID.Text = reader["MaPhongBan"].ToString();
                }
                reader.Close();
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "departmentPopup", "$('#departmentPopup').modal('show');", true);
        }






        // Code thêm sửa xóa 




        protected void btnAddPB_Click(object sender, EventArgs e)
        {
            string departmentID = txtDepartmentID.Text.Trim();
            string departmentName = txtDepartmentName.Text.Trim();

       
            if (string.IsNullOrEmpty(departmentID) || string.IsNullOrEmpty(departmentName))
            {
               
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "departmentPopup", "$('#departmentPopup').modal('show');", true);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string queryCheck = "SELECT COUNT(*) FROM Phong_Ban WHERE MaPhongBan = ?";
                    string queryInsert = "INSERT INTO Phong_Ban(MaPhongBan, TenPhongBan) VALUES(?, ?)";

                    conn.Open();

                    using (OleDbCommand cmdCheck = new OleDbCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("?", departmentID);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count > 0)
                        {
                           
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mã phòng ban đã tồn tại. Vui lòng nhập mã phòng ban khác.');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "departmentPopup", "$('#departmentPopup').modal('show');", true);
                            return;
                        }
                    }

                    using (OleDbCommand cmdInsert = new OleDbCommand(queryInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("?", departmentID);
                        cmdInsert.Parameters.AddWithValue("?", departmentName);
                        cmdInsert.ExecuteNonQuery();

                     
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm thành công');", true);
                       

                    }
                    conn.Close();
                    LoadPhongBanGrid();











                }
            }
            catch (Exception ex)
            {
            
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đã xảy ra lỗi: " + ex.Message.Replace("'", "\\'") + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "departmentPopup", "$('#departmentPopup').modal('show');", true);
            }
        }


        protected void btnDeleteDepartment_Click(object sender, EventArgs e)  
        {
            Button btnDelete = (Button)sender;
            string departmentId = btnDelete.CommandArgument;
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "Delete from Phong_Ban where MaPhongBan = ?";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("MaPhongBan", departmentId);
                    int row = cmd.ExecuteNonQuery();
                    if (row > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Xóa thành công');", true);

                    }
                    conn.Close();
                    LoadPhongBanGrid();

                }
            }
        }

        protected void btnSaveDepartment_Click(object sender, EventArgs e)
        {
            string query = "UPDATE Phong_Ban SET TenPhongBan = ? WHERE MaPhongBan = ?";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("?", txtDepartmentName.Text);
                    cmd.Parameters.AddWithValue("?", txtDepartmentID.Text);

                    cmd.ExecuteNonQuery();
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Sửa thành công');", true);
                    
                    connection.Close();
                    LoadPhongBanGrid();


                }
            }

            

        }







        // Khen Thưởng 

        private void loadmanv()
        {
            string query = string.Format("select * from Nhan_Vien");

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand(query, conn);

            conn.Open();
            OleDbDataAdapter ad = new OleDbDataAdapter(cmd);
            DataTable tb = new DataTable();
            ad.Fill(tb);
            ddl_manv.DataValueField = "MaNhanVien";
            ddl_manv.DataSource = tb;
            ddl_manv.DataBind();
            conn.Close();
        }



        protected void btnAddReward_Click(object sender, EventArgs e)    // MỞ POPUP THÊM 
        {
            btnSaveReward.Style["display"] = "none";
            btnAddKT.Style["display"] = "block";
            ClearDepartmentFields();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "rewardPopup", "$('#rewardPopup').modal('show');", true);
        }


        protected void btnEditReward_Click(object sender, EventArgs e)     // MỞ POPUP SỬA , CẦN PHẢI THÊM CODE LẤY THÔNG TIN ĐỂ HIỂN THỊ VÀO ĐÂY 
        {
            btnSaveReward.Style["display"] = "block";
            btnAddKT.Style["display"] = "none";

            Button btnEditReward = (Button)sender;
            string rewardID = btnEditReward.CommandArgument;


            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT * FROM Thuong_Phat WHERE MaThuongPhat = @MaThuongPhat";
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThuongPhat", rewardID);

                conn.Open();
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    // Lấy dữ liệu từ reader
                    string maNhanVien = reader["MaNhanVien"].ToString();
                    //DateTime ngayThang = Convert.ToDateTime(reader["NgayThang"]);
                    string ngayThang = Convert.ToDateTime(reader["NgayThang"]).ToString("yyyy-MM-dd");
                    string thuongPhat = reader["ThuongPhat"].ToString();
                    string lyDo = reader["LyDo"].ToString();
                    string giaTien = reader["GiaTien"].ToString();

                    // Đổ dữ liệu vào các điều khiển của modal popup
                    ddl_manv.SelectedValue = maNhanVien;
                    txtRewardNgayThang.Text = ngayThang;
                    ddl_thuongphat.SelectedValue = thuongPhat;
                    txtRewardLyDo.Text = lyDo;
                    txtRewardGiaTien.Text = giaTien;

                    hiddenRewardID.Value = rewardID;



                    ScriptManager.RegisterStartupScript(this, this.GetType(), "rewardPopup", "$('#rewardPopup').modal('show');", true);
                }
                conn.Close();
            }

        }



        protected void btnAddKT_Click(object sender, EventArgs e)   // CODE ĐỂ THÊM VÀO CSDL 
        {
            string ma = ddl_manv.SelectedValue.ToString();
            string ngaythang = txtRewardNgayThang.Text;
            string thuongphat = ddl_thuongphat.SelectedValue.ToString();
            string lydo = txtRewardLyDo.Text;
            string giatien = txtRewardGiaTien.Text;

            string query = string.Format("insert into Thuong_Phat (MaNhanVien, NgayThang, ThuongPhat, LyDo, GiaTien) values ('{0}','{1}', '{2}', '{3}', '{4}')", ma, ngaythang, thuongphat, lydo, giatien);

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand(query, conn);

            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            LoadKhenThuongGrid();
        }



        protected void btnDeleteReward_Click(object sender, EventArgs e)
        {
            Button btnDeleteReward = (Button)sender;
            string rewardID = btnDeleteReward.CommandArgument;

            string query = "DELETE FROM Thuong_Phat WHERE MaThuongPhat = @MaThuongPhat";

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                OleDbCommand cmd = new OleDbCommand(query, conn);
                cmd.Parameters.AddWithValue("@MaThuongPhat", rewardID);

                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }

            LoadKhenThuongGrid();
        }




        protected void btnSaveReward_Click(object sender, EventArgs e)  // CODE UPDATE VÀO CSDL
        {
            string rewardID = hiddenRewardID.Value;

            string maNhanVien = ddl_manv.SelectedValue;
            string ngayThang = txtRewardNgayThang.Text;
            string thuongPhat = ddl_thuongphat.SelectedValue;
            string lyDo = txtRewardLyDo.Text;
            string giaTien = txtRewardGiaTien.Text;

            string query = string.Format("UPDATE Thuong_Phat set MaNhanVien = '{0}', NgayThang = '{1}', ThuongPhat = '{2}', LyDo = '{3}', GiaTien = '{4}' where MaThuongPhat = {5}", maNhanVien, ngayThang, thuongPhat, lyDo, giaTien, rewardID);

            OleDbConnection conn = new OleDbConnection(connectionString);
            OleDbCommand cmd = new OleDbCommand(query, conn);

            conn.Open();

            cmd.ExecuteNonQuery();
            conn.Close();
            LoadKhenThuongGrid();

        }























        // Bảo Hiểm 

        private void ClearInsurance()
        {
            txtInsuranceID.Text = "";
            ddlEmployeeID.SelectedIndex = -1;
            ddlInsuranceType.Text = "";
            txtIssuedDate.Text = "";
            txtExpiryDate.Text = "";
            txtIssuedPlace.Text = "";
        }


        protected void btnAddInsurance_Click(object sender, EventArgs e)   // MỞ POPUP THÊM 
        {

            btnSaveInsurance.Style["display"] = "none";
            btnAddBH.Style["display"] = "block";
            ClearInsurance();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
        }

        protected void btnEditInsurance_Click(object sender, EventArgs e)
        {
            try
            {
             
                Button btnEditInsurance = (Button)sender;

              
                string insuranceID = btnEditInsurance.CommandArgument;
                Debug.WriteLine("Insurance ID: " + insuranceID); 

                
                btnSaveInsurance.Style["display"] = "block";
                btnAddBH.Style["display"] = "none";

              
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
             
                    string query = "SELECT * FROM Bao_Hiem WHERE MaBaoHiem = @MaBaoHiem";
                    Debug.WriteLine("Query: " + query); 

                  
                    OleDbCommand cmd = new OleDbCommand(query, conn);

                    
                    cmd.Parameters.AddWithValue("@MaBaoHiem", insuranceID);
                    Debug.WriteLine("Parameter added: @MaBaoHiem = " + insuranceID); 

             
                    conn.Open();
                    Debug.WriteLine("Database connection opened."); 

              
                    OleDbDataReader reader = cmd.ExecuteReader();
                    Debug.WriteLine("Query executed."); 

                  
                    if (reader.Read())
                    {
                      
                        ddlEmployeeID.Text = reader["MaNhanVien"].ToString();
                        ddlInsuranceType.Text = reader["LoaiBH"].ToString();
                        txtIssuedDate.Text = Convert.ToDateTime(reader["NgayCap"]).ToString("yyyy-MM-dd");
                        txtExpiryDate.Text = Convert.ToDateTime(reader["NgayHetHan"]).ToString("yyyy-MM-dd");
                        txtIssuedPlace.Text = reader["NoiCap"].ToString();
                        txtInsuranceID.Text = reader["MaBaoHiem"].ToString();
                        Debug.WriteLine("Data successfully retrieved and populated."); 
                    }
                    else
                    {
                   
                        Debug.WriteLine("No data found for the provided insurance ID: " + insuranceID);
                    }

                 
                    reader.Close();
                    Debug.WriteLine("Reader closed."); 
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
                Debug.WriteLine("Popup triggered to show."); 
            }
            catch (Exception ex)
            {
              
                Debug.WriteLine("An error occurred: " + ex.Message);
            }
        }

        private void Loadcbo()
        {

            string query = "SELECT MaNhanVien FROM Nhan_Vien";

            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                connection.Open();
                using (OleDbCommand cmd = new OleDbCommand(query, connection))
                {

                    OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    ddlEmployeeID.DataSource = dataTable;
                    ddlEmployeeID.DataValueField = "MaNhanVien";
                    ddlEmployeeID.DataBind();
                }

            }


        }


        protected void btnAddBH_Click(object sender, EventArgs e)
        {
            string insuranceID = txtInsuranceID.Text.Trim();
            string employeeID = ddlEmployeeID.SelectedValue.Trim();
            string insuranceType = ddlInsuranceType.Text.Trim();
            string issuedDate = txtIssuedDate.Text.Trim();
            string expiryDate = txtExpiryDate.Text.Trim();
            string issuedPlace = txtIssuedPlace.Text.Trim();

            // Kiểm tra nếu các trường đầu vào trống
            if (string.IsNullOrEmpty(insuranceID) || string.IsNullOrEmpty(employeeID) ||
                string.IsNullOrEmpty(insuranceType) || string.IsNullOrEmpty(issuedDate) ||
                string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(issuedPlace))
            {
                // Thông báo và mở lại popup
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string queryCheck = "SELECT COUNT(*) FROM Bao_Hiem WHERE MaBaoHiem = ?";
                    string queryInsert = "INSERT INTO Bao_Hiem(MaBaoHiem, MaNhanVien, LoaiBH, NgayCap, NgayHetHan, NoiCap) VALUES(?, ?, ?, ?, ?, ?)";

                    conn.Open();

                    using (OleDbCommand cmdCheck = new OleDbCommand(queryCheck, conn))
                    {
                        cmdCheck.Parameters.AddWithValue("?", insuranceID);
                        int count = (int)cmdCheck.ExecuteScalar();

                        if (count > 0)
                        {
                          
                            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Mã bảo hiểm đã tồn tại. Vui lòng nhập mã bảo hiểm khác.');", true);
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
                            return;
                        }
                    }

                    using (OleDbCommand cmdInsert = new OleDbCommand(queryInsert, conn))
                    {
                        cmdInsert.Parameters.AddWithValue("?", insuranceID);
                        cmdInsert.Parameters.AddWithValue("?", employeeID);
                        cmdInsert.Parameters.AddWithValue("?", insuranceType);
                        cmdInsert.Parameters.AddWithValue("?", DateTime.Parse(issuedDate));
                        cmdInsert.Parameters.AddWithValue("?", DateTime.Parse(expiryDate));
                        cmdInsert.Parameters.AddWithValue("?", issuedPlace);
                        cmdInsert.ExecuteNonQuery();

                       
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Thêm thành công');", true);
                        conn.Close();
                        LoadBaoHiemGrid();

                    }
                   
                   

                }
            }
            catch (Exception ex)
            {
          
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đã xảy ra lỗi: " + ex.Message.Replace("'", "\\'") + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
            }
        }


        protected void btnSaveInsurance_Click(object sender, EventArgs e)
        {
            string insuranceID = txtInsuranceID.Text.Trim();
            string employeeID = ddlEmployeeID.SelectedValue.Trim();
            string insuranceType = ddlInsuranceType.Text.Trim();
            string issuedDate = txtIssuedDate.Text.Trim();
            string expiryDate = txtExpiryDate.Text.Trim();
            string issuedPlace = txtIssuedPlace.Text.Trim();

            // Kiểm tra nếu các trường đầu vào trống
            if (string.IsNullOrEmpty(insuranceID) || string.IsNullOrEmpty(employeeID) ||
                string.IsNullOrEmpty(insuranceType) || string.IsNullOrEmpty(issuedDate) ||
                string.IsNullOrEmpty(expiryDate) || string.IsNullOrEmpty(issuedPlace))
            {
                // Thông báo và mở lại popup
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Vui lòng nhập đầy đủ thông tin.');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
                return;
            }

            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string queryUpdate = "UPDATE Bao_Hiem SET MaNhanVien = ?, LoaiBH = ?, NgayCap = ?, NgayHetHan = ?, NoiCap = ? WHERE MaBaoHiem = ?";

                    conn.Open();

                    using (OleDbCommand cmdUpdate = new OleDbCommand(queryUpdate, conn))
                    {
                        cmdUpdate.Parameters.AddWithValue("?", employeeID);
                        cmdUpdate.Parameters.AddWithValue("?", insuranceType);
                        cmdUpdate.Parameters.AddWithValue("?", DateTime.Parse(issuedDate));
                        cmdUpdate.Parameters.AddWithValue("?", DateTime.Parse(expiryDate));
                        cmdUpdate.Parameters.AddWithValue("?", issuedPlace);
                        cmdUpdate.Parameters.AddWithValue("?", insuranceID);
                        cmdUpdate.ExecuteNonQuery();

                     
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công');", true);
                        conn.Close();
                        LoadBaoHiemGrid();
                    }
        

   
                }
            }
            catch (Exception ex)
            {
                
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đã xảy ra lỗi: " + ex.Message.Replace("'", "\\'") + "');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "insurancePopup", "$('#insurancePopup').modal('show');", true);
            }
        }



        protected void btnDeleteInsurance_Click(object sender, EventArgs e)
        {
            Button btnDeleteInsurance = (Button)sender;
            string insuranceID = btnDeleteInsurance.CommandArgument;

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                string query = "DELETE FROM Bao_Hiem WHERE MaBaoHiem = ?";

                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("MaBaoHiem", insuranceID);
                    int row = cmd.ExecuteNonQuery();

                    if (row > 0)
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Xóa thành công');", true);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Không tìm thấy mã bảo hiểm.');", true);
                    }
                }
                conn.Close();
                LoadBaoHiemGrid();
            }
        }




        // Kiểu Nhân Viên




        private void LoadKieuNVDropdown()
        {
        
            ddlEmployeeTypeKieuNV.Items.Clear();
            ddlEmployeeTypeKieuNV.Items.Add(new ListItem("Chính thức", "Chính thức"));
            ddlEmployeeTypeKieuNV.Items.Add(new ListItem("Thời vụ", "Thời vụ"));
        }




        protected void btnEditEmployeeType_Click(object sender, EventArgs e)
        {
            btnSaveEmployeeType.Style["display"] = "block";

            LoadKieuNVDropdown();

   
            Button btnEdit = (Button)sender;
            GridViewRow row = (GridViewRow)btnEdit.NamingContainer;

            if (row != null && row.RowType == DataControlRowType.DataRow)
            {
             
                string maNV = row.Cells[0].Text; 

         
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = @"SELECT MaNhanVien, KieuNhanVien, HeSoLuong, LuongCung, LuongThoiVu 
                     FROM Kieu_NV 
                     WHERE MaNhanVien = ?";
                    OleDbCommand cmd = new OleDbCommand(query, conn);
                    cmd.Parameters.AddWithValue("?", maNV);
                    conn.Open();
                    OleDbDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                   
                        ddlEmployeeTypeMaNV.Text = maNV;
                        ddlEmployeeTypeKieuNV.SelectedValue = reader["KieuNhanVien"].ToString();
                        txtEmployeeTypeHeSoLuong.Text = reader["HeSoLuong"].ToString();
                        txtEmployeeTypeLuongCung.Text = reader["LuongCung"].ToString();
                        txtEmployeeTypeLuongThoiVu.Text = reader["LuongThoiVu"].ToString();
                    }
                    reader.Close();
                    conn.Close();
                    LoadKieuNVGrid();
                }

           
                ScriptManager.RegisterStartupScript(this, this.GetType(), "employeeTypePopup", "$('#employeeTypePopup').modal('show');", true);
            }
            else
            {
          
            }
        }

        protected void btnSaveEmployeeType_Click(object sender, EventArgs e)
        {
            string maNV = ddlEmployeeTypeMaNV.Text;
            string kieuNV = ddlEmployeeTypeKieuNV.SelectedValue;
            string heSoLuong = txtEmployeeTypeHeSoLuong.Text.Trim();
            string luongCung = txtEmployeeTypeLuongCung.Text.Trim();
            string luongThoiVu = txtEmployeeTypeLuongThoiVu.Text.Trim();

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(kieuNV))
            {
                return;
            }

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Câu lệnh UPDATE cho bảng Kieu_NV
                    string updateKieuNVQuery = @"
                UPDATE Kieu_NV 
                SET KieuNhanVien = ?, HeSoLuong = ?, LuongCung = ?, LuongThoiVu = ?
                WHERE MaNhanVien = ?";

                    using (OleDbCommand cmdKieuNV = new OleDbCommand(updateKieuNVQuery, conn, transaction))
                    {
                        cmdKieuNV.Parameters.AddWithValue("?", kieuNV);
                        cmdKieuNV.Parameters.AddWithValue("?", heSoLuong);
                        cmdKieuNV.Parameters.AddWithValue("?", luongCung);
                        cmdKieuNV.Parameters.AddWithValue("?", luongThoiVu);
                        cmdKieuNV.Parameters.AddWithValue("?", maNV);

                        int rowsAffectedKieuNV = cmdKieuNV.ExecuteNonQuery();

                    
                        if (rowsAffectedKieuNV > 0)
                        {
                         
                            string updateNhanVienQuery = @"
                        UPDATE Nhan_Vien 
                        SET KieuNhanVien = ?
                        WHERE MaNhanVien = ?";

                            using (OleDbCommand cmdNhanVien = new OleDbCommand(updateNhanVienQuery, conn, transaction))
                            {
                                cmdNhanVien.Parameters.AddWithValue("?", kieuNV);
                                cmdNhanVien.Parameters.AddWithValue("?", maNV);

                                int rowsAffectedNhanVien = cmdNhanVien.ExecuteNonQuery();

                                if (rowsAffectedNhanVien > 0)
                                {
                                    transaction.Commit(); 
                                    ShowSuccessMessage("Cập nhật thông tin nhân viên và loại nhân viên thành công.");
                                    LoadKieuNVGrid();
                                    LoadNhanSuGrid();
                                }
                                else
                                {
                                    transaction.Rollback(); 
                                    ShowErrorMessage("Cập nhật thông tin nhân viên không thành công.");
                                }
                            }
                        }
                        else
                        {
                            transaction.Rollback(); 
                            ShowErrorMessage("Cập nhật loại nhân viên không thành công.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); 
                    ShowErrorMessage("Lỗi: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
        }
















        // Chấm công 



        protected void btnUpdateAttendance_Click(object sender, EventArgs e)   
        {
            bool success = true; 


            foreach (GridViewRow row in gvAttendance.Rows)
            {
 
                string maNhanVien = row.Cells[0].Text; 
                DropDownList ddlStatus = (DropDownList)row.FindControl("ddlStatus"); 
                string ngayChamCong = txtDateCC.Text; 

                if (!UpdateAttendance(maNhanVien, ngayChamCong, ddlStatus.SelectedValue))
                {
                    success = false;
                }
            }

            if (success)
            {
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật thành công');", true);
                txtDateCC.Text = "";
            }
            else
            {

                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Cập nhật không thành công. Vui lòng thử lại.');", true);
            }
        }

        private bool UpdateAttendance(string maNhanVien, string ngayChamCong, string trangThai)
        {
            try
            {
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    string query = "INSERT INTO Cham_Cong (MaNhanVien, Ngay, TrangThai) VALUES (@MaNhanVien, @Ngay, @TrangThai)";
                    OleDbCommand cmd = new OleDbCommand(query, conn);


                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    cmd.Parameters.AddWithValue("@Ngay", ngayChamCong);
                    cmd.Parameters.AddWithValue("@TrangThai", trangThai);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    LoadChamCongGrid();
                    LoadMonthYearFromChamCong();
                }
                return true; 
            }
            catch (Exception ex)
            {
                return false; 
            }
        }



        // Lương

        private void LoadMonthYearFromChamCong()
        {
            DataTable dt = new DataTable();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                string query = "SELECT DISTINCT MONTH([Ngay]) AS [Month], YEAR([Ngay]) AS [Year] FROM Cham_Cong ORDER BY YEAR([Ngay]), MONTH([Ngay])";
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    conn.Open();
                    using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            ddlMonth.Items.Clear();
            ddlYear.Items.Clear();

            foreach (DataRow row in dt.Rows)
            {
                string month = row["Month"].ToString();
                string year = row["Year"].ToString();

                if (!ddlMonth.Items.Contains(new ListItem(month, month)))
                {
                    ddlMonth.Items.Add(new ListItem(month, month));
                }

                if (!ddlYear.Items.Contains(new ListItem(year, year)))
                {
                    ddlYear.Items.Add(new ListItem(year, year));
                }
            }
        }







        protected void btnViewSalary_Click(object sender, EventArgs e)
        {
            try
            {
                int selectedMonth = Convert.ToInt32(ddlMonth.SelectedValue);
                int selectedYear = Convert.ToInt32(ddlYear.SelectedValue);


                
                string deleteQuery = "DELETE FROM Luong WHERE MONTH(NgayLuong) = @Month AND YEAR(NgayLuong) = @Year";
                using (OleDbConnection conn = new OleDbConnection(connectionString))
                {
                    conn.Open();
                    using (OleDbCommand deleteCmd = new OleDbCommand(deleteQuery, conn))
                    {
                        deleteCmd.Parameters.AddWithValue("@Month", selectedMonth);
                        deleteCmd.Parameters.AddWithValue("@Year", selectedYear);
                        deleteCmd.ExecuteNonQuery();
                    }
                }

               
                string insertQuery = @"
                        INSERT INTO Luong (MaNhanVien, HoDem, Ten, NgayLuong, GiaTien, MaBaoHiem, TongLuong)
                        SELECT 
                            NV.MaNhanVien, NV.HoDem, NV.Ten, 
                            DateSerial(@Year, @Month, 1) AS NgayLuong,
                            IIF(TP.GiaTien IS NULL, 0, TP.GiaTien) AS GiaTien,
                            BH.MaBaoHiem,
                            (KieuNV.LuongCung * KieuNV.HeSoLuong 
                            + KieuNV.LuongThoiVu * 
                                (SELECT COUNT(*) 
                                 FROM Cham_Cong CC2 
                                 WHERE NV.MaNhanVien = CC2.MaNhanVien 
                                   AND MONTH(CC2.Ngay) = @Month 
                                   AND YEAR(CC2.Ngay) = @Year 
                                   AND CC2.TrangThai = 'Đi làm')
                            + IIF(TP.GiaTien IS NULL, 0, TP.GiaTien)
                            - IIF(BH.NgayHetHan > DATE(), 50000, 0)) AS TongLuong
                        FROM 
                            ((Nhan_Vien AS NV 
                            INNER JOIN Kieu_NV AS KieuNV ON NV.MaNhanVien = KieuNV.MaNhanVien) 
                            INNER JOIN Bao_Hiem AS BH ON NV.MaNhanVien = BH.MaNhanVien)
                            LEFT JOIN Thuong_Phat AS TP ON NV.MaNhanVien = TP.MaNhanVien
                        WHERE 
                            EXISTS (
                                SELECT 1 
                                FROM Cham_Cong CC 
                                WHERE NV.MaNhanVien = CC.MaNhanVien 
                                  AND MONTH(CC.Ngay) = @Month 
                                  AND YEAR(CC.Ngay) = @Year
                            )";

                                        using (OleDbConnection conn = new OleDbConnection(connectionString))
                                        {
                                            conn.Open();
                                            using (OleDbCommand cmd = new OleDbCommand(insertQuery, conn))
                                            {
                                                cmd.Parameters.AddWithValue("@Month", selectedMonth);
                                                cmd.Parameters.AddWithValue("@Year", selectedYear);
                                                cmd.ExecuteNonQuery();
                                            }
                                        }

                                 
                                        LoadGridView(selectedMonth, selectedYear);

                                    }
                                    catch (Exception ex)
                                    {
                                        lblMessage.Text = "Có lỗi xảy ra: " + ex.Message;
                                    }
                                }






        private void LoadGridView(int month, int year)
        {
            string query = @"
                    SELECT 
            NV.MaNhanVien, 
            NV.HoDem, 
            NV.Ten, 
            KieuNV.KieuNhanVien, 
            SUM(IIF(MONTH(CC.Ngay) = @Month AND YEAR(CC.Ngay) = @Year AND CC.TrangThai = 'Đi làm', 1, 0)) AS NgayLuong,
            KieuNV.HeSoLuong,
            KieuNV.LuongCung,
            KieuNV.LuongThoiVu,
            IIF(TP.GiaTien IS NULL, 0, TP.GiaTien) AS GiaTien,
            BH.MaBaoHiem,
            KieuNV.LuongCung * KieuNV.HeSoLuong 
            + KieuNV.LuongThoiVu * 
                SUM(IIF(MONTH(CC.Ngay) = @Month AND YEAR(CC.Ngay) = @Year AND CC.TrangThai = 'Đi làm', 1, 0))
            + IIF(TP.GiaTien IS NULL, 0, TP.GiaTien)
            - IIF(BH.NgayHetHan > Now(), 50000, 0) AS TongLuong
        FROM 
            (((Nhan_Vien AS NV 
            INNER JOIN Kieu_NV AS KieuNV ON NV.MaNhanVien = KieuNV.MaNhanVien) 
            LEFT JOIN Bao_Hiem AS BH ON NV.MaNhanVien = BH.MaNhanVien)
            LEFT JOIN Thuong_Phat AS TP ON NV.MaNhanVien = TP.MaNhanVien)
            LEFT JOIN Cham_Cong AS CC ON NV.MaNhanVien = CC.MaNhanVien
        WHERE 
            MONTH(CC.Ngay) = @Month AND YEAR(CC.Ngay) = @Year
        GROUP BY 
            NV.MaNhanVien, 
            NV.HoDem, 
            NV.Ten, 
            KieuNV.KieuNhanVien,
            KieuNV.HeSoLuong,
            KieuNV.LuongCung,
            KieuNV.LuongThoiVu,
            TP.GiaTien, 
            BH.MaBaoHiem,
            BH.NgayHetHan;";

                                using (OleDbConnection conn = new OleDbConnection(connectionString))
                                {
                                    using (OleDbCommand cmd = new OleDbCommand(query, conn))
                                    {
                                        cmd.Parameters.AddWithValue("@Month", month);
                                        cmd.Parameters.AddWithValue("@Year", year);

                                        DataTable dt = new DataTable();
                                        using (OleDbDataAdapter da = new OleDbDataAdapter(cmd))
                                        {
                                            da.Fill(dt);
                                        }

                                        gvSalary.DataSource = dt;
                                        gvSalary.DataBind();
                                    }
                                }
                            }



        protected void btnIn_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int rowIndex = Convert.ToInt32(btn.CommandArgument);

      
            string maNhanVien = gvSalary.Rows[rowIndex].Cells[0].Text;
            string hoDem = gvSalary.Rows[rowIndex].Cells[1].Text;
            string ten = gvSalary.Rows[rowIndex].Cells[2].Text;
            string kieuNhanVien = gvSalary.Rows[rowIndex].Cells[3].Text;
            string ngayLuong = gvSalary.Rows[rowIndex].Cells[4].Text;
            string heSoLuong = gvSalary.Rows[rowIndex].Cells[5].Text;
            string luongCung = gvSalary.Rows[rowIndex].Cells[6].Text;
            string luongThoiVu = gvSalary.Rows[rowIndex].Cells[7].Text;
            string giaTien = gvSalary.Rows[rowIndex].Cells[8].Text;
            string maBaoHiem = gvSalary.Rows[rowIndex].Cells[9].Text;
            string tongLuong = gvSalary.Rows[rowIndex].Cells[10].Text;

            // Lấy tháng và năm từ DropDownList
            string thang = ddlMonth.SelectedItem.Text;
            string nam = ddlYear.SelectedItem.Text;

            DateTime now = DateTime.Now;
            DateTime thoiHanBaoHiem;
            string thoiHanBaoHiemStr = "";

            string query = "SELECT NgayHetHan FROM Bao_Hiem WHERE MaNhanVien = @MaNhanVien";
            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaNhanVien", maNhanVien);
                    conn.Open();
                    object result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        thoiHanBaoHiem = Convert.ToDateTime(result);
                        thoiHanBaoHiemStr = $"Thời hạn bảo hiểm: {thoiHanBaoHiem:dd/MM/yyyy}";

                      
                        if (thoiHanBaoHiem > now) 
                        {
                            giaTien = (Convert.ToDouble(giaTien) - 50000).ToString();
                        }
                    }
                    else
                    {
                        thoiHanBaoHiemStr = "Chưa có thông tin bảo hiểm";
                        giaTien = "0"; 
                    }
                }
            }

            // Tính toán thông tin tổng lương chi tiết
            double luongCungValue = Convert.ToDouble(luongCung);
            double heSoLuongValue = Convert.ToDouble(heSoLuong);
            int ngayLuongValue = Convert.ToInt32(ngayLuong);
            double luongThoiVuValue = Convert.ToDouble(luongThoiVu);
            double giaTienValueFinal = Convert.ToDouble(giaTien);

            double tongLuongChiTiet = (luongCungValue * heSoLuongValue) + (luongThoiVuValue * ngayLuongValue) + giaTienValueFinal;

            // Chuẩn bị nội dung cho hộp thoại modal
            string header = $"Bản in lương tháng {thang} năm {nam}";
            string content = $"<p><strong>Mã nhân viên:</strong> {maNhanVien}</p>";
            content += $"<p><strong>Họ và tên:</strong> {hoDem} {ten}</p>";
            content += $"<p><strong>Kiểu nhân viên:</strong> {kieuNhanVien}</p>";
            content += $"<p><strong>Ngày lương:</strong> {ngayLuong}</p>";
            content += $"<p><strong>Hệ số lương:</strong> {heSoLuong}</p>";
            content += $"<p><strong>Lương cứng:</strong> {luongCung}</p>";
            content += $"<p><strong>Lương thời vụ:</strong> {luongThoiVu}</p>";
            content += $"<p><strong>Thưởng/phạt:</strong> {giaTien}</p>";
            content += $"<p><strong>Mã bảo hiểm:</strong> {maBaoHiem}</p>";
            content += $"<p><strong>{thoiHanBaoHiemStr}</strong></p>";
            content += $"<p><strong>Tổng lương thực nhận = (Lương cứng * Hệ số lương) + (Lương thời vụ * Ngày làm) + Thưởng/phạt - Bảo hiểm còn thời hạn = </strong> ({luongCung} * {heSoLuong}) + ({luongThoiVu} * {ngayLuong}) + {giaTien} = <strong>{tongLuongChiTiet} VND</strong></p>";

      
            string script = $"showModal('{header}', '{content}');";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", script, true);
        }





        // Tìm kiếm 


        protected void btnSearchByMonth_Click(object sender, EventArgs e)
        {
            int selectedMonth = int.Parse(DropDownList1.SelectedValue);
            string query = "SELECT MaNhanVien, HoDem, Ten, DiaChi, DienThoai, ChucVu, MaPhongBan " +
                           "FROM Nhan_Vien " +
                           "WHERE MONTH(NgaySinh) = ?";
            
            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    con.Open();
                    cmd.Parameters.AddWithValue("@Month", selectedMonth);
                    using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvSearchEmployeeResults.DataSource = dt;
                        gvSearchEmployeeResults.DataBind();
                  
                    }
                    
                }

            }

            //UpdatePanel1.Update();
        }


        protected void btnSearchByAttendance_Click(object sender, EventArgs e)
        {
            string attendanceStatus = ddlAttendanceStatus.SelectedValue;
            int day = Convert.ToInt32(ddlDay.SelectedValue);
            int month = Convert.ToInt32(ddlMonthAttendance.SelectedValue);


            string query = "SELECT C.MaNhanVien, NV.HoDem, NV.Ten, C.Ngay " +
                           "FROM Cham_Cong C INNER JOIN Nhan_Vien NV ON C.MaNhanVien = NV.MaNhanVien WHERE TrangThai = @Status ";

      
            if (day != 0 && month != 0)
            {
                query += "AND DAY(Ngay) = @Day AND MONTH(Ngay) = @Month";
            }
            else if (month != 0)
            {
                query += "AND MONTH(Ngay) = @Month";
            }
            else
            {
     
                return;
            }

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Status", attendanceStatus);

                 
                    if (day != 0 && month != 0)
                    {
                        cmd.Parameters.AddWithValue("@Day", day);
                        cmd.Parameters.AddWithValue("@Month", month);
                    }
                    else if (month != 0)
                    {
                        cmd.Parameters.AddWithValue("@Month", month);
                    }

                    con.Open();
                    using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvSearchAttendanceResults.DataSource = dt;
                        gvSearchAttendanceResults.DataBind();
                    }
                }
            }
        }






        protected void btnSearchByRewardType_Click(object sender, EventArgs e)
        {
            string rewardType = ddlRewardType.SelectedValue;
            int day = Convert.ToInt32(ddlDayReward.SelectedValue);
            int month = Convert.ToInt32(ddlMonthReward.SelectedValue);

            string query = "SELECT TP.MaNhanVien, NV.HoDem, NV.Ten, TP.NgayThang, TP.LyDo, TP.GiaTien " +
                           "FROM Thuong_Phat TP INNER JOIN Nhan_Vien NV ON TP.MaNhanVien = NV.MaNhanVien " +
                           "WHERE ThuongPhat = @RewardType";

            if (day != 0 && month != 0)
            {
                query += " AND DAY(TP.NgayThang) = @Day AND MONTH(TP.NgayThang) = @Month";
            }

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@RewardType", rewardType);

                    if (day != 0 && month != 0)
                    {
                        cmd.Parameters.AddWithValue("@Day", day);
                        cmd.Parameters.AddWithValue("@Month", month);
                    }

                    con.Open();
                    using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvSearchRewardResults.DataSource = dt;
                        gvSearchRewardResults.DataBind();
                    }
                }
            }
        }


        protected void btnSearchByInsuranceName_Click(object sender, EventArgs e)
        {
            string insuranceName = ddlInsuranceName.SelectedValue;

            string query = "SELECT BH.MaNhanVien, NV.HoDem, NV.Ten, BH.NgayCap, BH.NgayHetHan " +
                           "FROM Bao_Hiem BH " +
                           "INNER JOIN Nhan_Vien NV ON BH.MaNhanVien = NV.MaNhanVien " +
                           "WHERE BH.LoaiBH = @Name";

            using (OleDbConnection con = new OleDbConnection(connectionString))
            {
                using (OleDbCommand cmd = new OleDbCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Name", insuranceName);
                    using (OleDbDataAdapter sda = new OleDbDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        sda.Fill(dt);
                        gvSearchInsuranceResults.DataSource = dt;
                        gvSearchInsuranceResults.DataBind();
                    }
                }
            }
        }





        protected void confirmChangePassword_Click(object sender, EventArgs e)
        {
   
            string tenDangNhap = Session["TenDangNhap"].ToString();
         
            string oldPassword = txtOldPassword.Text;

            string newPassword = txtNewPassword.Text;



            using (OleDbConnection connection = new OleDbConnection(connectionString))
            {
                string updateQuery = "UPDATE Tai_Khoan SET MatKhau = ? WHERE TenDangNhap = ? AND MatKhau = ?";
                OleDbCommand command = new OleDbCommand(updateQuery, connection);
                command.Parameters.AddWithValue("@NewPassword", newPassword);
                command.Parameters.AddWithValue("@Username", tenDangNhap);
                command.Parameters.AddWithValue("@OldPassword", oldPassword);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                 
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đổi mật khẩu thành công'); window.location='Login.aspx';", true);
                    }
                    else
                    {
                       
                        ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Đổi mật khẩu không thành công. Mật khẩu cũ không đúng');", true);
                    }
                }
                catch (Exception ex)
                {
              
                    ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('Đã xảy ra lỗi trong quá trình đổi mật khẩu: {ex.Message}');", true);
                }
                finally
                {
                    connection.Close();
                }
            }
        }








        // Logout

        protected void btnLogout_Click(object sender, EventArgs e)
        {

            Session.Clear();
            Session.Abandon();


            Response.Redirect("Login.aspx");
        }







    }



}
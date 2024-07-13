<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Menu.aspx.cs" Inherits="BTL_QLNS.Menu" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hệ Thống Quản Lý Nhân Sự</title>
    <!-- Bootstrap CSS -->
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.5.1.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/@popperjs/core@2.9.2/dist/umd/popper.min.js"></script>
    <script src="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    
    <style>
        body {
            padding-top: 56px;
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }
        .navbar {
            margin-bottom: 20px;
        }
        .navbar-nav .nav-link {
            padding: 14px 20px;
            font-size: 16px;
            margin-right: 10px;
            transition: background-color 0.3s;
            white-space: nowrap;
        }
        .navbar-nav .nav-link:hover {
            background-color: #0056b3;
            color: white;
            border-radius: 5px;
        }
        .navbar-nav .nav-link.active {
            background-color: #0056b3;
            color: white;
            border-radius: 5px;
        }
        .content {
            padding: 20px;
            background-color: #ffffff;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            margin: 20px;
        }
        .content-section {
            display: none;
        }
        .content-section h2 {
            color: #333;
            border-bottom: 2px solid #007bff;
            padding-bottom: 10px;
            margin-bottom: 20px;
        }
        .content-section p {
            font-size: 14px;
            color: #666;
        }
        .navbar-nav .user-info {
            margin-left: 20px;
            margin-right: 50px;
            display: flex;
            align-items: center;
        }
        .navbar-nav .user-info i {
            font-size: 24px;
            margin-left: 10px;
        }
        .navbar-nav .user-info .nav-link {
            padding: 0;
            margin-left: 20px;
            font-size: 16px;
        }
        .grid-view-header {
            text-align: center;
            background-color: #007bff;
            color: white;
        }
        .grid-view-item {
            text-align: center;
            border: 1px solid #dee2e6;
        }
        .grid-view-item-center {
            text-align: center;
        }

        #btnShowCalendar {
            border-top-left-radius: 0;
            border-bottom-left-radius: 0;
        }

        .custom-calendar {
            display: none;
            position: absolute;
            z-index: 999;
            background-color: white;
            border: 1px solid #ccc;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }


        .calendar-container {
            position: relative;
            margin-top: 5px;
        }
        .show-calendar .custom-calendar {
            display: block;
        }
        #txtAttendanceDate {
            display: none;
        }

        .search-content {
            display: flex;
        }
        .search-menu {
            width: 25%;
            border-right: 1px solid #ddd;
            padding-right: 10px;
        }
        .search-results {
            width: 75%;
            padding-left: 10px;
        }
        ul li .list-group-item a{
            text-decoration: none;
            color : black;
        }
        .modal-footer {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-right: 300px;
        }

        .float-left {
            float: left;
        }

        .float-right {
            float: right;
        }
         #employeePopup{
             
         }


         .modal1 {
             display: none; /* Ẩn ban đầu */
             position: fixed;
             z-index: 1;
             left: 0;
             top: 0;
             width: 100%;
             height: 100%;
             overflow: auto;
             background-color: rgba(0,0,0,0.4);
         }

         .modal-content1 {
             background-color: #fefefe;
             margin: 15% auto;
             padding: 20px;
             border: 1px solid #888;
             width: 30%;
             text-align: center;
         }

         .close {
             color: #aaa;
             float: right;
             font-size: 28px;
             font-weight: bold;
         }

         .close:hover,
         .close:focus {
             color: black;
             text-decoration: none;
             cursor: pointer;
         }

 
    </style>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/5.15.3/css/all.min.css">
    
    <script type="text/javascript">
        $(document).ready(function () {
         
            function showTabFromUrl() {
                var hash = window.location.hash; 
                if (hash) {
                    var tabLink = $('a[href="' + hash + '"]');
                    if (tabLink.length > 0) {
                        tabLink.addClass('active'); 
                        showTabContent(hash);
                    }
                }
            }

          
            function showTabContent(tabId) {
                $('.content-section').hide(); 
                $(tabId).show(); 
            }

           
            $('.nav-link').click(function (e) {
                e.preventDefault();
                var target = $(this).data('target');
                $('.nav-link').removeClass('active');
                $(this).addClass('active');
                showTabContent(target);
                history.pushState({}, '', 'Menu.aspx' + target); 
            });

          
            showTabFromUrl();
        });
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            
            var selectedTab = $('#<%= hdnSelectedTab.ClientID %>').val();
        if (selectedTab) {
            var tabLink = $('a[href="' + selectedTab + '"]');
            if (tabLink.length > 0) {
                tabLink.addClass('active');
            }
        }

        
        $('.nav-link').click(function () {
            var selectedTabId = $(this).attr("href");
            $('#<%= hdnSelectedTab.ClientID %>').val(selectedTabId);
        });

        
        $('.navbar-nav a').click(function () {
            var target = $(this).data('target');
            if (target) {
                history.pushState({}, '', 'Menu.aspx' + target);
            }
        });
    });
    </script>



   

</head>
<body>
    
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

       

                <nav class="navbar navbar-expand-lg navbar-dark bg-dark fixed-top">
                    <a class="navbar-brand" href="#">Quản Lý Nhân Sự</a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Chuyển đổi điều hướng">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbarNav">
                        <ul class="navbar-nav">
                            <li class="nav-item">
                                <a class="nav-link" href="#employees-content" data-target="#employees-content">Nhân Viên</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#departments-content" data-target="#departments-content">Phòng Ban</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#rewards-content" data-target="#rewards-content">Thưởng/Phạt</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#insurance-content" data-target="#insurance-content">Bảo Hiểm</a>
                            </li>

                            <% if (Session["MaVaiTro"] != null && Session["MaVaiTro"].ToString() == "1") { %>
                                <li class="nav-item">
                                    <a class="nav-link" href="#employeeType-content" data-target="#employeeType-content">Cơ cấu lương</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="#attendance-content" data-target="#attendance-content">Chấm Công</a>
                                </li>
                                <li class="nav-item">
                                    <a class="nav-link" href="Menu.aspx#salaries-content" data-target="#salaries-content">Lương</a>
                                </li>
                            <% } %>
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-target="#search-content">Tìm kiếm</a>
                            </li>
                        </ul>
                        <ul class="navbar-nav user-info" style="margin-left: 380px;">
                            <li class="nav-item">
                                <a class="nav-link" href="#" data-target="#account-content">
                                    <% 
                                        string username = Session["TenDangNhap"] as string;
                                        if (!string.IsNullOrEmpty(username))
                                        {
                                            Response.Write(Server.HtmlEncode(username));
                                        }
                                    %>
                                    <i class="fas fa-user"></i>
                                </a>
                            </li>
                        </ul>
                    </div>
                </nav>
                



               <!-- Giao diện Nhân viên  -->

                <div class="content">
                    <div id="employees-content" class="content-section">
                        <!-- Bảng cho Nhân Viên -->
                        <h2>Nhân Viên</h2>
                        <asp:Button ID="btnAddEmployee" runat="server" Text="Thêm Nhân Viên" CssClass="btn btn-primary mb-3" OnClick="btnAddEmployee_Click"/>
                        <asp:GridView ID="gvEmployees" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaNhanVien" HeaderText="ID" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="HoDem" HeaderText="Họ Đệm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="Ten" HeaderText="Tên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="NgaySinh" HeaderText="Ngày Sinh" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" DataFormatString="{0:dd/MM/yyyy}"/>
                                <asp:BoundField DataField="DiaChi" HeaderText="Địa Chỉ" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />        
                                <asp:BoundField DataField="DienThoai" HeaderText="Điện thoại" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="ChucVu" HeaderText="Chức Vụ" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="MaPhongBan" HeaderText="Mã Phòng Ban" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="KieuNhanVien" HeaderText="Kiểu Nhân viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:TemplateField HeaderText="Lý Lịch" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkDownloadLyLich" runat="server" Text="Tải xuống" CommandArgument='<%# Eval("MaNhanVien") %>' OnClick="DownloadLyLich_Click" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Thao Tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item-center">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:Button ID="btnEditEmployee" runat="server" Text="Sửa" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("MaNhanVien") %>' OnClick="btnEditEmployee_Click" />
                                           
                                           <asp:Button ID="btnDeleteEmployee" runat="server" Text="Xóa" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("MaNhanVien") %>' OnClick="btnDeleteEmployee_Click" OnClientClick='<%# "return confirmDelete(\"Bạn có muốn xóa nhân viên có mã " + Eval("MaNhanVien") + "?\");" %>' />

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>


                    <div id="employeePopup" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="employeePopupLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="employeePopupLabel">Thông Tin Nhân Viên</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <!-- Điền thông tin của nhân viên -->
                                    <div class="form-group">
                                        <label for="txtMaNV">Mã Nhân Viên:</label>
                                        <asp:TextBox ID="txtMaNV" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtEmployeeFirstName">Họ Đệm:</label>
                                        <asp:TextBox ID="txtEmployeeFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtEmployeeLastName">Tên Nhân Viên:</label>
                                        <asp:TextBox ID="txtEmployeeLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDate">Ngày Sinh:</label>
                                        <asp:TextBox ID="txtDate" runat="server" CssClass="form-control" ></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlGender">Giới Tính:</label>
                                        <asp:DropDownList ID="ddlGender" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Nam" Value="Nam" />
                                            <asp:ListItem Text="Nữ" Value="Nữ" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDC">Địa Chỉ:</label>
                                        <asp:TextBox ID="txtDC" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDT">Điện Thoại:</label>
                                        <asp:TextBox ID="txtDT" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtPosition">Chức Vụ:</label>
                                        <asp:TextBox ID="txtPosition" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtType">Kiểu Nhân Viên:</label>
                                        <asp:DropDownList ID="txtType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Chính thức" Value="Chính thức" />
                                            <asp:ListItem Text="Thời vụ" Value="Thời vụ" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDateTD">Ngày Tuyển Dụng:</label>
                                        <asp:TextBox ID="txtDateTD" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDepartment">Phòng Ban:</label>
                                        <asp:DropDownList ID="txtDepartment" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlTrinhDo">Trình Độ:</label>
                                        <asp:DropDownList ID="ddlTrinhDo" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Chưa tốt nghiệp" Value="Chưa tốt nghiệp" />
                                            <asp:ListItem Text="Đại học" Value="Đại học" />
                                            <asp:ListItem Text="Cao Đẳng" Value="Cao đẳng" />
                                            <asp:ListItem Text="Thạc sỹ" Value="Thạc sỹ" />
                                            <asp:ListItem Text="Tiến sỹ" Value="Tiến sỹ" />
                                            <asp:ListItem Text="Khác" Value="Khác" />
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="fileLyLich">Lý Lịch:</label>
                                        <asp:FileUpload ID="fileLyLich" runat="server" CssClass="form-control" />
                                    </div>
                                    <asp:HiddenField ID="hdnEmployeeID" runat="server" />
                                </div>
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveEmployee" runat="server" Text="Lưu" CssClass="btn btn-primary" OnClick="btnSaveEmployee_Click"/>
                                    <asp:Button ID="btnAddNV" runat="server" Text="Thêm Nhân Viên" CssClass="btn btn-primary" OnClick="btnAddNV_Click"/>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>
                        </div>
                    </div>





                     <!-- Giao diện Phòng ban -->
 
                    <div id="departments-content" class="content-section">
                        <!-- Bảng cho Phòng Ban -->
                        <h2>Phòng Ban</h2>
                        <asp:Button ID="btnAddDepartment" runat="server" Text="Thêm Phòng Ban" CssClass="btn btn-primary mb-3" OnClick="btnAddDepartment_Click" />
                        <asp:GridView ID="gvDepartments" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaPhongBan" HeaderText="ID" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="TenPhongBan" HeaderText="Tên Phòng Ban" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:TemplateField HeaderText="Thao Tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item-center">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:Button ID="btnEditDepartment" runat="server" Text="Sửa" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("MaPhongBan") %>' OnClick="btnEditDepartment_Click" />
                                            <asp:Button ID="btnDeleteDepartment" runat="server" Text="Xóa" CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("MaPhongBan") %>' OnClick="btnDeleteDepartment_Click" />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                    <!-- Popup cho việc thêm hoặc sửa phòng ban -->
                    <div id="departmentPopup" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="departmentPopupLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="departmentPopupLabel">Thông Tin Phòng Ban</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <label for="txtDepartmentName">Mã Phòng Ban:</label>
                                        <asp:TextBox ID="txtDepartmentID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtDepartmentName">Tên Phòng Ban:</label>
                                        <asp:TextBox ID="txtDepartmentName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                 <asp:HiddenField ID="hdnDepartmentID" runat="server" />
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveDepartment" runat="server" Text="Lưu" CssClass="btn btn-primary" OnClick="btnSaveDepartment_Click" />
                                    <asp:Button ID="btnAddPB" runat="server" Text="Thêm Phòng Ban" CssClass="btn btn-primary" OnClick="btnAddPB_Click"/>
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>
                        </div>
                    </div>





                      <!-- Giao diện Khen thưởng  --> 

                   <div id="rewards-content" class="content-section">
                        <h2>Thưởng - Phạt</h2>
                        <asp:Button ID="btnAddReward" runat="server" Text="Thêm Khen Thưởng" CssClass="btn btn-primary mb-3"  OnClick="btnAddReward_Click"/>

                        <asp:GridView ID="gvRewards" runat="server" AutoGenerateColumns="False"  CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaThuongPhat" HeaderText="Ma Thuong Phat" Visible="false" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>

                                <asp:BoundField DataField="MaNhanVien" HeaderText="Mã Nhân Viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:BoundField DataField="HoTen" HeaderText="Họ Tên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />

                                <asp:BoundField DataField="NgayThang" HeaderText="Ngày Tháng" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:BoundField DataField="ThuongPhat" HeaderText="Chuyên mục" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:BoundField DataField="LyDo" HeaderText="Lý Do" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="GiaTien" HeaderText="Giá tiền" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>

                                <asp:TemplateField HeaderText="Thao tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditReward" runat="server" Text="Sửa" OnClick="btnEditReward_Click" CommandArgument='<%# Eval("MaThuongPhat") %>' CssClass="btn btn-primary" />
                                        <asp:Button ID="Button1" runat="server" Text="Xóa" CssClass="btn btn-primary" CommandArgument='<%# Eval("MaThuongPhat") %>' OnClick="btnDeleteReward_Click" OnClientClick='<%# "return confirmDelete(\"Bạn có muốn xóa phần thưởng/phạt của nhân viên có mã " + Eval("MaNhanVien") + " vào ngày "+ Eval("NgayThang") + "?\");" %>' />

                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>


                            <!-- Popup modal for adding/editing reward -->
                            <div class="modal fade" id="rewardPopup" tabindex="-1" role="dialog" aria-labelledby="rewardPopupLabel" aria-hidden="true">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="rewardPopupLabel">Khen Thưởng</h5>
                                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="false">&times;</span>
                                            </button>
                                        </div>
                                        <div class="modal-body">
                                            <div class="form-group">
                                                <label for="txtRewardMaNV">Mã Nhân Viên</label>
                                                <asp:DropDownList ID="ddl_manv" runat="server" CssClass="form-control"></asp:DropDownList>
                                                <asp:HiddenField ID="hiddenRewardID" runat="server" />

                                            </div>
                                            <div class="form-group">
                                                <label for="txtRewardNgayThang">Ngày Tháng</label>
                                                <asp:TextBox ID="txtRewardNgayThang" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtRewardThuongPhat">Thưởng/Phạt</label>
                                                <asp:DropDownList ID="ddl_thuongphat" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="Thưởng">Thưởng</asp:ListItem>
                                                    <asp:ListItem Value="Phạt">Phạt</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtRewardLyDo">Lý Do</label>
                                                <asp:TextBox ID="txtRewardLyDo" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                            <div class="form-group">
                                                <label for="txtRewardGiaTien">Giá Tiền</label>
                                                <asp:TextBox ID="txtRewardGiaTien" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="modal-footer">
                                            <asp:Button ID="btnSaveReward" runat="server" CssClass="btn btn-primary" Text="Lưu"   OnClick="btnSaveReward_Click" />
                                            <asp:Button ID="btnAddKT" runat="server" CssClass="btn btn-primary" Text="Xác Nhận Thêm"   OnClick="btnAddKT_Click"/>
                                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>








                     <!-- Giao diện Bảo hiểm -->

                   <div id="insurance-content" class="content-section">
                        <!-- Bảng cho Bảo Hiểm -->
                        <h2>Bảo Hiểm</h2>
                        <asp:Button ID="btnAddInsurance" runat="server" Text="Thêm Bảo Hiểm" CssClass="btn btn-primary mb-3" OnClick="btnAddInsurance_Click"/>
                        <asp:GridView ID="gvInsurance" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaBaoHiem" HeaderText="Mã Bảo Hiểm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="MaNhanVien" HeaderText="Mã Nhân Viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="LoaiBH" HeaderText="Loại Bảo Hiểm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="NgayCap" HeaderText="Ngày Cấp" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="NgayHetHan" HeaderText="Ngày Hết Hạn" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="NoiCap" HeaderText="Nơi Cấp" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:TemplateField HeaderText="Thao Tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item-center">
                                    <ItemTemplate>
                                        <div class="btn-group" role="group">
                                            <asp:Button ID="btnEditInsurance" runat="server" Text="Sửa" CssClass="btn btn-info btn-sm" CommandArgument='<%# Eval("MaBaoHiem") %>' OnClick="btnEditInsurance_Click"/>
                                            <asp:Button ID="Button2" runat="server" Text="Xóa"  CssClass="btn btn-danger btn-sm" CommandArgument='<%# Eval("MaBaoHiem") %>' OnClick="btnDeleteInsurance_Click" OnClientClick='<%# "return confirmDelete(\"Bạn có muốn xóa bảo hiểm với mã " + Eval("MaBaoHiem") + "?\");" %>' />

                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                   </div>
                    <!-- Popup cho việc thêm hoặc sửa bảo hiểm -->
                    <div id="insurancePopup" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="insurancePopupLabel" aria-hidden="true">
                        <div class="modal-dialog" role="document">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h5 class="modal-title" id="insurancePopupLabel">Thông Tin Bảo Hiểm</h5>
                                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                        <span aria-hidden="true">&times;</span>
                                    </button>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                        <label for="txtInsuranceID">Mã Bảo Hiểm:</label>
                                        <asp:TextBox ID="txtInsuranceID" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlEmployeeID">Mã Nhân Viên:</label>
                                        <asp:DropDownList ID="ddlEmployeeID" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlInsuranceType">Loại Bảo Hiểm:</label>
                                        <asp:DropDownList ID="ddlInsuranceType" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Bảo Hiểm Y Tế" Value="BHYT"></asp:ListItem>
                                            <asp:ListItem Text="Bảo Hiểm Xã Hội" Value="BHXH"></asp:ListItem>
                                            <asp:ListItem Text="Bảo Hiểm Thất Nghiệp" Value="BHTN"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtIssuedDate">Ngày Cấp:</label>
                                        <asp:TextBox ID="txtIssuedDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtExpiryDate">Ngày Hết Hạn:</label>
                                        <asp:TextBox ID="txtExpiryDate" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label for="txtIssuedPlace">Nơi Cấp:</label>
                                        <asp:TextBox ID="txtIssuedPlace" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <asp:HiddenField ID="hdnInsuranceID" runat="server" />
                                <div class="modal-footer">
                                    <asp:Button ID="btnSaveInsurance" runat="server" Text="Lưu" CssClass="btn btn-primary"   OnClick="btnSaveInsurance_Click"/>
                                    <asp:Button ID="btnAddBH" runat="server" Text="Thêm Bảo Hiểm" CssClass="btn btn-primary" OnClick="btnAddBH_Click" />
                                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                </div>
                            </div>
                        </div>
                    </div>


                    <!-- Giao diện Kiểu nhân viên  -->
                    
                    <div id="employeeType-content" class="content-section">
                        <h2>Cơ cấu Lương nhân sự</h2>

                        <asp:GridView ID="gvEmployeeTypes" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">

                            <Columns>
                                <asp:BoundField DataField="MaNhanVien" HeaderText="Mã Nhân Viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="HoDem" HeaderText="Họ Đệm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="Ten" HeaderText="Tên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                               <asp:BoundField DataField="KieuNhanVien" HeaderText="Kiểu Nhân Viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:BoundField DataField="HeSoLuong" HeaderText="Hệ Số Lương" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:BoundField DataField="LuongCung" HeaderText="Lương Cứng" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                 <asp:BoundField DataField="LuongThoiVu" HeaderText="Lương Thời Vụ" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"/>
                                <asp:TemplateField HeaderText="Thao tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item">
                                    <ItemTemplate>
                                        <asp:Button ID="btnEditEmployeeType" runat="server" Text="Sửa" OnClick="btnEditEmployeeType_Click" CssClass="btn btn-primary" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <!-- Popup modal for adding/editing employee type -->
                        <div class="modal fade" id="employeeTypePopup" tabindex="-1" role="dialog" aria-labelledby="employeeTypePopupLabel" aria-hidden="true" style="margin-right:300px">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content" style="margin-right:300px">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="employeeTypePopupLabel">Cơ cấu Lương nhân sự</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        <div class="form-group">
                        <label for="ddlEmployeeTypeMaNV">Mã Nhân Viên</label>
                        <asp:Textbox ID="ddlEmployeeTypeMaNV" runat="server" CssClass="form-control"></asp:Textbox>
                    </div>
                    <div class="form-group">
                        <label for="ddlEmployeeTypeKieuNV">Kiểu Nhân Viên</label>
                        <asp:DropDownList ID="ddlEmployeeTypeKieuNV" runat="server" CssClass="form-control">
                            <asp:ListItem Text="Chính thức" Value="Chinh thuc"></asp:ListItem>
                            <asp:ListItem Text="Thời vụ" Value="Thoi vu"></asp:ListItem>
                        </asp:DropDownList>
                    </div>

                                        <div class="form-group">
                                            <label for="txtEmployeeTypeHeSoLuong">Hệ Số Lương</label>
                                            <asp:TextBox ID="txtEmployeeTypeHeSoLuong" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtEmployeeTypeLuongCung">Lương Cứng</label>
                                            <asp:TextBox ID="txtEmployeeTypeLuongCung" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label for="txtEmployeeTypeLuongCung">Lương Thời Vụ</label>
                                            <asp:TextBox ID="txtEmployeeTypeLuongThoiVu" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button ID="btnSaveEmployeeType" runat="server" CssClass="btn btn-primary" Text="Lưu" OnClick="btnSaveEmployeeType_Click" />
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Đóng</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>














                    <!-- Giao diện Chấm công  -->

                    <div id="attendance-content" class="content-section">
                        <!-- Bảng cho Chấm Công -->
                        <h2>Chấm Công</h2>
                        <asp:GridView ID="gvAttendance" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaNhanVien" HeaderText="ID" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="HoDem" HeaderText="Họ Đệm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="Ten" HeaderText="Tên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:TemplateField HeaderText="Trạng Thái" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlStatus" runat="server" CssClass="form-control">
                                            <asp:ListItem Text="Đi làm" Value="Đi làm"></asp:ListItem>
                                            <asp:ListItem Text="Đi muộn" Value="Đi muộn "></asp:ListItem>
                                            <asp:ListItem Text="Vắng" Value="Vắng"></asp:ListItem>
                                            <asp:ListItem Text="Nghỉ có phép" Value="Nghỉ có phép"></asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                        <div class="form-group">
                        <label for="txtAttendanceDate">Ngày Chấm Công:</label>
                        <asp:TextBox ID="txtDateCC" runat="server" CssClass="form-control" TextMode="Date"></asp:TextBox>
                    </div>

                        <asp:Button ID="btnUpdateAttendance" runat="server" Text="Cập Nhật Chấm Công" CssClass="btn btn-primary mb-3" OnClick="btnUpdateAttendance_Click"/>
                    </div>







                      <!-- Giao diện Lương  -->

                     <div id="salaries-content" class="content-section">
                        <h2>Lương</h2>
                        <div class="form-group">
                            <label for="ddlMonth">Chọn Tháng:</label>
                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <div class="form-group">
                            <label for="ddlYear">Chọn Năm:</label>
                            <asp:DropDownList ID="ddlYear" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                        <asp:Button ID="btnViewSalary" runat="server" Text="Xem Lương" CssClass="btn btn-primary mb-3" OnClick="btnViewSalary_Click" />
                        <asp:GridView ID="gvSalary" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
                            <Columns>
                                <asp:BoundField DataField="MaNhanVien" HeaderText="ID" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="HoDem" HeaderText="Họ Đệm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="Ten" HeaderText="Tên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="KieuNhanVien" HeaderText="Kiểu nhân viên" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="NgayLuong" HeaderText="Ngày Lương" DataFormatString="{0:N0}" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item"  />
                                <asp:BoundField DataField="HeSoLuong" HeaderText="Hệ số Lương" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="LuongCung" HeaderText="Lương cứng" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                               <asp:BoundField DataField="LuongThoiVu" HeaderText="Lương thời vụ" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="GiaTien" HeaderText="Thưởng/phạt" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="MaBaoHiem" HeaderText="Mã Bảo Hiểm" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:BoundField DataField="TongLuong" HeaderText="Tổng Lương" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item" />
                                <asp:TemplateField HeaderText="Thao Tác" HeaderStyle-CssClass="grid-view-header" ItemStyle-CssClass="grid-view-item-center"><ItemTemplate>
                                        <div class="btn-group" role="group">
                                        <asp:Button ID="btnIn" runat="server" Text="Xem chi tiết" CssClass="btn btn-info btn-sm" OnClick="btnIn_Click" CommandArgument='<%# Container.DataItemIndex %>' />
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>

                    <!-- Popup cho việc In lương -->

                    <div id="myModal" class="modal fade" role="dialog">
                        <div class="modal-dialog modal-lg">
                             <div class="modal-content" style="width: 700px; height: 850px; font-size: 20px;">
                                <div class="modal-header">
                                    <h4 class="modal-title" id="printHeader"></h4>
                                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                                </div>
                                <div class="modal-body" id="employeeInfo">
                                    <!-- Nội dung của modal sẽ được cập nhật bằng JavaScript -->
                                </div>
                                <div class="modal-footer">
                        <div class="float-left">Nhân viên (đã ký)</div>
                        <div class="float-right">Kế toán (đã ký)</div>
                        <button type="button" class="btn btn-primary" onclick="printModalContent();">In</button>
                    </div>

                            </div>
                        </div>
                    </div>
                    <script type="text/javascript">
                        function showModal(header, content) {
                            $('#printHeader').text(header);
                            $('#employeeInfo').html(content);
                            $('#myModal').modal('show');
                        }

                        function printModalContent() {
                            var printContents = document.getElementById('myModal').innerHTML;
                            var originalContents = document.body.innerHTML;

                            document.body.innerHTML = printContents;

                            window.print();

                            document.body.innerHTML = originalContents;
                        }
                    </script>



                    <div id="search-content" class="content-section">
                        <h2>Tìm kiếm</h2>
                        <div class="search-content">
                            <div class="search-menu">
                                <ul class="list-group">
                                    <li class="list-group-item"><a href="#search-emloyee" data-target="#search-employee">Tìm kiếm theo Tháng Sinh</a></li>
                                    <li class="list-group-item"><a href="#search-attendance" data-target="#search-attendance">Tìm kiếm theo Chấm Công</a></li>
                                    <li class="list-group-item"><a href="#search-reward" data-target="#search-reward">Tìm kiếm theo Khen Thưởng</a></li>
                                    <li class="list-group-item"><a href="#search-insurance" data-target="#search-insurance">Tìm kiếm theo Tên Bảo Hiểm</a></li>
                                </ul>
                            </div>
                            <div class="search-results">
                                <!-- Tìm kiếm theo Tháng Sinh -->
                                <div id="search-employee" class="search-section">
                                    <h3>Tìm kiếm theo Tháng Sinh</h3>
                                    <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="1" Text="Tháng 1" />
                                        <asp:ListItem Value="2" Text="Tháng 2" />
                                        <asp:ListItem Value="3" Text="Tháng 3" />
                                        <asp:ListItem Value="4" Text="Tháng 4" />
                                        <asp:ListItem Value="5" Text="Tháng 5" />
                                        <asp:ListItem Value="6" Text="Tháng 6" />
                                        <asp:ListItem Value="7" Text="Tháng 7" />
                                        <asp:ListItem Value="8" Text="Tháng 8" />
                                        <asp:ListItem Value="9" Text="Tháng 9" />
                                        <asp:ListItem Value="10" Text="Tháng 10" />
                                        <asp:ListItem Value="11" Text="Tháng 11" />
                                        <asp:ListItem Value="12" Text="Tháng 12" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearchByMonth" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearchByMonth_Click"/>
                                    <asp:GridView ID="gvSearchEmployeeResults" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="MaNhanVien" HeaderText="ID" />
                                            <asp:BoundField DataField="HoDem" HeaderText="Họ Đệm" />
                                            <asp:BoundField DataField="Ten" HeaderText="Tên" />
                                            <asp:BoundField DataField="DiaChi" HeaderText="Địa Chỉ" />
                                            <asp:BoundField DataField="DienThoai" HeaderText="Số Điện Thoại" />
                                            <asp:BoundField DataField="ChucVu" HeaderText="Chức Vụ" />
                                            <asp:BoundField DataField="MaPhongBan" HeaderText="Phòng Ban" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                                 <!-- Tìm kiếm theo Chấm Công -->
                                <div id="search-attendance" class="search-section">
                                    <h3>Tìm kiếm theo Chấm Công</h3>
                                    <asp:DropDownList ID="ddlAttendanceStatus" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="" Text="-- Chọn trạng thái --" />
                                        <asp:ListItem Value="Đi làm" Text="Đi làm" />
                                        <asp:ListItem Value="Vắng" Text="Nghỉ làm" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDay" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="0" Text="-- Chọn ngày --" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMonthAttendance" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="" Text="-- Chọn tháng --" />
                                        <asp:ListItem Value="1" Text="Tháng 1" />
                                        <asp:ListItem Value="2" Text="Tháng 2" />
                                        <asp:ListItem Value="3" Text="Tháng 3" />
                                        <asp:ListItem Value="4" Text="Tháng 4" />
                                        <asp:ListItem Value="5" Text="Tháng 5" />
                                        <asp:ListItem Value="6" Text="Tháng 6" />
                                        <asp:ListItem Value="7" Text="Tháng 7" />
                                        <asp:ListItem Value="8" Text="Tháng 8" />
                                        <asp:ListItem Value="9" Text="Tháng 9" />
                                        <asp:ListItem Value="10" Text="Tháng 10" />
                                        <asp:ListItem Value="11" Text="Tháng 11" />
                                        <asp:ListItem Value="12" Text="Tháng 12" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearchByAttendance" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearchByAttendance_Click"/>
                                    <asp:GridView ID="gvSearchAttendanceResults" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="MaNhanVien" HeaderText="ID" />
                                            <asp:BoundField DataField="HoDem" HeaderText="Họ đệm" />
                                            <asp:BoundField DataField="Ten" HeaderText="Tên" />
                                            <asp:BoundField DataField="Ngay" HeaderText="Ngày" DataFormatString="{0:dd/MM/yyyy}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <!-- Tìm kiếm theo Khen Thưởng -->
                                <div id="search-reward" class="search-section">
                                    <h3>Tìm kiếm theo Khen Thưởng</h3>
                                    <asp:DropDownList ID="ddlRewardType" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="" Text="-- Chọn loại --" />
                                        <asp:ListItem Value="Thưởng" Text="Thưởng" />
                                        <asp:ListItem Value="Phạt" Text="Phạt" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlDayReward" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="0" Text="-- Chọn ngày --" />
                                    </asp:DropDownList>
                                    <asp:DropDownList ID="ddlMonthReward" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="" Text="-- Chọn tháng --" />
                                        <asp:ListItem Value="1" Text="Tháng 1" />
                                        <asp:ListItem Value="2" Text="Tháng 2" />
                                        <asp:ListItem Value="3" Text="Tháng 3" />
                                        <asp:ListItem Value="4" Text="Tháng 4" />
                                        <asp:ListItem Value="5" Text="Tháng 5" />
                                        <asp:ListItem Value="6" Text="Tháng 6" />
                                        <asp:ListItem Value="7" Text="Tháng 7" />
                                        <asp:ListItem Value="8" Text="Tháng 8" />
                                        <asp:ListItem Value="9" Text="Tháng 9" />
                                        <asp:ListItem Value="10" Text="Tháng 10" />
                                        <asp:ListItem Value="11" Text="Tháng 11" />
                                        <asp:ListItem Value="12" Text="Tháng 12" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearchByRewardType" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearchByRewardType_Click"/>
                                    <asp:GridView ID="gvSearchRewardResults" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="MaNhanVien" HeaderText="ID" />
                                            <asp:BoundField DataField="HoDem" HeaderText="Họ đệm" />
                                            <asp:BoundField DataField="Ten" HeaderText="Tên" />
                                            <asp:BoundField DataField="NgayThang" HeaderText="Ngày Tháng" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="LyDo" HeaderText="Lý Do" />
                                            <asp:BoundField DataField="GiaTien" HeaderText="Giá Tiền" />
                                        </Columns>
                                    </asp:GridView>
                                </div>

                                <!-- Tìm kiếm theo Tên Bảo Hiểm -->
                                <div id="search-insurance" class="search-section">
                                    <h3>Tìm kiếm theo Tên Bảo Hiểm</h3>
                                    <asp:DropDownList ID="ddlInsuranceName" runat="server" CssClass="form-control mb-2">
                                        <asp:ListItem Value="BHTN" Text="Bảo hiểm thất nghiệp" />
                                        <asp:ListItem Value="BHYT" Text="Bảo hiểm y tế" />
                                        <asp:ListItem Value="BHXH" Text="Bảo hiểm xã hội" />
                                    </asp:DropDownList>
                                    <asp:Button ID="btnSearchByInsuranceName" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnSearchByInsuranceName_Click"/>
                                    <asp:GridView ID="gvSearchInsuranceResults" runat="server" AutoGenerateColumns="False" CssClass="table table-striped mt-3">
                                        <Columns>
                                            <asp:BoundField DataField="MaNhanVien" HeaderText="ID" />
                                            <asp:BoundField DataField="HoDem" HeaderText="Họ đệm" />
                                            <asp:BoundField DataField="Ten" HeaderText="Tên" />
                                            <asp:BoundField DataField="NgayCap" HeaderText="Ngày Cấp" DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:BoundField DataField="NgayHetHan" HeaderText="Ngày Hết Hạn" DataFormatString="{0:dd/MM/yyyy}" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>



            





                     <!-- tài khoản --> 

                    <div id="account-content" class="content-section">
                            <!-- Nội dung cho Thông Tin Tài Khoản -->
                            <h2>Thông Tin Tài Khoản</h2>
                            <div class="form-group">
                                <label for="txtUsername">Tên đăng nhập:</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label for="txtRole">Mã vai trò:</label>
                                <asp:TextBox ID="txtRole" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                            </div>
                            <asp:Button ID="btnChangePassword" runat="server" Text="Đổi mật khẩu" CssClass="btn btn-primary" OnClientClick="showChangePasswordPopup(); return false;" />
                            <asp:Button ID="btnLogout" runat="server" Text="Đăng xuất" CssClass="btn btn-danger" OnClick="btnLogout_Click" />
                        </div>

                        <!-- Hidden field để giữ giá trị của Tab được chọn -->
                        <asp:HiddenField ID="HiddenField1" runat="server" />

                        <!-- Popup đổi mật khẩu -->
                       <div id="changePasswordModal" class="modal1">
                            <div class="modal-content1">
                                <span class="close" onclick="closeChangePasswordPopup()">&times;</span>
                                <h3>Đổi mật khẩu cho <% = Session["TenDangNhap"] %></h3>
                                <div class="form-group">
                                    <label for="txtOldPassword">Mật khẩu cũ:</label>
                                    <asp:TextBox ID="txtOldPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <div class="form-group">
                                    <label for="txtNewPassword">Mật khẩu mới:</label>
                                    <asp:TextBox ID="txtNewPassword" TextMode="Password" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                                <asp:Button ID="btnConfirmChange" runat="server" CssClass="btn btn-primary" Text="Xác nhận" OnClick="confirmChangePassword_Click" />

                                <button class="btn btn-secondary" onclick="closeChangePasswordPopup()">Đóng</button>
                            </div>
                        </div>



                    <asp:HiddenField ID="hdnSelectedTab" runat="server" />
                </div>
           
           

    </form>


    
    

    <script>

        document.addEventListener("DOMContentLoaded", function () {
            var menuLinks = document.querySelectorAll(".navbar-nav .nav-link");
            menuLinks.forEach(function (link) {
                link.addEventListener("click", function (event) {
                    var hash = event.currentTarget.getAttribute("data-target");
                    document.getElementById("<%= hdnSelectedTab.ClientID %>").value = hash;
            });
        });
    });
    </script>

   <script> 
       $(document).ready(function () {
           $('.nav-link').click(function (e) {
               e.preventDefault();
               var target = $(this).data('target');
               $('.content-section').hide(); 
               $(target).show(); 
               $('.nav-link').removeClass('active'); 
               $(this).addClass('active'); 
               $('#<%= hdnSelectedTab.ClientID %>').val(target); 
    });

          var selectedTab = $('#<%= hdnSelectedTab.ClientID %>').val();
          if (selectedTab) {
              $('.nav-link[data-target="' + selectedTab + '"]').click(); 
          } else {
              $('.nav-link').first().click(); 
           }

            



      });

   </script>
  

    <script type="text/javascript">
        function confirmDelete(message) {
            return confirm(message);
        }
    </script>



    


   <script>
       $(document).ready(function () {
           $('.search-section').hide();

  
           $('.list-group-item a').click(function (e) {
               e.preventDefault(); 

               var target = $(this).data('target');

               $('.search-section').hide();
               $(target).show();
           });
       });
    </script>
    

    <script>
 
        $(document).ready(function () {
  
            $('.navbar-nav a').click(function () {
     
                var target = $(this).data('target');
  
                if (target) {
                    history.pushState({}, '', 'Menu.aspx' + target);
                }
            });
        });
    </script>

   


    <script>
        function showChangePasswordPopup() {
            var modal = document.getElementById("changePasswordModal");
            modal.style.display = "block";
        }

        function closeChangePasswordPopup() {
            var modal = document.getElementById("changePasswordModal");
            modal.style.display = "none";
        }
    </script>
   


   

</body>
</html>

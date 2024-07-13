<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="BTL_QLNS.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Đăng nhập và Đăng ký</title>

    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            padding-top: 56px;
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }
        .form-container {
            max-width: 400px;
            margin: 50px auto;
            background-color: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            display: none; 
        }
        .form-container.active {
            display: block; 
        }
        .form-container h2 {
            margin-bottom: 20px;
            color: #333;
            text-align: center;
        }
        .form-group {
            margin-bottom: 20px;
        }
        .form-group label {
            font-size: 16px;
        }
        .form-group input {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            border: 1px solid #ced4da;
            border-radius: 5px;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
        }
        .form-group input:focus {
            outline: none;
            border-color: #007bff;
            box-shadow: 0 0 0 0.2rem rgba(0, 123, 255, 0.25);
        }
        .btn-primary {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            background-color: #007bff;
            border: none;
            border-radius: 5px;
            transition: background-color 0.15s ease-in-out;
        }
        .btn-primary:hover {
            background-color: #0056b3;
        }
        .btn-secondary {
            width: 100%;
            padding: 10px;
            font-size: 16px;
            background-color: #6c757d;
            border: none;
            border-radius: 5px;
            transition: background-color 0.15s ease-in-out;
        }
        .btn-secondary:hover {
            background-color: #495057;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="form-container active" id="login-form"> 
            <h2>Đăng nhập</h2>
            <div class="form-group">
                <label for="txtUsername">Tên đăng nhập:</label>
                <asp:TextBox ID="txtUsername" runat="server" CssClass="form-control" placeholder="Nhập tên đăng nhập"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtPassword">Mật khẩu:</label>
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Nhập mật khẩu"></asp:TextBox>
            </div>
            <asp:Button ID="btnLogin" runat="server" Text="Đăng nhập" CssClass="btn btn-primary" OnClick="btnLogin_Click" />
            <p class="text-center mt-3">Chưa có tài khoản? <a href="#" id="show-register">Đăng ký</a></p>
        </div>
        <div class="form-container" id="register-form">
            <h2>Đăng ký</h2>
            <div class="form-group">
                <label for="txtNewUsername">Tên đăng nhập mới:</label>
                <asp:TextBox ID="txtNewUsername" runat="server" CssClass="form-control" placeholder="Nhập tên đăng nhập mới"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtNewPassword">Mật khẩu mới:</label>
                <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Nhập mật khẩu mới"></asp:TextBox>
            </div>
            <div class="form-group">
                <label for="txtConfirmPassword">Nhập lại mật khẩu mới:</label>
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" CssClass="form-control" placeholder="Nhập lại mật khẩu mới"></asp:TextBox>
            </div>
            <asp:Button ID="btnRegister" runat="server" Text="Đăng ký" CssClass="btn btn-secondary" OnClick="btnRegister_Click"/>
            <p class="text-center mt-3">Đã có tài khoản? <a href="#" id="show-login">Đăng nhập</a></p>
        </div>
    </form>

    <script>
        document.getElementById("show-register").addEventListener("click", function () {
            document.getElementById("login-form").classList.remove("active");
            document.getElementById("register-form").classList.add("active");
        });

        document.getElementById("show-login").addEventListener("click", function () {
            document.getElementById("register-form").classList.remove("active");
            document.getElementById("login-form").classList.add("active");
        });
    </script>
</body>
</html>

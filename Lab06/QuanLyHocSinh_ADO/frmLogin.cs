using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHocSinh_ADO
{
    public class frmLogin : Form
    {
        // Tài khoản cố định dùng cho bài thực hành
        private const string USERNAME = "admin";
        private const string PASSWORD = "123";

        private TextBox txtUsername;
        private TextBox txtPassword;
        private Button btnDangNhap;
        private Button btnThoat;

        public frmLogin()
        {
            Text = "Login Form";
            ClientSize = new Size(320, 150);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            Label lblUser = new Label();
            lblUser.Text = "Username";
            lblUser.Location = new Point(15, 22);
            lblUser.AutoSize = true;

            Label lblPass = new Label();
            lblPass.Text = "Password";
            lblPass.Location = new Point(15, 57);
            lblPass.AutoSize = true;

            txtUsername = new TextBox();
            txtUsername.Location = new Point(95, 19);
            txtUsername.Size = new Size(205, 23);

            txtPassword = new TextBox();
            txtPassword.Location = new Point(95, 54);
            txtPassword.Size = new Size(205, 23);
            txtPassword.UseSystemPasswordChar = true;

            btnDangNhap = new Button();
            btnDangNhap.Text = "Đăng nhập";
            btnDangNhap.Location = new Point(15, 100);
            btnDangNhap.Size = new Size(130, 36);
            btnDangNhap.Click += btnDangNhap_Click;

            btnThoat = new Button();
            btnThoat.Text = "Thoát";
            btnThoat.Location = new Point(170, 100);
            btnThoat.Size = new Size(130, 36);
            btnThoat.Click += (s, e) => Application.Exit();

            Controls.Add(lblUser);
            Controls.Add(lblPass);
            Controls.Add(txtUsername);
            Controls.Add(txtPassword);
            Controls.Add(btnDangNhap);
            Controls.Add(btnThoat);
            AcceptButton = btnDangNhap;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == USERNAME && txtPassword.Text == PASSWORD)
            {
                frmMain main = MdiParent as frmMain;
                if (main != null)
                    main.MoKhoa();
                Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu.", "Đăng nhập", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}

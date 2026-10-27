using System;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHocSinh_ADO
{
    public class frmMain : Form
    {
        private MenuStrip menuStrip1;
        private ToolStrip toolStrip1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lblThoiGian;
        private Timer timer1;

        private ToolStripMenuItem mnuDangNhap;
        private ToolStripMenuItem mnuLop;
        private ToolStripMenuItem mnuHoSo;
        private ToolStripMenuItem mnuNhapDiem;
        private ToolStripButton btnDangNhap;
        private ToolStripButton btnLop;

        public frmMain()
        {
            Text = "QUẢN LÝ HỌC SINH";
            ClientSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            IsMdiContainer = true;   // form chính là MDI container

            // ----- Menu -----
            menuStrip1 = new MenuStrip();

            ToolStripMenuItem mnuHeThong = new ToolStripMenuItem("Hệ thống");
            mnuDangNhap = new ToolStripMenuItem("Đăng nhập");
            mnuDangNhap.ShortcutKeys = Keys.Control | Keys.L;
            mnuDangNhap.Click += (s, e) => MoForm<frmLogin>();
            ToolStripMenuItem mnuThoat = new ToolStripMenuItem("Thoát");
            mnuThoat.ShortcutKeyDisplayString = "Alt+F4";
            mnuThoat.Click += (s, e) => Close();
            mnuHeThong.DropDownItems.Add(mnuDangNhap);
            mnuHeThong.DropDownItems.Add(mnuThoat);

            ToolStripMenuItem mnuDanhMuc = new ToolStripMenuItem("Danh mục");
            mnuLop = new ToolStripMenuItem("Lớp");
            mnuLop.Click += (s, e) => MoForm<frmLop>();
            mnuDanhMuc.DropDownItems.Add(mnuLop);

            ToolStripMenuItem mnuQuanLy = new ToolStripMenuItem("Quản lý Học sinh");
            mnuHoSo = new ToolStripMenuItem("Hồ sơ Học sinh");
            mnuHoSo.Click += (s, e) => MoForm<frmHocSinh>();
            mnuNhapDiem = new ToolStripMenuItem("Nhập điểm");
            mnuNhapDiem.Click += (s, e) => MessageBox.Show("Chức năng nhập điểm chưa được triển khai trong bài này.");
            mnuQuanLy.DropDownItems.Add(mnuHoSo);
            mnuQuanLy.DropDownItems.Add(mnuNhapDiem);

            // Menu sắp xếp cửa sổ (LayoutMdi)
            ToolStripMenuItem mnuCuaSo = new ToolStripMenuItem("Cửa sổ");
            ToolStripMenuItem mnuCascade = new ToolStripMenuItem("Cascade");
            mnuCascade.Click += (s, e) => this.LayoutMdi(MdiLayout.Cascade);
            ToolStripMenuItem mnuTileV = new ToolStripMenuItem("Tile Vertical");
            mnuTileV.Click += (s, e) => this.LayoutMdi(MdiLayout.TileVertical);
            ToolStripMenuItem mnuTileH = new ToolStripMenuItem("Tile Horizontal");
            mnuTileH.Click += (s, e) => this.LayoutMdi(MdiLayout.TileHorizontal);
            ToolStripMenuItem mnuArrange = new ToolStripMenuItem("Arrange Icons");
            mnuArrange.Click += (s, e) => this.LayoutMdi(MdiLayout.ArrangeIcons);
            mnuCuaSo.DropDownItems.Add(mnuCascade);
            mnuCuaSo.DropDownItems.Add(mnuTileV);
            mnuCuaSo.DropDownItems.Add(mnuTileH);
            mnuCuaSo.DropDownItems.Add(mnuArrange);

            ToolStripMenuItem mnuTroGiup = new ToolStripMenuItem("Trợ giúp");
            ToolStripMenuItem mnuGioiThieu = new ToolStripMenuItem("Giới thiệu");
            mnuGioiThieu.Click += (s, e) => MessageBox.Show("Quản lý học sinh - Lab 06 (ADO.NET, mô hình Connected)");
            mnuTroGiup.DropDownItems.Add(mnuGioiThieu);

            menuStrip1.Items.Add(mnuHeThong);
            menuStrip1.Items.Add(mnuDanhMuc);
            menuStrip1.Items.Add(mnuQuanLy);
            menuStrip1.Items.Add(mnuCuaSo);
            menuStrip1.Items.Add(mnuTroGiup);
            MainMenuStrip = menuStrip1;

            // ----- Toolbar -----
            toolStrip1 = new ToolStrip();
            btnDangNhap = new ToolStripButton("Đăng nhập");
            btnDangNhap.Click += (s, e) => MoForm<frmLogin>();
            btnLop = new ToolStripButton("Lớp");
            btnLop.Click += (s, e) => MoForm<frmLop>();
            toolStrip1.Items.Add(btnDangNhap);
            toolStrip1.Items.Add(btnLop);

            // ----- Status bar -----
            statusStrip1 = new StatusStrip();
            lblThoiGian = new ToolStripStatusLabel();
            statusStrip1.Items.Add(lblThoiGian);

            Controls.Add(toolStrip1);
            Controls.Add(menuStrip1);
            Controls.Add(statusStrip1);

            // ----- Timer cập nhật giờ -----
            timer1 = new Timer();
            timer1.Interval = 1000;
            timer1.Tick += (s, e) => lblThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            timer1.Start();
            lblThoiGian.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

            // Chưa đăng nhập thì khóa các chức năng
            KhoaChucNang(true);

            Shown += (s, e) => MoForm<frmLogin>();
        }

        private void KhoaChucNang(bool khoa)
        {
            mnuLop.Enabled = !khoa;
            mnuHoSo.Enabled = !khoa;
            mnuNhapDiem.Enabled = !khoa;
            btnLop.Enabled = !khoa;
        }

        // Gọi sau khi đăng nhập thành công
        public void MoKhoa()
        {
            KhoaChucNang(false);
        }

        // Mở form con (chỉ mở một cửa sổ cho mỗi loại)
        private void MoForm<T>() where T : Form, new()
        {
            foreach (Form f in MdiChildren)
            {
                if (f is T)
                {
                    f.Activate();
                    return;
                }
            }
            T frm = new T();
            frm.MdiParent = this;   // đang code ở form cha
            frm.Show();             // form con chỉ gọi Show()
        }
    }
}

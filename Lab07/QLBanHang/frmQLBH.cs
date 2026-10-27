using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace QLBanHang
{
    public class frmQLBH : Form
    {
        QLBHDataContext db = new QLBHDataContext(Db.ChuoiKetNoi);

        // chứa danh sách các mặt hàng đang chọn
        List<HangChonMua> dsmua = new List<HangChonMua>();

        private TextBox txtKhachHang;
        private Button btnLapHoaDon;
        private ComboBox cboHangHoa;
        private NumericUpDown nudSoLuong;
        private Button btnThem;
        private DataGridView dgvHangHoa;
        private Label lblTongTien;

        public frmQLBH()
        {
            Text = "Quản lý Bán hàng (Easy Simple)";
            ClientSize = new Size(640, 430);
            StartPosition = FormStartPosition.CenterScreen;
            Font = new Font("Segoe UI", 11);

            Label lblKhach = new Label(); lblKhach.Text = "Khách hàng"; lblKhach.Location = new Point(20, 27); lblKhach.AutoSize = true;
            txtKhachHang = new TextBox(); txtKhachHang.Location = new Point(125, 24); txtKhachHang.Size = new Size(320, 27);
            btnLapHoaDon = new Button(); btnLapHoaDon.Text = "Lập hóa đơn"; btnLapHoaDon.Location = new Point(470, 20); btnLapHoaDon.Size = new Size(150, 35);

            Label lblHang = new Label(); lblHang.Text = "Hàng hóa"; lblHang.Location = new Point(20, 77); lblHang.AutoSize = true;
            cboHangHoa = new ComboBox();
            cboHangHoa.DropDownStyle = ComboBoxStyle.DropDownList;
            cboHangHoa.Location = new Point(125, 74);
            cboHangHoa.Size = new Size(240, 27);

            // Số lượng: NumericUpDown (Minimum 1, Increment 1, lấy giá trị ở Value kiểu decimal)
            nudSoLuong = new NumericUpDown();
            nudSoLuong.Minimum = 1;
            nudSoLuong.Maximum = 1000;
            nudSoLuong.Increment = 1;
            nudSoLuong.Value = 1;
            nudSoLuong.Location = new Point(375, 74);
            nudSoLuong.Size = new Size(70, 27);

            btnThem = new Button(); btnThem.Text = "Thêm"; btnThem.Location = new Point(470, 70); btnThem.Size = new Size(150, 35);

            dgvHangHoa = new DataGridView();
            dgvHangHoa.Location = new Point(20, 120);
            dgvHangHoa.Size = new Size(600, 240);
            dgvHangHoa.AutoGenerateColumns = false;
            dgvHangHoa.ReadOnly = true;
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.AllowUserToDeleteRows = false;
            dgvHangHoa.RowHeadersVisible = false;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            // Cột dạng TextBox, DataPropertyName gắn với thuộc tính của HangChonMua
            ThemCot("MaHH", "Mã hàng", 15, null);
            ThemCot("TenHH", "Tên hàng hóa", 40, null);
            ThemCot("DonGia", "Đơn giá", 20, "#,##0");
            ThemCot("SoLuong", "Số lượng", 15, null);
            ThemCot("ThanhTien", "Thành tiền", 25, "#,##0");

            Label lblT = new Label(); lblT.Text = "Tổng tiền"; lblT.Location = new Point(200, 380); lblT.AutoSize = true;
            lblTongTien = new Label();
            lblTongTien.Text = "0.00 đ";
            lblTongTien.ForeColor = Color.Red;
            lblTongTien.Font = new Font("Segoe UI", 14);
            lblTongTien.Location = new Point(295, 376);
            lblTongTien.AutoSize = true;

            Controls.Add(lblKhach); Controls.Add(txtKhachHang); Controls.Add(btnLapHoaDon);
            Controls.Add(lblHang); Controls.Add(cboHangHoa); Controls.Add(nudSoLuong); Controls.Add(btnThem);
            Controls.Add(dgvHangHoa); Controls.Add(lblT); Controls.Add(lblTongTien);

            btnThem.Click += btnThem_Click;
            btnLapHoaDon.Click += btnLapHoaDon_Click;
            Load += Form1_Load;
        }

        private void ThemCot(string property, string header, float weight, string format)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = property;
            col.DataPropertyName = property;
            col.HeaderText = header;
            col.FillWeight = weight;
            if (format != null)
                col.DefaultCellStyle.Format = format;
            dgvHangHoa.Columns.Add(col);
        }

        // Load form, đổ dữ liệu vào combobox hàng hóa
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                cboHangHoa.DataSource = db.HangHoas.OrderBy(p => p.TenHH).ToList();
                cboHangHoa.DisplayMember = "TenHH";
                cboHangHoa.ValueMember = "MaHH";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không kết nối được CSDL: " + ex.Message);
            }
        }

        // Nút Thêm: chưa có thì thêm vào, có rồi thì cập nhật số lượng
        private void btnThem_Click(object sender, EventArgs e)
        {
            if (cboHangHoa.SelectedValue == null)
            {
                MessageBox.Show("Chưa có hàng hóa để chọn.");
                return;
            }

            int soluong = (int)nudSoLuong.Value;

            // lấy mã hàng hóa đang chọn
            int mahh = Convert.ToInt32(cboHangHoa.SelectedValue);

            // kiểm tra xem hàng hóa đó có hay chưa
            HangChonMua hcm = dsmua.SingleOrDefault(p => p.MaHH == mahh);
            if (hcm == null)   // chưa có
            {
                // lấy hàng hóa có mã
                HangHoa hh = db.HangHoas.SingleOrDefault(p => p.MaHH == mahh);

                // thêm vào list dsmua
                dsmua.Add(new HangChonMua
                {
                    MaHH = hh.MaHH,
                    TenHH = hh.TenHH,
                    DonGia = hh.DonGia,
                    SoLuong = soluong
                });
            }
            else
            {
                // cập nhật số lượng mới
                hcm.SoLuong = soluong;
                MessageBox.Show("Đã cập nhật!!!");
            }

            // hiển thị lưới
            hien_thi_ds_mua();
        }

        // Hiển thị danh sách mua và tổng tiền
        void hien_thi_ds_mua()
        {
            dgvHangHoa.DataSource = null;
            dgvHangHoa.DataSource = dsmua;
            lblTongTien.Text = dsmua.Sum(p => p.SoLuong * p.DonGia).ToString("#,##0.00") + " đ";
        }

        // Nút Lập hóa đơn: lưu CSDL rồi in report
        private void btnLapHoaDon_Click(object sender, EventArgs e)
        {
            string khach = txtKhachHang.Text.Trim();
            if (khach == "")
            {
                MessageBox.Show("Vui lòng nhập tên khách hàng.");
                return;
            }
            if (dsmua.Count == 0)
            {
                MessageBox.Show("Chưa chọn hàng hóa nào.");
                return;
            }

            // Lưu hóa đơn và chi tiết trong cùng một transaction
            db.Connection.Open();
            db.Transaction = db.Connection.BeginTransaction();
            try
            {
                // tạo mới hóa đơn
                HoaDon hd = new HoaDon
                {
                    NgayLapHD = DateTime.Now,
                    KhachHang = khach,
                    TongTien = dsmua.Sum(p => p.ThanhTien)
                };
                // thêm hóa đơn
                db.HoaDons.InsertOnSubmit(hd);
                // cập nhật (để có MaHD tự tăng)
                db.SubmitChanges();

                // thêm chi tiết hóa đơn
                foreach (HangChonMua hcm in dsmua)
                {
                    CTHoaDon cthd = new CTHoaDon
                    {
                        MaHD = hd.MaHD,
                        MaHH = hcm.MaHH,
                        SoLuong = hcm.SoLuong
                    };
                    db.CTHoaDons.InsertOnSubmit(cthd);
                }
                db.SubmitChanges();
                db.Transaction.Commit();
            }
            catch (Exception ex)
            {
                db.Transaction.Rollback();
                MessageBox.Show("Lập hóa đơn thất bại: " + ex.Message);
                db.Connection.Close();
                db = new QLBHDataContext(Db.ChuoiKetNoi);
                return;
            }
            db.Transaction = null;
            db.Connection.Close();

            MessageBox.Show("Đã thêm thành công");

            // Hiển thị Report (chép danh sách ra trước khi xóa)
            List<HangChonMua> hoaDon = new List<HangChonMua>(dsmua);
            txtKhachHang.Text = "";
            dsmua.Clear();
            hien_thi_ds_mua();   // cập nhật lưới

            using (frmReport f = new frmReport(khach, hoaDon))
            {
                f.ShowDialog();
            }
        }
    }
}

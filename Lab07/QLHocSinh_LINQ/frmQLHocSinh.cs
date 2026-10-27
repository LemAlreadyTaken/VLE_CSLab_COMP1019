using System;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace QLHocSinh_LINQ
{
    public class frmQLHocSinh : Form
    {
        QLHSDataContext db = new QLHSDataContext(Db.ChuoiKetNoi);

        private TextBox txtMaHS;
        private ComboBox cboLop;
        private TextBox txtTenHS;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtDiaChi;
        private TextBox txtDiemTB;
        private TextBox txtTim;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnTim;
        private DataGridView dgvHocSinh;

        public frmQLHocSinh()
        {
            Text = "QUẢN LÝ HỌC SINH";
            ClientSize = new Size(720, 560);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblTieuDe = new Label();
            lblTieuDe.Text = "THÔNG TIN HỌC SINH";
            lblTieuDe.Font = new Font("Times New Roman", 20, FontStyle.Bold);
            lblTieuDe.ForeColor = Color.Red;
            lblTieuDe.AutoSize = true;
            lblTieuDe.Location = new Point(190, 8);

            txtMaHS = Them("Mã học sinh", 55, 15, 120, 130);
            Label lblLop = new Label(); lblLop.Text = "Lớp"; lblLop.Location = new Point(280, 58); lblLop.AutoSize = true;
            cboLop = new ComboBox();
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(320, 55);
            cboLop.Size = new Size(140, 23);

            txtTenHS = Them("Tên học sinh", 90, 15, 120, 340);

            Label lblNS = new Label(); lblNS.Text = "Ngày sinh"; lblNS.Location = new Point(15, 128); lblNS.AutoSize = true;
            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(135, 124);
            dtpNgaySinh.Size = new Size(130, 23);

            txtDiaChi = Them("Địa chỉ", 160, 15, 120, 340);
            txtDiemTB = Them("Điểm trung bình", 195, 15, 120, 130);

            GroupBox grpChucNang = new GroupBox();
            grpChucNang.Text = "Chức năng";
            grpChucNang.Location = new Point(495, 40);
            grpChucNang.Size = new Size(210, 190);

            btnThem = new Button(); btnThem.Text = "Thêm"; btnThem.Location = new Point(20, 25); btnThem.Size = new Size(170, 33);
            btnSua = new Button(); btnSua.Text = "Sửa"; btnSua.Location = new Point(20, 65); btnSua.Size = new Size(170, 33);
            btnXoa = new Button(); btnXoa.Text = "Xóa"; btnXoa.Location = new Point(20, 105); btnXoa.Size = new Size(170, 33);
            btnTim = new Button(); btnTim.Text = "Tìm"; btnTim.Location = new Point(20, 145); btnTim.Size = new Size(170, 33);
            grpChucNang.Controls.Add(btnThem);
            grpChucNang.Controls.Add(btnSua);
            grpChucNang.Controls.Add(btnXoa);
            grpChucNang.Controls.Add(btnTim);

            Label lblTim = new Label(); lblTim.Text = "Tìm theo tên (gần đúng)"; lblTim.Location = new Point(15, 240); lblTim.AutoSize = true;
            txtTim = new TextBox(); txtTim.Location = new Point(175, 237); txtTim.Size = new Size(285, 23);

            GroupBox grpDS = new GroupBox();
            grpDS.Text = "Danh sách học sinh";
            grpDS.Location = new Point(15, 270);
            grpDS.Size = new Size(690, 275);

            dgvHocSinh = new DataGridView();
            dgvHocSinh.Dock = DockStyle.Fill;
            dgvHocSinh.ReadOnly = true;
            dgvHocSinh.AllowUserToAddRows = false;
            dgvHocSinh.AllowUserToDeleteRows = false;
            dgvHocSinh.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHocSinh.MultiSelect = false;
            dgvHocSinh.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grpDS.Controls.Add(dgvHocSinh);

            Controls.Add(lblTieuDe);
            Controls.Add(lblLop);
            Controls.Add(cboLop);
            Controls.Add(lblNS);
            Controls.Add(dtpNgaySinh);
            Controls.Add(grpChucNang);
            Controls.Add(lblTim);
            Controls.Add(txtTim);
            Controls.Add(grpDS);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnTim.Click += btnTim_Click;
            dgvHocSinh.SelectionChanged += dgvHocSinh_SelectionChanged;
            Load += frmQLHocSinh_Load;
        }

        // Tạo nhanh một dòng "nhãn + ô nhập"
        private TextBox Them(string caption, int top, int labelLeft, int textLeft, int width)
        {
            Label lbl = new Label();
            lbl.Text = caption;
            lbl.Location = new Point(labelLeft, top + 3);
            lbl.AutoSize = true;
            TextBox txt = new TextBox();
            txt.Location = new Point(textLeft + 15, top);
            txt.Size = new Size(width, 23);
            Controls.Add(lbl);
            Controls.Add(txt);
            return txt;
        }

        // Sự kiện load form: đổ dữ liệu vào combobox Lớp
        private void frmQLHocSinh_Load(object sender, EventArgs e)
        {
            cboLop.DataSource = db.Lops.OrderBy(p => p.MaLop).ToList();
            cboLop.DisplayMember = "TenLop";
            cboLop.ValueMember = "MaLop";
            cboLop.SelectedIndexChanged += cboLop_SelectedIndexChanged;

            // lấy học sinh theo lớp (đang chọn)
            fillHocSinhByLop();
        }

        // Lấy danh sách học sinh theo lớp, lọc gần đúng theo tên đang tìm
        private void fillHocSinhByLop()
        {
            if (cboLop.SelectedValue == null)
                return;

            string malop = cboLop.SelectedValue.ToString();
            string ten = txtTim.Text.Trim();

            var dsHocSinh = db.HocSinhs.Where(p => p.MaLop == malop && p.TenHS.Contains(ten));
            dgvHocSinh.DataSource = dsHocSinh
                .Select(p => new { p.MaHS, p.TenHS, p.NgaySinh, p.DiaChi, p.DTB, p.Lop.TenLop })
                .ToList();
        }

        private void cboLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillHocSinhByLop();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            fillHocSinhByLop();
        }

        // Chọn dòng trên lưới thì đổ thông tin lên các ô nhập
        private void dgvHocSinh_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHocSinh.CurrentRow == null)
                return;

            string mahs = Convert.ToString(dgvHocSinh.CurrentRow.Cells["MaHS"].Value);
            HocSinh hs = db.HocSinhs.SingleOrDefault(p => p.MaHS == mahs);
            if (hs == null)
                return;

            txtMaHS.Text = hs.MaHS;
            txtTenHS.Text = hs.TenHS;
            if (hs.NgaySinh.HasValue)
                dtpNgaySinh.Value = hs.NgaySinh.Value;
            txtDiaChi.Text = hs.DiaChi;
            txtDiemTB.Text = hs.DTB.HasValue ? hs.DTB.Value.ToString("0.##", CultureInfo.InvariantCulture) : "";
        }

        // Đọc điểm trung bình (rỗng = null)
        private bool docDTB(out float? dtb)
        {
            dtb = null;
            string s = txtDiemTB.Text.Trim().Replace(',', '.');
            if (s == "")
                return true;
            float v;
            if (!float.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out v) || v < 0 || v > 10)
            {
                MessageBox.Show("Điểm trung bình phải là số từ 0 đến 10.");
                return false;
            }
            dtb = v;
            return true;
        }

        // Tạo mới đối tượng học sinh
        private void btnThem_Click(object sender, EventArgs e)
        {
            string mahs = txtMaHS.Text.Trim();
            if (mahs == "" || txtTenHS.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập mã và tên học sinh.");
                return;
            }
            if (db.HocSinhs.Any(p => p.MaHS == mahs))
            {
                MessageBox.Show("Mã học sinh đã tồn tại.");
                return;
            }
            float? dtb;
            if (!docDTB(out dtb))
                return;

            try
            {
                // 1. Tạo đối tượng
                HocSinh hs = new HocSinh
                {
                    MaHS = mahs,
                    TenHS = txtTenHS.Text.Trim(),
                    NgaySinh = dtpNgaySinh.Value,
                    DiaChi = txtDiaChi.Text.Trim(),
                    MaLop = cboLop.SelectedValue.ToString(),
                    DTB = dtb
                };
                // 2. Thêm
                db.HocSinhs.InsertOnSubmit(hs);
                // 3. Cập nhật
                db.SubmitChanges();
                fillHocSinhByLop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                db = new QLHSDataContext(Db.ChuoiKetNoi);
            }
        }

        // Xóa học sinh theo mã
        private void btnXoa_Click(object sender, EventArgs e)
        {
            // 1. Tìm đối tượng cần xóa
            string maCanTim = txtMaHS.Text.Trim();
            HocSinh hs = db.HocSinhs.SingleOrDefault(p => p.MaHS == maCanTim);
            if (hs == null)
            {
                MessageBox.Show("Không tìm thấy học sinh có mã này.");
                return;
            }
            if (MessageBox.Show("Xóa học sinh " + hs.MaHS + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                // 2. Xóa
                db.HocSinhs.DeleteOnSubmit(hs);
                // 3. Cập nhật
                db.SubmitChanges();
                fillHocSinhByLop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                db = new QLHSDataContext(Db.ChuoiKetNoi);
            }
        }

        // Sửa học sinh theo mã
        private void btnSua_Click(object sender, EventArgs e)
        {
            // 1. Tìm đối tượng cần sửa
            string maCanTim = txtMaHS.Text.Trim();
            HocSinh hs = db.HocSinhs.SingleOrDefault(p => p.MaHS == maCanTim);
            if (hs == null)
            {
                MessageBox.Show("Không tìm thấy học sinh có mã này.");
                return;
            }
            float? dtb;
            if (!docDTB(out dtb))
                return;

            try
            {
                // 2. Tiến hành sửa
                hs.MaLop = cboLop.SelectedValue.ToString();
                hs.TenHS = txtTenHS.Text.Trim();
                hs.NgaySinh = dtpNgaySinh.Value;
                hs.DiaChi = txtDiaChi.Text.Trim();
                hs.DTB = dtb;

                // 3. Cập nhật
                db.SubmitChanges();
                fillHocSinhByLop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
                db = new QLHSDataContext(Db.ChuoiKetNoi);
            }
        }
    }
}

using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace QuanLyHocSinh_ADO
{
    // Bài tập về nhà: Hiển thị / Thêm / Xóa / Cập nhật học sinh bằng mô hình Connected
    public class frmHocSinh : Form
    {
        private ListView lvHocSinh;
        private TextBox txtMaHS;
        private TextBox txtTenHS;
        private DateTimePicker dtpNgaySinh;
        private TextBox txtDiaChi;
        private TextBox txtDTB;
        private ComboBox cboLop;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnThoat;

        public frmHocSinh()
        {
            Text = "Hồ sơ Học sinh";
            ClientSize = new Size(720, 520);
            StartPosition = FormStartPosition.CenterParent;

            lvHocSinh = new ListView();
            lvHocSinh.Location = new Point(15, 15);
            lvHocSinh.Size = new Size(690, 230);
            lvHocSinh.View = View.Details;
            lvHocSinh.FullRowSelect = true;
            lvHocSinh.GridLines = true;
            lvHocSinh.MultiSelect = false;
            lvHocSinh.HideSelection = false;
            lvHocSinh.Columns.Add("Mã HS", 80);
            lvHocSinh.Columns.Add("Tên học sinh", 170);
            lvHocSinh.Columns.Add("Ngày sinh", 90);
            lvHocSinh.Columns.Add("Địa chỉ", 190);
            lvHocSinh.Columns.Add("ĐTB", 60);
            lvHocSinh.Columns.Add("Mã lớp", 80);
            lvHocSinh.ItemSelectionChanged += lvHocSinh_ItemSelectionChanged;

            GroupBox grp = new GroupBox();
            grp.Text = "Thông tin học sinh";
            grp.Location = new Point(15, 260);
            grp.Size = new Size(470, 240);

            AddRow(grp, "Mã HS", 28, out txtMaHS);
            AddRow(grp, "Tên HS", 63, out txtTenHS);
            AddRow(grp, "Địa chỉ", 133, out txtDiaChi);
            AddRow(grp, "Điểm TB", 168, out txtDTB);

            Label lblNS = new Label(); lblNS.Text = "Ngày sinh"; lblNS.Location = new Point(15, 98); lblNS.AutoSize = true;
            dtpNgaySinh = new DateTimePicker();
            dtpNgaySinh.Format = DateTimePickerFormat.Short;
            dtpNgaySinh.Location = new Point(95, 94);
            dtpNgaySinh.Size = new Size(150, 23);

            Label lblLop = new Label(); lblLop.Text = "Lớp"; lblLop.Location = new Point(15, 203); lblLop.AutoSize = true;
            cboLop = new ComboBox();
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.Location = new Point(95, 199);
            cboLop.Size = new Size(200, 23);

            grp.Controls.Add(lblNS);
            grp.Controls.Add(dtpNgaySinh);
            grp.Controls.Add(lblLop);
            grp.Controls.Add(cboLop);

            btnThem = new Button(); btnThem.Text = "Thêm"; btnThem.Location = new Point(520, 280); btnThem.Size = new Size(160, 40);
            btnSua = new Button(); btnSua.Text = "Sửa"; btnSua.Location = new Point(520, 335); btnSua.Size = new Size(160, 40);
            btnXoa = new Button(); btnXoa.Text = "Xóa"; btnXoa.Location = new Point(520, 390); btnXoa.Size = new Size(160, 40);
            btnThoat = new Button(); btnThoat.Text = "Thoát"; btnThoat.Location = new Point(520, 445); btnThoat.Size = new Size(160, 40);
            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += (s, e) => Close();

            Controls.Add(lvHocSinh);
            Controls.Add(grp);
            Controls.Add(btnThem);
            Controls.Add(btnSua);
            Controls.Add(btnXoa);
            Controls.Add(btnThoat);

            Load += frmHocSinh_Load;
        }

        private void AddRow(GroupBox grp, string caption, int top, out TextBox txt)
        {
            Label lbl = new Label();
            lbl.Text = caption;
            lbl.Location = new Point(15, top + 3);
            lbl.AutoSize = true;
            txt = new TextBox();
            txt.Location = new Point(95, top);
            txt.Size = new Size(350, 23);
            grp.Controls.Add(lbl);
            grp.Controls.Add(txt);
        }

        private void frmHocSinh_Load(object sender, EventArgs e)
        {
            loadLop();
            loadDanhSach();
        }

        // Đổ danh sách lớp vào ComboBox
        private void loadLop()
        {
            try
            {
                DataTable dt = new DataTable();
                using (SqlConnection con = new SqlConnection(Db.ChuoiKetNoi))
                using (SqlCommand cmd = new SqlCommand("SELECT MaLop, TenLop FROM Lop ORDER BY MaLop", con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        dt.Load(dr);
                    }
                }
                cboLop.DataSource = dt;
                cboLop.DisplayMember = "TenLop";
                cboLop.ValueMember = "MaLop";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được danh sách lớp: " + ex.Message);
            }
        }

        // Hiển thị danh sách học sinh bằng SqlDataReader
        private void loadDanhSach()
        {
            lvHocSinh.Items.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(Db.ChuoiKetNoi))
                using (SqlCommand cmd = new SqlCommand(
                    "SELECT MaHS, TenHS, NgaySinh, DiaChi, DTB, MaLop FROM HocSinh ORDER BY MaHS", con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            ListViewItem lvi = new ListViewItem(dr.GetString(0));
                            lvi.Tag = dr.GetString(0);
                            lvi.SubItems.Add(dr.GetString(1));
                            lvi.SubItems.Add(dr.IsDBNull(2) ? "" : dr.GetDateTime(2).ToString("dd/MM/yyyy"));
                            lvi.SubItems.Add(dr.IsDBNull(3) ? "" : dr.GetString(3));
                            lvi.SubItems.Add(dr.IsDBNull(4) ? "" : dr.GetFloat(4).ToString("0.##", CultureInfo.InvariantCulture));
                            lvi.SubItems.Add(dr.IsDBNull(5) ? "" : dr.GetString(5));
                            lvHocSinh.Items.Add(lvi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu: " + ex.Message);
            }
        }

        private void lvHocSinh_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;
            txtMaHS.Text = e.Item.Text;
            txtTenHS.Text = e.Item.SubItems[1].Text;

            DateTime ns;
            if (DateTime.TryParseExact(e.Item.SubItems[2].Text, "dd/MM/yyyy",
                CultureInfo.InvariantCulture, DateTimeStyles.None, out ns))
                dtpNgaySinh.Value = ns;

            txtDiaChi.Text = e.Item.SubItems[3].Text;
            txtDTB.Text = e.Item.SubItems[4].Text;
            if (e.Item.SubItems[5].Text != "")
                cboLop.SelectedValue = e.Item.SubItems[5].Text;
        }

        // Đọc và kiểm tra điểm trung bình (rỗng = NULL)
        private bool docDTB(out object dtb)
        {
            dtb = DBNull.Value;
            string s = txtDTB.Text.Trim().Replace(',', '.');
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

        private static object giaTri(string s)
        {
            s = s.Trim();
            return s == "" ? (object)DBNull.Value : s;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maHS = txtMaHS.Text.Trim();
            string tenHS = txtTenHS.Text.Trim();
            object dtb;
            if (maHS == "" || tenHS == "")
            {
                MessageBox.Show("Vui lòng nhập mã và tên học sinh.");
                return;
            }
            if (!docDTB(out dtb))
                return;

            try
            {
                Db.ExecuteNonQuery(
                    "INSERT INTO HocSinh(MaHS, TenHS, NgaySinh, DiaChi, DTB, MaLop) " +
                    "VALUES (@MaHS, @TenHS, @NgaySinh, @DiaChi, @DTB, @MaLop)",
                    new SqlParameter("@MaHS", maHS),
                    new SqlParameter("@TenHS", tenHS),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value),
                    new SqlParameter("@DiaChi", giaTri(txtDiaChi.Text)),
                    new SqlParameter("@DTB", dtb),
                    new SqlParameter("@MaLop", cboLop.SelectedValue));
                loadDanhSach();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Mã học sinh đã tồn tại.");
                else
                    MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvHocSinh.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn học sinh cần sửa.");
                return;
            }
            string maHS = lvHocSinh.SelectedItems[0].Tag.ToString();
            string tenHS = txtTenHS.Text.Trim();
            object dtb;
            if (tenHS == "")
            {
                MessageBox.Show("Tên học sinh không được rỗng.");
                return;
            }
            if (!docDTB(out dtb))
                return;

            try
            {
                Db.ExecuteNonQuery(
                    "UPDATE HocSinh SET TenHS = @TenHS, NgaySinh = @NgaySinh, DiaChi = @DiaChi, " +
                    "DTB = @DTB, MaLop = @MaLop WHERE MaHS = @MaHS",
                    new SqlParameter("@MaHS", maHS),
                    new SqlParameter("@TenHS", tenHS),
                    new SqlParameter("@NgaySinh", dtpNgaySinh.Value),
                    new SqlParameter("@DiaChi", giaTri(txtDiaChi.Text)),
                    new SqlParameter("@DTB", dtb),
                    new SqlParameter("@MaLop", cboLop.SelectedValue));
                loadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvHocSinh.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn học sinh cần xóa.");
                return;
            }
            string maHS = lvHocSinh.SelectedItems[0].Tag.ToString();
            if (MessageBox.Show("Xóa học sinh " + maHS + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                Db.ExecuteNonQuery("DELETE FROM HocSinh WHERE MaHS = @MaHS", new SqlParameter("@MaHS", maHS));
                txtMaHS.Clear();
                txtTenHS.Clear();
                txtDiaChi.Clear();
                txtDTB.Clear();
                loadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace QuanLyHocSinh_ADO
{
    public class frmLop : Form
    {
        private ComboBox cboListType;
        private ListView lvLop;
        private TextBox txtMaLop;
        private TextBox txtTenLop;
        private TextBox txtSiSo;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnThoat;

        public frmLop()
        {
            Text = "Quản lý Lớp";
            ClientSize = new Size(560, 460);
            StartPosition = FormStartPosition.CenterParent;

            Label lblDS = new Label();
            lblDS.Text = "Danh sách lớp";
            lblDS.Location = new Point(15, 15);
            lblDS.AutoSize = true;

            Label lblKieu = new Label();
            lblKieu.Text = "Kiểu hiển thị";
            lblKieu.Location = new Point(330, 15);
            lblKieu.AutoSize = true;

            // Thứ tự item trùng với thứ tự của enum View (LargeIcon, Details, SmallIcon, List, Tile)
            cboListType = new ComboBox();
            cboListType.DropDownStyle = ComboBoxStyle.DropDownList;
            cboListType.Items.AddRange(new object[] { "Large Icon", "Details", "Small Icon", "List", "Tile" });
            cboListType.Location = new Point(420, 12);
            cboListType.Size = new Size(125, 23);
            cboListType.SelectedIndexChanged += cboListType_SelectedIndexChanged;

            lvLop = new ListView();
            lvLop.Location = new Point(15, 45);
            lvLop.Size = new Size(530, 220);
            lvLop.View = View.Details;
            lvLop.FullRowSelect = true;
            lvLop.GridLines = true;
            lvLop.MultiSelect = false;
            lvLop.HideSelection = false;
            lvLop.Columns.Add("Mã", 100);
            lvLop.Columns.Add("Tên lớp", 280);
            lvLop.Columns.Add("Sĩ số", 100);
            lvLop.ItemSelectionChanged += lvLop_ItemSelectionChanged;

            GroupBox grp = new GroupBox();
            grp.Text = "Thông tin lớp";
            grp.Location = new Point(15, 280);
            grp.Size = new Size(300, 150);

            Label l1 = new Label(); l1.Text = "Mã lớp"; l1.Location = new Point(15, 28); l1.AutoSize = true;
            Label l2 = new Label(); l2.Text = "Tên lớp"; l2.Location = new Point(15, 63); l2.AutoSize = true;
            Label l3 = new Label(); l3.Text = "Sĩ số"; l3.Location = new Point(15, 98); l3.AutoSize = true;

            txtMaLop = new TextBox(); txtMaLop.Location = new Point(85, 25); txtMaLop.Size = new Size(195, 23);
            txtTenLop = new TextBox(); txtTenLop.Location = new Point(85, 60); txtTenLop.Size = new Size(195, 23);
            txtSiSo = new TextBox(); txtSiSo.Location = new Point(85, 95); txtSiSo.Size = new Size(195, 23);

            grp.Controls.Add(l1); grp.Controls.Add(l2); grp.Controls.Add(l3);
            grp.Controls.Add(txtMaLop); grp.Controls.Add(txtTenLop); grp.Controls.Add(txtSiSo);

            btnThem = new Button(); btnThem.Text = "Thêm"; btnThem.Location = new Point(340, 290); btnThem.Size = new Size(95, 40);
            btnSua = new Button(); btnSua.Text = "Sửa"; btnSua.Location = new Point(450, 290); btnSua.Size = new Size(95, 40);
            btnXoa = new Button(); btnXoa.Text = "Xóa"; btnXoa.Location = new Point(340, 345); btnXoa.Size = new Size(95, 40);
            btnThoat = new Button(); btnThoat.Text = "Thoát"; btnThoat.Location = new Point(450, 345); btnThoat.Size = new Size(95, 40);

            btnThem.Click += btnThem_Click;
            btnSua.Click += btnSua_Click;
            btnXoa.Click += btnXoa_Click;
            btnThoat.Click += (s, e) => Close();

            Controls.Add(lblDS); Controls.Add(lblKieu); Controls.Add(cboListType);
            Controls.Add(lvLop); Controls.Add(grp);
            Controls.Add(btnThem); Controls.Add(btnSua); Controls.Add(btnXoa); Controls.Add(btnThoat);

            Load += frmLop_Load;
        }

        // Hiển thị danh sách lớp (SqlConnection + SqlCommand + SqlDataReader)
        private void loadDanhSach()
        {
            lvLop.Items.Clear();
            try
            {
                using (SqlConnection con = new SqlConnection(Db.ChuoiKetNoi))
                using (SqlCommand cmd = new SqlCommand("SELECT MaLop, TenLop, SiSo FROM Lop ORDER BY MaLop", con))
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            string malop = dr.GetString(0);
                            string tenlop = dr.GetString(1);
                            string siso = dr.IsDBNull(2) ? "" : dr.GetInt16(2).ToString();

                            ListViewItem lvi = new ListViewItem();
                            lvi.Tag = malop;
                            lvi.Text = malop;
                            lvi.SubItems.Add(tenlop);
                            lvi.SubItems.Add(siso);
                            lvLop.Items.Add(lvi);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu: " + ex.Message);
            }
        }

        private void frmLop_Load(object sender, EventArgs e)
        {
            loadDanhSach();
            cboListType.SelectedIndex = 1;   // Details
        }

        private void cboListType_SelectedIndexChanged(object sender, EventArgs e)
        {
            lvLop.View = (View)cboListType.SelectedIndex;
        }

        // Chọn item trên ListView thì đổ dữ liệu vào các TextBox
        private void lvLop_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!e.IsSelected)
                return;
            txtMaLop.Text = e.Item.Text;
            txtTenLop.Text = e.Item.SubItems[1].Text;
            txtSiSo.Text = e.Item.SubItems[2].Text;
        }

        // Đọc và kiểm tra sĩ số
        private bool docSiSo(out short siSo)
        {
            int v;
            siSo = 0;
            if (!int.TryParse(txtSiSo.Text.Trim(), out v) || v < 0 || v > short.MaxValue)
            {
                MessageBox.Show("Sĩ số phải là số nguyên không âm.");
                return false;
            }
            siSo = (short)v;
            return true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maLop = txtMaLop.Text.Trim();
            string tenLop = txtTenLop.Text.Trim();
            short siSo;
            if (maLop == "" || tenLop == "")
            {
                MessageBox.Show("Vui lòng nhập mã lớp và tên lớp.");
                return;
            }
            if (!docSiSo(out siSo))
                return;

            try
            {
                Db.ExecuteNonQuery(
                    "INSERT INTO Lop(MaLop, TenLop, SiSo) VALUES (@MaLop, @TenLop, @SiSo)",
                    new SqlParameter("@MaLop", maLop),
                    new SqlParameter("@TenLop", tenLop),
                    new SqlParameter("@SiSo", siSo));
                loadDanhSach();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 2627)
                    MessageBox.Show("Mã lớp đã tồn tại.");
                else
                    MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (lvLop.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn lớp cần sửa.");
                return;
            }
            string maLop = lvLop.SelectedItems[0].Tag.ToString();
            string tenLop = txtTenLop.Text.Trim();
            short siSo;
            if (tenLop == "")
            {
                MessageBox.Show("Tên lớp không được rỗng.");
                return;
            }
            if (!docSiSo(out siSo))
                return;

            try
            {
                Db.ExecuteNonQuery(
                    "UPDATE Lop SET TenLop = @TenLop, SiSo = @SiSo WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop),
                    new SqlParameter("@TenLop", tenLop),
                    new SqlParameter("@SiSo", siSo));
                loadDanhSach();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (lvLop.SelectedItems.Count == 0)
            {
                MessageBox.Show("Hãy chọn lớp cần xóa.");
                return;
            }
            string maLop = lvLop.SelectedItems[0].Tag.ToString();
            if (MessageBox.Show("Xóa lớp " + maLop + "?", "Xác nhận", MessageBoxButtons.YesNo) != DialogResult.Yes)
                return;

            try
            {
                Db.ExecuteNonQuery("DELETE FROM Lop WHERE MaLop = @MaLop",
                    new SqlParameter("@MaLop", maLop));
                txtMaLop.Clear();
                txtTenLop.Clear();
                txtSiSo.Clear();
                loadDanhSach();
            }
            catch (SqlException ex)
            {
                if (ex.Number == 547)
                    MessageBox.Show("Không xóa được: lớp này còn học sinh.");
                else
                    MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}

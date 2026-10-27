using System;
using System.Drawing;
using System.Windows.Forms;

namespace MangForm
{
    public class FrmNhapLieu : Form
    {
        private Label lblSoDong;
        private Label lblSoCot;
        private TextBox txtSoDong;
        private TextBox txtSoCot;
        private Button btnKhoiTao;

        public FrmNhapLieu()
        {
            Text = "Nhập liệu";
            ClientSize = new Size(360, 110);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            lblSoDong = new Label();
            lblSoDong.Text = "Số dòng";
            lblSoDong.Location = new Point(15, 22);
            lblSoDong.AutoSize = true;

            lblSoCot = new Label();
            lblSoCot.Text = "Số cột";
            lblSoCot.Location = new Point(15, 62);
            lblSoCot.AutoSize = true;

            txtSoDong = new TextBox();
            txtSoDong.Location = new Point(85, 19);
            txtSoDong.Size = new Size(110, 23);

            txtSoCot = new TextBox();
            txtSoCot.Location = new Point(85, 59);
            txtSoCot.Size = new Size(110, 23);

            btnKhoiTao = new Button();
            btnKhoiTao.Text = "Khởi tạo form";
            btnKhoiTao.Location = new Point(215, 19);
            btnKhoiTao.Size = new Size(130, 63);
            btnKhoiTao.Click += btnKhoiTao_Click;

            Controls.Add(lblSoDong);
            Controls.Add(lblSoCot);
            Controls.Add(txtSoDong);
            Controls.Add(txtSoCot);
            Controls.Add(btnKhoiTao);
            AcceptButton = btnKhoiTao;
        }

        private void btnKhoiTao_Click(object sender, EventArgs e)
        {
            int sodong, socot;
            if (!int.TryParse(txtSoDong.Text, out sodong))
                sodong = 1;
            if (!int.TryParse(txtSoCot.Text, out socot))
                socot = 1;

            // Giới hạn để form không quá lớn
            if (sodong < 1) sodong = 1;
            if (socot < 1) socot = 1;
            if (sodong > 30) sodong = 30;
            if (socot > 30) socot = 30;

            FrmXuat frmX = new FrmXuat(sodong, socot);
            // canh giữa
            frmX.StartPosition = FormStartPosition.CenterScreen;
            frmX.ShowDialog();   // hiện
        }
    }
}

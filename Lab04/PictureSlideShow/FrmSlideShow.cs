using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace PictureSlideShow
{
    public class FrmSlideShow : Form
    {
        private PictureBox pictureBox1;
        private Button btnPrevious;
        private Button btnOpen;
        private Button btnNext;
        private Button btnStart;
        private Timer timer1;

        // Bước 1: khai báo 4 biến
        private string[] folderFile = null;   // danh sách tập tin hình trong thư mục
        private int selected = 0;             // vị trí hình đang hiển thị
        private int begin = 0;                // vị trí đầu mảng
        private int end = 0;                  // vị trí cuối mảng

        public FrmSlideShow()
        {
            Text = "Picture Slide Show";
            ClientSize = new Size(720, 580);
            StartPosition = FormStartPosition.CenterScreen;

            pictureBox1 = new PictureBox();
            pictureBox1.Location = new Point(10, 10);
            pictureBox1.Size = new Size(700, 460);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.BorderStyle = BorderStyle.FixedSingle;
            pictureBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

            btnPrevious = new Button();
            btnPrevious.Text = "<< Trước";
            btnPrevious.Location = new Point(10, 480);
            btnPrevious.Size = new Size(200, 40);
            btnPrevious.Enabled = false;
            btnPrevious.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnPrevious.Click += btnPrevious_Click;

            btnOpen = new Button();
            btnOpen.Text = "Mở Thư Mục hình";
            btnOpen.Location = new Point(260, 480);
            btnOpen.Size = new Size(200, 40);
            btnOpen.Anchor = AnchorStyles.Bottom;
            btnOpen.Click += btnOpen_Click;

            btnNext = new Button();
            btnNext.Text = "Sau >>";
            btnNext.Location = new Point(510, 480);
            btnNext.Size = new Size(200, 40);
            btnNext.Enabled = false;
            btnNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnNext.Click += btnNext_Click;

            btnStart = new Button();
            btnStart.Text = "Bắt đầu";
            btnStart.Location = new Point(10, 530);
            btnStart.Size = new Size(700, 36);
            btnStart.Enabled = false;
            btnStart.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            btnStart.Click += btnStart_Click;

            timer1 = new Timer();
            timer1.Interval = 2000;   // 2 giây / hình
            timer1.Tick += Timer1_Tick;

            Controls.Add(pictureBox1);
            Controls.Add(btnPrevious);
            Controls.Add(btnOpen);
            Controls.Add(btnNext);
            Controls.Add(btnStart);
        }

        // Bước 2: mở thư mục, lấy danh sách hình (jpg, jpeg, bmp, png)
        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog())
            {
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;

                string[] exts = { ".jpg", ".jpeg", ".bmp", ".png" };
                string[] files = Directory.GetFiles(folderBrowserDialog1.SelectedPath)
                    .Where(f => exts.Contains(Path.GetExtension(f).ToLower()))
                    .OrderBy(f => f)
                    .ToArray();

                if (files.Length == 0)
                {
                    MessageBox.Show("Thư mục này không có hình (jpg, jpeg, bmp, png).");
                    return;
                }

                timer1.Enabled = false;
                btnStart.Text = "<< Bắt đầu Slide Show >>";

                folderFile = files;
                selected = 0;
                begin = 0;
                end = folderFile.Length - 1;
                showImage(folderFile[selected]);

                btnPrevious.Enabled = true;
                btnNext.Enabled = true;
                btnStart.Enabled = true;
            }
        }

        // Đọc hình qua stream để không khóa file, sau đó hiển thị
        private void showImage(string path)
        {
            Image old = pictureBox1.Image;
            try
            {
                using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
                using (Image img = Image.FromStream(fs))
                {
                    pictureBox1.Image = new Bitmap(img);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không mở được hình: " + ex.Message);
                return;
            }
            if (old != null)
                old.Dispose();

            Text = "Picture Slide Show - " + Path.GetFileName(path)
                 + " (" + (selected + 1) + "/" + folderFile.Length + ")";
        }

        // Bước 3: hình trước
        private void prevImage()
        {
            if (selected == begin)
                selected = end;
            else
                selected = selected - 1;
            showImage(folderFile[selected]);
        }

        // Bước 4: hình kế
        private void nextImage()
        {
            if (selected == end)
                selected = begin;
            else
                selected = selected + 1;
            showImage(folderFile[selected]);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            prevImage();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            nextImage();
        }

        // Bước 5: timer chạy slide show
        private void Timer1_Tick(object sender, EventArgs e)
        {
            nextImage();
        }

        // Bước 6: bắt đầu / kết thúc slide show
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Enabled = false;
                btnStart.Text = "<< Bắt đầu Slide Show >>";
            }
            else
            {
                timer1.Enabled = true;
                btnStart.Text = "<< Kết thúc Slide Show >>";
            }
        }
    }
}

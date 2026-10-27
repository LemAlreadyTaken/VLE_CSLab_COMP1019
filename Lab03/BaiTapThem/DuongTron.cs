using System;

namespace BaiDuongTron
{
    class Diem
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Diem()
        {
            x = 0;
            y = 0;
        }

        public Diem(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public Diem(Diem p)
        {
            X = p.X;
            Y = p.Y;
        }

        public double KhoangCach(Diem b)
        {
            double dx = x - b.X;
            double dy = y - b.Y;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public override string ToString()
        {
            return "(" + x + ", " + y + ")";
        }
    }

    class DuongTron
    {
        private Diem tam;
        private int banKinh;

        public Diem Tam
        {
            get { return tam; }
            set { tam = value; }
        }

        public int BanKinh
        {
            get { return banKinh; }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Ban kinh khong duoc am");
                banKinh = value;
            }
        }

        // Khoi tao mac dinh
        public DuongTron()
        {
            tam = new Diem();
            banKinh = 0;
        }

        // Khoi tao day du
        public DuongTron(Diem t, int r)
        {
            Tam = new Diem(t);
            BanKinh = r;
        }

        // Khoi tao sao chep
        public DuongTron(DuongTron dt)
        {
            Tam = new Diem(dt.Tam);   // sao chep sau, khong dung chung doi tuong Diem
            BanKinh = dt.BanKinh;
        }

        public void Nhap()
        {
            int x, y, r;
            Console.Write("Nhap hoanh do tam x: ");
            while (!int.TryParse(Console.ReadLine(), out x))
                Console.Write("Khong hop le, nhap lai: ");
            Console.Write("Nhap tung do tam y: ");
            while (!int.TryParse(Console.ReadLine(), out y))
                Console.Write("Khong hop le, nhap lai: ");
            Console.Write("Nhap ban kinh r (>= 0): ");
            while (!int.TryParse(Console.ReadLine(), out r) || r < 0)
                Console.Write("Khong hop le, nhap lai: ");

            Tam = new Diem(x, y);
            BanKinh = r;
        }

        public void Xuat()
        {
            Console.WriteLine("(" + tam.ToString() + ", " + banKinh + ")");
        }

        public double DienTich()
        {
            return Math.PI * banKinh * banKinh;   // pi * r^2
        }

        public double ChuVi()
        {
            return 2 * Math.PI * banKinh;         // 2 * pi * r
        }

        // Vi tri tuong doi giua hai duong tron
        public string ViTriTuongDoi(DuongTron b)
        {
            double d = tam.KhoangCach(b.Tam);
            double tong = banKinh + b.BanKinh;
            double hieu = Math.Abs(banKinh - b.BanKinh);
            const double eps = 1e-9;

            if (d < eps && banKinh == b.BanKinh)
                return "Hai duong tron trung nhau";
            if (d > tong + eps)
                return "Hai duong tron o ngoai nhau";
            if (Math.Abs(d - tong) < eps)
                return "Hai duong tron tiep xuc ngoai";
            if (d < hieu - eps)
                return "Mot duong tron nam trong duong tron kia";
            if (Math.Abs(d - hieu) < eps)
                return "Hai duong tron tiep xuc trong";
            return "Hai duong tron cat nhau";
        }

        // Vi tri tuong doi cua mot diem voi duong tron
        public string ViTriDiem(Diem p)
        {
            double d = tam.KhoangCach(p);
            const double eps = 1e-9;

            if (Math.Abs(d - banKinh) < eps)
                return "Diem nam tren duong tron";
            if (d < banKinh)
                return "Diem nam trong duong tron";
            return "Diem nam ngoai duong tron";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            DuongTron dt = new DuongTron();
            dt.Xuat();

            Diem point = new Diem(1, 3);
            DuongTron dt2 = new DuongTron(point, 10);
            dt2.Xuat();

            DuongTron dt3 = new DuongTron(dt2);
            dt3.Xuat();

            Console.WriteLine("Chu vi duong tron: " + dt2.ChuVi());
            Console.WriteLine("Dien tich duong tron: " + dt2.DienTich());

            Console.WriteLine("dt2 va dt3: " + dt2.ViTriTuongDoi(dt3));
            DuongTron dt4 = new DuongTron(new Diem(15, 3), 5);
            Console.WriteLine("dt2 va dt4: " + dt2.ViTriTuongDoi(dt4));    // d = 14 < 15 -> cat nhau
            DuongTron dt5 = new DuongTron(new Diem(1, 3), 3);
            Console.WriteLine("dt2 va dt5: " + dt2.ViTriTuongDoi(dt5));    // nam trong

            Console.WriteLine("Diem (1, 13) voi dt2: " + dt2.ViTriDiem(new Diem(1, 13)));   // tren
            Console.WriteLine("Diem (0, 0) voi dt2: " + dt2.ViTriDiem(new Diem(0, 0)));     // trong
            Console.WriteLine("Diem (50, 50) voi dt2: " + dt2.ViTriDiem(new Diem(50, 50))); // ngoai

            Console.WriteLine("\nNhap mot duong tron moi:");
            DuongTron moi = new DuongTron();
            moi.Nhap();
            moi.Xuat();
            Console.WriteLine("Moi va dt2: " + moi.ViTriTuongDoi(dt2));
        }
    }
}

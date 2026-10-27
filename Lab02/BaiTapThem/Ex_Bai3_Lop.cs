using System;
using System.Text;

namespace Ex_Bai3_Lop
{
    // 1.1 Lop Sinh vien
    class SinhVien
    {
        public int MaSV { get; set; }
        public string HoTen { get; set; }
        public DateTime NgaySinh { get; set; }
        public string DiaChi { get; set; }
        public string DienThoai { get; set; }

        // Property chi doc
        public int LayTuoi
        {
            get { return DateTime.Now.Year - NgaySinh.Year; }
        }

        public SinhVien() { }

        public SinhVien(int maSV, string hoTen, DateTime ngaySinh, string diaChi, string dienThoai)
        {
            MaSV = maSV;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
            DienThoai = dienThoai;
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} sinh {2}, {3} tuoi, {4}, {5}.",
                MaSV, HoTen, NgaySinh.ToString("dd/MM/yyyy"), LayTuoi, DienThoai, DiaChi);
        }
    }

    // 1.2 Lop Hinh chu nhat
    class HinhChuNhat
    {
        private double mChieuCao;
        private double mChieuRong;

        public double ChieuCao
        {
            get { return mChieuCao; }
            set { mChieuCao = value; }
        }

        public double ChieuRong
        {
            get { return mChieuRong; }
            set { mChieuRong = value; }
        }

        public HinhChuNhat()
        {
            mChieuCao = 0;
            mChieuRong = 0;
        }

        public HinhChuNhat(double chieuCao, double chieuRong)
        {
            mChieuCao = chieuCao;
            mChieuRong = chieuRong;
        }

        public double ChuVi()
        {
            return (mChieuCao + mChieuRong) * 2;
        }

        public double DienTich()
        {
            return mChieuCao * mChieuRong;
        }

        public bool LaHinhVuong()
        {
            return mChieuCao == mChieuRong;
        }

        public void Xuat()
        {
            Console.WriteLine("Rong: {0}, Dai: {1}, Dien tich: {2}, Chu vi: {3}",
                mChieuRong, mChieuCao, DienTich(), ChuVi());
        }
    }

    // 1.3 Lop San pham
    class SanPham
    {
        private const double VAT = 0.1;   // thue VAT 10%

        public int Ma { get; set; }
        public string Ten { get; set; }
        public double Gia { get; set; }

        public SanPham() { }

        public SanPham(int ma, string ten, double gia)
        {
            Ma = ma;
            Ten = ten;
            Gia = gia;
        }

        public double TinhVAT()
        {
            return Gia * VAT;
        }

        public void Nhap()
        {
            int ma;
            Console.Write("Nhap ma: ");
            while (!int.TryParse(Console.ReadLine(), out ma))
                Console.Write("Khong hop le, nhap lai: ");
            Ma = ma;

            Console.Write("Nhap ten: ");
            Ten = Console.ReadLine();

            double gia;
            Console.Write("Nhap gia: ");
            while (!double.TryParse(Console.ReadLine(), out gia) || gia < 0)
                Console.Write("Khong hop le, nhap lai: ");
            Gia = gia;
        }

        public void Xuat()
        {
            Console.WriteLine("Ma: {0}, Ten: {1}, Gia: {2}, VAT: {3}", Ma, Ten, Gia, TinhVAT());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            // --- Test SinhVien ---
            Console.WriteLine("=== SinhVien ===");
            SinhVien sv1 = new SinhVien
            {
                MaSV = 1, HoTen = "Tran Van Teo",
                DienThoai = "0909789567", DiaChi = "123 Truong Dinh",
                NgaySinh = new DateTime(2000, 11, 28)
            };
            SinhVien sv2 = new SinhVien(2, "Tran Van Quang", new DateTime(1999, 11, 28),
                                        "123 Truong Cong Dinh", "0909567789");
            Console.WriteLine(sv1);
            Console.WriteLine(sv2);

            // --- Test HinhChuNhat ---
            Console.WriteLine("\n=== HinhChuNhat ===");
            HinhChuNhat h1 = new HinhChuNhat(5, 3);
            h1.Xuat();
            Console.WriteLine("La hinh vuong? " + h1.LaHinhVuong());
            HinhChuNhat h2 = new HinhChuNhat();
            h2.ChieuCao = 4;
            h2.ChieuRong = 4;
            h2.Xuat();
            Console.WriteLine("La hinh vuong? " + h2.LaHinhVuong());

            // --- Test SanPham: nhap 2 san pham ---
            Console.WriteLine("\n=== SanPham ===");
            SanPham[] ds = new SanPham[2];
            for (int i = 0; i < ds.Length; i++)
            {
                Console.WriteLine("San pham " + (i + 1) + ":");
                ds[i] = new SanPham();
                ds[i].Nhap();
            }
            Console.WriteLine("\nThong tin san pham:");
            foreach (SanPham sp in ds)
                sp.Xuat();
        }
    }
}

using System;
using System.Text.RegularExpressions;

namespace Baitap2
{
    // Cac ham kiem tra du lieu dung chung
    static class KiemTra
    {
        public static bool EmailHopLe(string s)
        {
            return s != null && Regex.IsMatch(s, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        public static bool DienThoaiHopLe(string s)
        {
            return s != null && Regex.IsMatch(s, @"^0\d{9,10}$");
        }

        public static bool KhacRong(string s)
        {
            return !string.IsNullOrWhiteSpace(s);
        }
    }

    class Nguoi
    {
        private string hoTen;
        private DateTime ngaySinh;
        private string diaChi;

        public string HoTen
        {
            get { return hoTen; }
            set
            {
                if (!KiemTra.KhacRong(value))
                    throw new ArgumentException("Ho ten khong duoc rong");
                hoTen = value.Trim();
            }
        }

        public DateTime NgaySinh
        {
            get { return ngaySinh; }
            set
            {
                if (value.Date > DateTime.Today)
                    throw new ArgumentException("Ngay sinh khong duoc o tuong lai");
                ngaySinh = value;
            }
        }

        public string DiaChi
        {
            get { return diaChi; }
            set { diaChi = value; }
        }

        // Property chi doc
        public int LayTuoi
        {
            get
            {
                DateTime today = DateTime.Today;
                int tuoi = today.Year - ngaySinh.Year;
                if (ngaySinh.Date > today.AddYears(-tuoi))
                    tuoi--;   // chua den sinh nhat nam nay
                return tuoi;
            }
        }

        public Nguoi()
        {
            hoTen = "";
            ngaySinh = DateTime.Today;
            diaChi = "";
        }

        public Nguoi(string hoTen, DateTime ngaySinh, string diaChi)
        {
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            DiaChi = diaChi;
        }

        public virtual void XemThongTin()
        {
            Console.WriteLine("Ho ten: " + HoTen);
            Console.WriteLine("Ngay sinh: " + NgaySinh.ToString("dd/MM/yyyy") + " (" + LayTuoi + " tuoi)");
            Console.WriteLine("Dia chi: " + DiaChi);
        }
    }

    class SinhVien : Nguoi
    {
        private string maSV;
        private string maLop;
        private string email;
        private string dienThoai;

        public string MaSV
        {
            get { return maSV; }
            set
            {
                if (!KiemTra.KhacRong(value))
                    throw new ArgumentException("Ma SV khong duoc rong");
                maSV = value.Trim();
            }
        }

        public string MaLop
        {
            get { return maLop; }
            set
            {
                if (!KiemTra.KhacRong(value))
                    throw new ArgumentException("Ma lop khong duoc rong");
                maLop = value.Trim();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!KiemTra.EmailHopLe(value))
                    throw new ArgumentException("Email khong hop le");
                email = value.Trim();
            }
        }

        public string DienThoai
        {
            get { return dienThoai; }
            set
            {
                if (!KiemTra.DienThoaiHopLe(value))
                    throw new ArgumentException("So dien thoai khong hop le");
                dienThoai = value.Trim();
            }
        }

        public SinhVien() : base()
        {
            maSV = "";
            maLop = "";
            email = "";
            dienThoai = "";
        }

        public SinhVien(string hoTen, DateTime ngaySinh, string diaChi,
                        string maSV, string maLop, string email, string dienThoai)
            : base(hoTen, ngaySinh, diaChi)
        {
            MaSV = maSV;
            MaLop = maLop;
            Email = email;
            DienThoai = dienThoai;
        }

        public override void XemThongTin()
        {
            base.XemThongTin();
            Console.WriteLine("Ma SV: " + MaSV);
            Console.WriteLine("Ma lop: " + MaLop);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Dien thoai: " + DienThoai);
        }
    }

    class NhanVien : Nguoi
    {
        private string maNhanVien;
        private string email;
        private string dienThoai;
        private DateTime ngayLamViec;
        private string maCongTy;

        public string MaNhanVien
        {
            get { return maNhanVien; }
            set
            {
                if (!KiemTra.KhacRong(value))
                    throw new ArgumentException("Ma nhan vien khong duoc rong");
                maNhanVien = value.Trim();
            }
        }

        public string Email
        {
            get { return email; }
            set
            {
                if (!KiemTra.EmailHopLe(value))
                    throw new ArgumentException("Email khong hop le");
                email = value.Trim();
            }
        }

        public string DienThoai
        {
            get { return dienThoai; }
            set
            {
                if (!KiemTra.DienThoaiHopLe(value))
                    throw new ArgumentException("So dien thoai khong hop le");
                dienThoai = value.Trim();
            }
        }

        public DateTime NgayLamViec
        {
            get { return ngayLamViec; }
            set
            {
                if (value.Date < NgaySinh.Date)
                    throw new ArgumentException("Ngay lam viec khong duoc truoc ngay sinh");
                ngayLamViec = value;
            }
        }

        public string MaCongTy
        {
            get { return maCongTy; }
            set
            {
                if (!KiemTra.KhacRong(value))
                    throw new ArgumentException("Ma cong ty khong duoc rong");
                maCongTy = value.Trim();
            }
        }

        public NhanVien() : base()
        {
            maNhanVien = "";
            email = "";
            dienThoai = "";
            ngayLamViec = DateTime.Today;
            maCongTy = "";
        }

        public NhanVien(string hoTen, DateTime ngaySinh, string diaChi,
                        string maNhanVien, string email, string dienThoai,
                        DateTime ngayLamViec, string maCongTy)
            : base(hoTen, ngaySinh, diaChi)
        {
            MaNhanVien = maNhanVien;
            Email = email;
            DienThoai = dienThoai;
            NgayLamViec = ngayLamViec;
            MaCongTy = maCongTy;
        }

        public override void XemThongTin()
        {
            base.XemThongTin();
            Console.WriteLine("Ma nhan vien: " + MaNhanVien);
            Console.WriteLine("Email: " + Email);
            Console.WriteLine("Dien thoai: " + DienThoai);
            Console.WriteLine("Ngay lam viec: " + NgayLamViec.ToString("dd/MM/yyyy"));
            Console.WriteLine("Ma cong ty: " + MaCongTy);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Nguoi ===");
            Nguoi n = new Nguoi("Nguyen Van A", new DateTime(1990, 5, 20), "12 Le Loi");
            n.XemThongTin();

            Console.WriteLine("\n=== SinhVien ===");
            SinhVien sv = new SinhVien("Tran Thi B", new DateTime(2006, 3, 15), "45 Tran Hung Dao",
                                       "SV001", "51.01.CNTT.A", "b@gmail.com", "0901234567");
            sv.XemThongTin();

            Console.WriteLine("\n=== NhanVien ===");
            NhanVien nv = new NhanVien("Le Van C", new DateTime(1995, 8, 1), "78 Hai Ba Trung",
                                       "NV001", "c@company.vn", "0912345678",
                                       new DateTime(2020, 2, 10), "CT01");
            nv.XemThongTin();

            Console.WriteLine("\n=== Kiem tra du lieu khong hop le ===");
            try
            {
                sv.Email = "email-sai";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
            try
            {
                nv.DienThoai = "123";
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }

            Console.WriteLine("\n=== Constructor khong tham so ===");
            SinhVien rong = new SinhVien();
            rong.HoTen = "Pham Van D";
            rong.MaSV = "SV002";
            Console.WriteLine(rong.HoTen + " - " + rong.MaSV);
        }
    }
}

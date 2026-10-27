using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Baitap1
{
    public class HinhHoc
    {
        public double DienTich { get; set; }
        public double ChuVi { get; set; }

        public virtual void tinhDienTichChuVi()
        {
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "S = {0:F2}; P = {1:F2}", DienTich, ChuVi);
        }
    }

    public class HinhChuNhat : HinhHoc
    {
        private double mChieuDai;
        private double mChieuRong;

        public double ChieuDai
        {
            get { return mChieuDai; }
            set
            {
                if (value > 0)
                    mChieuDai = value;
                else
                    throw new Exception("Chieu dai phai lon hon 0");
            }
        }

        public double ChieuRong
        {
            get { return mChieuRong; }
            set
            {
                if (value > 0)
                    mChieuRong = value;
                else
                    throw new Exception("Chieu rong phai lon hon 0");
            }
        }

        // Co tham so va khong tham so (gia tri mac dinh)
        public HinhChuNhat(double d = 1, double r = 0.5)
        {
            ChieuDai = d;
            ChieuRong = r;
            tinhDienTichChuVi();
        }

        public override void tinhDienTichChuVi()
        {
            DienTich = ChieuDai * ChieuRong;
            ChuVi = (ChieuDai + ChieuRong) * 2;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "L = {0}, W = {1}; {2}", ChieuDai, ChieuRong, base.ToString());
        }
    }

    public class HinhTron : HinhHoc
    {
        private double mBanKinh;

        public double BanKinh
        {
            get { return mBanKinh; }
            set
            {
                if (value > 0)
                    mBanKinh = value;
                else
                    throw new Exception("Ban kinh phai lon hon 0");
            }
        }

        public HinhTron(double r = 1)
        {
            BanKinh = r;
            tinhDienTichChuVi();
        }

        public override void tinhDienTichChuVi()
        {
            DienTich = Math.PI * BanKinh * BanKinh;
            ChuVi = 2 * Math.PI * BanKinh;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture,
                "R = {0}; {1}", BanKinh, base.ToString());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // ----- Phan 1: kiem tra constructor, properties, phuong thuc -----
            Console.WriteLine("=== Kiem tra cac lop ===");
            HinhChuNhat h1 = new HinhChuNhat();            // mac dinh
            HinhChuNhat h2 = new HinhChuNhat(5.3, 3.2);
            HinhTron t1 = new HinhTron();                  // mac dinh
            HinhTron t2 = new HinhTron(9.1);
            Console.WriteLine(h1);
            Console.WriteLine(h2);
            Console.WriteLine(t1);
            Console.WriteLine(t2);

            h1.ChieuDai = 4;
            h1.ChieuRong = 2;
            h1.tinhDienTichChuVi();
            Console.WriteLine("Sau khi doi kich thuoc: " + h1);

            try
            {
                h1.ChieuDai = -3;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }

            // ----- Phan 2: doc file, xu ly, ghi file -----
            Console.WriteLine("\n=== Doc file HinhHoc.txt ===");
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            string inPath = Path.Combine(dir, "HinhHoc.txt");
            string outPath = Path.Combine(dir, "output.txt");

            if (!File.Exists(inPath))
            {
                Console.WriteLine("Khong tim thay file: " + inPath);
                Console.WriteLine("Hay dat HinhHoc.txt vao thu muc chay chuong trinh (bin\\Debug).");
                return;
            }

            List<HinhHoc> ds = new List<HinhHoc>();
            string[] lines = File.ReadAllLines(inPath);
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i];
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;   // bo qua dong trong va dong chu thich

                string[] parts = line.Split(new char[] { '\t' }, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    int loai = int.Parse(parts[0].Trim());
                    if (loai == 1)
                    {
                        double dai = double.Parse(parts[1], CultureInfo.InvariantCulture);
                        double rong = double.Parse(parts[2], CultureInfo.InvariantCulture);
                        ds.Add(new HinhChuNhat(dai, rong));
                    }
                    else if (loai == 2)
                    {
                        double r = double.Parse(parts[1], CultureInfo.InvariantCulture);
                        ds.Add(new HinhTron(r));
                    }
                    else
                    {
                        Console.WriteLine("Dong {0}: loai hinh {1} khong hop le", i + 1, loai);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Dong {0} bi loi ({1}), bo qua.", i + 1, ex.Message);
                }
            }

            double tongS = ds.Sum(h => h.DienTich);
            double tongP = ds.Sum(h => h.ChuVi);

            List<HinhChuNhat> dsHcn = ds.OfType<HinhChuNhat>().ToList();
            List<HinhTron> dsTron = ds.OfType<HinhTron>().ToList();

            string dongHcn = "Khong co hinh chu nhat";
            if (dsHcn.Count > 0)
                dongHcn = dsHcn.OrderByDescending(h => h.DienTich).First().ToString();

            string dongTron = "Khong co hinh tron";
            if (dsTron.Count > 0)
                dongTron = dsTron.OrderBy(h => h.ChuVi).First().ToString();

            List<string> output = new List<string>();
            output.Add("So hinh: " + ds.Count);
            output.Add("Tong dien tich: " + tongS.ToString("F2", CultureInfo.InvariantCulture));
            output.Add("Tong chu vi: " + tongP.ToString("F2", CultureInfo.InvariantCulture));
            output.Add("Hinh chu nhat co dien tich lon nhat: " + dongHcn);
            output.Add("Hinh tron co chu vi nho nhat: " + dongTron);

            File.WriteAllLines(outPath, output, Encoding.UTF8);

            foreach (string s in output)
                Console.WriteLine(s);
            Console.WriteLine("\nDa ghi file: " + outPath);
        }
    }
}

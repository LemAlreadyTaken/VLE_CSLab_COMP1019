using System;
using System.Collections;
using System.Collections.Generic;

namespace Baitap3
{
    struct MatHang
    {
        public int MaMH;
        public string TenMH;
        public int SoLuong;
        public float DonGia;

        public MatHang(int MaMH, string TenMH, int SoLuong, float DonGia)
        {
            this.MaMH = MaMH;
            this.TenMH = TenMH;
            this.SoLuong = SoLuong;
            this.DonGia = DonGia;
        }

        public float ThanhTien()
        {
            return SoLuong * DonGia;
        }
    }

    class Program
    {
        static void ThemMatHang(Hashtable danhsach, MatHang m)
        {
            danhsach.Add(m.MaMH, m);
        }

        static bool TimMatHang(Hashtable danhsach, int MaMH)
        {
            return danhsach.ContainsKey(MaMH);
        }

        static void XoaMatHang(Hashtable danhsach, int MaMH)
        {
            if (TimMatHang(danhsach, MaMH))
                danhsach.Remove(MaMH);
        }

        static void XuatDanhSach(Hashtable danhsach)
        {
            if (danhsach.Count == 0)
            {
                Console.WriteLine("(Danh sach rong)");
                return;
            }

            // Hashtable khong giu thu tu, nen sap xep theo ma de de nhin
            List<int> keys = new List<int>();
            foreach (int k in danhsach.Keys)
                keys.Add(k);
            keys.Sort();

            Console.WriteLine("{0,-6} {1,-20} {2,8} {3,12} {4,14}", "Ma", "Ten", "So luong", "Don gia", "Thanh tien");
            foreach (int k in keys)
            {
                MatHang m = (MatHang)danhsach[k];
                Console.WriteLine("{0,-6} {1,-20} {2,8} {3,12} {4,14}",
                    m.MaMH, m.TenMH, m.SoLuong, m.DonGia, m.ThanhTien());
            }
        }

        static int DocSoNguyen(string prompt)
        {
            int v;
            Console.Write(prompt);
            while (!int.TryParse(Console.ReadLine(), out v))
                Console.Write("Khong hop le, nhap lai: ");
            return v;
        }

        static float DocSoThuc(string prompt)
        {
            float v;
            Console.Write(prompt);
            while (!float.TryParse(Console.ReadLine(), out v))
                Console.Write("Khong hop le, nhap lai: ");
            return v;
        }

        static void Main(string[] args)
        {
            Hashtable danhsach = new Hashtable();

            // 1. Nhap danh sach mat hang
            string tiepTuc = "y";
            do
            {
                int ma = DocSoNguyen("Nhap ma mat hang: ");
                if (TimMatHang(danhsach, ma))
                {
                    Console.WriteLine("Ma mat hang da ton tai, vui long nhap ma khac.");
                    continue;
                }

                Console.Write("Nhap ten mat hang: ");
                string ten = Console.ReadLine();
                int sl = DocSoNguyen("Nhap so luong: ");
                float gia = DocSoThuc("Nhap don gia: ");

                ThemMatHang(danhsach, new MatHang(ma, ten, sl, gia));

                Console.Write("Tiep tuc nhap? (y/n): ");
                tiepTuc = Console.ReadLine();
            } while (tiepTuc == null || tiepTuc.Trim().ToUpper() == "Y");

            // 2. Xuat danh sach
            Console.WriteLine("\nDanh sach mat hang:");
            XuatDanhSach(danhsach);

            // 3. Tim va xoa theo ma
            int maCanTim = DocSoNguyen("\nNhap ma mat hang can tim: ");
            if (TimMatHang(danhsach, maCanTim))
            {
                Console.WriteLine("Tim thay, tien hanh xoa.");
                XoaMatHang(danhsach, maCanTim);
                Console.WriteLine("\nDanh sach sau khi xoa:");
                XuatDanhSach(danhsach);
            }
            else
            {
                Console.WriteLine("Khong tim thay mat hang co ma " + maCanTim);
            }
        }
    }
}

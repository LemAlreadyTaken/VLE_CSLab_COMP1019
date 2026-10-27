using System;
using System.Collections.Generic;
using System.Globalization;

namespace Baitap3
{
    // Da thuc P = a0 + a1*x + a2*x^2 + ... + an*x^n
    class DaThuc
    {
        private double[] heSo;   // heSo[k] = ak

        // Ham khoi tao: tao da thuc bac n co moi he so bang 0
        public DaThuc(int bac)
        {
            if (bac < 0)
                throw new ArgumentException("Bac cua da thuc phai >= 0");
            heSo = new double[bac + 1];
        }

        // Property cho biet bac n
        public int Bac
        {
            get { return heSo.Length - 1; }
        }

        // Indexer doc/ghi he so ak
        public double this[int k]
        {
            get
            {
                if (k < 0 || k > Bac)
                    throw new IndexOutOfRangeException("k phai tu 0 den " + Bac);
                return heSo[k];
            }
            set
            {
                if (k < 0 || k > Bac)
                    throw new IndexOutOfRangeException("k phai tu 0 den " + Bac);
                heSo[k] = value;
            }
        }

        // Phep cong
        public static DaThuc operator +(DaThuc a, DaThuc b)
        {
            int bac = Math.Max(a.Bac, b.Bac);
            DaThuc kq = new DaThuc(bac);
            for (int k = 0; k <= bac; k++)
            {
                double ak = k <= a.Bac ? a[k] : 0;
                double bk = k <= b.Bac ? b[k] : 0;
                kq[k] = ak + bk;
            }
            return kq;
        }

        // Phep tru
        public static DaThuc operator -(DaThuc a, DaThuc b)
        {
            int bac = Math.Max(a.Bac, b.Bac);
            DaThuc kq = new DaThuc(bac);
            for (int k = 0; k <= bac; k++)
            {
                double ak = k <= a.Bac ? a[k] : 0;
                double bk = k <= b.Bac ? b[k] : 0;
                kq[k] = ak - bk;
            }
            return kq;
        }

        // Tinh gia tri P(b) bang so do Horner
        public double TinhGiaTri(double b)
        {
            double kq = 0;
            for (int k = Bac; k >= 0; k--)
                kq = kq * b + heSo[k];
            return kq;
        }

        public override string ToString()
        {
            string s = "";
            for (int k = 0; k <= Bac; k++)
            {
                double a = heSo[k];
                if (a == 0)
                    continue;

                string hang = Math.Abs(a).ToString(CultureInfo.InvariantCulture);
                if (k == 1)
                    hang += "x";
                else if (k > 1)
                    hang += "x^" + k;

                if (s == "")
                    s = (a < 0 ? "-" : "") + hang;
                else
                    s += (a < 0 ? " - " : " + ") + hang;
            }
            return s == "" ? "0" : s;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // P = 1 + 2x + 3x^2
            DaThuc p = new DaThuc(2);
            p[0] = 1;
            p[1] = 2;
            p[2] = 3;

            // Q = 4 - x
            DaThuc q = new DaThuc(1);
            q[0] = 4;
            q[1] = -1;

            Console.WriteLine("P(x) = " + p + "  (bac " + p.Bac + ")");
            Console.WriteLine("Q(x) = " + q + "  (bac " + q.Bac + ")");
            Console.WriteLine("P + Q = " + (p + q));
            Console.WriteLine("P - Q = " + (p - q));
            Console.WriteLine("P(2) = " + p.TinhGiaTri(2));   // 1 + 4 + 12 = 17
            Console.WriteLine("Q(2) = " + q.TinhGiaTri(2));   // 2
            Console.WriteLine("He so a1 cua P: " + p[1]);

            try
            {
                Console.WriteLine(p[5]);
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }
}

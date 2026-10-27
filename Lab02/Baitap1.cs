using System;
using System.Text;

namespace Baitap1
{
    // Ngoai le tu dinh nghia: gia tri duoi dau can < 0
    class NotNegativeException : Exception
    {
        public NotNegativeException()
            : base("Gia tri duoi dau can < 0")
        { }
    }

    class Program
    {
        // H = sqrt((3x + 2y) / (2x - y))
        static double TinhH(int x, int y)
        {
            int mau = 2 * x - y;
            if (mau == 0)
                throw new DivideByZeroException("Mau so bang 0 (2x - y = 0)");

            // Ep kieu double de khong bi chia nguyen
            double bieuThuc = (3.0 * x + 2.0 * y) / mau;
            if (bieuThuc < 0)
                throw new NotNegativeException();

            return Math.Sqrt(bieuThuc);
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                Console.Write("Nhap x: ");
                int x = int.Parse(Console.ReadLine());
                Console.Write("Nhap y: ");
                int y = int.Parse(Console.ReadLine());

                double h = TinhH(x, y);
                Console.WriteLine("H = " + h);
            }
            catch (FormatException)
            {
                Console.WriteLine("Loi: du lieu nhap vao phai la so nguyen.");
            }
            catch (OverflowException)
            {
                Console.WriteLine("Loi: so nhap vao qua lon hoac qua nho.");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Loi chia cho 0: " + ex.Message);
            }
            catch (NotNegativeException ex)
            {
                Console.WriteLine("Loi: " + ex.Message);
            }
        }
    }
}

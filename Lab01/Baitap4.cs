using System;
using System.Text;

namespace Baitap4
{
    class Program
    {
        // Cau 1: nhap ho, ten lot, ten -> xuat ho ten day du
        static void Cau1()
        {
            Console.Write("Nhap ho: ");
            string ho = Console.ReadLine();
            Console.Write("Nhap ten lot: ");
            string tenlot = Console.ReadLine();
            Console.Write("Nhap ten: ");
            string ten = Console.ReadLine();

            Console.WriteLine("Ho va ten day du: {0} {1} {2}", ho, tenlot, ten);
        }

        // Cau 2: nhap ho ten day du -> tach thanh ho, ten lot, ten
        static void Cau2()
        {
            Console.Write("Nhap ho ten: ");
            string fullname = Console.ReadLine();

            // RemoveEmptyEntries de khong bi phan tu rong khi co nhieu khoang trang
            string[] parts = fullname.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 2)
            {
                Console.WriteLine("Can nhap it nhat ho va ten.");
                return;
            }

            string ho = parts[0];
            string ten = parts[parts.Length - 1];
            // Ten lot co the gom nhieu tu, hoac rong neu khong co
            string tenlot = parts.Length > 2
                ? string.Join(" ", parts, 1, parts.Length - 2)
                : "(khong co)";

            Console.WriteLine("Ho: {0}, ten lot: {1}, ten: {2}", ho, tenlot, ten);
        }

        // Cau 3: chuan hoa ho ten (hoa thuong, khoang trang)
        static void Cau3()
        {
            Console.Write("Nhap ho ten (khong can theo chuan): ");
            string fullname = Console.ReadLine();

            string[] parts = fullname.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length == 0)
            {
                Console.WriteLine("Ban chua nhap gi.");
                return;
            }

            for (int i = 0; i < parts.Length; i++)
                parts[i] = parts[i].Substring(0, 1).ToUpper() + parts[i].Substring(1).ToLower();

            Console.WriteLine("Ten theo dung chuan la: " + string.Join(" ", parts));
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;  // de hien thi/nhap tieng Viet co dau
            Console.InputEncoding = Encoding.UTF8;

            Cau1();
            Console.WriteLine();
            Cau2();
            Console.WriteLine();
            Cau3();

            Console.ReadKey(true);
        }
    }
}

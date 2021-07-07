using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba14
{
    public static class CheckInput
    {
        public static int ParseInt(string input)
        {
            int n;
            bool ok;
            Console.Write(input);
            do
            {
                string str = Console.ReadLine();
                ok = int.TryParse(str, out n);
                if (!((ok) && (n >= 0)))
                {
                    ok = false;
                    Console.WriteLine("Число должно быть целым и неотрицательным!");
                }
            }
            while (!ok);
            return n;
        }

        public static double ParseDouble(string input)
        {
            double n;
            bool ok;
            Console.WriteLine(input);
            do
            {
                string str = Console.ReadLine();
                ok = double.TryParse(str, out n);
                if (!ok)
                    Console.WriteLine("Число должно быть целым!");
            }
            while (!ok);
            return n;
        }
    }
}

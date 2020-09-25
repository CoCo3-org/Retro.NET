using System;
using System.Collections.Generic;
using System.Text;

namespace MC10
{
    class Program
    {
        static void Main(string[] args)
        {
            byte b = 0;
            for (int x = 0; x < 512; x++)
                MC10.VDG.RAM[x] = b++;

            for (int x = 0; x < 512; x++)
                MC10.Poke(x * (16 * 1024), b++);
        }
    }
}

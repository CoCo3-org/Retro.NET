using System;
using System.Collections.Generic;
using System.Text;

namespace MC10
{
    public static class MC10
    {
        public static byte[] RAM = new byte[1024 * 64];

        public static byte Peek(int address) 
        {
            return 0;
        }

        public static void Poke(int address, byte value) 
        {

        }

        public static class VDG 
        {
            public static byte[] RAM = new byte[512];
        }
    }
}

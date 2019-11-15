﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace testproj
{
    class Program
    {
        const int magic = 0xFEED;

        [DllImport("client.dll")]
        public static extern int Init(int magic, int number);

        [DllImport("client.dll")]
        public static extern int GetOffset(int magic);

        [DllImport("client.dll")]
        public static extern void CopyDriverMemory(int magic, ref CopyStruct str, int number);

        unsafe public struct CopyStruct
        {
            public int dpid;
            public ulong daddr;

            public int spid;
            public ulong saddr;

            public int size;

            public fixed char buffer[1024];

            public int handled;
        }

        static void Main(string[] args)
        {
            Console.ReadKey();
            
            int ini = Init(magic, 10);
            Console.WriteLine("Init: " + ini.ToString("X"));
            
            int offset = GetOffset(magic);
            Console.WriteLine("Static: " + offset.ToString("x"));

            CopyStruct ics = new CopyStruct();
            ics.handled = 0;
            ics.size = 10;
            ics.daddr = 0xFEED;
            CopyDriverMemory(magic, ref ics, 2);

            Console.WriteLine("Dpid: " + ics.dpid.ToString("X"));
            Console.WriteLine("Spid: " + ics.spid.ToString("X"));
            Console.WriteLine("Saddr: " + ics.saddr.ToString("X"));
            Console.WriteLine("Handled: " + ics.handled.ToString("X"));

            Console.ReadKey();
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stock_crawl
{
    class Program
    {
        static void Main(string[] args)
        {
            Web_crawl crawl = new Web_crawl();
            crawl.run();
            Console.ReadKey();
        }
    }
}

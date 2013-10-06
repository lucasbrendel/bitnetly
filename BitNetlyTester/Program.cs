using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitNetly.Objects;

namespace BitNetlyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            string l = Link.ShortenURL("http://google.com", "25d28cd775edccdb0e26cb2b4f79b8ffc78ff900");
        }
    }
}

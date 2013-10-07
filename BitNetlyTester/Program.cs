using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BitNetly.Objects;
using BitNetly;

namespace BitNetlyTester
{
    class Program
    {
        static void Main(string[] args)
        {
            //string l = Link.ShortenURL("http://google.com", "25d28cd775edccdb0e26cb2b4f79b8ffc78ff900");

            BitNetlyService s = new BitNetlyService("o_5kbb1rm8hg", "winph820", "575d16b79a3f0afb5b8919e562a769107ccd4ec5", "072f9d8e5dba4d703ef5b54fc219af5bb21f882e", BitNetlyService.LoginRoute.Basic);

            Link l = Link.ExpandURL("http://bit.ly/lucasandsara", s.AccessToken);
        }
    }
}

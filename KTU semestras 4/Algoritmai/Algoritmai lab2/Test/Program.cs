using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Task t = Task.Factory.StartNew(() => {
                // Just loop.
                int ctr = 0;
                for (ctr = 0; ctr <= 1000; ctr++)
                {
                    Console.WriteLine(ctr); }
                Console.WriteLine("Finished {0} loop iterations", ctr);
            });
            t.Wait();            
        }         
    }

    class path
    {
        public int cost { get; set; }
        public int node1 { get; set; }
        public int node2 { get; set; }

        public path(int node1, int node2, int cost)
        {
            this.node1 = node1;
            this.node2 = node2;
            this.cost = cost;
        }
    }

}

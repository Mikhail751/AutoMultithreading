using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoMultithreading
{
    class InData
    {
        public long s, e;
        public InData(long start, long end)
        {
            this.s = start;
            this.e = end;
        }
    }
    class A : AutoThreadsCore
    {
        public override object Work(object args)
        {
            InData data = (InData)args;
            long res = 0;
            for (long i = data.s; i < data.e; i++)
            {
                res += i;
            }
            return res;
        }
        public override object MergeData(object[] data)
        {
            long sum = 0;
            foreach (var item in this.result)
            {
                sum += (long)item;
            }
            return sum;
        }
    }
    class Program
    {
        private static long x(long s, long e)
        {
            long res = 0;
            for (long i = s; i < e; i++)
            {
                res += i;
            }
            return res;
        }
        static void Main(string[] args)
        {
            Stopwatch s1 = new Stopwatch();
            Stopwatch s2 = new Stopwatch();
            long factor = 100000000;
            A a = new A();
            InData[] datas = new InData[4];
            datas[0] = new InData(1, factor);
            datas[1] = new InData(factor + 1, factor * 2);
            datas[2] = new InData(factor * 2 + 1, factor * 3);
            datas[3] = new InData(factor * 3 + 1, factor * 4);
            s1.Start();
            a.Run(datas);
            s1.Stop();
            s2.Start();
            x(1, factor * 4);
            s2.Stop();
            double t1 = (double)s1.ElapsedMilliseconds / 1000;
            double t2 = (double)s2.ElapsedMilliseconds / 1000;
            Console.WriteLine($"MT time: {t1}, ST time: {t2} -> MT {t2 / t1} faster");
        }
    }
}

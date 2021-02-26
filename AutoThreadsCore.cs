using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutoMultithreading
{
    class AutoThreadsCore
    {
        public int Counter;
        public object[] result;
        public virtual object Work(object args) { return null; }
        public object Run(object[] InputData)
        {
            Counter = InputData.Length;
            result = new object[InputData.Length];
            Thread[] streams = new Thread[result.Length];
            for (int i = 0; i < result.Length; i++)
            {
                streams[i] = new Thread(new ParameterizedThreadStart(LowWork));
            }
            for (int i = 0; i < result.Length; i++)
            {
                streams[i].Name = i.ToString();
                streams[i].Start(InputData[i]);
            }
            while (Counter != 0) { continue; }
            return MergeData(result);
        }
        private void LowWork(object data)
        { 
            this.result[Int32.Parse(Thread.CurrentThread.Name)] = Work(data);
            this.Counter -= 1;
        }
        public virtual object MergeData(object[] data) { return null; }
    }
}

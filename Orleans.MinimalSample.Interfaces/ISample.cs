using System;
using System.Threading.Tasks;

namespace Orleans.MinimalSample.Interfaces
{
        public interface ISample : IGrainWithStringKey
        {
            Task<string> Ping(string message);
        }
}

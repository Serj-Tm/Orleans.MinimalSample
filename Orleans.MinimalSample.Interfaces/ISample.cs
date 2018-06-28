using System;

namespace Orleans.MinimalSample.Interfaces
{
    public interface ISample
    {
        string Ping(string message);
    }
}

using System;

namespace GreeterLogic
{
    public class GreeterProvider
    {
        public string SayHello(string name)
        {
			return $"Hello {name} ({Environment.MachineName})";
        }
    }
}

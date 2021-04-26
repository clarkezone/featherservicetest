using System;
using Xunit;

namespace GreeterLogic.Tests
{
    public class UnitTest
    {
        [Fact]
        public void Test1()
        {
            GreeterProvider provider = new GreeterProvider();
            var greeting = provider.SayHello("James");
            Assert.True(greeting == "Hello James");
        }
    }
}

using System;
using Moq;

namespace Worker.Tests
{
    public static class ItShould
    {
        public static T Be<T>(T value)
        {
            return It.Is<T>(v => v.Equals(value));
        }
    }
}

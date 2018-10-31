namespace Lab2test
{
    public static class MathProviderConstructors
    {
        public static void Construct()
        {
            MathProvider<byte>.Subtract = (byte a, byte b) =>
            {
                return (decimal)(a - b);
            };

            MathProvider<short>.Subtract = (short a, short b) =>
            {
                return (decimal)(a - b);
            };

            MathProvider<int>.Subtract = (int a, int b) =>
            {
                return (decimal)(a - b);
            };

            MathProvider<long>.Subtract = (long a, long b) =>
            {
                return (decimal)(a - b);
            };
        }
    }
}

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace TestHelperExtensions.Test.Helpers
{
    [ExcludeFromCodeCoverage]
    internal static class Randomizer
    {
        const int _maxSeederUses = 100;
        private static Random _seeder = new Random();
        private static int _seederUses = 0;

        private static readonly object _threadLock = new object();

        private static readonly ThreadLocal<Random> _threadRandom = new ThreadLocal<Random>(Create);


        private static Random Instance
        {
            get { return _threadRandom.Value; }
        }

        internal static Random Create()
        {
            lock (_threadLock)
            {
                _seederUses++;
                if (_seederUses > _maxSeederUses)
                    _seeder = new Random();
                return new Random(_seeder.Next());
            }
        }


        internal static int Next()
        {
            return Instance.Next();
        }

        internal static int Next(int maxValue)
        {
            return Instance.Next(maxValue);
        }

        internal static int Next(int minValue, int maxValue)
        {
            return Instance.Next(minValue, maxValue);
        }


        internal static double NextDouble()
        {
            return Instance.NextDouble();
        }

        internal static void NextBytes(byte[] buffer)
        {
            Instance.NextBytes(buffer);
        }
    }
}

using System;
using System.Collections.Generic;

namespace Services.Randomizer
{
    public interface IRandomizer
    {
        int Range(int min, int max);
        float Range(float min, float max);
    }

    public static class RandomizerExtensions
    {
        public static T GetRandom<T>(this IRandomizer randomizer, IReadOnlyList<T> items)
        {
            if (items.Count == 0)
            {
                throw new ArgumentException("Can't get random from collection with 0 Count");
            }
            var rnd = randomizer.Range(0, items.Count);
            return items[rnd];
        }
    }
}
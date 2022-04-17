using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Utility
{
    public static class Util
    {
        public static T Raffle<T>(IEnumerable<KeyValuePair<T, float>> weightPairs)
        {
            var sortedPairs = weightPairs.OrderByDescending(x => x.Value).ToArray();
            var total = sortedPairs.Select(x => x.Value).Sum();
            float randomPoint = Random.Range(0, total);

            foreach (var elem in sortedPairs)
            {
                if (randomPoint < elem.Value)
                {
                    return elem.Key;
                }

                randomPoint -= elem.Value;
            }

            return sortedPairs[sortedPairs.Length - 1].Key;
        }
    }
}

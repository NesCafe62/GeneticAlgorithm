using System;
using System.Collections.Generic;
using System.Linq;

namespace GeneticAlgorithm {

	class Utils {

		public static int[] GetUniqueRandom(Random random,int from, int to, int count) {
			List<int> result = new List<int>(count);
			var indexes = Enumerable.Range(from, to - from).ToList();
			for (var i = 0; i < count; i++) {
				var randomIndex = random.Next(0, indexes.Count);
				result.Add(indexes[randomIndex]);
				indexes.RemoveAt(randomIndex);
			}
			result.Sort();
			return result.ToArray();
		}

	}

}

using System;
using System.Collections.Generic;
using Random = System.Random;

namespace CsCat
{
	//与Unity的Random保持一致
	public class RandomManager
	{
		private int _seed;

		private Random _randomObj = new Random(
			(int)DateTimeUtil.NowTicks());

		public RandomManager()
		{
		}

		public void SetSeed(int seed)
		{
			this._seed = seed;
			_randomObj = new Random(seed);
		}

		/// <summary>
		/// 返回0.0到1.0之间的一个double数
		/// </summary>
		public double RandomDouble()
		{
			var result = _randomObj.NextDouble();
			if (result >= 0.99999f)
				return 1;
			return result;
		}

		public double RandomDouble(double min, double max)
		{
			return min + RandomDouble() * (max - min);
		}

		/// <summary>
		/// 返回0.0到1.0之间的一个float数
		/// </summary>
		public float RandomFloat()
		{
			return (float)RandomDouble();
		}

		public float RandomFloat(float min, float max)
		{
			return (float)RandomDouble(min, max);
		}

		/// <summary>
		/// 返回一个非负的长整数
		/// </summary>
		public long RandomLong()
		{
			return _randomObj.RandomLong(DigitSign.Positive);
		}


		//
		public long RandomLong(long min, long max)
		{
			return min + (long)((max - min) * _randomObj.NextDouble());
		}

		/// <summary>
		/// 返回一个非负的整数
		/// </summary>
		public int RandomInt()
		{
			return _randomObj.Next();
		}

		public int RandomInt(int min, int max)
		{
			return _randomObj.Next(min, max);
		}

		public bool RandomBoolean()
		{
			return _randomObj.RandomBool();
		}

		/// <summary>
		/// 随机填充b
		/// </summary>
		/// <param name="b"></param>
		/// <returns></returns>
		public void RandomBytes(byte[] b)
		{
			_randomObj.NextBytes(b);
		}


		/// <summary>
		/// 随机dict里面的元素count次,dict里面有随机的元素和对应的权重
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="dict"></param>
		/// <param name="count">个数</param>
		/// <param name="isUnique">是否唯一</param>
		/// <returns></returns>
		public List<T> RandomList<T>(IDictionary<T, float> dict, int count, bool isUnique)
		{
			List<T> list = new List<T>(dict.Keys);
			List<float> weightList = new List<float>(dict.Values);
			return RandomList(list, count, isUnique, weightList);
		}

		public T[] RandomArray<T>(IDictionary<T, float> dict, int count, bool isUnique)
		{
			List<T> list = new List<T>(dict.Keys);
			List<float> weightList = new List<float>(dict.Values);
			return RandomArray(list, count, isUnique, weightList);
		}

		/// <summary>
		/// 返回随机长度的，由chars中字符组成的字符串
		/// </summary>
		/// <param name="len"></param>
		/// <param name="isUnique"></param>
		/// <returns></returns>
		public string RandomString(int len, bool isUnique)
		{
			return new string(RandomArray(CharConst.DigitsAndCharsAll, len, isUnique));
		}

		public T Random<T>(IList<T> list)
		{
			int randomIndex = RandomInt(0, list.Count + 1);
			return list[randomIndex];
		}

		/// <summary>
		/// 随机list里面的元素count次
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		/// <param name="count">个数</param>
		/// <param name="isUnique">是否唯一</param>
		/// <param name="weights">权重数组</param>
		/// <returns></returns>
		public List<T> RandomList<T>(IList<T> list, int count, bool isUnique, IList<float> weights = null)
		{
			List<T> result = new List<T>(count);
			int[] randomIndexes = RandomIndexArrayByWeight(count, isUnique, weights);
			foreach (int randomIndex in randomIndexes)
				result.Add(list[randomIndex]);
			return result;
		}

		public T[] RandomArray<T>(IList<T> list, int count, bool isUnique, IList<float> weights = null)
		{
			T[] result = new T[count];
			int[] randomIndexes = RandomIndexArrayByWeight(count, isUnique, weights);
			for (int i = 0; i < randomIndexes.Length; i++)
			{
				int randomIndex = randomIndexes[i];
				result[i] = list[randomIndex];
			}

			return result;
		}

		/// <summary>
		/// 根据权重来随机indexes
		/// </summary>
		/// <param name="count">个数</param>
		/// <param name="isUnique">是否唯一</param>
		/// <param name="weights">权重数组</param>
		/// <returns></returns>
		public int[] RandomIndexArrayByWeight(int count, bool isUnique, IList<float> weights)
		{
			int[] result = new int[count];
			if (!isUnique)
			{
				if (weights == null)
				{
					for (int i = 0; i < count; i++)
						result[i] = RandomInt(0, count + 1);
					return result;
				}

				for (int i = 0; i < count; i++)
					result[i] = RandomIndexByWeights(weights, null);
				return result;
			}

			if (weights == null)
			{
				weights = new float[count];
				for (int i = 0; i < count; i++)
					weights[i] = 1;
			}

			Dictionary<int, bool> exceptIndexDict = new Dictionary<int, bool>(count);
			for (int i = 0; i < count; i++)
			{
				int randomIndex = RandomIndexByWeights(weights, exceptIndexDict);
				result.Add(randomIndex);
				exceptIndexDict[randomIndex] = true;
			}

			return result;
		}

		public List<int> RandomIndexListByWeight(int count, bool isUnique, IList<float> weights)
		{
			List<int> result = new List<int>(count);
			if (!isUnique)
			{
				if (weights == null)
				{
					for (int i = 0; i < count; i++)
						result.Add(RandomInt(0, count + 1));
					return result;
				}

				for (int i = 0; i < count; i++)
					result.Add(RandomIndexByWeights(weights, null));
				return result;
			}

			if (weights == null)
			{
				weights = new float[count];
				for (int i = 0; i < count; i++)
					weights[i] = 1;
			}

			Dictionary<int, bool> exceptIndexDict = new Dictionary<int, bool>(count);
			for (int i = 0; i < count; i++)
			{
				int randomIndex = RandomIndexByWeights(weights, exceptIndexDict);
				result.Add(randomIndex);
				exceptIndexDict[randomIndex] = true;
			}

			return result;
		}

		/// <summary>
		/// 根据权重来随机index
		/// </summary>
		/// <param name="weights">权重数组</param>
		/// <returns></returns>
		public int RandomIndexByWeights(IList<float> weights, Dictionary<int, bool> exceptIndexDict)
		{
			float total = 0;
			for (int i = 0; i < weights.Count; i++)
			{
				if (exceptIndexDict != null && exceptIndexDict.ContainsKey(i))
					continue;
				total += weights[i];
			}

			float randomValue = total * RandomFloat();
			float compare = 0;
			for (int i = 0; i < weights.Count; i++)
			{
				if (exceptIndexDict != null && exceptIndexDict.ContainsKey(i))
					continue;
				compare += weights[i];
				if (randomValue < compare)
					return i;
			}

			return -1;
		}
	}
}
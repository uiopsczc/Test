using System.Collections.Generic;
using Random = System.Random;

namespace CsCat
{
  //与Unity的Random保持一致
  public class RandomManager
  {
    private int seed;

    public Random random_obj = new Random(
      (int)DateTimeUtil.NowTicks());

    public RandomManager()
    {
    }

    public void SetSeed(int seed)
    {
      this.seed = seed;
      random_obj = new Random(seed);
    }

    /// <summary>
    /// 返回0.0到1.0之间的一个double数
    /// </summary>
    public double RandomDouble()
    {
      var result = random_obj.NextDouble();
      if (result >= 0.99999f)
        return 1;
      else
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
      return random_obj.RandomLong(DigitSign.Positive);
    }


    //
    public long RandomLong(long min, long max)
    {
      return min + (long)((max - min) * random_obj.NextDouble());
    }

    /// <summary>
    /// 返回一个非负的整数
    /// </summary>
    public int RandomInt()
    {
      return random_obj.Next();
    }

    public int RandomInt(int min, int max)
    {

      return random_obj.Next(min, max);
    }

    public bool RandomBoolean()
    {
      return random_obj.RandomBool();
    }

    /// <summary>
    /// 随机填充b
    /// </summary>
    /// <param name="b"></param>
    /// <returns></returns>
    public void RandomBytes(byte[] b)
    {
      random_obj.NextBytes(b);
    }


    /// <summary>
    /// 随机dict里面的元素count次,dict里面有随机的元素和对应的权重
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dict"></param>
    /// <param name="count">个数</param>
    /// <param name="is_unique">是否唯一</param>
    /// <returns></returns>
    public List<T> RandomList<T>(IDictionary<T, float> dict, int count, bool is_unique)
    {
      List<T> list = new List<T>(dict.Keys);
      List<float> weight_list = new List<float>(dict.Values);
      return RandomList(list, count, is_unique, weight_list);
    }

    /// <summary>
    /// 返回随机长度的，由chars中字符组成的字符串
    /// </summary>
    /// <param name="len"></param>
    /// <param name="is_unique"></param>
    /// <returns></returns>
    public string RandomString(int len, bool is_unique)
    {
      return new string(RandomList(ConstExtensions.GetDigitsAndCharsAll().ToList(), len, is_unique).ToArray());
    }

    /// <summary>
    /// 随机list里面的元素count次
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="count">个数</param>
    /// <param name="is_unique">是否唯一</param>
    /// <param name="weight_list">权重数组</param>
    /// <returns></returns>
    public List<T> RandomList<T>(IList<T> list, int count, bool is_unique, List<float> weight_list = null)
    {
      List<T> result = new List<T>();
      if (weight_list.IsNullOrEmpty())
      {
        weight_list = new List<float>();
        for (int i = 0; i < list.Count; i++)
          weight_list.Add(1);
      }

      List<int> random_index_list = RandomIndexesByWeight(count, is_unique, weight_list.ToArray());
      foreach (int random_index in random_index_list)
        result.Add(list[random_index]);
      return result;
    }

    /// <summary>
    /// 根据权重来随机indexes
    /// </summary>
    /// <param name="count">个数</param>
    /// <param name="is_unique">是否唯一</param>
    /// <param name="weights">权重数组</param>
    /// <returns></returns>
    public List<int> RandomIndexesByWeight(int count, bool is_unique, params float[] weights)
    {
      List<int> result = new List<int>();
      List<float> weight_list = new List<float>(weights);

      for (int i = 0; i < count; i++)
      {
        if (!is_unique)
          result.Add(RandomIndexByWeights(weights));
        else
        {
          int random_index = RandomIndexByWeights(weight_list.ToArray());
          result.Add(random_index);
          weight_list.Remove(random_index);
        }
      }

      return result;
    }

    /// <summary>
    /// 根据权重来随机index
    /// </summary>
    /// <param name="weights">权重数组</param>
    /// <returns></returns>
    public int RandomIndexByWeights(params float[] weights)
    {
      float total = 0;
      for (int i = 0; i < weights.Length; i++)
        total += weights[i];
      float randomValue = total * RandomFloat();
      float compare = 0;
      for (int i = 0; i < weights.Length; i++)
      {
        compare += weights[i];
        if (randomValue < compare)
          return i;
      }

      return -1;
    }

  }
}
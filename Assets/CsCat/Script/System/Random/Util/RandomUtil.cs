using System.Collections.Generic;

namespace CsCat
{
    public class RandomUtil
    {
        private static RandomManager randomManager = new RandomManager();

        public static void SetSeed(int seed)
        {
            randomManager.SetSeed(seed);
        }

        /// <summary>
        /// 返回0.0到1.0之间的一个double数
        /// </summary>
        public static double RandomDouble()
        {
            return randomManager.RandomDouble();
        }

        public static double RandomDouble(double min, double max)
        {
            return randomManager.RandomDouble(min, max);
        }

        /// <summary>
        /// 返回0.0到1.0之间的一个float数
        /// </summary>
        public static float RandomFloat()
        {
            return randomManager.RandomFloat();
        }

        public static float RandomFloat(float min, float max)
        {
            return randomManager.RandomFloat(min, max);
        }

        /// <summary>
        /// 返回一个非负的长整数
        /// </summary>
        public static long RandomLong()
        {
            return randomManager.RandomLong();
        }


        //
        public static long RandomLong(long min, long max)
        {
            return randomManager.RandomLong(min, max);
        }

        /// <summary>
        /// 返回一个非负的整数
        /// </summary>
        public static int RandomInt()
        {
            return randomManager.RandomInt();
        }

        public static int RandomInt(int min, int max)
        {
            return randomManager.RandomInt(min, max);
        }

        public static bool RandomBoolean()
        {
            return randomManager.RandomBoolean();
        }

        /// <summary>
        /// 随机填充b
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static void RandomBytes(byte[] b)
        {
            randomManager.RandomBytes(b);
        }


        /// <summary>
        /// 随机dict里面的元素count次,dict里面有随机的元素和对应的权重
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <param name="count">个数</param>
        /// <param name="isUnique">是否唯一</param>
        /// <returns></returns>
        public static List<T> RandomList<T>(IDictionary<T, float> dict, int count, bool isUnique)
        {
            return randomManager.RandomList(dict, count, isUnique);
        }

        public static T[] RandomArray<T>(IDictionary<T, float> dict, int count, bool isUnique)
        {
            return randomManager.RandomArray(dict, count, isUnique);
        }

        /// <summary>
        /// 返回随机长度的，由chars中字符组成的字符串
        /// </summary>
        /// <param name="len"></param>
        /// <param name="isUnique"></param>
        /// <returns></returns>
        public static string RandomString(int len, bool isUnique)
        {
            return randomManager.RandomString(len, isUnique);
        }

        public static T Random<T>(IList<T> list)
        {
            return randomManager.Random(list);
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
        public static List<T> RandomList<T>(IList<T> list, int count, bool isUnique, IList<float> weights = null)
        {
            return randomManager.RandomList(list, count, isUnique, weights);
        }

        public static T[] RandomArray<T>(IList<T> list, int count, bool isUnique, IList<float> weights = null)
        {
            return randomManager.RandomArray(list, count, isUnique, weights);
        }

        /// <summary>
        /// 根据权重来随机indexes
        /// </summary>
        /// <param name="count">个数</param>
        /// <param name="isUnique">是否唯一</param>
        /// <param name="weights">权重数组</param>
        /// <returns></returns>
        public static int[] RandomIndexArrayByWeight(int count, bool isUnique, IList<float> weights)
        {
            return randomManager.RandomIndexArrayByWeight(count, isUnique, weights);
        }

        public static List<int> RandomIndexListByWeight(int count, bool isUnique, IList<float> weights)
        {
            return randomManager.RandomIndexListByWeight(count, isUnique, weights);
        }

        /// <summary>
        /// 根据权重来随机index
        /// </summary>
        /// <param name="weights">权重数组</param>
        /// <returns></returns>
        public static int RandomIndexByWeights(IList<float> weights, Dictionary<int, bool> exceptIndexDict)
        {
            return randomManager.RandomIndexByWeights(weights, exceptIndexDict);
        }
    }
}
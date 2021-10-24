using System;
using System.Collections.Generic;

namespace CsCat
{
    /// <summary>
    ///   属性观察者工厂
    /// </summary>
    public class PropertyObserverFactory
    {
        #region field

        /// <summary>
        ///   所有的属性观察者
        /// </summary>
        private static readonly Dictionary<Type, Func<object, IPropertyObserver>> observerDict =
            new Dictionary<Type, Func<object, IPropertyObserver>>();

        #endregion

        #region static method

        /// <summary>
        ///   创建属性观察者
        /// </summary>
        /// <param name="propOwner"></param>
        /// <returns></returns>
        public static IPropertyObserver CreatePropertyObserver(object propOwner)
        {
            if (propOwner == null) return null;
            foreach (var current in observerDict)
                if (current.Key.IsInstanceOfType(propOwner))
                    return current.Value(propOwner);
            return null;
        }

        /// <summary>
        ///   对type注册属性观察者PropertyObserver
        /// </summary>
        /// <param name="type"></param>
        /// <param name="creator"></param>
        public static void RegistPropertyObserver(Type type, Func<object, IPropertyObserver> creator)
        {
            observerDict[type] = creator;
        }

        /// <summary>
        ///   移除对type注册属性观察者PropertyObserver
        /// </summary>
        /// <param name="type"></param>
        public static void UnregistPropertyObserver(Type type)
        {
            if (observerDict.ContainsKey(type)) observerDict[type] = null;
        }

        #endregion
    }
}
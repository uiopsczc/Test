using System;

namespace CsCat
{
    /// <summary>
    ///   绑定属性观察者
    /// </summary>
    public class BindPropObserver : AbstractPropertyObserver
    {
        #region ctor

        public BindPropObserver(object propOwner) : base(propOwner)
        {
        }

        #endregion

        #region override method

        /// <summary>
        ///   添加propertyName属性更改listener
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="listener"></param>
        public override void AddPropertyChangedListener(string propertyName, Action<string, object, object> listener)
        {
            ((PropObserver) propOwner).AddPropListener(propertyName, listener);
        }

        /// <summary>
        ///   移除propertyName属性的listener
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="listener"></param>
        public override void RemovePropertyChangedListener(string propertyName,
            Action<string, object, object> listener)
        {
            ((PropObserver) propOwner).RemovePropListener(propertyName, listener);
        }

        #endregion
    }
}
namespace CsCat
{
    public class MethodToRestoreWithoutArgs : MethodToRestoreBase
    {
        #region ctor

        /// <summary>
        ///   ctor
        /// </summary>
        /// <param name="cause">引起还原的对应的名称</param>
        /// <param name="owner">需要还原的方法</param>
        /// <param name="methodNameToRestore">需要还原的方法名</param>
        public MethodToRestoreWithoutArgs(object cause, object owner, string methodNameToRestore) : base(cause,
            owner,
            methodNameToRestore)
        {
            var type = owner.GetType();
            methodInfoToRestore = type.GetMethodInfo2(methodNameToRestore);
        }

        #endregion

        #region public method

        /// <summary>
        ///   进行还原
        /// </summary>
        public override void Restore()
        {
            methodInfoToRestore.Invoke(toRestoreBase.owner, null);
        }

        #endregion
    }
}
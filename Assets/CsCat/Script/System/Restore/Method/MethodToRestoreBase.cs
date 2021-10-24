using System.Reflection;

namespace CsCat
{
    public abstract class MethodToRestoreBase : MemberToRestoreBase
    {
        #region field

        protected MethodInfo methodInfoToRestore;

        #endregion


        #region ctor

        /// <summary>
        ///   ctor
        /// </summary>
        /// <param name="cause">引起还原的对应的名称</param>
        /// <param name="owner">需要还原的方法</param>
        /// <param name="methodNameToRestore">需要还原的方法名</param>
        public MethodToRestoreBase(object cause, object owner, string nameToRestore) : base(cause, owner,
            nameToRestore)
        {
            toRestoreBase = new ToRestoreBase(cause, owner, nameToRestore);
        }

        #endregion
    }
}
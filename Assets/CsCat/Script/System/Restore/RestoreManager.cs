using System.Collections.Generic;


namespace CsCat
{
	/// <summary>
	/// 请使用 MemberToRestoreProxy  插入需要还原的数据
	/// </summary>
	public class RestoreManager : ISingleton
	{
		/// <summary>
		/// 所有的需要还原的属性列表
		/// </summary>
		List<IRestore> _restoreList = new List<IRestore>();

		/// <summary>
		/// 里面的元素用于还原后从restoreList中删除
		/// </summary>
		List<IRestore> _toRemoveList = new List<IRestore>();


		public static RestoreManager instance => SingletonFactory.instance.Get<RestoreManager>();


		public void SingleInit()
		{
		}

		/// <summary>
		/// 添加需要还原的restore
		/// 不会重复添加
		/// </summary>
		/// <param name="restore"></param>
		public void Add(IRestore restore)
		{
			if (_restoreList.Contains(restore))
				return;
			_restoreList.Add(restore);
		}

		/// <summary>
		/// 进行还原
		/// </summary>
		/// <param name="source"></param>
		public void Restore(object source)
		{
			_toRemoveList.Clear();
			for (int i = 0; i < _restoreList.Count; i++)
			{
				var element = _restoreList[i];
				if (element.Equals(source))
				{
					element.Restore();
					_toRemoveList.Add(element);
				}
			}

			for (int i = 0; i < _toRemoveList.Count; i++)
			{
				var element = _toRemoveList[i];
				_restoreList.Remove(element);
			}
			_toRemoveList.Clear();
		}
	}
}
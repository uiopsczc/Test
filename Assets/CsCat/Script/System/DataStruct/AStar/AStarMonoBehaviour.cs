using UnityEngine;

namespace CsCat
{
	[ExecuteInEditMode]
	public partial class AStarMonoBehaviour : MonoBehaviour
	{
		[SerializeField] public AStarConfigData astarConfigData = new AStarConfigData();

		private void Awake()
		{
		}

		public void Save()
		{
			astarConfigData.SetTransform(transform);
			astarConfigData.Save();
		}

		public void Load()
		{
			astarConfigData.SetTransform(transform);
			astarConfigData.Load();
			astarConfigData.ResetTransformInfo();
		}
	}
}
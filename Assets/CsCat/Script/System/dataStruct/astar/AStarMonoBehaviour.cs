using UnityEngine;

namespace CsCat
{
  [ExecuteInEditMode]
  public partial class AStarMonoBehaviour : MonoBehaviour
  {
    [SerializeField] public AStarData astarData = new AStarData();

    private void Awake()
    {
    }

    public void Save()
    {
      astarData.SetTransform(transform);
      astarData.Save();
    }

    public void Load()
    {
      astarData.SetTransform(transform);
      astarData.Load();
      astarData.ResetTransformInfo();
    }
  }
}

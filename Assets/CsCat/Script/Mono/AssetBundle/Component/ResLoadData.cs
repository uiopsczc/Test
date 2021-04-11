using System;

namespace CsCat
{
  public class ResLoadData
  {
    public AssetCat assetCat;
    public int ref_count;

    public ResLoadData(AssetCat assetCat)
    {
      this.assetCat = assetCat;
      assetCat.AddRefCount();
    }

    public void AddRefCount()
    {
      ref_count++;
    }

    public void SubRefCount(int sub_value = 1)
    {
      sub_value = Math.Abs(sub_value);
      ref_count = ref_count - sub_value;
    }

    public void Destroy()
    {
      ref_count = 0;
      assetCat.SubRefCount(1, true);
    }
  }
}
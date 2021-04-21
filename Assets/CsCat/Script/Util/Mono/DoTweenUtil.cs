using DG.Tweening;
using System.Collections.Generic;

namespace CsCat
{
  public class DOTweenUtil
  {

    public static DOTweenId GetDOTweenId(object source = null, string prefix = ConstUtil.DOTweenId_Use_GameTime)
    {
      return new DOTweenId(source, prefix);
    }

    public static List<Tween> GetDOTweens(object source = null, string prefix = ConstUtil.DOTweenId_Use_GameTime)
    {
      List<Tween> tweens = new List<Tween>();
      if (DOTween.PlayingTweens() != null)
        foreach (Tween t in DOTween.PlayingTweens())
        {
          if (source == null)
          {
            if (t.id is DOTweenId && ((DOTweenId)t.id).prefix == prefix)
              tweens.Add(t);

          }
          else
          {
            if (t.id is DOTweenId && t.id.Equals(new DOTweenId(source, prefix)))
              tweens.Add(t);
          }

          if (t.id is string && (string)t.id == prefix)
            tweens.Add(t);


        }

      return tweens;
    }
  }
}

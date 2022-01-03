using DG.Tweening;
using System.Collections.Generic;

namespace CsCat
{
	public class DOTweenUtil
	{
		public static DOTweenId GetDOTweenId(object source = null,
			string prefix = StringConst.String_DOTweenId_Use_GameTime)
		{
			return new DOTweenId(source, prefix);
		}

		public static List<Tween> GetDOTweens(object source = null,
			string prefix = StringConst.String_DOTweenId_Use_GameTime)
		{
			List<Tween> tweenList = new List<Tween>();
			if (DOTween.PlayingTweens() == null) return tweenList;
			foreach (var tween in DOTween.PlayingTweens())
			{
				if (source == null)
				{
					if (tween.id is DOTweenId id && id.prefix == prefix)
						tweenList.Add(tween);
				}
				else
				{
					if (tween.id is DOTweenId && tween.id.Equals(new DOTweenId(source, prefix)))
						tweenList.Add(tween);
				}

				if (tween.id is string s && s.Equals(prefix))
					tweenList.Add(tween);
			}

			return tweenList;
		}
	}
}
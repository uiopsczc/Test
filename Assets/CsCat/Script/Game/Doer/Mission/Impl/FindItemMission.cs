using System.Collections.Generic;
using System.Text;

namespace CsCat
{
	public class FindItemMission : Mission
	{
		public override bool OnAccept(User user)
		{
			if (!base.OnAccept(user))
				return false;
			DoerAttrParser doerAttrParser = new DoerAttrParser(Client.instance.user, this, this.GetOwner(), null);
			Dictionary<string, string> findItemDict = GetCfgMissionData().findItemDict;
			Dictionary<string, int> curFindItemDict = new Dictionary<string, int>();
			foreach (var keyValue in findItemDict)
			{
				var itemId = keyValue.Key;
				string itemCountString = findItemDict[itemId];
				int itemCount = doerAttrParser.ParseInt(itemCountString, 0);
				curFindItemDict[itemId] = itemCount;
			}
			this.Set("findItemDict", curFindItemDict);
			return true;
		}

		public override bool IsReady()
		{
			if (base.IsReady())
				return true;
			Dictionary<string, int> findItemDict = this.Get<Dictionary<string, int>>("findItemDict");
			foreach (var keyValue in findItemDict)
			{
				var itemId = keyValue.Key;
				var user = this.GetOwner() as User;
				if (user.GetItemCount(itemId) < findItemDict[itemId])
					return false;
			}
			return this.Get<bool>("isReady");
		}

		public override string GetStatusString()
		{
			var user = this.GetOwner() as User;
			Dictionary<string, int> findItemDict = this.Get<Dictionary<string, int>>("findItemDict");
			bool isFinished = true;
			StringBuilder stringBuilder = new StringBuilder();
			foreach (var keyValue in findItemDict)
			{
				var itemId = keyValue.Key;
				string itemName = CfgItem.Instance.GetById(itemId).name;
				int curCount = user.GetItemCount(itemId);
				int needCount = findItemDict[itemId];
				if (curCount > needCount)
					curCount = needCount;
				if (curCount < needCount)
					isFinished = false;
				stringBuilder.Append(string.Format("{0}:{1}/{2}\n", itemName, curCount, needCount)); // {name}: {cur}/{total};
			}

			if (isFinished || this.CheckFinishCondition())
				stringBuilder.Append(Lang.GetText("已完成") + "\n");
			string result = stringBuilder.ToString();
			return result.Substring(0, result.Length - 1); //去掉最后一个\n
		}
	}
}
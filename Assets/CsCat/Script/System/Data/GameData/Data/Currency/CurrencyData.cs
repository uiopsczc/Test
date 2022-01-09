using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	// 货币
	public class CurrencyData : PropObserver
	{
		[Serialize] public Dictionary<string, int> currencyDict = new Dictionary<string, int>();
		private float powerAddRemainDuration;
		private float powerLastUpdateTime;

		public override void OnNewCreate()
		{
			AddPower(CurrencyConst.Max_Power_Count);
		}

		public override void OnLoaded()
		{
			//在这里面的只能使用GameData._instance，不能使用GameData.instance，否则会造成死循环
			OnPowerLoaded();
		}

		private void OnPowerLoaded()
		{
			var currentPowerCount = GetPowerCount();
			if (GameData._instance.quitTimeTicks > 0 && currentPowerCount < CurrencyConst.Max_Power_Count)
			{
				var diffSeconds = (int)(DateTimeUtil.NowDateTime() - new DateTime(GameData._instance.quitTimeTicks))
				  .TotalSeconds;
				var addPowerCount = diffSeconds / CurrencyConst.Add_Power_Period * CurrencyConst.Add_Power_Count;
				AddPower(addPowerCount, true);
				powerAddRemainDuration = diffSeconds % CurrencyConst.Add_Power_Period;
			}
			else
			{
				powerAddRemainDuration = CurrencyConst.Add_Power_Period;
			}


			powerLastUpdateTime = Time.realtimeSinceStartup;
			Client.instance.timerManager.AddTimer(args =>
			{
				currentPowerCount = GetPowerCount();
				var currentTime = Time.realtimeSinceStartup;
				var deltaTime = currentTime - powerLastUpdateTime;
				powerLastUpdateTime = currentTime;
				if (currentPowerCount >= CurrencyConst.Max_Power_Count)
				{
					powerAddRemainDuration = CurrencyConst.Add_Power_Period;
					return true;
				}

				powerAddRemainDuration = powerAddRemainDuration - deltaTime;
				while (powerAddRemainDuration <= 0)
				{
					powerAddRemainDuration = powerAddRemainDuration + CurrencyConst.Add_Power_Period;
					if (currentPowerCount >= CurrencyConst.Max_Power_Count)
						break;
					var addPowerCount = AddPower(CurrencyConst.Add_Power_Count, true);
					currentPowerCount = currentPowerCount + addPowerCount;
				}

				return true;
			}, 1, 1);
		}

		public bool CanCost(string currencyId, int costCount)
		{
			if (costCount < 0)
				costCount = -costCount;
			if (!currencyDict.ContainsKey(currencyId))
				return false;
			if (currencyDict[currencyId] < costCount)
				return false;
			return true;
		}

		public void Add(string currencyId, int addCount)
		{
			var currentCount = currencyDict.GetOrGetDefault(currencyId, () => 0);
			currentCount = currentCount + addCount;
			currencyDict[currencyId] = currentCount;
		}

		public bool TryCost(string currencyId, int costCount)
		{
			if (costCount < 0)
				costCount = -costCount;
			if (!CanCost(currencyId, costCount))
				return false;
			Add(currencyId, -costCount);
			return true;
		}

		public int GetCount(string currencyId)
		{
			return currencyDict.GetOrGetDefault(currencyId, () => 0);
		}

		//金币
		public bool CanCostCoin(int costCount)
		{
			return CanCost(CurrencyConst.Coin_Item_Id, costCount);
		}

		public void AddCoin(int addCount)
		{
			Add(CurrencyConst.Coin_Item_Id, addCount);
		}

		public bool TryCostCoin(int costCount)
		{
			return TryCost(CurrencyConst.Coin_Item_Id, costCount);
		}

		public int GetCoinCount()
		{
			return GetCount(CurrencyConst.Coin_Item_Id);
		}

		//钻石
		public bool CanCostDiamond(int costCount)
		{
			return CanCost(CurrencyConst.Diamond_Item_Id, costCount);
		}

		public void AddDiamond(int addCount)
		{
			Add(CurrencyConst.Diamond_Item_Id, addCount);
		}

		public bool TryCostDiamond(int costCount)
		{
			return TryCost(CurrencyConst.Diamond_Item_Id, costCount);
		}

		public int GetDiamondCount()
		{
			return GetCount(CurrencyConst.Diamond_Item_Id);
		}

		//体力
		public bool CanCostPower(int costCount)
		{
			return CanCost(CurrencyConst.Power_Item_Id, costCount);
		}

		public int AddPower(int addCount, bool isLimitMaxPower = false)
		{
			var currentPowerCount = GetPowerCount();
			if (isLimitMaxPower)
				if (currentPowerCount + addCount > CurrencyConst.Max_Power_Count)
					addCount = CurrencyConst.Max_Power_Count - currentPowerCount;

			if ((currentPowerCount >= CurrencyConst.Max_Power_Count &&
				 currentPowerCount + addCount < CurrencyConst.Max_Power_Count) // 本来》=Power的最大值，加完后比Power的最大值小，则设置倒计时
				||
				(currentPowerCount < CurrencyConst.Max_Power_Count &&
				 currentPowerCount + addCount >= CurrencyConst.Max_Power_Count)) //本来<Power的最大值，加完后比Power的最大值大，则取消倒计时
			{
				powerAddRemainDuration = CurrencyConst.Add_Power_Period;
				powerLastUpdateTime = Time.realtimeSinceStartup;
			}

			Add(CurrencyConst.Power_Item_Id, addCount);
			return addCount;
		}

		public bool TryCostPower(int costCount)
		{
			return TryCost(CurrencyConst.Power_Item_Id, costCount);
		}

		public int GetPowerCount()
		{
			return GetCount(CurrencyConst.Power_Item_Id);
		}
	}
}
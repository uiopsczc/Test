using System;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	// 货币
	public class CurrencyData : PropObserver
	{
		[Serialize] public Dictionary<string, int> currency_dict = new Dictionary<string, int>();
		private float power_add_remain_duration;
		private float power_last_update_time;

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
			var current_power_count = GetPowerCount();
			if (GameData._instance.quit_time_ticks > 0 && current_power_count < CurrencyConst.Max_Power_Count)
			{
				var diff_seconds = (int)(DateTimeUtil.NowDateTime() - new DateTime(GameData._instance.quit_time_ticks))
				  .TotalSeconds;
				var add_power_count = diff_seconds / CurrencyConst.Add_Power_Period * CurrencyConst.Add_Power_Count;
				AddPower(add_power_count, true);
				power_add_remain_duration = diff_seconds % CurrencyConst.Add_Power_Period;
			}
			else
			{
				power_add_remain_duration = CurrencyConst.Add_Power_Period;
			}


			power_last_update_time = Time.realtimeSinceStartup;
			Client.instance.timerManager.AddTimer(args =>
			{
				current_power_count = GetPowerCount();
				var current_time = Time.realtimeSinceStartup;
				var deltaTime = current_time - power_last_update_time;
				power_last_update_time = current_time;
				if (current_power_count >= CurrencyConst.Max_Power_Count)
				{
					power_add_remain_duration = CurrencyConst.Add_Power_Period;
					return true;
				}

				power_add_remain_duration = power_add_remain_duration - deltaTime;
				while (power_add_remain_duration <= 0)
				{
					power_add_remain_duration = power_add_remain_duration + CurrencyConst.Add_Power_Period;
					if (current_power_count >= CurrencyConst.Max_Power_Count)
						break;
					var add_power_count = AddPower(CurrencyConst.Add_Power_Count, true);
					current_power_count = current_power_count + add_power_count;
				}

				return true;
			}, 1, 1);
		}

		public bool CanCost(string currency_id, int cost_count)
		{
			if (cost_count < 0)
				cost_count = -cost_count;
			if (!currency_dict.ContainsKey(currency_id))
				return false;
			if (currency_dict[currency_id] < cost_count)
				return false;
			return true;
		}

		public void Add(string currency_id, int add_count)
		{
			var current_count = currency_dict.GetOrGetDefault(currency_id, () => 0);
			current_count = current_count + add_count;
			currency_dict[currency_id] = current_count;
		}

		public bool TryCost(string currency_id, int cost_count)
		{
			if (cost_count < 0)
				cost_count = -cost_count;
			if (!CanCost(currency_id, cost_count))
				return false;
			Add(currency_id, -cost_count);
			return true;
		}

		public int GetCount(string currency_id)
		{
			return currency_dict.GetOrGetDefault(currency_id, () => 0);
		}

		//金币
		public bool CanCostCoin(int cost_count)
		{
			return CanCost(CurrencyConst.Coin_Item_Id, cost_count);
		}

		public void AddCoin(int add_count)
		{
			Add(CurrencyConst.Coin_Item_Id, add_count);
		}

		public bool TryCostCoin(int cost_count)
		{
			return TryCost(CurrencyConst.Coin_Item_Id, cost_count);
		}

		public int GetCoinCount()
		{
			return GetCount(CurrencyConst.Coin_Item_Id);
		}

		//钻石
		public bool CanCostDiamond(int cost_count)
		{
			return CanCost(CurrencyConst.Diamond_Item_Id, cost_count);
		}

		public void AddDiamond(int add_count)
		{
			Add(CurrencyConst.Diamond_Item_Id, add_count);
		}

		public bool TryCostDiamond(int cost_count)
		{
			return TryCost(CurrencyConst.Diamond_Item_Id, cost_count);
		}

		public int GetDiamondCount()
		{
			return GetCount(CurrencyConst.Diamond_Item_Id);
		}

		//体力
		public bool CanCostPower(int cost_count)
		{
			return CanCost(CurrencyConst.Power_Item_Id, cost_count);
		}

		public int AddPower(int add_count, bool is_limit_max_power = false)
		{
			var current_power_count = GetPowerCount();
			if (is_limit_max_power)
				if (current_power_count + add_count > CurrencyConst.Max_Power_Count)
					add_count = CurrencyConst.Max_Power_Count - current_power_count;

			if ((current_power_count >= CurrencyConst.Max_Power_Count &&
				 current_power_count + add_count < CurrencyConst.Max_Power_Count) // 本来》=Power的最大值，加完后比Power的最大值小，则设置倒计时
				||
				(current_power_count < CurrencyConst.Max_Power_Count &&
				 current_power_count + add_count >= CurrencyConst.Max_Power_Count)) //本来<Power的最大值，加完后比Power的最大值大，则取消倒计时
			{
				power_add_remain_duration = CurrencyConst.Add_Power_Period;
				power_last_update_time = Time.realtimeSinceStartup;
			}

			Add(CurrencyConst.Power_Item_Id, add_count);
			return add_count;
		}

		public bool TryCostPower(int cost_count)
		{
			return TryCost(CurrencyConst.Power_Item_Id, cost_count);
		}

		public int GetPowerCount()
		{
			return GetCount(CurrencyConst.Power_Item_Id);
		}
	}
}
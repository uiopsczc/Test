// 逻辑对象

using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
	public class Doer : TickObject
	{
		private DBase dbase;
		public DoerFactory factory;

		public override void PostInit()
		{
			this.OnInit();
			base.PostInit();
		}


		////////////////////////////DoXXX//////////////////////////////
		public virtual void Destruct()
		{
			factory?.ReleaseDoer(this);
		}

		public virtual void PrepareSave(Hashtable dict, Hashtable dictTmp)
		{
			this.DoSave(dict, dictTmp);
		}

		public virtual void FinishRestore(Hashtable dict, Hashtable dictTmp)
		{
			this.DoRestore(dict, dictTmp);
		}

		public virtual bool CheckUpgrade(string key)
		{
			return OnCheckUpgrade(key);
		}

		public virtual void Upgrade(Critter critter)
		{
		}

		//////////////////////////////OnXXX////////////////////////
		public virtual void OnInit()
		{
		}

		public void OnRelease()
		{
		}

		//存储数据事件
		public virtual void OnSave(Hashtable dict, Hashtable dictTmp)
		{
		}

		//导出数据事件
		public virtual void OnRestore(Hashtable dict, Hashtable dictTmp)
		{
		}

		//修改属性事件
		public void OnAttrChange(string key)
		{
		}

		//重新载入定义数据事件
		public void OnReloadCfg()
		{
		}

		public virtual bool OnCheckUpgrade(string key)
		{
			return true;
		}

		public virtual void OnUpgrade(string key)
		{
		}

		//////////////////////////////DoXXX////////////////////////
		public virtual void DoSave(Hashtable dict, Hashtable dictTmp)
		{
			this.GetSaveData(dict);
			this.GetSaveTmpData(dictTmp);
			this.OnSave(dict, dictTmp);
		}

		public virtual void DoRestore(Hashtable dict, Hashtable dictTmp)
		{
			this.OnRestore(dict, dictTmp);
			if (dictTmp != null)
				this.AddTmpAll(dictTmp);
			if (dict != null)
				this.AddAll(dict);
		}

		public void NotifyAttrChange(string key)
		{
			this.OnAttrChange(key);
		}

		public virtual void DoRelease()
		{
			this.OnRelease();
		}


		//////////////////////////////GetXXX////////////////////////
		public string GetId()
		{
			return this.dbase.GetId();
		}

		public string GetRid()
		{
			return this.dbase.GetRid();
		}

		public string GetRidSeq()
		{
			return this.dbase.GetRidSeq();
		}

		public DBase GetDBase()
		{
			return this.dbase;
		}

		public T GetDBase<T>() where T : DBase
		{
			return GetDBase() as T;
		}


		public string GetShort()
		{
			return string.Format("{0}", this.GetRid());
		}

		public override string ToString()
		{
			return this.GetShort();
		}

		// 获得需存储数据
		public void GetSaveData(Hashtable dict)
		{
			if (dict == null)
				return;
			this.GetAll(dict);
		}

		// 拷贝数据到指定dict
		public void GetAll(Hashtable dict)
		{
			dict.Combine(this.dbase.db);
		}

		// 获得需存储运行时数据
		public void GetSaveTmpData(Hashtable dict)
		{
			if (dict == null)
				return;
			this.GetTmpAll(dict);
			dict.RemoveByFunc((key, value) => key.ToString().StartsWith(StringConst.String_o_));
		}

		public T Get<T>(string key, T defaultValue = default)
		{
			if (!this.dbase.db.ContainsKey(key))
				return defaultValue;
			return this.dbase.db[key].To<T>();
		}

		// 拷贝运行时数据到指定dict
		public void GetTmpAll(Hashtable dict)
		{
			dict.Combine(this.dbase.dbTmp);
		}

		public T GetTmp<T>(string key, T defaultValue = default)
		{
			if (!this.dbase.dbTmp.ContainsKey(key))
				return defaultValue;
			return this.dbase.dbTmp[key].To<T>();
		}


		public int GetCount()
		{
			return this.Get<int>(StringConst.String_count);
		}


		// 获得物件所在环境
		public Doer GetEnv()
		{
			return GetTmp<Doer>(StringConst.String_o_env, null);
		}

		public T GetEnv<T>() where T : class
		{
			return GetEnv() as T;
		}

		// 拥有者，如发放任务的npc
		public Doer GetOwner()
		{
			return GetTmp<Doer>(StringConst.String_o_owner);
		}

		public Vector2 GetPos2()
		{
			var pos = Get<Hashtable>(StringConst.String_pos2);
			if (pos == null)
				return Vector2Const.Default;
			return new Vector2(pos.Get<float>(StringConst.String_x), pos.Get<float>(StringConst.String_z));
		}

		public Vector3 GetPos3()
		{
			var pos = Get<Hashtable>(StringConst.String_pos3);
			if (pos == null)
				return Vector3Const.Default;
			return new Vector3(pos.Get<float>(StringConst.String_x), pos.Get<float>(StringConst.String_y),
				pos.Get<float>(StringConst.String_z));
		}

		public string GetBelong()
		{
			return GetTmp(StringConst.String_belong, StringConst.String_Empty);
		}
		//////////////////////////////SetXXXX////////////////////////

		public void SetDBase(DBase dbase)
		{
			this.dbase = dbase;
		}

		public void Set(string key, object value)
		{
			this.dbase.db[key] = value;
		}

		public void SetTmp(string key, object value)
		{
			this.dbase.dbTmp[key] = value;
		}

		public void SetCount(int value)
		{
			Set(StringConst.String_count, value);
		}

		public void SetEnv(object env)
		{
			SetTmp(StringConst.String_o_env, env);
		}

		//拥有者，如发放任务的npc
		public void SetOwner(Doer owner)
		{
			SetTmp(StringConst.String_o_owner, owner);
		}

		public void SetPos2(float x, float y)
		{
			var pos = new Hashtable { [StringConst.String_x] = x, [StringConst.String_y] = y };
			Set(StringConst.String_pos2, pos);
		}

		public void SetPos3(float x, float y, float z)
		{
			var pos = new Hashtable
			{
				[StringConst.String_x] = x,
				[StringConst.String_y] = y,
				[StringConst.String_z] = z
			};
			Set(StringConst.String_pos3, pos);
		}

		public void SetBelong(string belong)
		{
			SetTmp(StringConst.String_belong, belong);
		}
		//////////////////////////////AddXXXX////////////////////////

		public void AddAll(Hashtable dict)
		{
			this.dbase.db.Combine(dict);
		}

		public void Add(string key, float addValue)
		{
			if (!this.dbase.db.ContainsKey(key))
				this.dbase.db[key] = 0;
			this.dbase.db[key] = this.dbase.db[key].To<float>() + addValue;
		}

		public void Add(string key, int addValue)
		{
			if (!this.dbase.db.ContainsKey(key))
				this.dbase.db[key] = 0;
			this.dbase.db[key] = this.dbase.db[key].To<int>() + addValue;
		}

		public void Add(string key, string addValue)
		{
			if (!this.dbase.db.ContainsKey(key))
				this.dbase.db[key] = StringConst.String_Empty;
			this.dbase.db[key] = this.dbase.db[key].To<string>() + addValue;
		}

		public void AddCount(int addValue)
		{
			this.Add(StringConst.String_count, addValue);
		}

		public void AddTmp(string key, float addValue)
		{
			if (!this.dbase.dbTmp.ContainsKey(key))
				this.dbase.dbTmp[key] = 0;
			this.dbase.dbTmp[key] = this.dbase.dbTmp[key].To<float>() + addValue;
		}

		public void AddTmp(string key, string addValue)
		{
			if (!this.dbase.dbTmp.ContainsKey(key))
				this.dbase.dbTmp[key] = StringConst.String_Empty;
			this.dbase.dbTmp[key] = this.dbase.dbTmp[key].To<string>() + addValue;
		}

		public void AddTmpAll(Hashtable dict)
		{
			this.dbase.dbTmp.Combine(dict);
		}

		/////////////////////////////Remove/////////////////////////////
		public T Remove<T>(string key)
		{
			T result = default;
			if (dbase.db.ContainsKey(key))
			{
				result = (T)dbase.db[key];
				dbase.db.Remove(key);
			}

			return result;
		}

		public T RemoveTmp<T>(string key)
		{
			T result = default;
			if (dbase.dbTmp.ContainsKey(key))
			{
				result = (T)dbase.dbTmp[key];
				dbase.dbTmp.Remove(key);
			}

			return result;
		}

		/////////////////////////////GetOrAddXXXX/////////////////////////////
		public T GetOrAddTmp<T>(string key, Func<T> addFunc)
		{
			return this.dbase.dbTmp.GetOrAddDefault2(key, addFunc);
		}

		public T GetOrAdd<T>(string key, Func<T> addFunc)
		{
			return this.dbase.db.GetOrAddDefault2(key, addFunc);
		}
	}
}
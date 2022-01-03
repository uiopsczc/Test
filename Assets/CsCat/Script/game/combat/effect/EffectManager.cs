namespace CsCat
{
	public partial class EffectManager : TickObject
	{

		public override void Init()
		{
			base.Init();
			this.resLoadComponent.resLoad.is_not_check_destroy = true;//effectManager销毁的时候才去把assetBundle销毁
			this.AddListener<Unit>(null, UnitEventNameConst.On_Unit_Destroy, this.DestroyByUnit);
		}





		public void RemoveEffectEntity(string key)
		{
			RemoveChild(key);
		}

		public EffectEntity GetEffectEntity(string key)
		{
			return GetChild(key) as EffectEntity;
		}



		void DestroyByUnit(Unit unit)
		{
			foreach (var effectEntity in ForeachChild<EffectEntity>())
			{
				if (effectEntity.unit == unit)
					this.RemoveEffectEntity(effectEntity.key);
			}
		}

		protected override void _Destroy()
		{
			base._Destroy();
			foreach (var pool_name in gameObject_pool_name_list)
			{
				PoolCatManagerUtil.RemovePool(pool_name);
			}
			gameObject_pool_name_list.Clear();
		}
	}
}
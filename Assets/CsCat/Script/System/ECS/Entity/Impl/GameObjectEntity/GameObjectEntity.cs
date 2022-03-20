using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class GameObjectEntity : GameEntity
	{
		public TransformComponent transformComponent;
		public ResLoadComponent resLoadComponent;
		public GraphicComponent graphicComponent;
		public bool isAllAssetsLoadDone;
		public Action allAssetsLoadDoneCallback;

		public override void Init()
		{
			base.Init();
			resLoadComponent = this.AddComponent<ResLoadComponent>(null, new ResLoad());
			transformComponent = this.AddComponent<TransformComponent>(null);
			graphicComponent = CreateGraphicComponent();
		}

		protected virtual GraphicComponent CreateGraphicComponent()
		{
			return this.AddComponent<GraphicComponent>(null, this.resLoadComponent);
		}

		public void ApplyTransformComponent(TransformComponent transformComponent = null)
		{
			transformComponent = transformComponent ?? this.transformComponent;
			this.GetGraphicComponent().ApplyTransformComponent(transformComponent);
		}


		public override void PostInit()
		{
			base.PostInit();
			graphicComponent.LoadPrefabPath();
			PreLoadAssets();
			CheckIsAllAssetsLoadDone();
		}

		public void InvokeAfterAllAssetsLoadDone(Action callback)
		{
			if (isAllAssetsLoadDone)
				callback();
			else
				allAssetsLoadDoneCallback += callback;
		}

		// 通过resLoadComponent操作reload的东西
		public virtual void PreLoadAssets()
		{
			//resLoadComponent.LoadAssetAsync("resPath");
		}

		protected void CheckIsAllAssetsLoadDone()
		{
			this.StartCoroutine(IECheckIsAllAssetsLoadDone());
		}

		protected virtual IEnumerator IECheckIsAllAssetsLoadDone()
		{
			yield return this.resLoadComponent.IEIsAllLoadDone();
			if (!graphicComponent.prefabPath.IsNullOrEmpty())
				yield return new WaitUntil(() => graphicComponent.IsLoadDone());
			OnAllAssetsLoadDone();
		}

		public virtual void OnAllAssetsLoadDone()
		{
			this.Broadcast(this.eventDispatchers, ECSEventNameConst.OnAllAssetsLoadDone);
			isAllAssetsLoadDone = true;
			allAssetsLoadDoneCallback?.Invoke();
			allAssetsLoadDoneCallback = null;
		}

		public GameObject InstantiateGameObject(GameObject prefab)
		{
			return GameObject.Instantiate(prefab);
		}

		public void InitGameObjectChildren()
		{
		}

		public void SetParentTransform(Transform parentTransform)
		{
			this.transformComponent.SetParentTransform(parentTransform);
			this.graphicComponent.SetParentTransform(parentTransform);
		}

		public Transform GetParentTransform()
		{
			return this.transformComponent.GetParentTransform();
		}


		public void SetIsShow(bool isShow)
		{
			this.transformComponent.SetIsShow(isShow);
			this.graphicComponent.SetIsShow(isShow);
		}

		public bool IsShow()
		{
			return this.transformComponent.IsShow();
		}

		protected  override void _Reset()
		{
			base._Reset();
			this.isAllAssetsLoadDone = false;
			allAssetsLoadDoneCallback = null;
		}
	}
}
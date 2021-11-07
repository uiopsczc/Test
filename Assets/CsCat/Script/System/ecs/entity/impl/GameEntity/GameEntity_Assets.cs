using System;
using System.Collections;
using UnityEngine;

namespace CsCat
{
    public partial class GameEntity
    {
        public bool isAllAssetsLoadDone;
        public Action allAssetsLoadDoneCallback;

        // 通过resLoadComponent操作reload的东西
        public virtual void PreLoadAssets()
        {
            //resLoadComponent.LoadAssetAsync("resPath");
        }

        public void InvokeAfterAllAssetsLoadDone(Action callback)
        {
            if (isAllAssetsLoadDone)
                callback();
            else
                allAssetsLoadDoneCallback += callback;
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
    }
}
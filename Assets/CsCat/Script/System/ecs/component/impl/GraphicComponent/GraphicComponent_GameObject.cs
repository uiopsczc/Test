using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CsCat
{
    public partial class GraphicComponent
    {
        public Transform parentTransform;
        public GameObject gameObject;
        private bool isHide = false;
        private bool isNotDestroyGameObject;


        public Transform transform => cache.GetOrAddDefault(() => this.gameObject.transform);

        public RectTransform rectTransform =>
            cache.GetOrAddDefault(() => this.gameObject.GetComponent<RectTransform>());


        public void SetParentTransform(Transform parentTransform)
        {
            this.parentTransform = parentTransform;
            if (this.gameObject != null)
                this.transform.SetParent(this.parentTransform,
                    !LayerMask.LayerToName(this.gameObject.layer).Equals("UI"));
        }


        public virtual GameObject InstantiateGameObject(GameObject prefab)
        {
            return Object.Instantiate(prefab);
        }

        public virtual void SetIsShow(bool isShow)
        {
            this.isHide = !isShow;
            if (this.gameObject != null)
                this.gameObject.SetActive(!this.isHide);
        }

        protected virtual void InitGameObjectChildren()
        {
            GetGameEntity().InitGameObjectChildren();
        }


        public virtual void SetGameObject(GameObject gameObject, bool? isNotDestroyGameObject)
        {
            this.cache.Remove2(typeof(Transform).ToString());
            this.cache.Remove2(typeof(RectTransform).ToString());
            this.gameObject = gameObject;
            if (gameObject == null)
                return;
            if (isNotDestroyGameObject != null)
                this.isNotDestroyGameObject = isNotDestroyGameObject.Value;
            InitGameObjectChildren();
            SetIsShow(!isHide);
        }

        public bool IsShow()
        {
            return !this.isHide;
        }


        public virtual void DestroyGameObject()
        {
            if (this.gameObject != null && !isNotDestroyGameObject)
                gameObject.Destroy();
        }
    }
}
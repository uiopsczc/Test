using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace CsCat
{
    public partial class GameComponent
    {
        protected CoroutinePlugin coroutinePlugin => cache.GetOrAddDefault(() =>
            new CoroutinePlugin(GetGameEntity().GetComponent<CoroutinePluginComponent>().coroutinePlugin.mono));


        public string StartCoroutine(IEnumerator ie, string key = null)
        {
            return coroutinePlugin.StartCoroutine(ie, key);
        }

        /// <summary>
        /// 此处的key与StartCoroutine的key保持一致
        /// </summary>
        /// <param name="key"></param>
        public void StopCoroutine(string key)
        {
            coroutinePlugin.StopCoroutine(key);
        }

        public void StopAllCoroutines()
        {
            if (cache.ContainsKey<CoroutinePlugin>())
                coroutinePlugin.StopAllCoroutines();
        }
    }
}
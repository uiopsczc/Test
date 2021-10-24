using System.Collections.Generic;
using System.Linq;
using DG.Tweening;

namespace CsCat
{
    public class DOTweenPlugin
    {
        Dictionary<string, Tween> _dict = new Dictionary<string, Tween>();
        private IdPool _idPool = new IdPool();

        public Sequence AddDOTweenSequence(string key)
        {
            if (key != null && _dict.ContainsKey(key))
                RemoveDOTween(key);
            key = key ?? _idPool.Get().ToString();
            var sequence = DOTween.Sequence();
            _dict[key] = sequence;
            sequence.OnKill(() => RemoveDOTween(key));
            return sequence;
        }

        public Tween AddDOTween(string key, Tween tween)
        {
            if (key != null && _dict.ContainsKey(key))
                RemoveDOTween(key);
            key = key ?? _idPool.Get().ToString();
            _dict[key] = tween;
            tween.OnKill(() => RemoveDOTween(key));
            return tween;
        }

        public void RemoveDOTween(string key)
        {
            if (_dict.ContainsKey(key))
            {
                Tween tween = _dict[key];
                if (tween.IsActive())
                    _dict[key].Kill();
                _idPool.Despawn(key);
                _dict.Remove(key);
            }
        }

        public void RemoveDOTween(Tween tween)
        {
            string key = null;
            foreach (var dictKey in _dict.Keys)
            {
                if (_dict[dictKey] != tween) continue;
                key = dictKey;
                break;
            }

            if (key != null)
                RemoveDOTween(key);
        }

        public void SetIsPaused(bool isPaused)
        {
            foreach (var tween in _dict.Values)
            {
                if (!tween.IsActive())
                    continue;
                if (isPaused)
                    tween.Pause();
                else
                    tween.Play();
            }
        }

        public void RemoveDOTweens()
        {
            List<string> keyList = new List<string>(_dict.Keys);
            for (int i = 0; i < keyList.Count; i++)
            {
                var key = keyList[i];
                _dict[key].Kill();
            }
        }

        public void Reset()
        {
            this.RemoveDOTweens();
        }

        public void Destroy()
        {
            Reset();
            _dict = null;
            _idPool = null;
        }
    }
}
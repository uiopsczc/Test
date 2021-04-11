using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  public class AudioManager : TickObject
  {
    private AudioListener audioListener;
    private Transform audioSource_container_transform;
    private AudioSource bgm_audioSource;
    private Transform bgm_container_transform;
    public string last_bgm_path;
    private readonly List<AudioSource> playing_audioSource_list = new List<AudioSource>();

    public override void Init()
    {
      base.Init();
      var gameObject = GameObject.Find("AudioManager");
      graphicComponent.SetGameObject(gameObject, true);
      audioListener = graphicComponent.transform.Find("AudioListener").GetComponent<AudioListener>();
      audioSource_container_transform = graphicComponent.transform.Find("AudioSourceContainer");
      bgm_container_transform = graphicComponent.transform.Find("BGMContainer");
      bgm_audioSource = bgm_container_transform.GetComponent<AudioSource>();
      bgm_audioSource.SetAudioMixerOutput("bgm");
      SetGroupVolume("Master", GameData.instance.audioData.volume);
    }

    protected override void __Update(float deltaTime = 0, float unscaledDeltaTime = 0)
    {
      base.__Update(deltaTime, unscaledDeltaTime);
      for (var i = playing_audioSource_list.Count - 1; i >= 0; i--)
      {
        var playing_audioSource = playing_audioSource_list[i];
        if (!playing_audioSource.isPlaying)
        {
          playing_audioSource_list.RemoveAt(i);
          playing_audioSource.gameObject.Destroy();
          return; //每次只删除一个
        }
      }
    }

    // value [0,1]
    public void SetGroupVolume(string group_name, float value)
    {
      value = Mathf.Clamp01(value);
      var decibel = Mathf.Lerp(-80, 0, Mathf.Pow(value, 0.3f)); //分贝与真实听到的声音并不是线性的
      SetGroupDecibel(group_name, decibel);
    }

    // value [-80,0] 分贝
    public void SetGroupDecibel(string group_name, float decibel)
    {
      decibel = Mathf.Clamp(decibel, -80f, 0f);
      SingletonMaster.instance.audioMixer.SetFloat(AudioMixerConst.Group_Dict[group_name].volume_name, decibel);
    }


    // value [0,1]
    public void SetAllGroupVolume(float value)
    {
      foreach (var group_name in AudioMixerConst.Group_Dict.Keys)
        SetGroupVolume(group_name, value);
    }

    public void SetAudioListenerPosition(Vector3 position)
    {
      audioListener.transform.position = position;
    }

    public void PlaySound(string asset_path, string group_name, GameObject target, Vector3? target_localPosition,
      bool is_loop = false,
      float? range = null)
    {
      resLoadComponent.GetOrLoadAsset(asset_path,
        assetCat =>
        {
          OnSoundLoadSuccess(assetCat.Get<AudioClip>(asset_path.GetSubAssetPath()), group_name, target, target_localPosition,
            is_loop, range);
        },null, null, this);
    }

    private void OnSoundLoadSuccess(AudioClip audioClip, string group_name, GameObject target, Vector3? target_localPosition,
      bool is_loop = false, float? range = null)
    {
      //如果没有target，音频挂AudioMgr上
      target = target ?? audioSource_container_transform.gameObject;
      // 如果有pos，生成原地音频
      if (target_localPosition != null)
      {
        var clone = new GameObject("AudioSource");
        clone.transform.SetParent(target.transform);
        clone.transform.localPosition = target_localPosition.Value;
        target = clone;
      }

      var audioSource = GetAudioSource(target);

      if (target_localPosition != null)
        playing_audioSource_list.Add(audioSource);

      if (range != null)
      {
        audioSource.spatialBlend = 1; //0表示2D，1表示3D
        audioSource.minDistance = range.Value / 2;
        audioSource.maxDistance = range.Value;
      }
      else
      {
        audioSource.spatialBlend = 0; //0表示2D，1表示3D
      }

      audioSource.clip = audioClip;
      audioSource.SetAudioMixerOutput(group_name);
      audioSource.loop = is_loop;
      audioSource.playOnAwake = false;
      audioSource.Play();
    }

    private AudioSource GetAudioSource(GameObject target)
    {
      //选择AudioSource，如果attachGmaeobj上有不在播放的AudioSource，
      //使用其播放，没有则创建新AudioSource
      var target_audioSources = target.GetComponents<AudioSource>();
      for (var i = 0; i < target_audioSources.Length; i++)
        if (!target_audioSources[i].isPlaying)
          return target_audioSources[i];
      return target.AddComponent<AudioSource>();
    }


    public void PlayUISound(string sound_path)
    {
      PlaySound(sound_path, "ui", null, null, false);
    }

    public void PlayBGMSound(string sound_path, bool is_loop = true)
    {
      if (sound_path.Equals(last_bgm_path))
        return;

      resLoadComponent.GetOrLoadAsset(sound_path,
        asset_cat =>
        {
          OnBGMLoadSuccess(asset_cat.Get<AudioClip>(sound_path.GetSubAssetPath()), is_loop);
          last_bgm_path = sound_path;
        },null, null, this);
    }

    public void PauseBGMSound(bool is_paused = true)
    {
      if (is_paused)
        bgm_audioSource.Pause();
      else
        bgm_audioSource.UnPause();
    }

    public void StopBGMSound()
    {
      bgm_audioSource.Stop();
    }


    private void OnBGMLoadSuccess(AudioClip audioClip, bool is_loop)
    {
      var audioSource = bgm_audioSource;
      audioSource.clip = audioClip;
      audioSource.loop = is_loop;
      audioSource.playOnAwake = false;
      audioSource.Play();
    }
  }
}
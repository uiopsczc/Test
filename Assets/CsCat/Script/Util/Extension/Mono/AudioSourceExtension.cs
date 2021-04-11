using UnityEngine;
using UnityEngine.Audio;

namespace CsCat
{
  public static class AudioSourceExtension
  {
    public static bool SetAudioMixerOutput(this AudioSource self, string group_name, AudioMixer audioMixer = null)
    {
      audioMixer = audioMixer ?? SingletonMaster.instance.audioMixer;
      AudioMixerGroup[] groups = audioMixer.FindMatchingGroups(AudioMixerConst.Group_Dict[group_name].group_path);
      if (groups.Length > 0)
      {
        self.outputAudioMixerGroup = groups[0];
        return true;
      }

      return false;
    }

  }
}
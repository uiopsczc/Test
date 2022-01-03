using System.Collections.Generic;

namespace CsCat
{
	public class AudioMixerConst
	{
		public static Dictionary<string, AudioMixerGroupInfo> Group_Dict = new Dictionary<string, AudioMixerGroupInfo>
	{
	  {"Master", new AudioMixerGroupInfo("Master", "Master_volume")},
	  {"bgm", new AudioMixerGroupInfo("Master/bgm", "bgm_volume")},
	  {"ui", new AudioMixerGroupInfo("Master/ui", "ui_volume")},
	  {"sfx", new AudioMixerGroupInfo("Master/sfx", "sfx_volume")},
	  {"ambient", new AudioMixerGroupInfo("Master/ambient", "ambient_volume")}
	};
	}
}
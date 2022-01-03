namespace CsCat
{
	public class AudioMixerGroupInfo
	{
		public string group_path;
		public string volume_name;

		public AudioMixerGroupInfo(string group_path, string volume_name)
		{
			this.group_path = group_path;
			this.volume_name = volume_name;
		}
	}
}
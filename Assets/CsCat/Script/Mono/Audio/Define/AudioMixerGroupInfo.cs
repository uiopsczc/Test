namespace CsCat
{
	public class AudioMixerGroupInfo
	{
		public string groupPath;
		public string volumeName;

		public AudioMixerGroupInfo(string groupPath, string volumeName)
		{
			this.groupPath = groupPath;
			this.volumeName = volumeName;
		}
	}
}
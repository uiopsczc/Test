using UnityEngine;

namespace CsCat
{
	public static class EditorModeConst
	{
		private const string _Is_Simulation_Mode = "IsSimulationMode"; //

		public static bool Is_Simulation_Mode
		{
			get
			{
				if (!PlayerPrefs.HasKey(_Is_Simulation_Mode))
				{
					if (Application.isEditor)
						PlayerPrefs.SetInt(_Is_Simulation_Mode, 0);
					else
						PlayerPrefs.SetInt(_Is_Simulation_Mode, 1);
				}

				return PlayerPrefs.GetInt(_Is_Simulation_Mode) == 1;
			}
			set
			{
				var newValue = value ? 1 : 0;
				PlayerPrefs.SetInt(_Is_Simulation_Mode, newValue);
			}
		}

		public static bool IsEditorMode
		{
			get { return !Is_Simulation_Mode; }
			set { Is_Simulation_Mode = !value; }
		}
	}
}
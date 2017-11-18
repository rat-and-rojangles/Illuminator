using UnityEngine;
using UnityEditor;
using System.Diagnostics;

public static class OpenCSharp {
	[MenuItem ("Assets/Open C# Project in Code")]
	public static void OpenProjectFolderInVSCode () {
		ProcessStartInfo startInfo = new ProcessStartInfo ("code", "\"" + Application.dataPath + " /..\"");
		startInfo.WindowStyle = ProcessWindowStyle.Hidden;
		Process.Start (startInfo);
	}
}

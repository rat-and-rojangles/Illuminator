#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Build;
class PreBuild : IPreprocessBuild {
	public int callbackOrder { get { return 0; } }
	public void OnPreprocessBuild (BuildTarget target, string path) {
		PlayerSettings.Android.bundleVersionCode++;
	}
}
#endif
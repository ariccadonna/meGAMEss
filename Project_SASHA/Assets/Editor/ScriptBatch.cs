// C# example.
using UnityEditor;
using UnityEngine;

using System.Diagnostics;

public class ScriptBatch : MonoBehaviour 
{
	[MenuItem("Build/Build With Postprocess")]
	public static void BuildGame ()
	{
		// Get filename.
        string path = EditorUtility.SaveFolderPanel("Choose Location of Built Game", "C:/Users/Hilzari/Desktop/", "ps");
       // string path = "C:/Users/Hilzari/Desktop/ps";
		string[] levels = new string[] {"Assets/Assets/Scenes/worldBoard2.unity"}; 
		
		// Build player.
        BuildPipeline.BuildPlayer(levels, path + "/project_SASHA.exe", BuildTarget.StandaloneWindows, BuildOptions.None);
		
		// Copy a file from the project folder to the build folder, alongside the built game.
		//FileUtil.CopyFileOrDirectory("Assets/WebPlayerTemplates/Readme.txt", path + "Readme.txt");
		
		// Run the game (Process class from System.Diagnostics).
		Process proc = new Process();
		proc.StartInfo.FileName = path + "project_SASHA.exe" ;
		proc.Start();
	}
}
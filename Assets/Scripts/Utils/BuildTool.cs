#if UNITY_EDITOR
using UnityEditor;
public static class BuildTool
{
    [MenuItem("Tools/Build/Build Client")]
    public static void BuildClient()
    {
        var bpo = new BuildPlayerOptions
        {
            scenes = new[]{"Assets/Scenes/MainClient.unity"},
            locationPathName = "Build/Client/",
            options = BuildOptions.None,
            target = BuildTarget.WebGL,
        };
        BuildPipeline.BuildPlayer(bpo);
    }
 
    [MenuItem("Tools/Build/Build Server")]
    public static void BuildServer()
    {
        var bpo = new BuildPlayerOptions
        {
            scenes = new[] { "Assets/Scenes/MainServer.unity" },
            locationPathName = "Build/Server/MyServer.x86_64",
            target = BuildTarget.StandaloneWindows64,
            options = BuildOptions.EnableHeadlessMode,
        };
        BuildPipeline.BuildPlayer(bpo);
    }
}
#endif
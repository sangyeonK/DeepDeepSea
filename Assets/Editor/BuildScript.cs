using System;
using System.Linq;
using UnityEditor;

public class BuildScript{

    public static void BuildAndroid()
    {
        Build(BuildTarget.Android);
    }

    public static void BuildIOS()
    {
        Build(BuildTarget.iOS);
    }

    public static void BuildWindows()
    {
        Build(BuildTarget.StandaloneWindows);
    }

    static void Build(BuildTarget target)
    {
        BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, Environment.GetCommandLineArgs().Last(), target, BuildOptions.None);
    }

}

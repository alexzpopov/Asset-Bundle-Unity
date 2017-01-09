using UnityEditor;

public class CreateAssetBundles
{

    [MenuItem("Assets/Build AssetBundlesStl")]
    static void BuildAllAssetBundles()
    {
        //BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);

    }
}
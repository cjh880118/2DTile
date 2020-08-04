using UnityEditor;
using UnityEngine;


namespace JHchoi
{
    public static class AssetbundlesMenuItems
    {
        const string simulateAssetBundlesMenu = "JHchoi/AssetBundles/Simulate AssetBundles";


        [MenuItem(simulateAssetBundlesMenu)]
        public static void ToggleSimulateAssetBundle()
        {
            AssetBundleManager.SimulateAssetBundleInEditor = !AssetBundleManager.SimulateAssetBundleInEditor;
        }

        [MenuItem(simulateAssetBundlesMenu, true)]
        public static bool ToggleSimulateAssetBundleValidate()
        {
            Menu.SetChecked(simulateAssetBundlesMenu, AssetBundleManager.SimulateAssetBundleInEditor);
            return true;
        }

        [MenuItem("JHchoi/AssetBundles/Build AssetBundles", false, 100)]
        static public void BuildAssetBundles()
        {
            AssetBundleBuildScript.BuildAssetBundles();
        }
    }
}

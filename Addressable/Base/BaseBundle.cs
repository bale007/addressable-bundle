using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class BaseBundle : ScriptableObject
{
    public abstract void Build();

#if UNITY_EDITOR
        
    private static string defaultBundleRootPath = "Assets/Settings/Bundle";
        
    public static void BuildAll()
    {
        var typeFilter = "t:" + nameof(BaseBundle);
        var guids = AssetDatabase.FindAssets(typeFilter, new [] {defaultBundleRootPath});
        foreach (var guid in guids)
        {
            var assetPath = AssetDatabase.GUIDToAssetPath(guid);
            var bundle = AssetDatabase.LoadAssetAtPath<BaseBundle>(assetPath);
            bundle.Build();
        }
    }
        
#endif
}
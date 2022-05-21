using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public abstract class GenericBundle<T> : BaseBundle where T: Object
    {
        [ReadOnly, ListDrawerSettings(Expanded = true)]
        public List<T> assets;
        
        [FolderPath(ParentFolder = "Assets", RequireExistingPath = true)][ListDrawerSettings(Expanded = true)]
        public string[] folderPaths;

        [Button(ButtonSizes.Medium)]
        public override void Build()
        {
#if UNITY_EDITOR
            var folders = MakeSearchFolders();
            var guids = AssetDatabase.FindAssets("t:" + typeof(T).Name, folders);

            if (!IsDataDirty(guids))
            {
                return;
            }
            
            assets.Clear();
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var asset = AssetDatabase.LoadAssetAtPath<T>(assetPath);
                assets.Add(asset);
            }
            
            EditorUtility.SetDirty(this);
#endif
        }

        private string[] MakeSearchFolders()
        {
            var folders = new string[folderPaths.Length];
            for (var i = 0; i < folderPaths.Length; i++)
            {
                folders[i] = "Assets/" + folderPaths[i];
            }
            return folders;
        }
        
#if UNITY_EDITOR
        private bool IsDataDirty(IReadOnlyList<string> newData)
        {
            // compare length first 
            var newLength = newData.Count;
            var oldLength = assets.Count;

            // length doest not match, return true 
            if (newLength != oldLength) return true;
            
            // im trusting asset data to always return guid in path order
            for (var i = 0; i < newLength; i++)
            {
                var oldAsset = assets[i];
                var newGuid = newData[i];

                var oldPath = AssetDatabase.GetAssetPath(oldAsset);
                var oldGuid = AssetDatabase.AssetPathToGUID(oldPath);
                
                // if guid does not match, something changed, return false
                if (oldGuid != newGuid)
                {
                    return true;
                }
            }
            
            // nothing changed
            return false;
        }
#endif
    }
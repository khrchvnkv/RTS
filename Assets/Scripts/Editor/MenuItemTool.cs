using Common.StaticData;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public static class MenuItemTool
    {
        [MenuItem("Shortcuts/Open Bootstrap Scene", false, 1)]
        public static void OpenBootstrapScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity", OpenSceneMode.Single);
        }
        
        [MenuItem("Shortcuts/Open Game Scene", false, 1)]
        public static void OpenGameScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/GameScene.unity", OpenSceneMode.Single);
        }
        
        [MenuItem("Shortcuts/StaticData/Open GameStaticData")]
        public static void OpenGameStaticData()
        {
            GameStaticData staticData = Resources.Load<GameStaticData>("StaticData/GameStaticData");

            Selection.activeObject = staticData;
            
            EditorGUIUtility.PingObject(staticData);
        }
    }
}
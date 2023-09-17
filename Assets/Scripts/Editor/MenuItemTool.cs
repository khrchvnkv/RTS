using UnityEditor;
using UnityEditor.SceneManagement;

namespace Editor
{
    public static class MenuItemTool
    {
        [MenuItem("Shortcuts/Open Bootstrap Scene", false, 1)]
        public static void OpenBootstrapScene()
        {
            EditorSceneManager.OpenScene("Assets/Scenes/Bootstrap.unity", OpenSceneMode.Single);
        }
        [MenuItem("Shortcuts/StaticData/Open GameStaticData")]
        public static void OpenGameStaticData()
        {
            //GameStaticData staticData = Resources.Load<GameStaticData>("GameStaticData");

            //Selection.activeObject = staticData;

            //EditorGUIUtility.PingObject(staticData);
        }
    }
}
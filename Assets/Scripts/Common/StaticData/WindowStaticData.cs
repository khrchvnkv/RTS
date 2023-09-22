using Common.UnityLogic.UI.LoadingScreen;
using Common.UnityLogic.UI.Windows;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(fileName = "WindowStaticData", menuName = "Static Data/WindowStaticData")]
    public sealed class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public UIRoot UIRoot { get; private set; }
        [field: SerializeField] public LoadingCurtain LoadingCurtainPrefab { get; private set; }
    }
}
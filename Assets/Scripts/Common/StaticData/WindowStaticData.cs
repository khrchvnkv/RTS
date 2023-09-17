using Common.UnityLogic.LoadingCurtain;
using UnityEngine;

namespace Common.StaticData
{
    [CreateAssetMenu(menuName = nameof(WindowStaticData), fileName = nameof(WindowStaticData), order = 0)]
    public sealed class WindowStaticData : ScriptableObject
    {
        [field: SerializeField] public GameObject UIRoot { get; private set; }
        [field: SerializeField] public LoadingCurtain LoadingCurtainPrefab { get; private set; }
    }
}
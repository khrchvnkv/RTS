using UnityEngine;

namespace Common.Infrastructure.Services.DontDestroyOnLoadCreator
{
    public class DontDestroyOnLoadCreator : MonoBehaviour, IDontDestroyOnLoadCreator
    {
        public GameObject MarkAsDontDestroy(GameObject instance)
        {
            DontDestroyOnLoad(instance);
            return instance;
        }
    }
}
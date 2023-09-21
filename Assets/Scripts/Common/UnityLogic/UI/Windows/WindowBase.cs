using Common.Infrastructure.WindowsManagement;
using UnityEngine;

namespace Common.UnityLogic.UI.Windows
{
    public abstract class WindowBase<TData> : MonoBehaviour, IWindow where TData : IWindowData
    {
        protected TData WindowData;

        public void Show(IWindowData windowData)
        {
            WindowData = (TData)windowData;
            Subscribe();
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            Unsubscribe();
            if (WindowData.DestroyOnClosing)
            {
                Destroy(gameObject);
            }
            else
            {
                gameObject.SetActive(false);
            }

            WindowData = default;
        }
        protected virtual void Subscribe() { }
        protected virtual void Unsubscribe() { }
    }
}
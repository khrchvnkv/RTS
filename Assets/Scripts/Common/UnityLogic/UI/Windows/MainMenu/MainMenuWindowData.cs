using Common.Infrastructure.WindowsManagement;

namespace Common.UnityLogic.UI.Windows.MainMenu
{
    public struct MainMenuWindowData : IWindowData
    {
        public string WindowName => "MainMenu";
        public bool DestroyOnClosing => true;
    }
}
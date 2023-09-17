namespace Common.Infrastructure.Factories.UIFactory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        void ShowLoadingCurtain();
        void HideLoadingCurtain();
    }
}
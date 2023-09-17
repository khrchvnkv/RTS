using Common.StaticData;

namespace Common.Infrastructure.Services.AssetsManagement
{
    public interface IAssetProvider
    {
        GameStaticData LoadGameStaticData();
    }
}
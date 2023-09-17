using Common.StaticData;
using UnityEngine;

namespace Common.Infrastructure.Services.AssetsManagement
{
    public class AssetProvider : IAssetProvider
    {
        private const string GAME_STATIC_DATA_PATH = "StaticData/GameStaticData";

        public GameStaticData LoadGameStaticData() => Resources.Load<GameStaticData>(GAME_STATIC_DATA_PATH);
    }
}
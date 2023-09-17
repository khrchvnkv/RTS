using System;

namespace Common.Infrastructure.Services.SceneLoading
{
    public interface ISceneLoader
    {
        void LoadScene(string sceneName, Action onLoaded = null);
    }
}
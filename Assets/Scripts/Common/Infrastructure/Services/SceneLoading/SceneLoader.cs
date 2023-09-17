using System;
using System.Collections;
using Common.Infrastructure.Services.Coroutines;
using UnityEngine.SceneManagement;

namespace Common.Infrastructure.Services.SceneLoading
{
    public sealed class SceneLoader : ISceneLoader
    {
        private readonly ICoroutineRunner _coroutineRunner;

        public SceneLoader(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void LoadScene(string sceneName, Action onLoaded = null) => 
            _coroutineRunner.StartCoroutine(LoadSceneCoroutine(sceneName, onLoaded));
        private IEnumerator LoadSceneCoroutine(string sceneName, Action onLoaded = null)
        {
            var waitNextScene = SceneManager.LoadSceneAsync(sceneName);
            while (!waitNextScene.isDone)
            {
                yield return null;
            }
            onLoaded?.Invoke();
        }
    }
}
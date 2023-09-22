using System.Collections;
using Common.Infrastructure.Services.Coroutines;
using UnityEngine;
using VContainer;

namespace Common.UnityLogic.UI.LoadingScreen
{
    public class LoadingCurtain : MonoBehaviour
    {
        private const float FADE_SPEED = 1.5f;

        [SerializeField] private CanvasGroup _canvasGroup;
        
        private ICoroutineRunner _coroutineRunner;
        private Coroutine _coroutine;

        [Inject]
        public void Construct(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void Show()
        {
            StopCoroutine();
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1.0f;
        }
        public void Hide()
        {
            StopCoroutine();
            _coroutine = _coroutineRunner.StartCoroutine(FadeCoroutine());
        }
        private void StopCoroutine() => _coroutineRunner.StopCoroutineSafe(_coroutine);
        private IEnumerator FadeCoroutine()
        {
            while (_canvasGroup.alpha > 0.0f)
            {
                _canvasGroup.alpha -= FADE_SPEED * Time.deltaTime;
                yield return null;
            }
            _canvasGroup.gameObject.SetActive(false);
        }
    }
}
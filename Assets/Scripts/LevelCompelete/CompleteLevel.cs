using UnityEngine;
using Zenject;
using DG.Tweening;
using System.Collections;

public class CompleteLevel : MonoBehaviour
{
    [SerializeField] private CanvasGroup _winScreen;
    [SerializeField] private CanvasGroup _canExitCanvas;
    [SerializeField] private GameObject _limiter;

    [SerializeField] private CanvasGroup _playerStatUI;

    [Inject] private LevelCompleteRequirement _levelCompleteRequirement;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            EndLevel();
        }
    }

    private void DisableLimit()
    {
        _limiter.SetActive(false);
        _canExitCanvas.DOFade(1, 0.2f).OnComplete(() => StartCoroutine(DisableExitCanvas()));
    }

    private IEnumerator DisableExitCanvas()
    {
        float elapsedTime = 0;

        while(true)
        {
            yield return null;
            elapsedTime += Time.deltaTime;

            if(elapsedTime > 3)
            {
                _canExitCanvas.DOFade(0, 0.2f);
                yield break;
            }
        }
    }

    private void OnEnable() => _levelCompleteRequirement.allEnemyKilled += DisableLimit;
    private void OnDisable() => _levelCompleteRequirement.allEnemyKilled -= DisableLimit;

    private void EndLevel()
    {
        _winScreen.DOFade(1, 0.2f);
        _winScreen.interactable = true;
        _winScreen.blocksRaycasts = true;

        _playerStatUI.DOFade(0, 0.2f);
        _playerStatUI.interactable = false;
        _playerStatUI.blocksRaycasts = false;
    }
}

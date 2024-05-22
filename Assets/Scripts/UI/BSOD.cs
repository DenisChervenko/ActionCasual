using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class BSOD : MonoBehaviour
{
    [SerializeField] private CanvasGroup _screenOfDeath;
    [SerializeField] private CanvasGroup _mainCanvas;

    [Inject] private PlayerStats _playerStats;
    private void ShowDeathScreen()
    {
        _screenOfDeath.interactable = true;
        _screenOfDeath.blocksRaycasts = true;

        _mainCanvas.interactable = false;
        _mainCanvas.blocksRaycasts = false;

        _mainCanvas.DOFade(0, 0.2f).OnComplete(() => _screenOfDeath.DOFade(1, 0.2f).OnComplete(() => Time.timeScale = 0));
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void HomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }


    private void OnEnable() => _playerStats.playerIsDead += ShowDeathScreen;
    private void OnDisable() => _playerStats.playerIsDead -= ShowDeathScreen;
}

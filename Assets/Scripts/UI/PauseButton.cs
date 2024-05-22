using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseButton : MonoBehaviour
{
    private CanvasGroup _mainCanvas;
    [SerializeField] private CanvasGroup _pauseWindow;
    [SerializeField] private CanvasGroup _permanentStatCanvas;

    private void Start()
    {
        _mainCanvas = gameObject.GetComponent<CanvasGroup>();
    }

    public void OnShowPauseScreen()
    {
        _mainCanvas.interactable = false;
        _mainCanvas.blocksRaycasts = false;
        _pauseWindow.interactable = true;
        _pauseWindow.blocksRaycasts = true;

        _permanentStatCanvas.DOFade(0, 0.2f);
        _mainCanvas.DOFade(0, 0.2f);
        _pauseWindow.DOFade(1, 0.2f).OnComplete(() => Time.timeScale = 0);
    }

    public void OnHidePauseScreen()
    {
        Time.timeScale = 1;
        _pauseWindow.DOFade(0, 0.2f);
        _mainCanvas.DOFade(1, 0.2f);
        _permanentStatCanvas.DOFade(1, 0.2f);

        _pauseWindow.interactable = false;
        _pauseWindow.blocksRaycasts = false;
        _mainCanvas.interactable = true;
        _mainCanvas.blocksRaycasts = true;
    }

    public void OnHomeButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
}

using UnityEngine;
using DG.Tweening;

public class CanvasChanger : MonoBehaviour
{
    [SerializeField] private float _durationFade;
    private CanvasGroup _currentCanvas;

    private void Start() => _currentCanvas = gameObject.GetComponent<CanvasGroup>();
    public void ChangeCanvas(CanvasGroup _targetCanvas)
    {
        _currentCanvas.interactable = false;
        _currentCanvas.blocksRaycasts = false;

        _currentCanvas.DOFade(0, _durationFade).OnComplete(() => 
        _targetCanvas.DOFade(1, _durationFade).OnComplete(() => 
        {
            _targetCanvas.interactable = true;
            _targetCanvas.blocksRaycasts = true;
        }));
    }

    public void SettingChanger(CanvasGroup _targetCanvas)
    {
        _targetCanvas.DOFade(_targetCanvas.alpha == 1 ? 0 : 1, _durationFade).OnComplete(() =>
        {
            _targetCanvas.interactable = _targetCanvas.interactable ? false : true;
            _targetCanvas.blocksRaycasts = _targetCanvas.blocksRaycasts ? false : true;

        });
    }
}

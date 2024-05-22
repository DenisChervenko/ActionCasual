using TMPro;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
public class CounterUpdate : MonoBehaviour
{
    public delegate void UpdateInfo(int value);
    public UpdateInfo updateInfo;

    public UnityAction changeState;

    [SerializeField] private CanvasGroup _counterCanvas;
    [SerializeField] private TMP_Text _countOfEnemy;

    private int _countOfDeth;

    public void CanvasStateChanger()
    {
        int targetAlpha = _counterCanvas.alpha == 1 ? 0 : 1;
        _counterCanvas.DOFade(targetAlpha, 0.2f).OnComplete(() => _countOfEnemy.text = $"{0}");
        changeState.Invoke();
    }

    public void UpdateCount(int allEnemyCount, int defeatedEnemyCount)
    {
        _countOfDeth++;
        _countOfEnemy.text = $"{defeatedEnemyCount + " / " + allEnemyCount}";
        updateInfo?.Invoke(_countOfDeth);
    }
}

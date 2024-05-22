using UnityEngine;
using DG.Tweening;
using System.Collections;
using Zenject;
using TMPro;
using UnityEngine.Events;
public class LevelCompleteRequirement : MonoBehaviour
{
    public UnityAction allEnemyKilled;

    [SerializeField] private CanvasGroup _requirementCanvasBanner;
    [SerializeField] private CanvasGroup _requirementPermanent;

    [SerializeField] private TMP_Text _countOfKillBanner;
    [SerializeField] private TMP_Text _countOfKillPermanent;
    public int countForComplete;
    private float _elapsedTime;

    private int _countOfKilled;

    [Inject] private CounterUpdate _counterUpdate;

    private void Start() 
    {
        _requirementCanvasBanner.DOFade(1, 0.5f).OnComplete(() => StartCoroutine(DisableRequirement()));
        _countOfKillBanner.text = $"{countForComplete}";

        _counterUpdate.changeState += ChangeStateUI;
        _counterUpdate.updateInfo += RefreshInfo;
    }

    public void SetMaxCountEnemy(int count)
    {
        countForComplete += count;
        _countOfKillPermanent.text = $"{0 + " / " +countForComplete}";
    } 
        
    private IEnumerator DisableRequirement()
    {
        while(true)
        {
            yield return null;

            _elapsedTime += Time.deltaTime;
            if(_elapsedTime > 3)
            {
                _elapsedTime = 0;
                _requirementCanvasBanner.DOFade(0, 0.5f).OnComplete(() => _requirementPermanent.DOFade(1, 0.2f));
                yield break;
            }
        }
    }

    private void ChangeStateUI()
    {
        if(_requirementPermanent.alpha == 1)
            _requirementPermanent.DOFade(0, 0.2f);
        else
            _requirementPermanent.DOFade(1,0.2f);
    }

    private void RefreshInfo(int value)
    {
        _countOfKillPermanent.text = $"{value + " / " + countForComplete}";
        if(value >= countForComplete)
            allEnemyKilled.Invoke();
    } 
        
    private void OnEbable()
    {
        if(_counterUpdate.changeState == null)
            _counterUpdate.changeState += ChangeStateUI;
        if(_counterUpdate.updateInfo == null)
            _counterUpdate.updateInfo += RefreshInfo;
    }
    private void OnDisable()
    {
        _counterUpdate.changeState -= ChangeStateUI;
        _counterUpdate.updateInfo -= RefreshInfo;   
    } 

} 

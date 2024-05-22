using TMPro;
using UnityEngine;
using Zenject;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _coinReward;
    [SerializeField] private TMP_Text _enemyKilled;

    [Inject] private LevelCompleteRequirement _levelCompleteRequirement;

    private void UpdateRewardInfo()
    {
        _coinReward.text = $"{_levelCompleteRequirement.countForComplete * 2}";
        _enemyKilled.text = $"{_levelCompleteRequirement.countForComplete}";
    }

    private void OnEnable() => _levelCompleteRequirement.allEnemyKilled += UpdateRewardInfo;
    private void OnDisable() => _levelCompleteRequirement.allEnemyKilled -= UpdateRewardInfo;
}

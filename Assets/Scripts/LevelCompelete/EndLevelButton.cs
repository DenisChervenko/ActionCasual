using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class EndLevelButton : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;
    [Inject] private LevelCompleteRequirement _levelCompleteRequirement;

    public void OnHome()
    {
        _playerInfo.coinBalance += _levelCompleteRequirement.countForComplete * 2;
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        _playerInfo.coinBalance += _levelCompleteRequirement.countForComplete * 2;
        SceneManager.LoadScene(1);
    }
}

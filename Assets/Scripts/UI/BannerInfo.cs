using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class BannerInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _playerLevelText;
    [SerializeField] private TMP_Text _playerNicknameText;
    [SerializeField] private TMP_Text _playerProgressLevelText;

    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;
    private PlayerInfo _playerInfo;

    private void Start()
    {
        _playerInfo = _playerInfoBehaviour.GetPlayerInfo();
        UpdatePlayerInfo();
    }

    private void UpdatePlayerInfo()
    {
        _playerLevelText.text = $"{_playerInfo.playerLevel}";
        _playerNicknameText.text = $"{_playerInfo.playerName}";
    }
}

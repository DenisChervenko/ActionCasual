using TMPro;
using UnityEngine;
using Zenject;

public class PlayerInfoBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerInfo _playerInfo;

    [SerializeField] private TMP_Text _coinBalance;
    [SerializeField] private TMP_Text _gemBalance;

    [Inject] private UpgradeCharacter _upgradeCharacter;
    [Inject] private BuyCharacter _buyCharacter;

    private void Start()
    {
        UpdateCoinBalance(_playerInfo.coinBalance);
        UpdateGemBalance(_playerInfo.gemBalance);
    }

    public int GetSelectedCharacterIndex()
    {
        return _playerInfo.selectedCharacter;
    }

    public void SetSelectedCharacterIndex(int index) => _playerInfo.selectedCharacter = index;

    public void UpdateCoinBalance(int newBalance)
    {
        _playerInfo.coinBalance = newBalance;
        _coinBalance.text = $"{_playerInfo.coinBalance}";
    } 
    public void UpdateGemBalance(int newBalance) 
    {
        _playerInfo.gemBalance = newBalance;
        _gemBalance.text = $"{_playerInfo.gemBalance}";
    } 

    public int GetCoinBalance()
    {
        return _playerInfo.coinBalance;
    }

    public int GetGemBalance()
    {
        return _playerInfo.gemBalance;
    }

    public PlayerInfo GetPlayerInfo()
    {
        return _playerInfo;
    }

    private void OnEnable()
    {
        _buyCharacter.onBuyCharacterCoin += UpdateCoinBalance;
        _buyCharacter.onBuyCharacterGem += UpdateGemBalance;
        _upgradeCharacter.onRefreshBalance += UpdateCoinBalance;
    } 

    private void OnDisable()
    {
        _buyCharacter.onBuyCharacterCoin -= UpdateCoinBalance;
        _buyCharacter.onBuyCharacterGem -= UpdateGemBalance; 
        _upgradeCharacter.onRefreshBalance -= UpdateCoinBalance;
    }
}

using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class BuyCharacter : MonoBehaviour
{
    public delegate void OnBuyCharacter(int newBalance);
    public OnBuyCharacter onBuyCharacterCoin;
    public OnBuyCharacter onBuyCharacterGem;

    public UnityAction onBuyCharacter;

    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;
    [Inject] private CharacterControllerMarket _characterControllerMarket;
    private CharacterContainer _characterContainer;

    public void Buy()
    {
        _characterContainer = _characterControllerMarket.CharacterGetter();

        if(_characterContainer.Character.priceInCoin)
        {
            int newBalance = _playerInfoBehaviour.GetCoinBalance();
            if(newBalance >= _characterContainer.Character.price)
            {
                _characterContainer.Character.isBuyed = true;
                newBalance -= _characterContainer.Character.price;
                onBuyCharacterCoin.Invoke(newBalance);
            }          
            else
                return;
        }
        else
        {
            int newBalance = _playerInfoBehaviour.GetCoinBalance();
            if(newBalance >= _characterContainer.Character.price)
            {
                _characterContainer.Character.isBuyed = true;
                newBalance = _playerInfoBehaviour.GetGemBalance();
                onBuyCharacterGem.Invoke(newBalance);
            }
            else
                return;
        }

        onBuyCharacter.Invoke();
    }

}

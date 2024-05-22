using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class UpgradeCharacter : MonoBehaviour
{
    public delegate void OnUpgraded(int newBalance);
    public OnUpgraded onRefreshBalance;

    public UnityAction OnWasUpgraded;


    [Inject] private CharacterControllerMarket _characterControllerMarket;
    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;

    private CharacterContainer _characterContainer;

    public void Upgrade()
    {
        _characterContainer = _characterControllerMarket.CharacterGetter();

        int currentBalance = _playerInfoBehaviour.GetCoinBalance();
        if(currentBalance >= _characterContainer.Character.upgradePrice[_characterContainer.Character.level])
        {

            int currentLevel = _characterContainer.Character.level;

            _characterContainer.Character.health += _characterContainer.Character.healthModifier[currentLevel];
            _characterContainer.Character.defend += _characterContainer.Character.defendModifier[currentLevel];
            _characterContainer.Character.speedMovement += _characterContainer.Character.spdMoveModifier[currentLevel];
            _characterContainer.Character.meleeDamage += _characterContainer.Character.meleeDamageModifier[currentLevel];
            _characterContainer.Character.rangeDamage += _characterContainer.Character.rangeDamageModifier[currentLevel];

            _characterContainer.Character.level++;

            if(_characterContainer.Character.level >= _characterContainer.Character.maxLevel)
                _characterContainer.Character.isUpgradeToMax = true;

            int balance = _playerInfoBehaviour.GetCoinBalance();
            onRefreshBalance.Invoke(balance - _characterContainer.Character.price);
            OnWasUpgraded.Invoke();
        }
    }
}

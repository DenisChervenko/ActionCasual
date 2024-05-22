using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
public class ButtonControllerMarket : MonoBehaviour
{
    

    [SerializeField] private GameObject _buyButton;
    [SerializeField] private GameObject _upgradeButton;
    [SerializeField] private GameObject _selectButton;

    private CharacterContainer _characterContainer;
    
    [Inject] private CharacterControllerMarket _characterControllerMarket;
    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;
    [Inject] private UpgradeCharacter _upgradeCharacter;
    [Inject] private CharacterSelect _characterSelect;
    [Inject] private CharacterList _characterList;
    [Inject] private BuyCharacter _buyCharacter;

    private void Start() => GetCharacterContainer();

    private void GetCharacterContainer()
    {
        _characterContainer = _characterControllerMarket.CharacterGetter();
        UpdateButtonState();
    } 

    private void UpdateButtonState()
    {
        if(_characterContainer.Character.isBuyed)
        {
            _buyButton.SetActive(false);

            if(!_characterContainer.Character.isUpgradeToMax)
                    _upgradeButton.SetActive(true);
                else
                    _upgradeButton.SetActive(false);

            if(_playerInfoBehaviour.GetSelectedCharacterIndex() == _characterList.CurrentIndexOfCharacter)
                _selectButton.SetActive(false);
            else
                _selectButton.SetActive(true);
        }
        else
        {
            _buyButton.SetActive(true);
            _selectButton.SetActive(false);
            _upgradeButton.SetActive(false);
        }
    }

    private void OnEnable()
    {
        _buyCharacter.onBuyCharacter += UpdateButtonState;
        _characterList.OnUpdateButton += GetCharacterContainer;
        _upgradeCharacter.OnWasUpgraded += UpdateButtonState;
        _characterSelect.onCharacterSelected += UpdateButtonState;
    } 
    private void OnDisable()
    {
        _buyCharacter.onBuyCharacter -= UpdateButtonState;
        _characterList.OnUpdateButton -= GetCharacterContainer;
        _upgradeCharacter.OnWasUpgraded -= UpdateButtonState;
        _characterSelect.onCharacterSelected -= UpdateButtonState;
    } 
}

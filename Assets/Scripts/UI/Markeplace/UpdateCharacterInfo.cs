using UnityEngine;
using TMPro;
using Zenject;

public class UpdateCharacterInfo : MonoBehaviour
{
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private TMP_Text _levelText;
    [SerializeField] private TMP_Text _maxLevelText;
    [SerializeField] private TMP_Text _priceText;

    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private TMP_Text _defendText;
    [SerializeField] private TMP_Text _speedMovementText;
    [SerializeField] private TMP_Text _meleeDamageText;
    [SerializeField] private TMP_Text _rangeDamageText;

    [SerializeField] private TMP_Text _healthModifier;
    [SerializeField] private TMP_Text _defendModifier;
    [SerializeField] private TMP_Text _spdMoveModifier;
    [SerializeField] private TMP_Text _meleeDamageModifier;
    [SerializeField] private TMP_Text _rangeDamageModifier;

    private CharacterContainer _tempCharacter;

    [Inject] private CharacterControllerMarket _characterControllerMarket;
    [Inject] private UpgradeCharacter _upgradeCharacter;
    [Inject] private CharacterList _characterList;
    [Inject] private BuyCharacter _buyCharacter;

    private void Start() => TakeCharacterContainer();

    private void TakeCharacterContainer() 
    {
        _tempCharacter = _characterControllerMarket.CharacterGetter();
        UpdateInfo();
    } 
    private void UpdateInfo()
    {
        _nameText.text = $"{_tempCharacter.Character.name}";
        _levelText.text = $"{_tempCharacter.Character.level}"; 
        _maxLevelText.text = $"{_tempCharacter.Character.maxLevel}";
        _priceText.text = $"{_tempCharacter.Character.price}";

        _healthText.text = $"{_tempCharacter.Character.health}";
        _defendText.text = $"{_tempCharacter.Character.defend}";
        _speedMovementText.text = $"{_tempCharacter.Character.speedMovement}";
        _meleeDamageText.text = $"{_tempCharacter.Character.meleeDamage}";
        _rangeDamageText.text = $"{_tempCharacter.Character.rangeDamage}";

        UpgradeInfo();
    }

    private void UpgradeInfo()
    {
        if(_tempCharacter.Character.isBuyed)
        {
            int currentLevel = _tempCharacter.Character.level;

            _priceText.text = $"{_tempCharacter.Character.upgradePrice[currentLevel]}";

            _healthModifier.text = $"{"(+" + _tempCharacter.Character.healthModifier[currentLevel] + ")"}";
            _defendModifier.text = $"{"(+" + _tempCharacter.Character.defendModifier[currentLevel] + ")"}";
            _spdMoveModifier.text = $"{"(+" + _tempCharacter.Character.spdMoveModifier[currentLevel] + ")"}";
            _meleeDamageModifier.text = $"{"(+" + _tempCharacter.Character.meleeDamageModifier[currentLevel] + ")"}";
            _rangeDamageModifier.text = $"{"(+" + _tempCharacter.Character.rangeDamageModifier[currentLevel] + ")"}";
        }
        else
        {
            _healthModifier.text = null;
            _defendModifier.text = null;
            _spdMoveModifier.text = null;
            _meleeDamageModifier.text = null;
            _rangeDamageModifier.text = null;
        }
    }

    private void OnEnable()
    {
        _characterList.OnUpdateInfo += TakeCharacterContainer;
        _upgradeCharacter.OnWasUpgraded += UpdateInfo; 
        _buyCharacter.onBuyCharacter += UpgradeInfo;
    } 
    private void OnDisable() 
    {
        _characterList.OnUpdateInfo -= TakeCharacterContainer;
        _upgradeCharacter.OnWasUpgraded -= UpdateInfo;
        _buyCharacter.onBuyCharacter -= UpgradeInfo;

    } 
}

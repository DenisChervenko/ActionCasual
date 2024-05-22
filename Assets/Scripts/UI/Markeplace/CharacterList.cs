using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CharacterList : MonoBehaviour
{
    public delegate void OnCharacterChange(bool state);
    public OnCharacterChange onCharacterChange;

    public UnityAction OnUpdateInfo;
    public UnityAction OnUpdateButton;

    private int _currentIndexOfCharacter;
    public int CurrentIndexOfCharacter {get {return _currentIndexOfCharacter;}}

    [Inject] private CharacterControllerMarket _characterControllerMarket;
    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;

    private void Start()
    {
        _currentIndexOfCharacter = _playerInfoBehaviour.GetSelectedCharacterIndex();
        onCharacterChange.Invoke(true);
    }

    public void ChangeCurrentCharacter(bool toNextCharacter) 
    {
        onCharacterChange?.Invoke(false);

        _currentIndexOfCharacter += toNextCharacter ? 1 : -1;

        if(_currentIndexOfCharacter == _characterControllerMarket.CharacterContainers)
            _currentIndexOfCharacter = 0;
        else if(_currentIndexOfCharacter < 0)
            _currentIndexOfCharacter = _characterControllerMarket.CharacterContainers -1;

        onCharacterChange?.Invoke(true);
        OnUpdateInfo?.Invoke();
        OnUpdateButton?.Invoke();
    } 
}

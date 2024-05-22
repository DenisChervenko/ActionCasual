using UnityEngine.Events;
using UnityEngine;
using Zenject;

public class CharacterSelect : MonoBehaviour
{
    public UnityAction onCharacterSelected;

    [Inject] private CharacterList _characterList;
    [Inject] private PlayerInfoBehaviour _playerInfoBehaviour;

    public void OnSelectCharacter()
    {
        _playerInfoBehaviour.SetSelectedCharacterIndex(_characterList.CurrentIndexOfCharacter);
        onCharacterSelected?.Invoke();
    }
}

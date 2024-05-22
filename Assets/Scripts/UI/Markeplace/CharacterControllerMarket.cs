using UnityEngine;
using Zenject;
public class CharacterControllerMarket : MonoBehaviour
{
    [SerializeField] private CharacterContainer[] _characters;
    public int CharacterContainers { get { return _characters.Length; } }

    [Inject] private CharacterList _characterList;

    public void StateController(bool state)
    {
        _characters[_characterList.CurrentIndexOfCharacter].StateCharacter(state);
    }   

    public CharacterContainer CharacterGetter()
    {
        return _characters[_characterList.CurrentIndexOfCharacter];
    }

    private void OnEnable() => _characterList.onCharacterChange += StateController;
    private void OnDisable() => _characterList.onCharacterChange -= StateController;
}

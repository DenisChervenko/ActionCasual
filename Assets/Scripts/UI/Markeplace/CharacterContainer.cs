using UnityEngine;

public class CharacterContainer : MonoBehaviour
{
    [SerializeField] private Character _character;
    public Character Character { get { return _character; } }
    public void StateCharacter(bool state) => gameObject.SetActive(state);
}

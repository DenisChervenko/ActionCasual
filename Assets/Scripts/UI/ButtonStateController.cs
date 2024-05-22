using UnityEngine;
using UnityEngine.UI;

public class ButtonStateController : MonoBehaviour
{
    [SerializeField] private Button _attackButton;
    [SerializeField] private Button _stateCombatChange;
    [SerializeField] private Button _shiftButton;

    public void OnAttackButtonState(bool state) => _attackButton.interactable = state;
    public void OnStateCombatChange(bool state) => _stateCombatChange.interactable = state;
    public void OnShiftButton(bool state) => _shiftButton.interactable = state;
}

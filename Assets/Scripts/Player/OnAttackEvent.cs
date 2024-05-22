using UnityEngine;
using UnityEngine.Events;

public class OnAttackEvent : MonoBehaviour
{
    public UnityAction _onMeleeAttackCallback;
    public UnityAction _onRangeAttackCallback;

    public void OnMeleeAttack() => _onMeleeAttackCallback?.Invoke();
    public void OnRangeAttack() => _onRangeAttackCallback?.Invoke();
}

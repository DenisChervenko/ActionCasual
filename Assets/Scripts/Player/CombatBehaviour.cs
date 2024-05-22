using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Zenject;

public class CombatBehaviour : MonoBehaviour
{   
    public CombatState _combatState;
    public enum CombatState
    {
        Melee,
        Range
    }

    public UnityAction _onStateChangeCallback;

    [Header("Combat State Change")]
    [Space()]
    [SerializeField] private float _changeButtonCooldown;
    private float _buttonElapsedTime;

    [Header("Combo properties melee")]
    [Space()]
    [SerializeField] private int _comboCounter;
    [SerializeField] private float _comboDuration;
    private bool _comboStart = false;
    private float _comboElapsedTime;

    private Animator _animator;
    [Inject] ButtonStateController buttonStateController;

    private void Start() => _animator = gameObject.GetComponentInChildren<Animator>();
        
    public void OnStateChange()
    {
        _combatState = _combatState == 0 ? CombatState.Range : CombatState.Melee;
        buttonStateController.OnStateCombatChange(false);
        
        _onStateChangeCallback.Invoke();

        ResetComboValue();
        StartCoroutine(ButtonCooldown());
    }

    public void OnAttackButton()
    {
        buttonStateController.OnAttackButtonState(false);

        _animator.SetLayerWeight(_combatState == 0 ? 1 : 2, 1);
        _animator.SetLayerWeight(_combatState == 0 ? 2 : 1, 0);
        
        if(_combatState == CombatState.Melee)
        {
            _comboCounter++;
            if(_comboCounter > 4)
                _comboCounter = 1;

            _animator.SetInteger("ComboMelee", _comboCounter);
        }
        else
        {
            _animator.SetTrigger("AttackRange");
        }

        if(!_comboStart && _combatState == CombatState.Melee)
        {
            StartCoroutine(CombooDuration());
            _comboStart = true;
        }
    }

    public void OnComboStateRefresh() => _comboElapsedTime = 0;

    private void ResetComboValue()
    {
        _comboStart = false;
        _comboElapsedTime = 0;
        _comboCounter = 0;
        _animator.SetInteger("ComboMelee", _comboCounter);
    }

    private IEnumerator CombooDuration()
    {
        while(true)
        {
            yield return null;
            _comboElapsedTime += Time.deltaTime;

            if(_comboElapsedTime >= _comboDuration)
            {
                ResetComboValue();
                yield break;
            }
        }
    }

    private IEnumerator ButtonCooldown()
    {
        while(true)
        {
            yield return null;
            _buttonElapsedTime += Time.deltaTime;

            if(_buttonElapsedTime >= _changeButtonCooldown)
            {
                buttonStateController.OnStateCombatChange(true);
                _buttonElapsedTime = 0;
                yield break;
            }
        }
    }
}
 
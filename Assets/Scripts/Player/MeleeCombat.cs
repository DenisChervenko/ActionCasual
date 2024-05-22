using System.Collections;
using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MeleeCombat : MonoBehaviour
{
    [SerializeField] private CapsuleCollider[] _enemyInRange = new CapsuleCollider[10];

    [Space()]
    [SerializeField] private float _attackRange;
    [SerializeField] private float _damageRate;

    [Space()]
    [SerializeField] private LayerMask _layer;

    [Header("Cooldown")]
    [Space()]
    [SerializeField] private Button _attackButton;
    [SerializeField] private float _cooldownTime;
    private float _elapsedTime;

    private OnAttackEvent onAttackEvent;
    [Inject] ButtonStateController buttonStateController;

    private void Start()
    {
        onAttackEvent = GetComponentInChildren<OnAttackEvent>();
        onAttackEvent._onMeleeAttackCallback += OnAttackMelee;
    }

    private void OnAttackMelee()
    {
        Array.Clear(_enemyInRange, 0, _enemyInRange.Length);

        int countEnemy = Physics.OverlapSphereNonAlloc(gameObject.transform.position + gameObject.transform.forward * _attackRange, _attackRange, _enemyInRange, _layer);
        
        if(countEnemy != 0)
        {
            foreach(Collider enemy in _enemyInRange)
            {
                if(enemy == null)
                continue;

                IDamagable damagable = enemy.GetComponent<IDamagable>();

                if(damagable != null)
                    damagable.TakeDamage(_damageRate);
            }
        }

        StartCoroutine(CooldownReset());
    }

    private IEnumerator CooldownReset()
    {
        while(true)
        {
            yield return null;
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= _cooldownTime)
            {
                buttonStateController.OnAttackButtonState(true);
                _elapsedTime = 0;
                yield break;
            }
        }
    }

    private void OnButtonState(bool state) => _attackButton.interactable = state;

    private void  OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward * _attackRange, _attackRange);
    }
    private void OnEnable()
    {
        if(onAttackEvent == null)
            return;

        if(onAttackEvent._onMeleeAttackCallback  == null)
            onAttackEvent._onMeleeAttackCallback += OnAttackMelee;
    } 
    private void OnDisable() => onAttackEvent._onMeleeAttackCallback -= OnAttackMelee;
}

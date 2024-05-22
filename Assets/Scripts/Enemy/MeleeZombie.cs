using System;
using UnityEngine;

public class MeleeZombie : EnemyBase
{
    [Header("Combat")]

    [SerializeField] protected CharacterController[] _player;
    [SerializeField] protected float _attackRange;
    [SerializeField] protected LayerMask _layerMask;

    private CombatZone _combatZone;
    public override void Attack() => _animator.SetTrigger("Attack"); 


    private void PlayerDetected() => _inCombat = true;
    private void PlayerLeave() => _inCombat = false;

//using on animation event
    public void OnSubAttack()
    {
        int count = Physics.OverlapSphereNonAlloc(gameObject.transform.position + gameObject.transform.forward + gameObject.transform.up, _attackRange, _player,_layerMask);

        if (count == 0)
            return;

        foreach(Collider collider in _player)
        {
            IDamagable damagable = collider?.GetComponent<IDamagable>();
            if(damagable != null)
                damagable.TakeDamage(_attackPower);
        }
    }

    private void  OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward + gameObject.transform.up * _attackRange, _attackRange);
    }

    new private void OnEnable()
    {
        base.OnEnable();

        if(_combatZone == null)
            _combatZone = GetComponentInChildren<CombatZone>();

        _combatZone._onPlayerDetected += PlayerDetected;
        _combatZone._onPlayerLeave += PlayerLeave;
    }

    private void OnDisable()
    {
        _combatZone._onPlayerDetected -= PlayerDetected;
        _combatZone._onPlayerLeave -= PlayerLeave;
    }
}

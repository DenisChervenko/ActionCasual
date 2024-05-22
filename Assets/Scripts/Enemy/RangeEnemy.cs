using UnityEngine;
using System;
using System.Collections.Generic;
public class RangeEnemy : EnemyBase
{
    [SerializeField] ParticleSystem _attackTrail;
    
    [SerializeField] FireballProjectile _fireballPrefab;
    [SerializeField] private List<FireballProjectile> _fireballProjectiles;
    [SerializeField] private Transform _projectileMuzzle;

    [SerializeField] private int _countProjectile;
    private int _currentTurnProjectile;

    private CombatZone _rangeCombatZone;

    new private void Start()
    {
        base.Start();

        for(int i = 0; i < _countProjectile; i++)
        {
            _fireballProjectiles.Add(Instantiate(_fireballPrefab));
            _fireballProjectiles[i].StateProjectile(false);
        }
    }

    public override void Attack() => _animator.SetTrigger("Attack");

    public void PlayerDetected() => _inCombat = true;
    public void PlayerLeave() => _inCombat = false;


    public void ProjectileCast()
    {
        _fireballProjectiles[_currentTurnProjectile].transform.position = _projectileMuzzle.position;
        _fireballProjectiles[_currentTurnProjectile].transform.rotation = gameObject.transform.rotation;

        _fireballProjectiles[_currentTurnProjectile].StateProjectile(true);

        Debug.Log(_currentTurnProjectile);

        _currentTurnProjectile++;
        if(_currentTurnProjectile >= _fireballProjectiles.Count)
            _currentTurnProjectile = 0;
    }
    public void OnEnableTrail() => _attackTrail.Play();
    public void OnDisableTrail() => _attackTrail.Stop();

    new private void OnEnable()
    {
        base.OnEnable();

        if(_rangeCombatZone == null)
            _rangeCombatZone = GetComponentInChildren<CombatZone>();

        _rangeCombatZone._onPlayerDetected += PlayerDetected;
        _rangeCombatZone._onPlayerLeave += PlayerLeave;
    }

    private void OnDisable()
    {
        _rangeCombatZone._onPlayerDetected -= PlayerDetected;
        _rangeCombatZone._onPlayerLeave -= PlayerLeave;
    }
}

using System;
using UnityEngine;

public class EnemyDetected : MonoBehaviour
{
    [SerializeField] private Collider[] _enemyDetected;
    [SerializeField] private int _enemyDetectedSize;

    [SerializeField] private float _radiusSearch;
    [SerializeField] private LayerMask _enemyLayer;

    private float _closetEnemyDistance = Mathf.Infinity;

    private Transform _targetEnemy;

    #region Singleton
    public static EnemyDetected _instance;
    private void Awake()
    {
        if(_instance == null)
            _instance = this;
    }
    
    #endregion

    private void Start() => _enemyDetected = new Collider[_enemyDetectedSize];

    private void Update()
    {
        Array.Clear(_enemyDetected, 0, _enemyDetected.Length);

        int num = Physics.OverlapSphereNonAlloc(gameObject.transform.position, _radiusSearch, _enemyDetected, _enemyLayer);
        if(num != 0)
        {
            foreach(Collider potentialEnemy in _enemyDetected)
            {
                if(potentialEnemy == null)
                    continue;

                //get potential target position
                Vector3 distance = potentialEnemy.transform.position - gameObject.transform.position;
                float sqrToTarget = distance.sqrMagnitude;
                if(sqrToTarget < _closetEnemyDistance)
                {
                    _closetEnemyDistance = sqrToTarget;
                    _targetEnemy = potentialEnemy.transform;
                }

                //update position of current target
                if(_closetEnemyDistance != Mathf.Infinity)
                {
                    Vector3 currentEnemyDistance = _targetEnemy.transform.position - gameObject.transform.position;
                    float sqrEnemyDistance = currentEnemyDistance.sqrMagnitude;
                    _closetEnemyDistance = sqrEnemyDistance;
                }
            }

            if(_targetEnemy != null)
                gameObject.transform.LookAt(new Vector3(_targetEnemy.position.x, gameObject.transform.position.y, _targetEnemy.position.z));
            else
                OnTargetUpdate();
        }
    }

    public void OnTargetUpdate()
    {
        _targetEnemy = null;
        _closetEnemyDistance = Mathf.Infinity;
    }
}

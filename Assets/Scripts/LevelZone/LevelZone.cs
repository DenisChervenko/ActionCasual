using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class LevelZone : MonoBehaviour
{
    [Header("Enemy")]
    [Space()]
    [SerializeField] private GameObject[] _prefabEnemy;
    [SerializeField] private int _countEnemy;// if 10 if 20 if 30
    [SerializeField] private int _maxEnemyAtOnce; // then 2 then 4 then 6
    [SerializeField] private List<EnemyBase> _enemyBase;
    [SerializeField] private List<GameObject> _enemySpawned;
    private int _killedEnemyCount;
    private int _turnSpawnEnemy;

    [Header("EnemyVariationCount")]
    [Space()]
    [SerializeField] private int _minionCount;
    [SerializeField] private int _rogueCount;
    [SerializeField] private int _warriorCount;
    [SerializeField] private int _mageCount;
    

    [Header("LevelElement")]
    [Space()]
    [SerializeField] private GameObject _door;
    [SerializeField] private Transform[] _spawnPoint;
    [SerializeField] private GameObject _collidersEnter;
    [SerializeField] private GameObject _collidersExit;

    [Inject] private CounterUpdate _counterUpdate;
    [Inject] private LevelCompleteRequirement _levelCompleteRequirement;

    private void CloseLevel() => _door.transform.DOMoveY(0, 0.2f).OnComplete(() => EnemyState());
    private void OpenLevel() => _door.transform.DOMoveY(-4, 0.2f);


    private void EnemyState()
    {
        int spawnPointIndex = 0;

        for(int i = 0; i < _maxEnemyAtOnce; i++)
        {
            _enemySpawned[_turnSpawnEnemy].transform.position = _spawnPoint[spawnPointIndex].position;
            _enemySpawned[_turnSpawnEnemy].SetActive(true);
            spawnPointIndex++;
            _turnSpawnEnemy++;
        }
    }

    private void OnEnemyKilled()
    {
        _killedEnemyCount++;
        _counterUpdate.UpdateCount(_countEnemy, _killedEnemyCount);

        if(_killedEnemyCount >= _countEnemy)
        {
            OpenLevel();
            return;
        }
            
        if(_killedEnemyCount % _maxEnemyAtOnce == 0)
            EnemyState();
    }

    public void EnterTriggerZone()
    {
        CloseLevel();
        _counterUpdate.CanvasStateChanger();

        _collidersEnter.SetActive(false);
        _collidersExit.SetActive(true);
    }

    public void ExitTriggerZone()
    {
        _counterUpdate.CanvasStateChanger();
        _collidersExit.SetActive(false);
    }

    private void OnEnable()
    {
        for(int j = 0; j < _minionCount; j++)
            _enemySpawned.Add(Instantiate(_prefabEnemy[0]));
        for(int k = 0; k < _rogueCount; k++)
            _enemySpawned.Add(Instantiate(_prefabEnemy[1]));
        for(int o = 0; o < _warriorCount; o++)
            _enemySpawned.Add(Instantiate(_prefabEnemy[2]));
        for(int u = 0; u < _mageCount; u++)
            _enemySpawned.Add(Instantiate(_prefabEnemy[3]));

        for(int i = 0; i < _countEnemy; i++)
        {
            _enemySpawned[i].SetActive(false);
            _enemyBase.Add(_enemySpawned[i].GetComponent<EnemyBase>());
        }

        foreach(var enemy in _enemyBase)
        {
            enemy.isDead += OnEnemyKilled;
        }

        _levelCompleteRequirement.SetMaxCountEnemy(_countEnemy);
    }

    private void OnDisable()
    {
        foreach(var enemy in _enemyBase)
        {
            enemy.isDead -= OnEnemyKilled;
        }
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RangeCombat : MonoBehaviour
{   
    [Header("Pool object")]
    [Space()]
    [SerializeField] private GameObject[] _pooledObject = new GameObject[20];
    [SerializeField] private GameObject _arrowForWeapon;
    private int _currentPoolTurn;

    [Header("Cooldown")]
    [Space()]
    [SerializeField] private float _attackCooldown;
    [SerializeField] private Button _attackButton;
    private float _elapsedTime;

    private OnAttackEvent onAttackEvent;

    private void Start()
    {
        onAttackEvent = GetComponentInChildren<OnAttackEvent>();
        onAttackEvent._onRangeAttackCallback += OnAttackRange;

        for(int i = 0; i < _pooledObject.Length; i++)
        {
            _pooledObject[i] = Instantiate(_arrowForWeapon);
            _pooledObject[i].SetActive(false);
        }
    }

    private void OnAttackRange()
    {
        _pooledObject[_currentPoolTurn].SetActive(true);
        _pooledObject[_currentPoolTurn].transform.position = gameObject.transform.position;
        _pooledObject[_currentPoolTurn].transform.rotation = gameObject.transform.rotation;
        
        _currentPoolTurn++;

        if(_currentPoolTurn > _pooledObject.Length - 1)
            _currentPoolTurn = 0;

        OnButtonState(false);
        StartCoroutine(AttackCooldown());
    }

    private  IEnumerator AttackCooldown()
    {
        while(true)
        {
            yield return null;
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= _attackCooldown)
            {
                OnButtonState(true);
                _elapsedTime = 0;
                yield break;
            }
        }
    }

    private void OnButtonState(bool state) => _attackButton.interactable = state;
    private void OnEnable()
    {
        if(onAttackEvent == null)
            return;

        if(onAttackEvent._onRangeAttackCallback == null)
            onAttackEvent._onRangeAttackCallback += OnAttackRange;
    } 
    private void OnDisable() => onAttackEvent._onRangeAttackCallback -= OnAttackRange;
}
 
using UnityEngine.AI;
using UnityEngine;
using Zenject;
using System.Collections;
using DG.Tweening;
using UnityEngine.Events;

public abstract class EnemyBase : MonoBehaviour, IDamagable
{
    public UnityAction isDead;

    [Header("Base Stat")]
    [Space()]
    protected bool _inCombat = false;
    private bool _isDead = false;
    public float _health;
    public float _attackPower;

    [Header("Movement")]
    [Space()]
    [SerializeField] private bool _canMove = true;
    [SerializeField] private float _movementCooldown;
    [SerializeField] private float _resumeFollowDistance = 2;//the value should be 2.5 times greater than the stopping distance at navMeshAgent
    private float _elapsedTime;
    protected bool _cooldownStart = false;

    [Header("Animation")]
    [Space()]
    protected Animator _animator;
    private float _blendSpeed = 8;
    private float _lerpTime;
    private int _animatorHash;

    [Header("Damage impact")]
    [Space()]
    [SerializeField] private ParticleSystem _damageImpact;
    [SerializeField] private float _durationScale;
    [SerializeField] private float _strenghtScale;
    [SerializeField] private int _vibrationScale;

    [Header("State Change Impact")]
    [Space()]
    [SerializeField] private ParticleSystem _enableEffect;
    [SerializeField] private GameObject _rig;

    [SerializeField] private float _timeScale;

    private CapsuleCollider _capsuleCollider;
    private NavMeshAgent _agent;
    private Transform _playerPosition;

    EnemyDetected enemyDetected;

    protected void Start()
    {
        enemyDetected = EnemyDetected._instance;
        isDead += enemyDetected.OnTargetUpdate;

        _capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        _agent = gameObject.GetComponent<NavMeshAgent>();

        _animatorHash = Animator.StringToHash("MovementBlend");
        _playerPosition = GameObject.Find("Character").GetComponent<Transform>();
    }

    private void Update()
    {
        Movement();
    }

    public virtual void Movement()
    {
        if(!_isDead)
        {
            gameObject.transform.LookAt(new Vector3(_playerPosition.position.x, gameObject.transform.position.y, _playerPosition.position.z));

            if(_canMove)
            {   
                _agent.SetDestination(_playerPosition.position);
                AnimationBlend(2);//must be highest then max value from blend tree

                _elapsedTime = 0;
            }

            if((_playerPosition.position - gameObject.transform.position).sqrMagnitude <= _resumeFollowDistance)
            {
                _canMove = false;
                AnimationBlend(-1);//must be lowest then min value from blend tree

                if(_inCombat)
                {
                    AnimatorWeightController(1, 1, false);
                    Attack();
                }
            }

            if(!_canMove)
            {
                float distance = (_playerPosition.position - gameObject.transform.position).sqrMagnitude;

                if(distance >= _resumeFollowDistance)
                {
                    if(!_cooldownStart && !_inCombat)
                        StartCoroutine(CooldownMovement());
                }   
            }
        }
    }

    protected IEnumerator CooldownMovement()
    {
        _cooldownStart = true;

        while(true)
        {
            yield return null;
            _elapsedTime += Time.deltaTime;

            if(_elapsedTime >= _movementCooldown)
            {
                _animator.ResetTrigger("Attack");
                AnimatorWeightController(0, 1, false);
                _cooldownStart = false;
                _elapsedTime = 0;
                _canMove = true;
                yield break;
            }
        }
    }
    
    public void TakeDamage(float amount)
    {
        _health -= amount;

        gameObject.transform.DOShakeScale(_durationScale, _strenghtScale, _vibrationScale);
        _damageImpact.Play();

        if(_health <= 0)
            Die();
    }
    protected virtual void Die()
    {
        isDead?.Invoke();
        _isDead = true;
        _capsuleCollider.enabled = false;
        _agent.enabled = false;

        int animationIndex = Random.Range(1, 3);
        _animator.SetInteger("DeathIndex", animationIndex);

        AnimatorWeightController(2, 1, false);
        _rig.transform.DOScale(0, 0.6f).OnComplete(() => gameObject.SetActive(false));
    }

    private void AnimationBlend(float endValue)
    {
        _lerpTime = Mathf.Lerp(_lerpTime, endValue, Time.deltaTime * _blendSpeed);
        _animator.SetFloat(_animatorHash, Mathf.Clamp(_lerpTime, 0, 1));
    }

    private void AnimatorWeightController(int layer, float weight, bool resetAll)
    {
        if(resetAll)
        {
            layer = 0;
            while(layer != 3)
            {
                _animator.SetLayerWeight(layer, 0);
                layer++;
            }
        }

        if(layer != 2)
        {
            _animator.SetLayerWeight(layer, weight);
            _animator.SetLayerWeight(layer == 0 ? 1 : 0, 0);//weight always must be 0
        }
        else
        {
            int layerTemp = 0;
            while(layerTemp != 3)
            {
                _animator.SetLayerWeight(layerTemp, layerTemp == 2 ? 1 : 0);
                layerTemp++;
            }
        }
    }

    public abstract void Attack();
    public void OnEnable()
    {
        if(enemyDetected != null)
            isDead += enemyDetected.OnTargetUpdate;
        
        if(_animator == null)
            _animator = gameObject.GetComponent<Animator>();

        _isDead = false;
        if(_capsuleCollider != null)
            _capsuleCollider.enabled = true;
        AnimatorWeightController(0, 0, true);

        _enableEffect.Play();
        _rig.transform.DOScale(1, _timeScale);
    }

    private void OnDisable()
    {
        _rig.transform.DOScale(0.1f, 0);
        isDead -= enemyDetected.OnTargetUpdate;
    }
}

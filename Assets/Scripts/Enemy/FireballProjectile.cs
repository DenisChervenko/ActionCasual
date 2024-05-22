using UnityEngine;
using System.Collections;
using Unity.VisualScripting;
public class FireballProjectile : MonoBehaviour
{
    [Header("ParticleSystem")]
    [Space()]

    [SerializeField] private ParticleSystem _trailEffect;
    [SerializeField] private ParticleSystem _explosionImpact;

    [Header("BaseStats")]
    [Space()]

    [SerializeField] private float _speedMovement;
    [SerializeField] private float _damage;
    private bool _avaliableProjectile = true;
    public bool AvaliableProjectile { get { return _avaliableProjectile;}}

    [Header("Time")]
    [Space()]

    [SerializeField] private float _lifeTime;
    private float _elapsedTime;

    private Rigidbody _rb;
    private Collider _collider;

    private void Start()
    {
        _collider = gameObject.GetComponent<Collider>();
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() =>
        _rb.velocity = gameObject.transform.forward * _speedMovement * Time.fixedDeltaTime;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IDamagable damagable = other.GetComponent<IDamagable>();
            damagable.TakeDamage(_damage);
        }

        _collider.enabled = false;
        ExplosionProctile();
    }

    public void ExplosionProctile()
    {
        _trailEffect.Stop();
        _explosionImpact.Play();
    }

    private IEnumerator LifeTime()
    {
        while(true)
        {
            yield return null;

            _elapsedTime += Time.deltaTime;
            if(_elapsedTime >= _lifeTime)
            {
                _elapsedTime = 0;
                StateProjectile(false);
                yield break;
            }
        }
    }

    public void StateProjectile(bool state) => gameObject.SetActive(state);
    
    private void OnEnable()
    {
        _elapsedTime = 0;
        _avaliableProjectile = false;

        if(_collider != null)
            _collider.enabled = true;
        if(!_trailEffect.isPlaying)
            _trailEffect.Play();

        StartCoroutine(LifeTime());
    }

    private void OnDisable()
    {
        _avaliableProjectile = true;
        gameObject.transform.localPosition = Vector3.zero;
    } 
}

using UnityEngine;

public class ArrowBehaviour : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private float _speedMovement;
    private Rigidbody _rb;

    private void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate() =>
        _rb.velocity = gameObject.transform.forward * _speedMovement * Time.fixedDeltaTime;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();

        if(damagable != null)
            damagable.TakeDamage(_damage);
            
        gameObject.SetActive(false);
    }
}

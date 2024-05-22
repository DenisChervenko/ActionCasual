using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    [SerializeField] private float _damage;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damagable = other.GetComponent<IDamagable>();

        if(damagable != null)
            damagable.TakeDamage(_damage);
            
        gameObject.SetActive(false);
    }
}

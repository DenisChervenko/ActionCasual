using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [SerializeField] private MeshRenderer _meshRenderer;
    public void OnStateWeapon() => _meshRenderer.enabled = !_meshRenderer.enabled;
}

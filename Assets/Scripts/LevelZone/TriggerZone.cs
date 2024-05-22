using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    [SerializeField] private bool _isEnterTrigger;
    private LevelZone levelZone;

    private void Start() => levelZone = GetComponentInParent<LevelZone>();

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(_isEnterTrigger)
                levelZone.EnterTriggerZone();
            else
                levelZone.ExitTriggerZone();
        }
    }
}

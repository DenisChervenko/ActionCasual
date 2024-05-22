using UnityEngine;
using UnityEngine.Events;

public class CombatZone : MonoBehaviour
{
    public UnityAction _onPlayerDetected;
    public UnityAction _onPlayerLeave;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _onPlayerDetected.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
            _onPlayerLeave.Invoke();
    }
}

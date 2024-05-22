using System.Collections;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    [Header("Base movement value")]
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private FloatingJoystick _fixedJoystick;
    [SerializeField] private float _rotationSpeed;
    private float _speedMovement;

    [Header("Dash value")]
    [Space()]
    [SerializeField] private float _dashDistance;
    [SerializeField] private float _dashTime;
    [SerializeField] private float _dashCooldown;
    private float _dashColdownElapsedTime;
    private Vector3 _dashDirection;
    
    private Animator _animator;
    private int _animatorHash;

    [Inject] PlayerStats playerStats;
    [Inject] ButtonStateController buttonStateController;

    private void Start()
    {
        _animator = gameObject.GetComponentInChildren<Animator>();
        _animatorHash = Animator.StringToHash("AnimationBlend");

        _characterController = gameObject.GetComponent<CharacterController>();

        _speedMovement = playerStats.SpeedMovement;
    }

    private void FixedUpdate()
    {
        Vector3 move = Vector3.forward * _fixedJoystick.Vertical + Vector3.right * _fixedJoystick.Horizontal;
        _characterController.Move(move * _speedMovement * Time.deltaTime);

        if(move != Vector3.zero)
        {
            _animator.SetFloat(_animatorHash, move.magnitude);

            Quaternion rotation = Quaternion.LookRotation(move);
            _characterController.transform.rotation = Quaternion.Slerp(_characterController.transform.rotation, rotation, Time.deltaTime * _rotationSpeed);

            _dashDirection = move;
        }
        else
        {
            _animator.SetFloat(_animatorHash, 0);
        }
    }

    public void OnDashActive()
    {
        buttonStateController.OnShiftButton(false);

        if(_dashDirection == Vector3.zero)
            _dashDirection = gameObject.transform.forward;

        StartCoroutine(DashThread());
        StartCoroutine(DashCooldown());            
    }

    private IEnumerator DashThread()
    {
        float startTime = Time.time;
        while(Time.time < startTime + _dashTime)
        {
            _characterController.Move(_dashDirection * _dashDistance / _dashTime * Time.deltaTime);
            yield return null;
        }

        _dashDirection = Vector3.zero;
    }

    private IEnumerator DashCooldown()
    {
        while(true)
        {
            yield return null;

            _dashColdownElapsedTime += Time.deltaTime;
            if(_dashColdownElapsedTime > _dashCooldown)
            {
                _dashColdownElapsedTime = 0;
                buttonStateController.OnShiftButton(true);
                yield break;
            }
        }
    }
}

using UnityEngine;
using UnityEngine.Events;
using Zenject;
using DG.Tweening;

public class PlayerStats : MonoBehaviour, IDamagable
{
    public delegate void onGetDamageCallback(float damage);
    public onGetDamageCallback OnGetDamageCallback;

    public UnityAction playerIsDead;

    [SerializeField] private Character[] _characterBaseStat;
    [SerializeField] private PlayerInfo _playerInfo;
    [SerializeField] private GameObject[] _characters;
    private int _currentPlayer;

    [Header("Player stats")]
    [Space()]
    private int _currentLevel;
    public int CurrentLevel { get { return _currentLevel; } }
    private int _maxLevel;
    public int MaxLevel { get { return _maxLevel;}}

    private float _health;
    public float Health {get {return _health;}}
    private float _defend;
    public float Defend {get{return _defend;}}
    private float _speedMovement;
    public float SpeedMovement {get {return _speedMovement;}}

    private float _meleeDamage;
    public float MeleeDamage {get {return _meleeDamage;}}
    private float _rangeDamage;
    public float RangeDamage {get {return _rangeDamage;}}

    [Header("Weapon")]
    [Space()]
    [SerializeField] private Transform[] _rightHand;
    [SerializeField] private Transform[] _leftHand;
    private WeaponController _currentRightHand;
    private WeaponController _currentLeftHand;
    private WeaponController _currentRangeWeapon;

    [Header("DamageImpact")]
    [Space()]
    [SerializeField] private float _durationScale;
    [SerializeField] private float _strenghtScale;
    [SerializeField] private int _vibrationScale;

    [Inject] private CombatBehaviour _combatBehaviour;

    private void Awake()
    {
        _currentPlayer = _playerInfo.selectedCharacter;
        for(int i = 0; i < _characters.Length; i++)
        {
            if(i == _currentPlayer)
                continue;

            _characters[i].SetActive(false);
        }

        _health = _characterBaseStat[_currentPlayer].health;
        _defend = _characterBaseStat[_currentPlayer].defend;
        _speedMovement = _characterBaseStat[_currentPlayer].speedMovement;
        _meleeDamage = _characterBaseStat[_currentPlayer].meleeDamage;
        _rangeDamage = _characterBaseStat[_currentPlayer].rangeDamage;

        _currentLevel = _characterBaseStat[_currentPlayer].level;
        _maxLevel = _characterBaseStat[_currentPlayer].maxLevel;

        _currentRightHand = Instantiate(_currentLevel != _maxLevel ? 
        _characterBaseStat[_currentPlayer].defaultRightHand : _characterBaseStat[_currentPlayer].upgradedRightHand, _rightHand[_currentPlayer]);
        _currentLeftHand = Instantiate(_currentLevel != _maxLevel ? 
        _characterBaseStat[_currentPlayer].defaultLeftHand : _characterBaseStat[_currentPlayer].upgradedLeftHand, _leftHand[_currentPlayer]);
        _currentRangeWeapon = Instantiate(_currentLevel != _maxLevel ? 
        _characterBaseStat[_currentPlayer].defaultRangeWeapon : _characterBaseStat[_currentPlayer].upgradedRangeWeapon, _rightHand[_currentPlayer]);
        
        _currentRangeWeapon.OnStateWeapon();
    }
    
    private void ChangeWeapon()
    {
        _currentRightHand.OnStateWeapon();
        _currentLeftHand.OnStateWeapon();

        _currentRangeWeapon.OnStateWeapon();
    }

    public void TakeDamage(float damage)
    {
        damage /= _defend;
        _health -= damage;

        gameObject.transform.DOShakeScale(_durationScale, _strenghtScale, _vibrationScale);
        OnGetDamageCallback.Invoke(damage);

        if(_health <= 0)
            CharacterDie();
    }

    private void CharacterDie() => playerIsDead.Invoke();

    private void OnEnable() =>  _combatBehaviour._onStateChangeCallback += ChangeWeapon;
    private void OnDisable() => _combatBehaviour._onStateChangeCallback -= ChangeWeapon;
}

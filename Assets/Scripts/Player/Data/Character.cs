using UnityEngine;

[CreateAssetMenu(fileName = "Character_Name", menuName = "Data/Characters", order = 1)]
public class Character : ScriptableObject
{
    new public string name;

    public int level;
    public int maxLevel;

    public float health;
    public float defend;
    public float speedMovement;
    public float meleeDamage;
    public float rangeDamage;

    public WeaponController defaultRightHand;
    public WeaponController defaultLeftHand;
    public WeaponController upgradedRightHand;
    public WeaponController upgradedLeftHand;

    public WeaponController defaultRangeWeapon;
    public WeaponController upgradedRangeWeapon;

    public float[] healthModifier;
    public float[] defendModifier;
    public float[] spdMoveModifier;
    public float[] meleeDamageModifier;
    public float[] rangeDamageModifier;

    public int price;
    public bool priceInCoin;

    public bool isBuyed;
    public bool isUpgradeToMax;

    public int[] upgradePrice;
}

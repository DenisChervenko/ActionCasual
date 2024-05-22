using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade_Character_Name", menuName = "Data/UpgradeInfo", order = 4)]
public class CharacterUpgradeInfo : ScriptableObject
{
    public float healthModifier;
    public float defendModifier;
    public float speedMovementModifier;
    public float meleeDamageModifier;
    public float rangeDamageModifier;

    public int upgradePrice;
    public int updgradePriceModifier;
}

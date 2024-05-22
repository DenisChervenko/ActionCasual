using UnityEngine;

[CreateAssetMenu(menuName = "GeneralInfo", fileName = "Data/CharactersInfo", order = 2)]
public class CharacterTransfactionInfo : ScriptableObject
{
    public bool[] isBuyed;
    public bool[] isUpgradeMax;

    public int[] price;
    public bool[] isCoinPrice;

    public int indexSelectedCharacter;
}

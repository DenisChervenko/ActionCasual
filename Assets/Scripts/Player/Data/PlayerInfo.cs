using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Player", order = 3)]
public class PlayerInfo : ScriptableObject
{
    public string playerName;

    public int coinBalance;
    public int gemBalance;

    public int playerLevel;

    public int selectedCharacter;
}

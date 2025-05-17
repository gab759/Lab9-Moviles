using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayerData", menuName = "Game/Current Player Data")]
public class CurrentPlayerDataSO : ScriptableObject
{
    [Header("Datos de sesión")]
    public string playerName;
    public float survivalTime;
}

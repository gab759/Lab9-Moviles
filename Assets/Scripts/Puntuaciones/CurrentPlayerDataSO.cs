using UnityEngine;

[CreateAssetMenu(fileName = "CurrentPlayerData", menuName = "Game/Current Player Data")]
public class CurrentPlayerDataSO : ScriptableObject
{
    [Header("Datos de sesi�n")]
    public string playerName;
    public float survivalTime;
}

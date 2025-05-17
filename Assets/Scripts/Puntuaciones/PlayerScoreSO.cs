using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerScore", menuName = "Game/Player Score")]
public class PlayerScoreSO : ScriptableObject
{
    [Header("Datos del Jugador")]
    public string playerName;
    public float score;

    // (Opcional) M�todo para actualizar
    public void SetScore(string name, float newScore)
    {
        playerName = name;
        score = newScore;
    }
}

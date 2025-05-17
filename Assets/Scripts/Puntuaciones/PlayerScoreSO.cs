using UnityEngine;

[CreateAssetMenu(fileName = "NewPlayerScore", menuName = "Game/Player Score")]
public class PlayerScoreSO : ScriptableObject
{
    [Header("Datos del Jugador")]
    public string playerName;
    public int score;

    // (Opcional) Método para actualizar
    public void SetScore(string name, int newScore)
    {
        playerName = name;
        score = newScore;
    }
}

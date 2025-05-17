using UnityEngine;

[CreateAssetMenu(fileName = "ScoreDatabase", menuName = "Game/Score Database")]
public class ScoreDatabaseSO : ScriptableObject
{
    [Header("Top 5 Jugadores")]
    public PlayerScoreSO[] topScores = new PlayerScoreSO[5];

    public void AddNewScore(string name, int newScore)
    {
        // Creas una lista temporal para ordenarla
        var list = new System.Collections.Generic.List<PlayerScoreSO>(topScores);

        // Si el array aún tiene elementos nulos, crea nuevos SO en blanco
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i] == null)
            {
                list[i] = ScriptableObject.CreateInstance<PlayerScoreSO>();
                list[i].name = $"Slot{i + 1}";
                list[i].playerName = "---";
                list[i].score = 0;
            }
        }

        // Añade un PlayerScoreSO temporal para comparar
        var newEntry = ScriptableObject.CreateInstance<PlayerScoreSO>();
        newEntry.SetScore(name, newScore);
        list.Add(newEntry);

        // Ordena de mayor a menor
        list.Sort((a, b) => b.score.CompareTo(a.score));

        // Toma los primeros 5
        for (int i = 0; i < 5; i++)
            topScores[i] = list[i];
    }
}

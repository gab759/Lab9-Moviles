using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System.Linq;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField] private string UserID;
    [SerializeField] private CurrentPlayerDataSO currentPlayerData;
    [SerializeField] private ScoreDatabaseSO scoreDatabase;

    private DatabaseReference reference;

    private void Awake()
    {
        UserID = SystemInfo.deviceUniqueIdentifier;
    }

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void UploadPlayerScore()
    {
        string playerName = currentPlayerData.playerName;
        float score = currentPlayerData.survivalTime;

        PlayerScoreData newEntry = new PlayerScoreData(playerName, score);

        // Obtiene datos actuales de Firebase
        reference.Child("Puestos").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted || !task.IsCompleted) return;

            DataSnapshot snapshot = task.Result;

            // Lista temporal con todos los scores existentes
            List<PlayerScoreData> currentScores = new List<PlayerScoreData>();

            foreach (var child in snapshot.Children)
            {
                string name = child.Child("playerName").Value.ToString();
                float sc = float.Parse(child.Child("score").Value.ToString());

                currentScores.Add(new PlayerScoreData(name, sc));
            }

            // Añade nuevo score
            currentScores.Add(newEntry);

            // Ordena de mayor a menor y toma top 5
            var top5 = currentScores.OrderByDescending(s => s.score).Take(5).ToList();

            // Sube nuevamente los top 5 a Firebase bajo claves fijas
            for (int i = 0; i < top5.Count; i++)
            {
                string key = $"Ranking{i + 1}";
                string json = JsonUtility.ToJson(top5[i]);

                reference.Child("Puestos").Child(key).SetRawJsonValueAsync(json);
            }

            // También los guarda localmente en el ScriptableObject
            for (int i = 0; i < 5; i++)
            {
                if (i < top5.Count)
                    scoreDatabase.topScores[i].SetScore(top5[i].playerName, top5[i].score);
                else
                    scoreDatabase.topScores[i].SetScore("---", 0);
            }
        });
    }
}

[System.Serializable]
public class PlayerScoreData
{
    public string playerName;
    public float score;

    public PlayerScoreData(string name, float score)
    {
        this.playerName = name;
        this.score = score;
    }
}
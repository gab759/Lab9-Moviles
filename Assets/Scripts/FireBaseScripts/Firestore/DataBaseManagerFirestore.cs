using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Firestore;
using UnityEngine;

public class DataBaseManagerFirestore : MonoBehaviour
{
    [SerializeField] private CurrentPlayerDataSO currentPlayerData;
    [SerializeField] private ScoreDatabaseSO scoreDatabase;

    private FirebaseFirestore firestore;

    private void Start()
    {
        firestore = FirebaseFirestore.DefaultInstance;
    }

    public async void UploadPlayerScore()
    {
        string playerName = currentPlayerData.playerName;
        int score = Mathf.FloorToInt(currentPlayerData.survivalTime * 2);

        PlayerScoreNewData newEntry = new PlayerScoreNewData(playerName, score);

        CollectionReference rankingRef = firestore.Collection("rankings");

        await rankingRef.AddAsync(newEntry);

        QuerySnapshot snapshot = await rankingRef
            .OrderByDescending("score")
            .Limit(5)
            .GetSnapshotAsync();

        List<PlayerScoreNewData> top5 = new List<PlayerScoreNewData>();

        foreach (DocumentSnapshot doc in snapshot.Documents)
        {
            if (doc.Exists)
            {
                PlayerScoreNewData data = doc.ConvertTo<PlayerScoreNewData>();
                top5.Add(data);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (i < top5.Count)
                scoreDatabase.topScores[i].SetScore(top5[i].playerName, top5[i].score);
            else
                scoreDatabase.topScores[i].SetScore("---", 0);
        }
    }
}
using Firebase.Firestore;
using UnityEngine;

[FirestoreData]
public struct PlayerScoreNewData
{
    [FirestoreProperty]
    public string playerName { get; set; }

    [FirestoreProperty]
    public int score { get; set; }

    public PlayerScoreNewData(string name, int score)
    {
        this.playerName = name;
        this.score = score;
    }
}
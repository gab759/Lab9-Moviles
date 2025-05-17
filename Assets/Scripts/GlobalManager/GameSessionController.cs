using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSessionController : MonoBehaviour
{
    [Header("Datos de sesi�n")]
    [SerializeField] private CurrentPlayerDataSO currentPlayerData;
    [SerializeField] private ScoreDatabaseSO database;
    [Header("UI de puntuaci�n")]
    [SerializeField] private TMP_Text scoreText;   // Arrastra aqu� tu TextMeshProUGUI

    private float startTime;
    private bool isGameOver = false;
    private const float pointsPerSecond = 10f;    // Ajusta a tu gusto

    void Start()
    {
        startTime = Time.time;
        UpdateScoreUI(0);
    }

    void Update()
    {
        if (isGameOver) return;

        // Calcula puntuaci�n provisional
        float elapsed = Time.time - startTime;
        int currentScore = Mathf.FloorToInt(elapsed * pointsPerSecond);
        UpdateScoreUI(currentScore);
    }

    private void UpdateScoreUI(int score)
    {
        // Formatea como quieras: prefijo, sufijo, ceros�
        scoreText.text = $"Score: {score}";
    }

    // Llama a este m�todo cuando el jugador muera
    public void OnPlayerDeath()
    {
        isGameOver = true;

        // Calcula el tiempo final y la puntuaci�n
        float survivalTime = Time.time - startTime;
        currentPlayerData.survivalTime = survivalTime;
        int finalScore = Mathf.FloorToInt(survivalTime * pointsPerSecond);

        UpdateScoreUI(finalScore);

        database.AddNewScore(currentPlayerData.playerName, finalScore);
        SceneManager.LoadScene("Inicio");
    }

    
}

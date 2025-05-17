using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSessionController : MonoBehaviour
{
    [Header("Datos de sesión")]
    [SerializeField] private CurrentPlayerDataSO currentPlayerData;
    [SerializeField] private ScoreDatabaseSO database;
    [Header("UI de puntuación")]
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private GameObject Paneldeirmenu;

    private float startTime;
    private bool isGameOver = false;
    private const float pointsPerSecond = 2f;

    public void OnEnable()
    {
        ShipMovement.Muerte += OnPlayerDeath;
    }
    public void OnDisable()
    {
        ShipMovement.Muerte -= OnPlayerDeath;

    }

    void Start()
    {
        startTime = Time.time;
        UpdateScoreUI(0);
        Paneldeirmenu.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        float elapsed = Time.time - startTime;
        int currentScore = Mathf.FloorToInt(elapsed * pointsPerSecond);
        UpdateScoreUI(currentScore);
    }

    private void UpdateScoreUI(int score)
    {
        scoreText.text = $"Score: {score}";
    }

    public void OnPlayerDeath()
    {
        isGameOver = true;

        float survivalTime = Time.time - startTime;
        currentPlayerData.survivalTime = survivalTime;
        int finalScore = Mathf.FloorToInt(survivalTime * pointsPerSecond);

        UpdateScoreUI(finalScore);

        database.AddNewScore(currentPlayerData.playerName, finalScore);

        Paneldeirmenu.SetActive(true);

       
    }
    public void IrMenu()
    {
        SceneManager.LoadScene("Inicio");
    }

    
}

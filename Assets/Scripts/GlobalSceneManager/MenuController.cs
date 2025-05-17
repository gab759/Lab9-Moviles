using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("Referencias UI")]
    public GameObject panelResults;
    public TMP_Text[] resultTexts;           // 5 textos en orden
    [Header("Datos")]
    public ScoreDatabaseSO database;         // Asignar el asset

    // Llama en el botón “Jugar”
    public void OnPlayClicked()
    {
        SceneManager.LoadScene("MainGame"); // o nombre de tu escena de juego
    }

    // Llama en el botón “Resultados”
    public void OnResultsClicked()
    {
        panelResults.SetActive(true);
        UpdateResultsUI();
    }

    // Actualiza cada línea con nombre y score
    private void UpdateResultsUI()
    {
        for (int i = 0; i < database.topScores.Length; i++)
        {
            var entry = database.topScores[i];
            resultTexts[i].text = $"{i + 1}. {entry.playerName} — {entry.score}";
        }
    }

    // Botón “Cerrar Resultados” en el panel
    public void OnCloseResults()
    {
        panelResults.SetActive(false);
    }
    public void OnCloseAply()
    {
        Application.Quit();
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [Header("UI de Men�")]
    [SerializeField] private GameObject panelResults;
    [SerializeField] private GameObject panelEnterName;
    [SerializeField] private TMP_InputField inputNameField;
    [SerializeField] private TMP_Text[] resultTexts;

    [Header("Datos")]
    [SerializeField] private ScoreDatabaseSO database;
    [SerializeField] private CurrentPlayerDataSO currentPlayerData;

    private void Start()
    {
        //panelEnterName.SetActive(false);
    }
    public void OnPlayClickedPanel()
    {
        cambioJUGAR();
    }
    public void OnPlayClicked()
    {
        string name = inputNameField.text.Trim();
        if (string.IsNullOrEmpty(name))
        {
            // Podr�as mostrar un aviso �Introduce un nombre v�lido�
            return;
        }

        currentPlayerData.playerName = name;

      //  
    }

    // Llama en el bot�n �Resultados�
    public void OnResultsClicked()
    {
        panelResults.SetActive(true);
        UpdateResultsUI();
    
    }

    // Actualiza cada l�nea con nombre y score
    private void UpdateResultsUI()
    {
        for (int i = 0; i < database.topScores.Length; i++)
        {
            var entry = database.topScores[i];
            resultTexts[i].text = $"{i + 1}. {entry.playerName} � {entry.score}";
        }
    }

    // Bot�n �Cerrar Resultados� en el panel
    public void OnCloseResults()
    {
        panelResults.SetActive(false);
    }
    public void OnCloseAply()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
    public void cambioJUGAR()
    {
        SceneManager.LoadScene("MainGame"); // o nombre de tu escena de juego
    }
}

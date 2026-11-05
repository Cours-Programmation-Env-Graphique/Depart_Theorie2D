using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MainMenuUI : MonoBehaviour
{
    // Index de la scène de jeu dans la Scene List, juste après ce menu
    private const int GameSceneIndex = 1;

    [Header("Panneaux")]
    [Tooltip("Panneau principal, avec les boutons du menu")]
    [SerializeField] private GameObject _mainPanel;
    [Tooltip("Panneau des instructions")]
    [SerializeField] private GameObject _instructionsPanel;
    [Tooltip("Panneau des meilleurs pointages")]
    [SerializeField] private GameObject _highScoresPanel;

    [Header("Navigation")]
    [Tooltip("Bouton sélectionné à l'affichage du panneau principal")]
    [SerializeField] private GameObject _mainFirstSelected;
    [Tooltip("Bouton sélectionné à l'affichage des instructions")]
    [SerializeField] private GameObject _instructionsFirstSelected;
    [Tooltip("Bouton sélectionné à l'affichage des meilleurs pointages")]
    [SerializeField] private GameObject _highScoresFirstSelected;

    private void Start()
    {
        ShowMainPanel();
    }

    /// <summary>
    /// Lance une nouvelle partie.
    /// </summary>
    public void PlayGame()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    /// <summary>
    /// Affiche le menu principal.
    /// </summary>
    public void ShowMainPanel()
    {
        ShowPanel(_mainPanel, _mainFirstSelected);
    }

    /// <summary>
    /// Affiche les instructions.
    /// </summary>
    public void ShowInstructions()
    {
        ShowPanel(_instructionsPanel, _instructionsFirstSelected);
    }

    /// <summary>
    /// Affiche le tableau des meilleurs pointages.
    /// </summary>
    public void ShowHighScores()
    {
        ShowPanel(_highScoresPanel, _highScoresFirstSelected);
    }

    /// <summary>
    /// Ferme le jeu, ou arrête le mode Play dans l'éditeur.
    /// </summary>
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void ShowPanel(GameObject panel, GameObject firstSelected)
    {
        _mainPanel.SetActive(panel == _mainPanel);
        _instructionsPanel.SetActive(panel == _instructionsPanel);
        _highScoresPanel.SetActive(panel == _highScoresPanel);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}

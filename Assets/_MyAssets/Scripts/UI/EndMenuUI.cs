using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class EndMenuUI : MonoBehaviour
{
    // Index des scènes dans la Scene List
    private const int MainMenuSceneIndex = 0;
    private const int GameSceneIndex = 1;

    [Header("Panneaux")]
    [Tooltip("Panneau des résultats, avec le tableau des meilleurs pointages")]
    [SerializeField] private GameObject _resultsPanel;
    [Tooltip("Panneau de saisie du nom, pour un nouveau meilleur pointage")]
    [SerializeField] private GameObject _nameEntryPanel;
    [Tooltip("Panneau de saisie du mot de passe qui efface les meilleurs pointages")]
    [SerializeField] private GameObject _resetPanel;

    [Header("Affichage")]
    [Tooltip("Texte du pointage final")]
    [SerializeField] private TMP_Text _scoreText;
    [Tooltip("Tableau des meilleurs pointages")]
    [SerializeField] private HighScoreTableUI _highScoreTable;

    [Header("Navigation")]
    [Tooltip("Bouton sélectionné à l'affichage des résultats")]
    [SerializeField] private GameObject _resultsFirstSelected;
    [Tooltip("Bouton sélectionné à l'affichage de la saisie du nom")]
    [SerializeField] private GameObject _nameEntryFirstSelected;
    [Tooltip("Bouton sélectionné à l'affichage de la saisie du mot de passe")]
    [SerializeField] private GameObject _resetFirstSelected;

    [Header("Réinitialisation")]
    [Tooltip("Mot de passe qui efface les meilleurs pointages, en majuscules")]
    [SerializeField] private string _resetPassword = "RESET";

    private void Start()
    {
        int score = GameResults.FinalScore;
        _scoreText.text = $"Pointage : {score}";

        if (score > 0 && HighScoreStore.Qualifies(score))
        {
            ShowPanel(_nameEntryPanel, _nameEntryFirstSelected);
        }
        else
        {
            ShowResults();
        }
    }

    /// <summary>
    /// Enregistre le pointage de la partie sous le nom saisi, puis affiche les résultats.
    /// </summary>
    /// <param name="playerName">Nom saisi par le joueur.</param>
    public void SaveHighScore(string playerName)
    {
        HighScoreStore.Add(playerName, GameResults.FinalScore);
        ShowResults();
    }

    /// <summary>
    /// Affiche le panneau de saisie du mot de passe de réinitialisation.
    /// </summary>
    public void ShowResetPanel()
    {
        ShowPanel(_resetPanel, _resetFirstSelected);
    }

    /// <summary>
    /// Efface les meilleurs pointages si le mot de passe est le bon, puis affiche les résultats.
    /// </summary>
    /// <param name="password">Mot de passe saisi.</param>
    public void TryResetHighScores(string password)
    {
        if (password == _resetPassword)
        {
            HighScoreStore.Clear();
        }

        ShowResults();
    }

    /// <summary>
    /// Affiche le pointage final et le tableau des meilleurs pointages.
    /// </summary>
    public void ShowResults()
    {
        ShowPanel(_resultsPanel, _resultsFirstSelected);
        _highScoreTable.Refresh();
    }

    /// <summary>
    /// Lance une nouvelle partie.
    /// </summary>
    public void PlayAgain()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    /// <summary>
    /// Retourne au menu principal.
    /// </summary>
    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene(MainMenuSceneIndex);
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
        _resultsPanel.SetActive(panel == _resultsPanel);
        _nameEntryPanel.SetActive(panel == _nameEntryPanel);
        _resetPanel.SetActive(panel == _resetPanel);
        EventSystem.current.SetSelectedGameObject(firstSelected);
    }
}

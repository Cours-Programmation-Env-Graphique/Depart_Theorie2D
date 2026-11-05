using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PauseMenuUI : MonoBehaviour
{
    // Index du menu principal dans la Scene List
    private const int MainMenuSceneIndex = 0;

    [Header("Références")]
    [Tooltip("Source des entrées, pour le bouton de pause")]
    [SerializeField] private GameInput _gameInput;
    [Tooltip("Panneau affiché pendant la pause")]
    [SerializeField] private GameObject _pausePanel;
    [Tooltip("Bouton sélectionné à l'ouverture du menu")]
    [SerializeField] private GameObject _firstSelected;

    private bool _isPaused;

    private void Start()
    {
        _pausePanel.SetActive(false);
    }

    private void OnEnable()
    {
        _gameInput.PausePressed += HandlePausePressed;
    }

    private void OnDisable()
    {
        _gameInput.PausePressed -= HandlePausePressed;
    }

    /// <summary>
    /// Ferme le menu de pause et remet le temps du jeu en marche.
    /// </summary>
    public void Resume()
    {
        _isPaused = false;
        _pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Abandonne la partie en cours et retourne au menu principal.
    /// </summary>
    public void ReturnToMainMenu()
    {
        // timeScale survit au changement de scène
        Time.timeScale = 1f;
        SceneManager.LoadScene(MainMenuSceneIndex);
    }

    private void Pause()
    {
        _isPaused = true;
        _pausePanel.SetActive(true);
        Time.timeScale = 0f;
        EventSystem.current.SetSelectedGameObject(_firstSelected);
    }

    private void HandlePausePressed()
    {
        // Le même bouton ouvre et ferme le menu
        if (_isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }
}

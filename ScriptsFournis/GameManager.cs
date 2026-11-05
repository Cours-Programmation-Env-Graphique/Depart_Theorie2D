using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Difficulté")]
    [Tooltip("Points à accumuler pour passer au niveau de difficulté suivant")]
    [SerializeField] private int _pointsPerLevel = 200;
    [Tooltip("Hausse de la vitesse des ennemis à chaque niveau, en proportion de leur vitesse de base")]
    [SerializeField] private float _speedIncreasePerLevel = 0.1f;

    [Header("Fin de partie")]
    [Tooltip("Délai avant l'écran de fin, après la mort du joueur, en secondes")]
    [SerializeField] private float _endDelay = 2f;

    private int _score;
    private bool _isGameOver;

    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Déclenché quand le pointage change, avec le nouveau pointage.
    /// </summary>
    public static event Action<int> ScoreChanged;

    /// <summary>
    /// Niveau de difficulté actuel, qui commence à 0 et augmente avec le pointage.
    /// </summary>
    public int DifficultyLevel => _score / _pointsPerLevel;

    /// <summary>
    /// Facteur appliqué à la vitesse de base des ennemis selon la difficulté.
    /// </summary>
    public float SpeedMultiplier => 1f + DifficultyLevel * _speedIncreasePerLevel;

    private void Awake()
    {
        // Un gestionnaire par partie : il n'a pas à survivre à sa scène
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void OnEnable()
    {
        Enemy.Killed += HandleEnemyKilled;
        Player.Died += HandlePlayerDied;
    }

    private void OnDisable()
    {
        Enemy.Killed -= HandleEnemyKilled;
        Player.Died -= HandlePlayerDied;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void HandleEnemyKilled(int points)
    {
        // Après la mort du joueur, les lasers encore en vol ne comptent plus
        if (_isGameOver)
        {
            return;
        }

        _score += points;
        ScoreChanged?.Invoke(_score);
    }

    private void HandlePlayerDied()
    {
        _isGameOver = true;

        // Une classe statique survit au changement de scène
        GameResults.Record(_score);
        StartCoroutine(LoadEndSceneRoutine());
    }

    private IEnumerator LoadEndSceneRoutine()
    {
        // Le temps de voir l'explosion du vaisseau
        yield return new WaitForSeconds(_endDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}

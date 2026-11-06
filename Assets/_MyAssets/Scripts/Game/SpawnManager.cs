using System.Collections;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Hauteur d'apparition au-dessus de l'écran, en unités
    private const float SpawnMarginY = 1f;
    // Distance minimale aux bords gauche et droit, en unités
    private const float SpawnMarginX = 0.5f;

    [Header("Ennemis")]
    [Tooltip("Prefab de l'ennemi à créer")]
    [SerializeField] private Enemy _enemyPrefab;
    [Tooltip("Parent des ennemis créés, pour garder la Hierarchy lisible")]
    [SerializeField] private Transform _enemyContainer;
    [Tooltip("Délai avant le premier ennemi, en secondes")]
    [SerializeField] private float _firstEnemyDelay = 2f;
    [Tooltip("Délai entre deux ennemis, en secondes")]
    [SerializeField] private float _enemyDelay = 2f;

    /* BLOC 13-4.3 À ACTIVER : effacez cette ligne et la ligne de fin du bloc
    [Header("Power-ups")]
    [Tooltip("Power-ups, choisis au hasard")]
    [SerializeField] private PowerUp[] _powerUpPrefabs;
    [Tooltip("Délai minimal entre deux power-ups, en secondes")]
    [SerializeField] private float _minPowerUpDelay = 8f;
    [Tooltip("Délai maximal entre deux power-ups, en secondes")]
    [SerializeField] private float _maxPowerUpDelay = 15f;
    FIN DU BLOC 13-4.3 : effacez cette ligne */

    private float _halfWidth;
    private float _spawnY;
    private bool _isSpawning = true;

    private void OnEnable()
    {
        Player.Died += HandlePlayerDied;
    }

    private void OnDisable()
    {
        Player.Died -= HandlePlayerDied;
    }

    private void Start()
    {
        Camera mainCamera = Camera.main;
        float halfScreenWidth = mainCamera.orthographicSize * mainCamera.aspect;
        _halfWidth = halfScreenWidth - SpawnMarginX;
        _spawnY = mainCamera.orthographicSize + SpawnMarginY;

        // À COMPLÉTER 7.2 : lancer la coroutine des ennemis
        /* BLOC 13-4.3 À ACTIVER : effacez cette ligne et la ligne de fin du bloc
        StartCoroutine(SpawnPowerUpsRoutine());
        FIN DU BLOC 13-4.3 : effacez cette ligne */
    }

    // À COMPLÉTER 7.2 : la coroutine des vagues d'ennemis

    /* BLOC 13-4.3 À ACTIVER : effacez cette ligne et la ligne de fin du bloc
    private IEnumerator SpawnPowerUpsRoutine()
    {
        while (_isSpawning)
        {
            float delay = Random.Range(_minPowerUpDelay, _maxPowerUpDelay);
            yield return new WaitForSeconds(delay);

            // Le joueur a pu mourir pendant l'attente
            if (!_isSpawning)
            {
                yield break;
            }

            int index = Random.Range(0, _powerUpPrefabs.Length);
            PowerUp prefab = _powerUpPrefabs[index];
            Instantiate(prefab, GetRandomSpawnPosition(), Quaternion.identity);
        }
    }
    FIN DU BLOC 13-4.3 : effacez cette ligne */

    private void HandlePlayerDied()
    {
        // La boucle de la coroutine s'arrête à son prochain tour
        _isSpawning = false;
    }
}

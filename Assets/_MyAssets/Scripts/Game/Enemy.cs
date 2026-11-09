using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    // Marge sous le bas de l'écran avant la réapparition, en unités
    private const float OffScreenMargin = 1f;
    // Hauteur de réapparition au-dessus de l'écran, en unités
    private const float RespawnMarginY = 1f;
    // Distance minimale aux bords gauche et droit, en unités
    private const float RespawnMarginX = 0.5f;

    [Header("Déplacement")]
    [Tooltip("Vitesse de descente, en unités par seconde")]
    [SerializeField] private float _speed = 3f;

    [Header("Pointage")]
    [Tooltip("Points gagnés quand le joueur détruit cet ennemi")]
    [SerializeField] private int _points = 100;

    [Header("Effets")]
    [Tooltip("Explosion créée à la destruction de l'ennemi")]
    [SerializeField] private GameObject _explosionPrefab;

    private Rigidbody2D _rb;
    private float _limitY;
    private float _topY;
    private float _halfWidth;
    private bool _isDestroyed;

    /// <summary>
    /// Déclenché quand un laser du joueur détruit un ennemi, avec ses points.
    /// </summary>
    public static event Action<int> Killed;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rb.linearVelocity = Vector2.down * _speed;

        Camera mainCamera = Camera.main;
        _limitY = -(mainCamera.orthographicSize + OffScreenMargin);
        _topY = mainCamera.orthographicSize + RespawnMarginY;
        _halfWidth = mainCamera.orthographicSize * mainCamera.aspect - RespawnMarginX;
    }

    private void FixedUpdate()
    {
        // Sorti par le bas sans être détruit : il revient en haut
        if (_rb.position.y < _limitY)
        {
            float x = UnityEngine.Random.Range(-_halfWidth, _halfWidth);
            _rb.position = new Vector2(x, _topY);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Deux lasers peuvent toucher l'ennemi pendant la même image
        if (_isDestroyed)
        {
            return;
        }

        // À COMPLÉTER 6.2 : réagir au laser ou au joueur
    }

    private void Explode()
    {
        _isDestroyed = true;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

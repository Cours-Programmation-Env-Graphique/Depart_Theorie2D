using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    // Marge sous le bas de l'écran avant la destruction, en unités
    private const float OffScreenMargin = 1f;

    [Header("Déplacement")]
    [Tooltip("Vitesse de descente, en unités par seconde")]
    [SerializeField] private float _speed = 3f;

    [Header("Pointage")]
    [Tooltip("Points gagnés quand le joueur détruit cet ennemi")]
    [SerializeField] private int _points = 10;

    [Header("Effets")]
    [Tooltip("Explosion créée à la destruction de l'ennemi")]
    [SerializeField] private GameObject _explosionPrefab;

    private Rigidbody2D _rb;
    private float _limitY;
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
        _limitY = -(Camera.main.orthographicSize + OffScreenMargin);
    }

    private void FixedUpdate()
    {
        // Sorti par le bas de l'écran : le joueur l'a évité, sans le détruire
        if (_rb.position.y < _limitY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Deux lasers peuvent toucher l'ennemi pendant la même image
        if (_isDestroyed)
        {
            return;
        }

        // À COMPLÉTER 6.1 : réagir au laser ou au joueur
    }

    private void Explode()
    {
        _isDestroyed = true;
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

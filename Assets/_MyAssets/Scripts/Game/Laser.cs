using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Laser : MonoBehaviour
{
    // Marge au-delà du bord de l'écran avant la destruction, en unités
    private const float OffScreenMargin = 1f;

    [Tooltip("Vitesse du laser, en unités par seconde")]
    [SerializeField] private float _speed = 12f;
    [Tooltip("Direction : vers le haut pour le joueur, vers le bas pour un ennemi")]
    [SerializeField] private Vector2 _direction = Vector2.up;

    private Rigidbody2D _rb;
    private float _limitY;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        // À COMPLÉTER 5.1 : donner sa vitesse au laser
        _limitY = Camera.main.orthographicSize + OffScreenMargin;
    }

    private void FixedUpdate()
    {
        // Sorti de l'écran par le haut ou par le bas : il ne touchera plus rien
        if (Mathf.Abs(_rb.position.y) > _limitY)
        {
            Destroy(gameObject);
        }
    }
}

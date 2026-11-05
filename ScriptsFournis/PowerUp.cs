using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PowerUp : MonoBehaviour
{
    // Marge sous le bas de l'écran avant la destruction, en unités
    private const float OffScreenMargin = 1f;

    [Tooltip("Effet donné au joueur qui ramasse ce power-up")]
    [SerializeField] private PowerUpType _type;
    [Tooltip("Vitesse de descente, en unités par seconde")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Son joué quand le joueur ramasse le power-up")]
    [SerializeField] private AudioClip _pickupSound;

    private Rigidbody2D _rb;
    private float _limitY;

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
        if (_rb.position.y < _limitY)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.TryGetComponent<Player>(out Player player))
        {
            return;
        }

        player.ApplyPowerUp(_type);

        // Le power-up disparaît aussitôt : un objet temporaire joue le son,
        // placé à la caméra pour ne pas être atténué par la distance
        Vector3 cameraPosition = Camera.main.transform.position;
        AudioSource.PlayClipAtPoint(_pickupSound, cameraPosition);
        Destroy(gameObject);
    }
}

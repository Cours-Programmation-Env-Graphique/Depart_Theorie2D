using UnityEngine;

/// <summary>
/// Explosion animée et sonore, qui s'efface puis disparaît d'elle-même.
/// </summary>
public class Explosion : MonoBehaviour
{
    [Tooltip("Durée de vie, en secondes : au moins celle de l'animation et du son")]
    [SerializeField] private float _lifetime = 2.5f;
    [Tooltip("Vitesse de dérive vers le bas : les débris continuent leur course")]
    [SerializeField] private float _driftSpeed = 1.5f;
    [Tooltip("Délai avant le fondu, en secondes : vers la fin de l'animation")]
    [SerializeField] private float _fadeDelay = 0.6f;
    [Tooltip("Durée du fondu, en secondes")]
    [SerializeField] private float _fadeDuration = 0.4f;

    private SpriteRenderer _spriteRenderer;
    private float _startTime;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        _startTime = Time.time;
        // Destroy accepte un délai : aucune minuterie à écrire
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        // Aucun Rigidbody : on peut déplacer l'objet par son Transform
        transform.Translate(Vector3.down * (_driftSpeed * Time.deltaTime));

        // L'opacité passe de 1 à 0 pendant le fondu, puis reste à 0
        float fade = Mathf.Clamp01((Time.time - _startTime - _fadeDelay) / _fadeDuration);
        Color color = _spriteRenderer.color;
        color.a = 1f - fade;
        _spriteRenderer.color = color;
    }
}

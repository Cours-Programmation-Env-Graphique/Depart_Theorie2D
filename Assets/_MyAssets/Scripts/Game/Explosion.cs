using UnityEngine;

/// <summary>
/// Explosion qui joue son animation et son son, puis disparaît d'elle-même.
/// </summary>
public class Explosion : MonoBehaviour
{
    [Tooltip("Durée de vie, en secondes : au moins celle de l'animation et du son")]
    [SerializeField] private float _lifetime = 2.5f;
    [Tooltip("Vitesse de dérive vers le bas : les débris continuent leur course")]
    [SerializeField] private float _driftSpeed = 1.5f;

    private void Start()
    {
        // Destroy accepte un délai : aucune minuterie à écrire
        Destroy(gameObject, _lifetime);
    }

    private void Update()
    {
        // Aucun Rigidbody : on peut déplacer l'objet par son Transform
        transform.Translate(Vector3.down * (_driftSpeed * Time.deltaTime));
    }
}

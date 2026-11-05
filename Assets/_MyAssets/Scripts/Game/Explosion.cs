using UnityEngine;

/// <summary>
/// Explosion qui joue son animation et son son, puis disparaît d'elle-même.
/// </summary>
public class Explosion : MonoBehaviour
{
    [Tooltip("Durée de vie, en secondes : au moins celle de l'animation et du son")]
    [SerializeField] private float _lifetime = 1.5f;

    private void Start()
    {
        // Destroy accepte un délai : aucune minuterie à écrire
        Destroy(gameObject, _lifetime);
    }
}

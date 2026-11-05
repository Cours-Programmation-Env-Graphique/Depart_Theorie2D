using UnityEngine;

/// <summary>
/// Joue la musique de fond sans interruption d'une scène à l'autre.
/// </summary>
[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private void Awake()
    {
        // Au retour au menu, un deuxième apparaît : pas de musique en double
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}

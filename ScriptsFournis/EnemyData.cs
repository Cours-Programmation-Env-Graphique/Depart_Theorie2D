using UnityEngine;

/// <summary>
/// Caractéristiques d'un type d'ennemi, partagées par tous les ennemis de ce type.
/// </summary>
[CreateAssetMenu(fileName = "NewEnemyData", menuName = "Space Shooter/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Déplacement")]
    [Tooltip("Vitesse de descente de base, en unités par seconde")]
    [SerializeField] private float _speed = 3f;

    [Header("Pointage")]
    [Tooltip("Points gagnés quand le joueur détruit cet ennemi")]
    [SerializeField] private int _points = 10;

    [Header("Tir")]
    [Tooltip("Cet ennemi tire-t-il sur le joueur ?")]
    [SerializeField] private bool _canShoot;
    [Tooltip("Niveau de difficulté à partir duquel cet ennemi commence à tirer")]
    [SerializeField] private int _shootFromLevel;
    [Tooltip("Prefab du laser tiré par cet ennemi")]
    [SerializeField] private Laser _laserPrefab;
    [Tooltip("Délai minimal entre deux tirs, en secondes")]
    [SerializeField] private float _minFireDelay = 2f;
    [Tooltip("Délai maximal entre deux tirs, en secondes")]
    [SerializeField] private float _maxFireDelay = 4f;
    [Tooltip("Distance à laquelle l'ennemi voit le joueur, en unités")]
    [SerializeField] private float _sightDistance = 12f;

    public float Speed => _speed;
    public int Points => _points;
    public bool CanShoot => _canShoot;
    public int ShootFromLevel => _shootFromLevel;
    public Laser LaserPrefab => _laserPrefab;
    public float MinFireDelay => _minFireDelay;
    public float MaxFireDelay => _maxFireDelay;
    public float SightDistance => _sightDistance;
}

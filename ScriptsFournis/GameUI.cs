using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [Header("Textes")]
    [Tooltip("Texte du pointage")]
    [SerializeField] private TMP_Text _scoreText;
    [Tooltip("Texte du temps de jeu")]
    [SerializeField] private TMP_Text _timeText;

    [Header("Vies")]
    [Tooltip("Image qui montre les vies restantes")]
    [SerializeField] private Image _livesImage;
    [Tooltip("Une image par nombre de vies : l'élément 0 pour aucune vie")]
    [SerializeField] private Sprite[] _livesSprites;

    private void OnEnable()
    {
        GameManager.ScoreChanged += HandleScoreChanged;
        Player.LivesChanged += HandleLivesChanged;
    }

    private void OnDisable()
    {
        GameManager.ScoreChanged -= HandleScoreChanged;
        Player.LivesChanged -= HandleLivesChanged;
    }

    private void Start()
    {
        _scoreText.text = "Pointage : 0";
    }

    private void Update()
    {
        // Le temps change à chaque image : on le relit à chaque image
        float elapsed = Time.timeSinceLevelLoad;
        int minutes = (int)(elapsed / 60f);
        int seconds = (int)(elapsed % 60f);
        _timeText.text = $"{minutes:00}:{seconds:00}";
    }

    private void HandleScoreChanged(int score)
    {
        _scoreText.text = $"Pointage : {score}";
    }

    private void HandleLivesChanged(int lives)
    {
        // Un nombre de vies hors du tableau ne doit pas planter
        int index = Mathf.Clamp(lives, 0, _livesSprites.Length - 1);
        _livesImage.sprite = _livesSprites[index];
    }
}

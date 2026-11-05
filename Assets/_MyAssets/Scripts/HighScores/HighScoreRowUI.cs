using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Une ligne affichée du tableau des meilleurs pointages.
/// </summary>
public class HighScoreRowUI : MonoBehaviour
{
    [Header("Textes")]
    [Tooltip("Texte du rang")]
    [SerializeField] private TMP_Text _rankText;
    [Tooltip("Texte du nom")]
    [SerializeField] private TMP_Text _nameText;
    [Tooltip("Texte du pointage")]
    [SerializeField] private TMP_Text _scoreText;

    [Header("Podium")]
    [Tooltip("Fond de la ligne")]
    [SerializeField] private Image _background;
    [Tooltip("Couleurs de fond des premières places : or, argent, bronze")]
    [SerializeField] private Color[] _podiumColors;

    /// <summary>
    /// Remplit la ligne.
    /// </summary>
    /// <param name="rank">Rang, à partir de 1.</param>
    /// <param name="playerName">Nom du joueur.</param>
    /// <param name="score">Pointage.</param>
    public void SetValues(int rank, string playerName, int score)
    {
        _rankText.text = rank == 1 ? "1er" : $"{rank}e";
        _nameText.text = playerName;
        _scoreText.text = score.ToString();

        if (rank <= _podiumColors.Length)
        {
            _background.color = _podiumColors[rank - 1];
        }
    }
}

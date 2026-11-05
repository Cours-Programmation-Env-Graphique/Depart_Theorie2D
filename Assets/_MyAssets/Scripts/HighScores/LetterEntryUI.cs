using TMPro;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Saisie de texte par des boutons de lettres, à la manette, sans clavier.
/// </summary>
public class LetterEntryUI : MonoBehaviour
{
    [Header("Saisie")]
    [Tooltip("Texte qui affiche la saisie en cours")]
    [SerializeField] private TMP_Text _displayText;
    [Tooltip("Nombre maximal de caractères")]
    [SerializeField] private int _maxLength = 3;
    [Tooltip("Affiche des astérisques plutôt que les lettres, pour un mot de passe")]
    [SerializeField] private bool _isMasked;

    [Header("Validation")]
    [Tooltip("Méthodes appelées avec la saisie quand le joueur valide")]
    [SerializeField] private UnityEvent<string> _submitted;

    private string _input = "";

    private void OnEnable()
    {
        // Chaque affichage du panneau repart d'une saisie vide
        _input = "";
        RefreshDisplay();
    }

    /// <summary>
    /// Ajoute un caractère à la fin de la saisie, s'il reste de la place.
    /// </summary>
    /// <param name="character">Caractère du bouton enfoncé.</param>
    public void AddCharacter(string character)
    {
        if (_input.Length >= _maxLength)
        {
            return;
        }

        _input += character;
        RefreshDisplay();
    }

    /// <summary>
    /// Efface le dernier caractère saisi.
    /// </summary>
    public void RemoveLastCharacter()
    {
        if (_input.Length == 0)
        {
            return;
        }

        _input = _input.Substring(0, _input.Length - 1);
        RefreshDisplay();
    }

    /// <summary>
    /// Valide la saisie et la transmet aux méthodes abonnées.
    /// </summary>
    public void Submit()
    {
        // Une saisie vide n'est pas acceptée
        if (_input.Length == 0)
        {
            return;
        }

        _submitted.Invoke(_input);
    }

    private void RefreshDisplay()
    {
        if (_isMasked)
        {
            // Une étoile par caractère : le mot de passe ne s'affiche pas
            _displayText.text = new string('*', _input.Length);
        }
        else
        {
            _displayText.text = _input;
        }
    }
}

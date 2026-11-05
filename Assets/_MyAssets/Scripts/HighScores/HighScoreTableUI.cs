using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Affiche les meilleurs pointages, une ligne par pointage.
/// </summary>
public class HighScoreTableUI : MonoBehaviour
{
    [Tooltip("Parent des lignes, muni d'un Vertical Layout Group")]
    [SerializeField] private Transform _rowContainer;
    [Tooltip("Prefab d'une ligne du tableau")]
    [SerializeField] private HighScoreRowUI _rowPrefab;

    private void OnEnable()
    {
        Refresh();
    }

    /// <summary>
    /// Reconstruit le tableau à partir des pointages enregistrés.
    /// </summary>
    public void Refresh()
    {
        // On repart d'un tableau vide ; Destroy n'agit qu'à la fin de l'image
        foreach (Transform child in _rowContainer)
        {
            Destroy(child.gameObject);
        }

        IReadOnlyList<HighScoreEntry> entries = HighScoreStore.Load();
        for (int i = 0; i < entries.Count; i++)
        {
            HighScoreRowUI row = Instantiate(_rowPrefab, _rowContainer);
            row.SetValues(i + 1, entries[i].Name, entries[i].Score);
        }
    }
}

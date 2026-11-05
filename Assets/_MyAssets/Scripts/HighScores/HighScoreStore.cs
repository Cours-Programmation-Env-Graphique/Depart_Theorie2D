using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Conserve les meilleurs pointages sur l'ordinateur, d'une partie et d'un lancement à l'autre.
/// </summary>
public static class HighScoreStore
{
    /// <summary>
    /// Nombre de pointages conservés dans le tableau.
    /// </summary>
    public const int MaxEntries = 10;

    // Clé sous laquelle le tableau est enregistré dans les PlayerPrefs
    private const string PrefsKey = "HighScores";

    // JsonUtility ne sait pas écrire une liste seule : elle doit être le champ d'un objet
    [Serializable]
    private class HighScoreList
    {
        [SerializeField] private List<HighScoreEntry> _entries = new List<HighScoreEntry>();

        public List<HighScoreEntry> Entries => _entries;
    }

    /// <summary>
    /// Les meilleurs pointages, du meilleur au moins bon.
    /// </summary>
    public static IReadOnlyList<HighScoreEntry> Load()
    {
        return LoadList().Entries;
    }

    /// <summary>
    /// Indique si un pointage mérite une place dans le tableau.
    /// </summary>
    /// <param name="score">Pointage à vérifier.</param>
    public static bool Qualifies(int score)
    {
        List<HighScoreEntry> entries = LoadList().Entries;

        // Une place libre, ou un pointage meilleur que le dernier du tableau
        return entries.Count < MaxEntries || score > entries[entries.Count - 1].Score;
    }

    /// <summary>
    /// Ajoute un pointage au tableau, à sa place, et retire celui qui en sort.
    /// </summary>
    /// <param name="name">Nom du joueur.</param>
    /// <param name="score">Pointage obtenu.</param>
    public static void Add(string name, int score)
    {
        HighScoreList list = LoadList();
        list.Entries.Add(new HighScoreEntry(name, score));
        list.Entries.Sort(CompareByScoreDescending);

        if (list.Entries.Count > MaxEntries)
        {
            list.Entries.RemoveRange(MaxEntries, list.Entries.Count - MaxEntries);
        }

        PlayerPrefs.SetString(PrefsKey, JsonUtility.ToJson(list));
        PlayerPrefs.Save();
    }

    /// <summary>
    /// Efface tous les meilleurs pointages.
    /// </summary>
    public static void Clear()
    {
        PlayerPrefs.DeleteKey(PrefsKey);
        PlayerPrefs.Save();
    }

    private static HighScoreList LoadList()
    {
        string json = PlayerPrefs.GetString(PrefsKey, "");
        if (string.IsNullOrEmpty(json))
        {
            return new HighScoreList();
        }

        return JsonUtility.FromJson<HighScoreList>(json);
    }

    private static int CompareByScoreDescending(HighScoreEntry first, HighScoreEntry second)
    {
        // second avant first : du plus grand au plus petit
        return second.Score.CompareTo(first.Score);
    }
}

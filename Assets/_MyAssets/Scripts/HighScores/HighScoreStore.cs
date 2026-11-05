using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Conserve les meilleurs pointages sur l'ordinateur, d'une partie et d'un lancement à l'autre.
/// </summary>
public static class HighScoreStore
{
    // Nombre de pointages conservés dans le tableau
    private const int MaxEntries = 10;

    // Début des clés des PlayerPrefs : HighScoreName0, HighScoreScore0, HighScoreName1…
    private const string NameKey = "HighScoreName";
    private const string ScoreKey = "HighScoreScore";

    /// <summary>
    /// Lit les meilleurs pointages, du meilleur au moins bon.
    /// </summary>
    public static List<HighScoreEntry> Load()
    {
        List<HighScoreEntry> entries = new List<HighScoreEntry>();
        for (int i = 0; i < MaxEntries; i++)
        {
            // Les places se remplissent dans l'ordre : la première vide termine le tableau
            if (!PlayerPrefs.HasKey(NameKey + i))
            {
                break;
            }

            string name = PlayerPrefs.GetString(NameKey + i);
            int score = PlayerPrefs.GetInt(ScoreKey + i);
            entries.Add(new HighScoreEntry(name, score));
        }

        return entries;
    }

    /// <summary>
    /// Indique si un pointage mérite une place dans le tableau.
    /// </summary>
    /// <param name="score">Pointage à vérifier.</param>
    public static bool Qualifies(int score)
    {
        List<HighScoreEntry> entries = Load();

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
        List<HighScoreEntry> entries = Load();

        // On avance tant que les pointages du tableau sont au moins aussi bons
        int index = 0;
        while (index < entries.Count && entries[index].Score >= score)
        {
            index++;
        }

        entries.Insert(index, new HighScoreEntry(name, score));

        // Le onzième pointage sort du tableau
        if (entries.Count > MaxEntries)
        {
            entries.RemoveAt(MaxEntries);
        }

        for (int i = 0; i < entries.Count; i++)
        {
            PlayerPrefs.SetString(NameKey + i, entries[i].Name);
            PlayerPrefs.SetInt(ScoreKey + i, entries[i].Score);
        }

        PlayerPrefs.Save();
    }

    /// <summary>
    /// Efface tous les meilleurs pointages.
    /// </summary>
    public static void Clear()
    {
        for (int i = 0; i < MaxEntries; i++)
        {
            PlayerPrefs.DeleteKey(NameKey + i);
            PlayerPrefs.DeleteKey(ScoreKey + i);
        }

        PlayerPrefs.Save();
    }
}

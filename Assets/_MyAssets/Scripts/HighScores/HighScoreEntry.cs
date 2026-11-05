using System;
using UnityEngine;

/// <summary>
/// Une ligne du tableau des meilleurs pointages : un nom et un pointage.
/// </summary>
[Serializable]
public struct HighScoreEntry
{
    // Champs sérialisés : JsonUtility les écrit et les relit, même privés
    [SerializeField] private string _name;
    [SerializeField] private int _score;

    public HighScoreEntry(string name, int score)
    {
        _name = name;
        _score = score;
    }

    public string Name => _name;
    public int Score => _score;
}

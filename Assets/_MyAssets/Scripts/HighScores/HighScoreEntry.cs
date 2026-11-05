/// <summary>
/// Une ligne du tableau des meilleurs pointages : un nom et un pointage.
/// </summary>
public struct HighScoreEntry
{
    // Nom du joueur, en trois lettres
    private string _name;
    public string Name => _name;

    // Pointage obtenu
    private int _score;
    public int Score => _score;

    /// <summary>
    /// Crée une ligne du tableau.
    /// </summary>
    /// <param name="name">Nom du joueur.</param>
    /// <param name="score">Pointage obtenu.</param>
    public HighScoreEntry(string name, int score)
    {
        _name = name;
        _score = score;
    }
}

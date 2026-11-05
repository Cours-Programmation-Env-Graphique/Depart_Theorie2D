/// <summary>
/// Résultats de la dernière partie, transmis de la scène de jeu à l'écran de fin.
/// </summary>
public static class GameResults
{
    // Pointage de la dernière partie terminée
    private static int _finalScore;

    /// <summary>
    /// Pointage de la dernière partie terminée.
    /// </summary>
    public static int FinalScore => _finalScore;

    /// <summary>
    /// Enregistre le pointage de la partie qui vient de se terminer.
    /// </summary>
    /// <param name="finalScore">Pointage final de la partie.</param>
    public static void Record(int finalScore)
    {
        _finalScore = finalScore;
    }
}

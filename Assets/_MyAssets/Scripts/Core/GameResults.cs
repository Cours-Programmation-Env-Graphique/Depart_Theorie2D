/// <summary>
/// Résultats de la dernière partie, transmis de la scène de jeu à l'écran de fin.
/// </summary>
public static class GameResults
{
    // Pointage de la dernière partie terminée, lu par l'écran de fin
    private static int _finalScore;
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

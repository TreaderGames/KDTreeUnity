public enum GameStates
{
    Home,
    SinglePlayer,
    Multiplayer
}

public class GlobalVariables
{
    public static GameStates pCurrentGameState { get; set; }

    public static bool pIsLocalPlayerTurn { get; set; }
    public static bool pIsLocalPlayerWin { get; set; }
}

using UnityEngine;
public static class GlobalGameState 
{
    //current state of the game
    public static GameStates currentGameState = GameStates.UIShowOnScreen;
    public static void ChangeGameState(GameStates state)
    {
        switch (state)
        {
            case GameStates.UIShowOnScreen:
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                break;
            case GameStates.AbleToMove:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            case GameStates.NotAbleToMove:
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                break;
            default:
                break;
        }
        currentGameState = state;
    }
}
//public enum that is global coz its not inside a class
public enum GameStates
{
    UIShowOnScreen,
    AbleToMove,
    NotAbleToMove
}

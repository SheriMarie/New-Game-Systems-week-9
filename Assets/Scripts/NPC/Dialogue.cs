using UnityEngine;
public class Dialogue : MonoBehaviour
{
    public bool showDialogue = false;
    public string speakersName = "";
    public string[] dialogueText;
    public int currentLineOfText = 0;
   
    //Method that we can access when called upon in other scripts
    public void OpenDialogue()
    {
        //trigger bool
        showDialogue = true;
        //reset int
        currentLineOfText = 0;
        //any other thing you need
        GlobalGameState.ChangeGameState(GameStates.UIShowOnScreen);
    }
    public void CloseDialogue()
    {
        //trigger bool
        showDialogue = false;
        //reset int
        currentLineOfText = 0;
        //any other thing you need
        GlobalGameState.ChangeGameState(GameStates.AbleToMove);
    }
}

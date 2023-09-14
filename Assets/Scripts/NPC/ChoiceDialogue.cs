using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoiceDialogue : Dialogue
{
    public int questionLineIndex;

    private void OnGUI()
    {
        //if our dialogue can be seen on screen
        if (showDialogue)
        {
            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 6, 16, 3)), speakersName + ": " + dialogueText[currentLineOfText]);
            //if not at the end of the dialogue or not at the options part
            if (currentLineOfText < dialogueText.Length - 1 && currentLineOfText != questionLineIndex)
            {
                //next button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(15, 8.5f, 1, 0.5f)), "Next"))
                {
                    //incrementing currentLineIndex by 1 so that we go to next line
                    currentLineOfText++;
                }
            }
            //else if we are at options
            else if (currentLineOfText == questionLineIndex)
            {
                //Accept button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(14, 8.5f, 1, 0.5f)), "Accept"))
                {
                    currentLineOfText++;

                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(15, 8.5f, 1, 0.5f)), "Decline"))
                {
                    currentLineOfText = dialogueText.Length - 1;

                }
            }
            //else we are at the end
            else
            {
                //the Bye button allows up to end our dialogue
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(15, 8.5f, 1, 0.5f)), "Bye."))
                {
                    //close the dialogue box
                    CloseDialogue();
                }
            }
        }

    }
}


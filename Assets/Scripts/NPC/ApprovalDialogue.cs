using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApprovalDialogue : Dialogue
{
    // NPC gives different responses depending on an approval system
    /*
     Approval (can be an int or enum) has at least 3 tiers of response:
        - Dislike  -1
        - Neutral   0
        - Like      1
    */
    [Space(25)]
    [Header("Approval Specific Variables")]
    public int approvalValue = 0;
    public int questionLineIndex = 0;
    //3 extra arrays of strings one for each approval type
    public string[] dislikeText = new string[5];
    public string[] neutralText = new string[5];
    public string[] likeText = new string[5];
    /*
     Dialogue changes based on Approval rating

    Approval changes based on player interactions
         - have a way to ask a yes or no question
     */
    private void Start()
    {
        ChangeDisplayText();
    }
    void ChangeDisplayText()
    {
        approvalValue = Mathf.Clamp(approvalValue, -1, 1);
        if (approvalValue == 1)
        {
            dialogueText = likeText;
        }
        else if (approvalValue == -1)
        {
            dialogueText = dislikeText;
        }
        else
        {
            dialogueText = neutralText;
        }
    }
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
                    //incrementing currentLineOfText by 1 so that we go to next line
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
                    approvalValue++;
                    ChangeDisplayText();
                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(15, 8.5f, 1, 0.5f)), "Decline"))
                {
                    currentLineOfText = dialogueText.Length - 1;
                    approvalValue--;
                    ChangeDisplayText();
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

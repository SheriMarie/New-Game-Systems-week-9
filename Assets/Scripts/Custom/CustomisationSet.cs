using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//you will need to change Scenes
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class CustomisationSet : MonoBehaviour {

    #region Variables
   // [Header("Texture List")]
    //Texture2D List for skin,mouth, eyes, hair, clothes, armour
    public List<Texture2D> skinTextures = new List<Texture2D>();
    public List<Texture2D> mouthTextures = new List<Texture2D>();
    public List<Texture2D> eyeTextures = new List<Texture2D>();
    public List<Texture2D> hairTextures = new List<Texture2D>();
    public List<Texture2D> clothesTextures = new List<Texture2D>();
    public List<Texture2D> armourTextures = new List<Texture2D>();

    [Header("Max Index")]
    //max amount of skin, hair, mouth, eyes textures that our lists are filling with
    public int skinMax;
    public int mouthMax, eyeMax, hairMax, clothesMax, armourMax;

    [Space(20)]

    [Header("Character Name")]
    //name of our character that the user is making
    public string characterName = "Adventurer";

    [Header("Index")]
    //index numbers for our current skin, hair, mouth, eyes textures
    public int skinIndex;
    public int mouthIndex, eyeIndex, hairIndex, clothesIndex, armourIndex, helmIndex;
    [Header("Renderer")]
    //renderer for our character mesh so we can reference a material list
    public Renderer character;
    public Renderer helm;

    string[] customisationSets = new string[7] { "Skin", "Mouth", "Eyes", "Hair", "Clothes", "Armour", "Helm" };

    #endregion

    #region Start
    //in start we need to set up the following
    private void Start()
    {
        #region for loop to pull textures from file
        //for loop looping from 0 to less than the max amount of skin textures we need
        for (int i = 0; i < skinMax; i++) 
        {
            //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Skin_#
            Texture2D temp = Resources.Load("Character/skin_"+i) as Texture2D;
            //add our temp texture that we just found to the skin List
            skinTextures.Add(temp);
        }

        for (int i = 0; i < mouthMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Mouth_" + i) as Texture2D;
            mouthTextures.Add(temp);
        }

        for (int i = 0; i < eyeMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Eyes_" + i) as Texture2D;
            eyeTextures.Add(temp);
        }

        for (int i = 0; i < hairMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Hair_" + i) as Texture2D;
            hairTextures.Add(temp);
        }

        for (int i = 0; i < clothesMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Clothes_" + i) as Texture2D;
            clothesTextures.Add(temp);
        }

        for (int i = 0; i < armourMax; i++)
        {
            Texture2D temp = Resources.Load("Character/Armour_" + i) as Texture2D;
            armourTextures.Add(temp);
        }

        //for loop looping from 0 to less than the max amount of hair textures we need
        //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Hair_#
        //add our temp texture that we just found to the hair List
        //for loop looping from 0 to less than the max amount of mouth textures we need    
        //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Mouth_#
        //add our temp texture that we just found to the mouth List

        //for loop looping from 0 to less than the max amount of eyes textures we need
        //creating a temp Texture2D that it grabs using Resources.Load from the Character File looking for Eyes_#
        //add our temp texture that we just found to the eyes List            
        #endregion
        //connect and find the SkinnedMeshRenderer thats in the scene to the variable we made for Renderer
        character = GameObject.Find("Mesh").GetComponent<Renderer>();
        helm = GameObject.Find("cap").GetComponent<Renderer>();

        #region do this after making the function SetTexture
        //SetTexture skin, hair, mouth, eyes to the first texture 0
        SetTexture("Skin", 0);
        SetTexture("Mouth", 0);
        SetTexture("Eyes", 0);
        SetTexture("Hair", 0);
        SetTexture("Clothes", 0);
        SetTexture("Armour", 0);
        #endregion
    }

    #endregion

    #region SetTexture
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing

    void SetTexture(string type, int dir)
    {
        //we need variables that exist only within this function
        //these are ints index numbers, max numbers, material index and Texture2D array of textures
        int index = 0, max = 0, materialIndex = 0;
        Texture2D[] textures = new Texture2D[0];
        Renderer rend = new Renderer();
        //inside a switch statement that is swapped by the string name of our material
        #region Switch Material
        switch (type)
        {        //case skin
            case "Skin":
                //index is the same as our skin index
                index = skinIndex;
                //max is the same as our skin max
                max = skinMax;
                //textures is our skin list .ToArray()
                textures = skinTextures.ToArray();
                //material index element number is 1
                materialIndex = 1;
                rend = character;
                //break
                break;
                //now repeat for each material 
               //case skin
            case "Mouth":
                //index is the same as our skin index
                index = mouthIndex;
                //max is the same as our skin max
                max = mouthMax;
                //textures is our skin list .ToArray()
                textures = mouthTextures.ToArray();
                //material index element number is 1
                materialIndex = 2;
                rend = character;
                //break
                break;
                //now repeat for each material 
             //case skin
            case "Eyes":
                //index is the same as our skin index
                index = eyeIndex;
                //max is the same as our skin max
                max = eyeMax;
                //textures is our skin list .ToArray()
                textures = eyeTextures.ToArray();
                //material index element number is 1
                materialIndex = 3;
                rend = character;
                //break
                break;
                //now repeat for each material 

             //case skin
            case "Hair":
                //index is the same as our skin index
                index = hairIndex;
                //max is the same as our skin max
                max = hairMax;
                //textures is our skin list .ToArray()
                textures = hairTextures.ToArray();
                //material index element number is 1
                materialIndex = 4;
                rend = character;
                //break
                break;
                //now repeat for each material 
             //case skin
            case "Clothes":
                //index is the same as our skin index
                index = clothesIndex;
                //max is the same as our skin max
                max = clothesMax;
                //textures is our skin list .ToArray()
                textures = clothesTextures.ToArray();
                //material index element number is 1
                materialIndex = 5;
                rend = character;
                //break
                break;
                //now repeat for each material 
            //case 
            case "Armour":
                //index is the same as our  index
                index = armourIndex;
                //max is the same as our  max
                max = armourMax;
                //textures is our  list .ToArray()
                textures = armourTextures.ToArray();
                //material index element number 
                materialIndex = 6;
                rend = character;
                //break
                break;

            case "Helm":
                //index is the same as our  index
                index = helmIndex;
                //max is the same as our  max
                max = armourMax;
                //textures is our  list .ToArray()
                textures = armourTextures.ToArray();
                //material index element number 
                materialIndex = 1;
                rend = helm;
                //break
                break;
                //now repeat for each material 

        }


        #endregion
        #region OutSide Switch
        //outside our switch statement
        //index plus equals our direction
        index += dir;
        //cap our index to loop back around if is is below 0 or above max take one
        if ( index < 0)
        {
            index = max - 1;
        }
        if ( index > max-1 )
        {
            index = 0;
        }

        //Material array is equal to our renderers material list
        Material[] mats = rend.materials;

        //our material arrays current material index's main texture is equal to our texture arrays current index
        mats[materialIndex].mainTexture = textures[index];

        //our characters materials are equal to the material array
        rend.materials = mats;
        #endregion
        //create another switch that is goverened by the same string name of our material
        #region Set Material Switch
        switch (type)
        {
            //case skin
            case "Skin":
                //skin index equals our index
                skinIndex = index;
                //break
                break;
            case "Mouth":
                mouthIndex = index;
                break;
            case "Eyes":
                eyeIndex = index;
                break;
            case "Hair":
                hairIndex = index;
                break;
            case "Clothes":
                clothesIndex = index;   
                break;
            case "Armour":
                armourIndex = index;
                break;
            case "Helm":
                helmIndex = index;  
                break;

        }

        #endregion
    }
    #endregion

    #region Save
    //Function called Save this will allow us to save our indexes to PlayerPrefs
    //SetInt for SkinIndex, HairIndex, MouthIndex, EyesIndex
    //SetString CharacterName
    #endregion

    #region OnGUI
    private void OnGUI()
    {
        //Function for our GUI elements
        //create the floats scrW and scrH that govern our 16:9 ratio
        //create an int that will help with shuffling your GUI elements under eachother
        int i = 0;
        for (i=0; i < customisationSets.Length; i++)
        {
            //GUI button on the left of the screen with the contence <
            if (GUI.Button(UIHandler.ScreenPlacement(0.25f, 1 + i * (0.5f), 0.5f, 0.5f), "<"))
            {
                //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  -1
                SetTexture(customisationSets[i],-1);
            }
            //GUI Box or Lable on the left of the screen with the contence Skin
            GUI.Box(UIHandler.ScreenPlacement(0.75f, 1 + i * (0.5f), 1, 0.5f), customisationSets[i]);

            //GUI button on the left of the screen with the contence >
            if (GUI.Button(UIHandler.ScreenPlacement(1.75f, 1 + i * (0.5f), 0.5f, 0.5f), ">"))
            {
                SetTexture(customisationSets[i], 1);
            }

            //when pressed the button will run SetTexture and grab the Skin Material and move the texture index in the direction  1
            //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        }
        #region Skin

        #endregion
        //set up same things for Hair, Mouth and Eyes
        #region Hair
        //GUI button on the left of the screen with the contence <
        //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
        //GUI Box or Lable on the left of the screen with the contence material Name
        //GUI button on the left of the screen with the contence >
        //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        #endregion
        #region Mouth
        //GUI button on the left of the screen with the contence <
        //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
        //GUI Box or Lable on the left of the screen with the contence material Name
        //GUI button on the left of the screen with the contence >
        //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        #endregion
        #region Eyes
        //GUI button on the left of the screen with the contence <
        //when pressed the button will run SetTexture and grab the Material and move the texture index in the direction  -1
        //GUI Box or Lable on the left of the screen with the contence material Name
        //GUI button on the left of the screen with the contence >
        //when pressed the button will run SetTexture and grab the  Material and move the texture index in the direction  1
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        #endregion




        #region Random Reset
        //create 2 buttons one Random and one Reset
        if (GUI.Button(UIHandler.ScreenPlacement(0.25f, 1 + i * (0.5f), 1f, 0.5f), "Random"))
        {
            //Random will feed a random amount to the direction 
            SetTexture("Skin", Random.Range(0,skinMax));
            SetTexture("Mouth", Random.Range(0, mouthMax));
            SetTexture("Eyes", Random.Range(0, eyeMax));
            SetTexture("Hair", Random.Range(0, hairMax));
            SetTexture("Clothes", Random.Range(0, clothesMax));
            SetTexture("Armour", Random.Range(0, armourMax));
            SetTexture("Helm", Random.Range(0, armourMax));
        }

        if (GUI.Button(UIHandler.ScreenPlacement(1.25f, 1 + i * (0.5f), 1f, 0.5f), "Reset"))
        {
            //reset will set all to 0 both use SetTexture
            SetTexture("Skin", skinIndex = 0);
            SetTexture("Mouth", mouthIndex = 0);
            SetTexture("Eyes", eyeIndex = 0);
            SetTexture("Hair", hairIndex = 0);
            SetTexture("Clothes", clothesIndex = 0);
            SetTexture("Armour", armourIndex = 0);
            SetTexture("Helm", armourIndex = 0);
        }


        //move down the screen with the int using ++ each grouping of GUI elements are moved using this
        #endregion
        #region Character Name and Save & Play
        //name of our character equals a GUI TextField that holds our character name and limit of characters
        //move down the screen with the int using ++ each grouping of GUI elements are moved using this

        //GUI Button called Save and Play
        //this button will run the save function and also load into the game level
        #endregion
    }

    #endregion
}

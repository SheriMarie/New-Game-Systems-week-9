using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

public class OptionsSaveLoad : MonoBehaviour
{
    
    public MainMenu menu;
    public string optionsSaveDataString;
    public string[] splitSaveData;
    public float masterVolume, musicVolume, sfxVolume, brightness;
    public bool fullScreenToggle;
    public int qualityValue;

    //This allows us to save
    public void WriteToFile(string pathThatWePassThrough, string contentThatWeAreSaving)
    {
        StreamWriter fileWriter = new StreamWriter(pathThatWePassThrough, false);

        fileWriter.Write(contentThatWeAreSaving);
        fileWriter.Close();
    }

    public void ReadFromFile(string pathThatWePassThrough)
    {
        //start looking at file
        StreamReader fileReader = new StreamReader(pathThatWePassThrough);
        //Tell us about info in file
        optionsSaveDataString = fileReader.ReadLine();
        splitSaveData = optionsSaveDataString.Split('|');

        masterVolume = float.Parse(splitSaveData[0]);
        menu.MasterVolume = masterVolume;

        musicVolume = float.Parse(splitSaveData[1]);
        menu.MusicVolume = musicVolume;


        sfxVolume = float.Parse(splitSaveData[2]);
        menu.SfxVolume = sfxVolume;

        brightness = float.Parse(splitSaveData[3]);
        menu.Brightness = brightness;

        fullScreenToggle = bool.Parse(splitSaveData[4]);
        menu.FullscreenToggle = fullScreenToggle;

        qualityValue = int.Parse(splitSaveData[5]);
        menu.QualityValue = qualityValue;

        //stop looking at file
        fileReader.Close();
    }
    //
  public string SaveData()
    {
        StringBuilder saveText = new StringBuilder(menu.MasterVolume + "|"+menu.MusicVolume+"|"+menu.SfxVolume+"|"+menu.Brightness+"|"+menu.FullscreenToggle+"|"+menu.QualityValue);
        string data = saveText.ToString();
        Debug.Log(data);
        return data;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ReadFromFile(Application.dataPath+"/OptionsSaveData.txt");
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            WriteToFile(Application.dataPath + "/OptionsSaveData.txt", SaveData());
        }

    }
}



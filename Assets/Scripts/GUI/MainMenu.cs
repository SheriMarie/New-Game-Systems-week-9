using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    #region Variables and Data
    private enum MenuState
    {
        MainPanel ,
        OptionsPanel,
        PlayPanel,
        AreYouSurePanel
    }

    [Header("Option Values and Ref")]
    [SerializeField] private float masterVolume;                            //
    [SerializeField] private float musicVolume, sfxVolume,lightIntensity;   //
    [SerializeField] private bool fullscreenToggle;                         /**/
    [SerializeField] private int qualityValue;
    #region Not Saving
    [SerializeField] MenuState _menuState = MenuState.MainPanel;
    private bool showResolution, showQuality;
    public Resolution[] resolutions = new Resolution[10];
    private Vector2 resolutionScrollPosition, qualityScrollPosition;
    private int[] qualityLevelIndex = new int[4];
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private RenderTexture cameraRender;
    [SerializeField] private Light directionalLight;
    #endregion
    #endregion

    public float MasterVolume
    {
        get { return masterVolume; } 
        set { masterVolume = value; }
    }

    public float MusicVolume
    {
        get { return musicVolume; }
        set { musicVolume = value; }
    }
    public float SfxVolume
    {
        get { return sfxVolume; }
        set { sfxVolume = value; }
    }
    public float Brightness
    {
        get { return lightIntensity; }
        set { lightIntensity = value; }
    }
    public bool FullscreenToggle
    {
        get { return fullscreenToggle; }
        set { fullscreenToggle = value; }
    }
    public int QualityValue
    {
        get { return qualityValue ; }
        set { qualityValue = value; }
    }

    #region Unity Event Behaviours
    private void OnGUI()
    {
        switch (_menuState)
        {
            case MenuState.MainPanel:
                MainPanel();
                break;
            case MenuState.OptionsPanel:
                OptionsPanel();
                break;
            case MenuState.PlayPanel:
                PlayPanel();
                break;
            case MenuState.AreYouSurePanel:
                AreYouSurePanel();
                break;
            default:
                _menuState = MenuState.MainPanel;
                break;
        }
    }
    #endregion
    #region GUI Panels
    void MainPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Main Menu Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 0.25f, 8, 2)), "Game Title");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(5, 2.5f, 6, 6.25f)), "Button Box");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 3.25f, 4, 0.75f)), "Play"))
        {
            _menuState = MenuState.PlayPanel;
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 5.25f, 4, 0.75f)), "Options"))
        {
            _menuState = MenuState.OptionsPanel;
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 7.25f, 4, 0.75f)), "Exit"))
        {
            _menuState = MenuState.AreYouSurePanel;
        }
    }
    void OptionsPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Options Panel");
        #region Audio
        //Audio Sliders
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1,1,3, 2.3f)), "Audio Sliders");
        //Master
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 1.4f, 2.5f, 0.4f)), "Master Volume");
        masterVolume = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 1.8f, 2.5f, 0.25f)), masterVolume, -20,20);
        audioMixer.SetFloat("MasterVolume", masterVolume);
        //Music
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 2f, 2.5f, 0.4f)), "Music Volume");
        musicVolume = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 2.4f, 2.5f, 0.25f)), musicVolume, -20, 20);
        audioMixer.SetFloat("MusicVolume", musicVolume);

        //SFX
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 2.6f, 2.5f, 0.4f)), "SFX Volume");
        sfxVolume = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 3f, 2.5f, 0.25f)), sfxVolume, -20, 20);
        audioMixer.SetFloat("SFXVolume", sfxVolume);

        #endregion
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 3.3f, 3, 2.3f)), "Render");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 3.7f, 2.5f, 1.75f)), cameraRender);

        #region Quality
        //Graphics Quality - Shadows and shiz
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1, 5.6f, 3, 2.3f)), "Graphics Quality");


        #region Brightness
        //Brightness - 3D Lights
        GUI.Box(new Rect(UIHandler.ScreenPlacement(1.25f, 6.4f, 2.5f, 0.4f)), "Light Intensity");
        lightIntensity = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(1.25f, 6.8f, 2.5f, 0.25f)), lightIntensity, 0, 1);
        if(lightIntensity != directionalLight.intensity)
        {
            directionalLight.intensity = lightIntensity;
        }
        #endregion

        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(1.25f, 5.9f, 2.5f, 0.5f)), QualitySettings.names[QualitySettings.GetQualityLevel()]))
        {
            showQuality = !showQuality;
        }
        if (showQuality)
        {
            //This is the start of the scroll view
           qualityScrollPosition = GUI.BeginScrollView(
                //This is the position and size on screen 
                new Rect(UIHandler.ScreenPlacement(1.25f, 6.4f, 2.5f, 1.6f)),
                //This is the current scroll amount/position on the X and Y axis of our view
                qualityScrollPosition,
                //The position and size of the Scrollable Content Space 
                new Rect(UIHandler.ScreenPlacement(0, 0, 2f, 0.5f * qualityLevelIndex.Length)),
                //Can we see the Horizontal Slider?
                false,
                //Can we see the Verticle Slider?
                true);

            //Content
            for (int i = 0; i < qualityLevelIndex.Length; i++)
            {
                if (GUI.Button(new Rect(UIHandler.ScreenPlacement(0.25f, 0.5f * i, 2, 0.5f)), QualitySettings.names[i]))
                {
                    //set resolution when clicked
                    QualitySettings.SetQualityLevel(i);
                    qualityValue = i;
                    showQuality = false;
                }
            }
            GUI.EndScrollView();
        }
#endregion

        #region Fullscreen Toggle
        //Fullscreen/Windowed Toggle
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 3.3f, 3, 2.3f)), "Fullscreen");
        fullscreenToggle = GUI.Toggle(new Rect(UIHandler.ScreenPlacement(4.25f, 4.3f, 2.5f, 1f)), fullscreenToggle, "Window Toggle");
        if (fullscreenToggle != Screen.fullScreen)
        {
            Screen.fullScreen = fullscreenToggle;
        }
        #endregion
        #region Mouse
        //Mouse Sensitivity for Mouse Look
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 5.6f, 3, 2.3f)), "Mouselook Settings");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4.25f, 6.3f, 2.5f, 0.4f)), "Sensitivity");
        MouseLook.sensitivity = GUI.HorizontalSlider(new Rect(UIHandler.ScreenPlacement(4.25f, 6.7f, 2.5f, 0.25f)), MouseLook.sensitivity, 5, 15);
        MouseLook.invertMouse = GUI.Toggle(new Rect(UIHandler.ScreenPlacement(4.25f, 6.9f, 2.5f, 1f)), MouseLook.invertMouse, "Invert Mouse");
        #endregion
        #region Resolution
        //Resolution
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 1f, 3, 2.3f)), "Resolution");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(4.25f, 1.4f, 2.5f, 0.5f)), "Resolution"))
        {
            showResolution = !showResolution;
        }
        if(showResolution)
        {
            GUI.Box(new Rect(UIHandler.ScreenPlacement(4.25f, 1.9f, 2.5f, 1.6f)), "View Space");
            //This is the start of the scroll view
            resolutionScrollPosition = GUI.BeginScrollView(
                //This is the position and size on screen 
                new Rect(UIHandler.ScreenPlacement(4.25f, 1.9f, 2.5f, 1.6f)),
                //This is the current scroll amount/position on the X and Y axis of our view
                resolutionScrollPosition, 
                //The position and size of the Scrollable Content Space 
                new Rect(UIHandler.ScreenPlacement(0, 0, 2f, 0.5f * resolutions.Length)),
                //Can we see the Horizontal Slider?
                false,
                //Can we see the Verticle Slider?
                true);

            //Content
            for (int i = 0; i < resolutions.Length; i++)
            {
                if(GUI.Button(new Rect(UIHandler.ScreenPlacement(0.25f,0.5f*i,2,0.5f)),i+""))
                {
                    //set resolution when clicked
                }
            }


            GUI.EndScrollView();
        }
        #endregion

        #region Keybinds
        //Keybinds
        GUI.Box(new Rect(UIHandler.ScreenPlacement(7, 1, 8, 6.9f)), "Keybinds");

        #endregion


        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(1, 8f, 4, 0.5f)), "Back"))
        {
            _menuState = MenuState.MainPanel;
        }
    }
    void PlayPanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Play Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 0.5f, 8, 8)), "Button Box");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(5, 1.25f, 6, 1.5f)), "PlayPanel");

        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 3.5f, 4, 0.75f)), "Continue"))
        {
            Debug.Log("In future we will load our lasted played saved game");
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 4.75f, 4, 0.75f)), "New Game"))
        {
            Debug.Log("Change Scene to Customisation scene and start a new game");
            ChangeSceneByIndexValue(1);
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 6, 4, 0.75f)), "Load"))
        {
            Debug.Log("In future we will load from a list of saved games");
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(6, 7.25f, 4, 0.75f)), "Back"))
        {
            _menuState = MenuState.MainPanel;
        }
    }
    void AreYouSurePanel()
    {
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "Are You Sure Panel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(0, 0, 16, 9)), "AreYouSurePanel");
        GUI.Box(new Rect(UIHandler.ScreenPlacement(4, 2.25f, 8, 4.5f)), "AreYouSurePanel");
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(5f, 4.25f, 2.75f, 0.75f)), "Yes"))
        {
            Debug.Log("Exit");
            ExitToDesktop();
        }
        if (GUI.Button(new Rect(UIHandler.ScreenPlacement(8.25f, 4.25f, 2.75f, 0.75f)), "No"))
        {
            _menuState = MenuState.MainPanel;
        }
    }
    #endregion
    #region Scene Management
    void ChangeSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    void ChangeSceneByIndexValue(int sceneIndexValue)
    {
        SceneManager.LoadScene(sceneIndexValue);
    }
    void ExitToDesktop()
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;    
        #endif
    }
    #endregion
}

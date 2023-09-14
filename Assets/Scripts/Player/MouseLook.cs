using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Game Systems/Player/Mouse Look
[AddComponentMenu("Game Systems/Player/Mouse Look")]
public class MouseLook : MonoBehaviour
{
    /*
     enums are what we call state value variables 
     they are comma separated lists of identifiers
     we can use them to create Types or Categorys
         */
    #region RotationalAxis
    //Create a enum called RotationalAxis
    private enum RotationalAxis
    {
        MouseX, MouseY
    }
    #endregion
    #region Variables
    [Header("Rotation")]
    //create a private link to the rotational axis called axis and set a defualt axis
    [SerializeField] private RotationalAxis _axis;
    //[Header("Sensitivity")]
    //public floats for our x and y sensitivity
    public static float sensitivity = 10; 
    //we will have to invert our mouse position later to calculate our mouse look correctly
    public static bool invertMouse;
    [Header("Y Rotation Clamp")]
    //max and min Y rotation
    [SerializeField] private Vector2 _clamp = new Vector2(-60,60);
    //float for rotation Mouse Y
    private float _rotationMouseY;
    #endregion
    #region Start
    private void Start()
    {
        //if our game object has a rigidbody attached to it
        if (GetComponent<Rigidbody>())
        {
            //set the rigidbodys freezRotaion to true
            GetComponent<Rigidbody>().freezeRotation = true;
        }
        //Check if the script is on player object vs camera object to set up correct movement
        if(gameObject.tag == "Player")
        {
            _axis = RotationalAxis.MouseX;
        }
        else//otherwise we be camera bois
        {
            _axis = RotationalAxis.MouseY;
        }    
    }
    #endregion
    #region Update
    private void Update()
    {
        if(GlobalGameState.currentGameState == GameStates.AbleToMove)
        {
            #region Mouse X
            //if we are rotating on the X
            if (_axis == RotationalAxis.MouseX)
            {
                //transform the rotation on our game objects Y by our Mouse input Mouse X * sensitivity
                //x                y                          z
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivity, 0);
            }
            #endregion
            #region Mouse Y
            //else we are only rotation on the Y
            else
            {
                //our rotation Y is pulse equals  our mouse input for Mouse Y times Y sensitivity
                _rotationMouseY += Input.GetAxis("Mouse Y") * sensitivity;
                //the rotation Y is clamped using Mathf and we are clamping the y rotation to the Y min and Y max
                _rotationMouseY = Mathf.Clamp(_rotationMouseY, _clamp.x, _clamp.y);
                //transform our local position to the nex vector3 rotaion - y rotaion on the x axis and local euler angle Y on the y axis
                if (invertMouse) // mouse movement is inverted ... aka plane controls
                {
                    transform.localEulerAngles = new Vector3(_rotationMouseY, 0, 0);
                }
                else // mouse movement is not inverted ... aka standard mouse controls 
                {
                    transform.localEulerAngles = new Vector3(-_rotationMouseY, 0, 0);
                }
            }
            #endregion
        }

    }
    #endregion
}














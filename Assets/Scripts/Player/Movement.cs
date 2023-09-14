using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Game Systems/Player/ Movement
[AddComponentMenu("Game Systems/Player/Movement")]
//This script requires the component Character controller to be attached to the Game Object
[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    #region Extra Study
    //Input Manager(https://docs.unity3d.com/Manual/class-InputManager.html)
    //Input(https://docs.unity3d.com/ScriptReference/Input.html)
    //CharacterController allows you to move the character kinda like Rigidbody (https://docs.unity3d.com/ScriptReference/CharacterController
    #endregion
    #region Variables
    [Header("Direction and Physics")]
    //vector3 called moveDir //we will use this to apply movement in worldspace
    [SerializeField] private Vector3 _moveDirection;
    //Character controller called _charC CharacterController.html) 
    [SerializeField] private CharacterController _characterController;
    //public float variables jumpSpeed 8 & speed 5 & gravity 20 
    [Space(25), Header("Movement Speeds"), Tooltip("THIS IS SPEED"), Range(0, 20), SerializeField] private float _movementSpeed = 5;
    [SerializeField] private float _jumpSpeed = 8, _gravity = 20, _crouchSpeed = 2.5f, _walkSpeed = 5, _sprintSpeed = 10;
    #endregion
    #region Start  
    private void Start()
    {
        //_charc is set to the Character controller on this GameObject
        _characterController = GetComponent<CharacterController>();
        GlobalGameState.ChangeGameState(GameStates.AbleToMove);
    }
    #endregion
    #region Update
    private void Update()
    {
        if (GlobalGameState.currentGameState == GameStates.AbleToMove)//only move if we should move (are we alive...is the game paused??? whats the state of things)
        {
            MoveTheCharacter();
        }
    }
    #endregion
    #region MoveCode
    void MoveTheCharacter()
    {
        //if our character is grounded
        if (_characterController.isGrounded)
        {
            //set moveDir to the inputs direction
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
            //moveDir's forward is changed from global z (forward) to the Game Objects local Z (forward)//allows us to move where player is facing
            _moveDirection = transform.TransformDirection(_moveDirection);
            //moveDir is multiplied by speed so we move at a decent pace
            _moveDirection *= _movementSpeed;          
            //_moveDirection = transform.TransformDirection(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")) * _movementSpeed);             

            //if the input button for jump is pressed then           
            if (Input.GetButton("Jump"))
            {
                //our moveDir.y is equal to our jump speed
                _moveDirection.y = _jumpSpeed;
            }
        }
        //if we are not grounded the players moveDir.y is always affected by gravity timesed my time.deltaTime to normalize it
        _moveDirection.y -= _gravity * Time.deltaTime;
        //we then tell the character Controller that it is moving in a direction multiplied Time.deltaTime
        _characterController.Move(_moveDirection * Time.deltaTime);
    }
    #endregion
}











using System;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerInputHandler : MonoBehaviour
{
    PlayerInput inputActions;

    #region 移动
    public Vector2 MovementValue{get;private set;}=Vector2.zero;
    public bool IsJump=>inputActions.GamePlay.Jump.WasPressedThisFrame();
    public bool IsDash=>inputActions.GamePlay.Dash.WasPressedThisFrame();
    public bool IsPressingJump=>inputActions.GamePlay.Jump.IsPressed();
    #endregion


    #region 战斗
    public Vector2 MouseMove{get;private set;}=Vector2.zero;
    #endregion
    void Awake()
    {
        if(inputActions==null)
        {
            inputActions=new();
        }

        inputActions.Enable();
        inputActions.GamePlay.Move.started+=OnMoveStarted;
        inputActions.GamePlay.Move.performed+=OnMovePerformed;
        inputActions.GamePlay.Move.canceled+=OnMoveCanceled;





    }







    #region Move
    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }

    private void OnMoveStarted(InputAction.CallbackContext context)
    {
        MovementValue = context.ReadValue<Vector2>();
    }
    #endregion






}

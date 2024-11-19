using System;
using MY_Framework.FSM;
using UnityEngine;

public class JumpDown_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;

    float elapsedTime;
    Vector2 currentVelocity;
    bool IsTag=true;
    public JumpDown_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }

    public void OnEnter()
    {
        //重置tag
        IsTag=true;

        elapsedTime = 0;
        currentVelocity = board.PC.Rb.velocity;
        board.PC.Rb.gravityScale=board.PC.Data_Player.JumpDownGravityScale;
    }

    public void OnExit()
    {
        //复原
        board.PC.cameraFollowObject.Down(true);
        board.PC.Rb.gravityScale=1;
    }

    public void OnUpdate()
    {
        if (board.PC.IsGrounded)
        {
            fsm.ChangeState(StateType.Action_Idle);
        }

    }
    public void OnFixedUpdate()
    {
        elapsedTime += Time.fixedDeltaTime;

        float targetSpeed=board.PC.MoveInputX*board.PC.Data_Player.JumpMoveSpeed;
        board.PC.Move(currentVelocity.x,targetSpeed,board.PC.Data_Player.ToMoveMaxSpeedTime,ref elapsedTime);

        if(MathF.Abs(board.PC.Rb.velocity.y)>=board.PC.Data_Player.JumpDownMaxSpeed)
        {
            //告诉相机到最大下落速度
            if(IsTag)
            {
                board.PC.cameraFollowObject.Down(false);
                IsTag=false;
            }
            board.PC.SetVelocityY(-board.PC.Data_Player.JumpDownMaxSpeed);
            Debug.Log(board.PC.Rb.velocity.y);
        }


    }



}

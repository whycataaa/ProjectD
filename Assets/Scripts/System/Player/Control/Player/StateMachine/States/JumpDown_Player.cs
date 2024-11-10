using System;
using MY_Framework.FSM;
using UnityEngine;

public class JumpDown_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;

    float elapsedTime;
    Vector2 currentVelocity;
    float EnterVelocityY;
    public JumpDown_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }

    public void OnEnter()
    {

        elapsedTime = 0;
        currentVelocity = board.PC.Rb.velocity;
        board.PC.Rb.gravityScale=board.PC.Data_Player.JumpDownGravityScale;
    }

    public void OnExit()
    {
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

        if(MathF.Abs(board.PC.Rb.velocity.y)>board.PC.Data_Player.JumpDownMaxSpeed)
        {
            board.PC.SetVelocityY(-board.PC.Data_Player.JumpDownMaxSpeed);
            Debug.Log(board.PC.Rb.velocity.y);
        }


    }



}

using MY_Framework.FSM;
using UnityEngine;

public class Ladder_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;

    float elapsedTime;
    public Ladder_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }
    public void OnEnter()
    {
        Debug.Log("Ladder_Player OnEnter");
        elapsedTime=0;
        board.PC.Rb.gravityScale = 0;
    }

    public void OnExit()
    {
        board.PC.Rb.gravityScale = 1;
    }

    public void OnFixedUpdate()
    {
        elapsedTime+= Time.fixedDeltaTime;
        float targetSpeed=board.PC.MoveInputX*board.PC.Data_Player.LadderMoveSpeedX;
        board.PC.Move(0,targetSpeed,board.PC.Data_Player.ToMoveMaxSpeedTime,ref elapsedTime);
        board.PC.Rb.velocity=new Vector2(board.PC.Rb.velocity.x,board.PC.Data_Player.LadderMoveSpeedY*board.PC.MoveInputY);
    }

    public void OnUpdate()
    {
        if(board.PC.IsJump)
        {
            fsm.ChangeState(StateType.Action_JumpUp);
        }
        if(!board.PC.IsLadder)
        {
            if(!board.PC.IsGrounded)
            {
                fsm.ChangeState(StateType.Action_JumpDown);
            }
            else
            {
                if(board.PC.MoveInputX!=0)
                {
                    fsm.ChangeState(StateType.Action_Move);
                }
                else
                {
                    fsm.ChangeState(StateType.Action_Idle);
                }
            }
        }
    }
}

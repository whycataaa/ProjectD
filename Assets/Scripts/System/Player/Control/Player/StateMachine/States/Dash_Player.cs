using UnityEngine;
using MY_Framework.FSM;
using System;
public class Dash_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;

    public float elapsedTime;
    int Dir;
    public Dash_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }

    public void OnEnter()
    {
        Dir=GetDashDir();
        elapsedTime=0f;
        board.PC.SetVelocity(Vector2.zero);
        board.PC.Rb.gravityScale=0;
    }

    public void OnExit()
    {
        board.PC.SetVelocity(Vector2.zero);
        board.PC.Rb.gravityScale=board.PC.Data_Player.GobalGravityScale;
    }

    public void OnUpdate()
    {
        elapsedTime+=Time.deltaTime;
        if(elapsedTime>board.PC.Data_Player.DashTime)
        {
            fsm.ChangeState(StateType.Action_Idle);
        }
    }

    public void OnFixedUpdate()
    {
        board.PC.Rb.velocity=new Vector2((board.PC.Data_Player.DashDistance/board.PC.Data_Player.DashTime)*Dir
                                            ,board.PC.Rb.velocity.y);

    }

    private int GetDashDir()
    {
        //没按方向键，按面朝方向冲刺
        if(board.PC.MoveInputX==0)
        {
            return board.PC.IsFacingRight?1:-1;
        }
        else
        {
            return board.PC.MoveInputX==1?1:-1;
        }
    }
}

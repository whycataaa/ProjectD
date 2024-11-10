using MY_Framework.FSM;
using UnityEngine;

public class JumpUp_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;


    Vector2 currentVelocity;
    float elapsedTime;

    float EnterHeight;
    float ExitHeight;
    public JumpUp_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }
    public void OnEnter()
    {

        EnterHeight=board.PC.transform.position.y;
        Debug.Log(EnterHeight);

        board.PC.JumpTimes--;
        //重置状态时间
        elapsedTime=0f;

        //给与向上初始速度
        board.PC.SetVelocityY(board.PC.Data_Player.JumpUpMaxSpeed);

        Debug.Log(board.PC.Rb.velocity);
    }

    public void OnExit()
    {
        ExitHeight=board.PC.transform.position.y;
        Debug.Log(ExitHeight);
        Debug.Log("跳跃高度:"+Mathf.Abs(EnterHeight-ExitHeight));
    }

    public void OnUpdate()
    {
        //停止跳跃按键立即下落实现长短跳
        if(!board.PC.IsPressingJump)
        {
            if(elapsedTime>board.PC.Data_Player.JumpToMinTime)
            {
                board.PC.SetVelocityY(0);
                fsm.ChangeState(StateType.Action_JumpDown);
            }
        }

        if(board.PC.Rb.velocity.y<=0)
        {
            fsm.ChangeState(StateType.Action_JumpDown);
        }


    }
    public void OnFixedUpdate()
    {
         elapsedTime+= Time.fixedDeltaTime;
         //空中移动
         float targetSpeed=board.PC.MoveInputX*board.PC.Data_Player.JumpMoveSpeed;
         board.PC.Move(currentVelocity.x,targetSpeed,board.PC.Data_Player.ToMoveMaxSpeedTime,ref elapsedTime);


    }

}

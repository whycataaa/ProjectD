using MY_Framework.FSM;
using UnityEngine;

public class Move_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;

    //目标速度
    float MoveMaxSpeed;
    //达到目标速度的时间
    float reachTargetTime;

    float elapsedTime=0f;

    public Move_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }
    public void OnEnter()
    {
        board.PC.Rb.velocity=Vector2.zero;
        elapsedTime=0f;
        reachTargetTime=board.PC.Data_Player.ToMoveMaxSpeedTime;
    }

    public void OnExit()
    {
        board.PC.Rb.gravityScale=1;
    }

    public void OnUpdate()
    {
        if(!board.PC.IsGrounded)
        {
            fsm.ChangeState(StateType.Action_JumpDown);
        }
        if(board.PC.MoveInputX==0)
        {
            fsm.ChangeState(StateType.Action_Idle);
        }
        if(board.PC.IsJump)
        {
            fsm.ChangeState(StateType.Action_JumpUp);
        }
        if(board.PC.IsLadder)
        {
            if(board.PC.MoveInputY!=0)
            {
                fsm.ChangeState(StateType.Action_Ladder);
            }
        }
    }
    public void OnFixedUpdate()
    {
        Run();
    }

    private void Run()
    {
        MoveMaxSpeed = board.PC.MoveInputX * board.PC.Data_Player.MoveMaxSpeed;
        if (elapsedTime < reachTargetTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            float t = elapsedTime / reachTargetTime;
            float currentSpeed = Mathf.Lerp(0, MoveMaxSpeed, t);

            board.PC.Rb.velocity = new Vector2(currentSpeed, board.PC.Rb.velocity.y);
        }
        else
        {
            board.PC.Rb.velocity = new Vector2(MoveMaxSpeed, board.PC.Rb.velocity.y);
        }
    }
}

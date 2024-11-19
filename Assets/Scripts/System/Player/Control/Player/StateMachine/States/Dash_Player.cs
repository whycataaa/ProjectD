using UnityEngine;
using MY_Framework.FSM;
public class Dash_Player : IState
{
    private FSM fsm;
    private BlackBoard_Player board;



    public Dash_Player(FSM fsm)
    {
        this.fsm = fsm;
        board = fsm.blackBoard as BlackBoard_Player;
    }

    public void OnEnter()
    {

    }

    public void OnExit()
    {

    }

    public void OnUpdate()
    {

    }

    public void OnFixedUpdate()
    {


    }

}

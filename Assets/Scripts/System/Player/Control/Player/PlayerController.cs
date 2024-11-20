using System;
using UnityEngine;
using MY_Framework.FSM;

[RequireComponent(typeof(PlayerInputHandler))]
public class PlayerController : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    GroundDetect groundDetect;
    LadderDetect ladderDetect;
    GameObject playerGO;
    public Animator BodyAnimator;
    public CameraFollowObject cameraFollowObject{get;private set;}

    #region Movement
    [DisplayOnly][SerializeField]
    public bool IsFacingRight=true;
    public Rigidbody2D Rb;
    //输入方向
    [DisplayOnly][SerializeField]
    Vector2 moveInputDir;
    public float MoveInputX{get;private set;}=0;
    public float MoveInputY{get;private set;}=0;
    public bool IsJump{get;private set;}
    public bool IsPressingJump{get;private set;}
    public int JumpTimes=0;


    public bool IsDash{get;private set;}=false;

    public bool IsGrounded
    {
        get
        {
            if(groundDetect.IsGrounded)
            {
                JumpTimes=Data_Player.MaxJumpTimes;
            }
            return groundDetect.IsGrounded;
        }
    }
    //玩家是否接触到梯子
    public bool IsLadder
    {
        get
        {
            return ladderDetect.IsLadder;
        }
    }
    #endregion

    public Data_Player Data_Player;

    #region FSM
    private FSM fsm;
    public BlackBoard_Player blackBoard;

    #endregion

    Vector2 baseScale;


    void Start()
    {
        baseScale=transform.localScale;
        JumpTimes=Data_Player.MaxJumpTimes;
        playerGO = GameObject.Find("Player");
        inputHandler = GetComponent<PlayerInputHandler>();
        cameraFollowObject=GameObject.Find("CameraFollowPoint").GetComponent<CameraFollowObject>();
        Rb = GetComponent<Rigidbody2D>();


        fsm = new FSM(blackBoard);
        groundDetect=GetComponentInChildren<GroundDetect>();
        ladderDetect=GetComponentInChildren<LadderDetect>();
        blackBoard.Init(this);

        fsm.AddState(StateType.Action_Idle, new Idle_Player(fsm));
        fsm.AddState(StateType.Action_Move, new Move_Player(fsm));
        fsm.AddState(StateType.Action_JumpUp, new JumpUp_Player(fsm));
        fsm.AddState(StateType.Action_JumpDown, new JumpDown_Player(fsm));
        fsm.AddState(StateType.Action_Ladder, new Ladder_Player(fsm));
        fsm.AddState(StateType.Action_Dash, new Dash_Player(fsm));

        fsm.ChangeState(StateType.Action_Idle);
    }


    void Update()
    {
        SetInputValue();
        fsm.OnUpdate();


        //调整朝向
        if(MoveInputX!=0)
        {
            SetFaceDir(MoveInputX);
        }
    
    
    
    }

    private void SetInputValue()
    {
        moveInputDir = inputHandler.MovementValue;
        MoveInputX = moveInputDir.x;
        MoveInputY = moveInputDir.y;
        IsJump = inputHandler.IsJump;
        IsDash = inputHandler.IsDash;
        IsPressingJump = inputHandler.IsPressingJump;
    }




    void FixedUpdate()
    {
        fsm.OnFixedUpdate();
    }

    void SetFaceDir(float _Dir)
    {
        playerGO.transform.localScale = new Vector2(Mathf.Sign(_Dir)*baseScale.x, 1*baseScale.y);
        if(_Dir<0&&IsFacingRight)
        {
            IsFacingRight=false;
            cameraFollowObject.Turn();
        }
        else if(_Dir>0&&!IsFacingRight)
        {
            IsFacingRight=true;
            cameraFollowObject.Turn();
        }
    }

    /// <summary>
    /// 移动
    /// </summary>
    /// <param name="_StartSpeed">开始速度</param>
    /// <param name="_TargetSpeed">目标速度</param>
    /// <param name="_Time">总时间</param>
    /// <param name="_ElapsedTime">已用时</param>
    public void Move(float _StartSpeed,float _TargetSpeed,float _Time,ref float _ElapsedTime)
    {
        if(_ElapsedTime<_Time)
        {
            _ElapsedTime+=Time.fixedDeltaTime;
            float t=_ElapsedTime/_Time;
            float currentSpeed=Mathf.Lerp(_StartSpeed,_TargetSpeed,t);

            Rb.velocity=new Vector2(currentSpeed,Rb.velocity.y);
        }
        else
        {
            Rb.velocity=new Vector2(_TargetSpeed,Rb.velocity.y);
        }
    }

    public void SetVelocityX(float VelocityX)
    {
        Rb.velocity=new Vector2(VelocityX,Rb.velocity.y);
    }
    public void SetVelocityY(float VelocityY)
    {
        Rb.velocity=new Vector2(Rb.velocity.x,VelocityY);
    }
    public void SetVelocity(Vector2 Velocity)
    {
        SetVelocityX(Velocity.x);
        SetVelocityY(Velocity.y);
    }
}

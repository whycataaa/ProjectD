using UnityEngine;


/// <summary>
/// 管理静态可配置数据
/// </summary>
[CreateAssetMenu(fileName = "Data_Player", menuName = "Data/Data_Player")]
public class Data_Player : ScriptableObject
{
    [Header("--Movement--")]

    public float GobalGravityScale=1;


    [Header("-Run-")]
    [Header("移动的最大速度")]
    public float MoveMaxSpeed;
    [Header("到达最大速度用时")]
    public float ToMoveMaxSpeedTime;


    [Header("-Idle-")]
    [Header("速度减为0用时")]
    public float StopTime;


    [Header("-JumpUp-")]
    [Header("最大跳跃次数")]
    public int MaxJumpTimes;
    [Header("起跳时的速度")]
    public float JumpUpMaxSpeed;
    [Header("最小跳跃时间")]
    public float JumpToMinTime;
    [Header("空中移动速度")]
    public float JumpMoveSpeed;


    [Header("-JumpDown-")]
    [Header("下落时的重力倍数")]
    public float JumpDownGravityScale;
    [Header("下落最大速度")]
    public float JumpDownMaxSpeed;


    [Header("-Ladder-")]
    [Header("爬梯子横向速度")]
    public float LadderMoveSpeedX;
    [Header("爬梯子纵向速度")]
    public float LadderMoveSpeedY;

    [Header("-Dash-")]
    [Header("冲刺距离")]
    public float DashDistance;
    [Header("冲刺时间")]
    public float DashTime;

}

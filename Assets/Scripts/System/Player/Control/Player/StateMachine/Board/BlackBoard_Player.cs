using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MY_Framework.FSM;



/// <summary>
/// 动态公用数据会放在这里
/// </summary>
[CreateAssetMenu(fileName = "BlackBoard_Player", menuName = "BlackBoard/BlackBoard_Player")]
public class BlackBoard_Player : BlackBoard
{

    [Header("Reference")]
    public PlayerController PC;

    public Vector2 JumpDir;


    public void Init(PlayerController playerController)
    {
        this.PC = playerController;
    }
}

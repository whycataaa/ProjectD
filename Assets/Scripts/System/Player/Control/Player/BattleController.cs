using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    PlayerInputHandler inputHandler;
    Data_Player data;
    Transform handL;
    Transform handR;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        inputHandler = GetComponent<PlayerInputHandler>();
        data=GetComponent<PlayerController>().Data_Player;


    }
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        if(inputHandler.MouseMove!=Vector2.zero)
        {
//            Debug.Log(inputHandler.MouseMove);
        }
    }
}

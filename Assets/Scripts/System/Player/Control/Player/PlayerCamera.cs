using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;


/// <summary>
/// 动态控制玩家相机
/// </summary>
public class PlayerCamera : MonoBehaviour
{

    CinemachineVirtualCamera CVM;

    Transform FollowPoint;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        CVM=GameObject.Find("CameraMain/Camera2D").GetComponent<CinemachineVirtualCamera>();
        FollowPoint = GameObject.Find("CameraFollowPoint").transform;


        InitFollowTarget();
    }

    private void InitFollowTarget()
    {
        CVM.m_Follow = FollowPoint;
    }





}

using System;
using System.Collections;
using UnityEngine;

public class CameraFollowObject : MonoBehaviour
{

    [SerializeField]
    Vector2 OFFSET;

    [DisplayOnly][SerializeField]
    private Vector2 offset;
    //过渡时间
    [SerializeField]
    private float transTimeX;
    [SerializeField]
    private float transTimeY;
    
    private Coroutine turnCoroutine;
    private Coroutine downCoroutine;
    [DisplayOnly][SerializeField]
    bool IsTurning=false;
    [DisplayOnly][SerializeField]
    bool IsDowning=false;
    private PlayerController playerController;

    [Header("下落到最大速度相机向下的偏移量")]
    [SerializeField]
    private float DownMaxSpeedOffset;

    void Awake()
    {
        offset = OFFSET;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        transform.position=new Vector2(transform.position.x+offset.x,transform.position.y+offset.y);
    }


    void Update()
    {
        transform.position=playerController.transform.position+new Vector3(offset.x,offset.y);
    }

    public void Turn()
    {
        if(IsTurning)
        {
            StopCoroutine(turnCoroutine);
        }
        turnCoroutine=StartCoroutine(Flip());
    }
    public void Down(bool IsRecover)
    {
        if(IsDowning)
        {
            StopCoroutine(downCoroutine);
        }
        downCoroutine=StartCoroutine(LookDown(IsRecover));
    }
    private IEnumerator Flip()
    {
        IsTurning=true;
        float startX=offset.x;
        //float startY=offset.y;
        float endX=GetEndX();
        //float endY=OFFSET.y;
        float elapsedTime = 0f;

        while (elapsedTime < transTimeX)
        {
            elapsedTime += Time.deltaTime;

            offset.x=Mathf.Lerp(startX,endX,elapsedTime/transTimeX);
            //offset.y=Mathf.Lerp(startY,endY,elapsedTime/transTime);
            yield return null;

            if(elapsedTime>=transTimeX)
            {
                IsTurning=false;
            }
        }

    }
    private IEnumerator LookDown(bool IsRecover)
    {
        IsDowning=true;
        float startY=offset.y;
        float endY=IsRecover?OFFSET.y:OFFSET.y-DownMaxSpeedOffset;
        float elapsedTime = 0f;
        while (elapsedTime < transTimeY)
        {
            elapsedTime += Time.deltaTime;

            offset.y=Mathf.Lerp(startY,endY,elapsedTime/transTimeY);
            yield return null;

            if(elapsedTime>=transTimeY)
            {
                IsDowning=false;
            }
        }
    }
    private float GetEndX()
    {
        if(playerController.IsFacingRight)
        {
            return OFFSET.x;
        }
        else
        {
            return -OFFSET.x;
        }
    }


}

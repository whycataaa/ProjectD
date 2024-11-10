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
    private float transTime;

    private Coroutine turnCoroutine;
    [DisplayOnly][SerializeField]
    bool IsTurning=false;
    private PlayerController playerController;



    void Awake()
    {
        offset = OFFSET;
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        transform.position=new Vector2(transform.position.x+offset.x,transform.position.y+offset.y);
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
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

    private IEnumerator Flip()
    {
        IsTurning=true;
        float startX=offset.x;
        float startY=offset.y;
        float endX=GetEndX();
        float endY=OFFSET.y;
        float elapsedTime = 0f;

        while (elapsedTime < transTime)
        {
            elapsedTime += Time.deltaTime;

            offset.x=Mathf.Lerp(startX,endX,elapsedTime/transTime);
            offset.y=Mathf.Lerp(startY,endY,elapsedTime/transTime);
            yield return null;

            if(elapsedTime>=transTime)
            {
                IsTurning=false;
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

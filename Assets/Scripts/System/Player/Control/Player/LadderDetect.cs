using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderDetect : MonoBehaviour
{
    public bool IsLadder=false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Ladder")
        {
            IsLadder = true;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            IsLadder = true;
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            IsLadder = false;
        }
    }
}

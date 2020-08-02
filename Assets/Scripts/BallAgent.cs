using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAgent : MonoBehaviour
{
    public Color32 Color;


    public bool IsEqual(BallAgent ball)
    {
        if (Color.r == ball.Color.r && Color.g == ball.Color.g && Color.b == ball.Color.b && Color.a == ball.Color.a)
            return true;
        else
            return false;
    }
}

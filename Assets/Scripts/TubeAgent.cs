using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TubeAgent : MonoBehaviour
{

    public BallAgent[] Balls = new BallAgent[4];
    public Transform BallParent;

    public BallAgent SelectedBall;

    public int BallCount;

    public bool IsSelected;
    public bool IsUnicolor;

    // Start is called before the first frame update
    void Start()
    {
        IsSelected = false;
        IsUnicolor = false;
        //BallCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddBall(int ballId)
    {
        if (BallCount == 4)
            return;

        GameObject go = GameObject.Instantiate(GameManager.instance.BallPrefabs[ballId]);
        BallAgent ball = go.GetComponent<BallAgent>();
        go.transform.SetParent(BallParent);
        go.transform.localPosition = new Vector3(0f, -1.5f + BallCount, 0f);
        Balls[BallCount] = ball;
        BallCount += 1;
    }

    public void PushBall(BallAgent ball)
    {
        if (BallCount == 4) return;

        Balls[BallCount] = ball;
        ball.transform.SetParent(BallParent);
        ball.transform.localPosition = new Vector3(0f, -1.5f + BallCount, 0f);

        BallCount += 1;
    }

    public void ClearBalls()
    {

    }

    public BallAgent PopSelectedBall()
    {
        BallAgent pop = SelectedBall;
        Balls[BallCount - 1] = null;
        BallCount -= 1;

        return pop;
    }

    public BallAgent PopLastBall()
    {
        BallAgent pop = Balls[BallCount - 1];
        Balls[BallCount - 1] = null;
        BallCount -= 1;

        return pop;
    }

    public void RemoveLastBall()
    {
        for (int i = 0; i < Balls.Length; i++)
        {
            if (Balls[Balls.Length - 1 - i] == null)
                continue;

            Destroy(Balls[Balls.Length - 1 - i].gameObject);
            Balls[Balls.Length - 1 - i] = null;
        }
    }

    public void SelectBall()
    {
        SelectedBall = Balls[BallCount - 1];

        if (SelectedBall != null)
        {
            IsSelected = true;
            Vector3 newPos = SelectedBall.transform.localPosition;
            newPos.y = 2.5f;
            SelectedBall.transform.localPosition = newPos;
        }

    }

    public void DeSelectBall()
    {
        IsSelected = false;
        Vector3 newPos = SelectedBall.transform.localPosition;
        newPos.y = -1.5f + (BallCount - 1);
        SelectedBall.transform.localPosition = newPos;
        SelectedBall = null;

    }

    public bool CheckBalls()
    {
        if (BallCount < 4)
            return false;

        if (Balls[0].IsEqual(Balls[1]) && Balls[0].IsEqual(Balls[2]) && Balls[0].IsEqual(Balls[3]))
            IsUnicolor = true;
        else
            IsUnicolor = false;

        return IsUnicolor;
    }
}

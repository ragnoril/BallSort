using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
                //DontDestroyOnLoad(_instance.gameObject);
            }

            return _instance;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            //DontDestroyOnLoad(this);
        }
        else
        {
            if (this != _instance)
                Destroy(this.gameObject);
        }
    }

    public int LevelId;
    
    public bool IsGameRunning;

    public UIManager UI;

    public GameObject TubePrefab;
    public GameObject[] BallPrefabs;

    public List<TubeAgent> Tubes;
    public TubeAgent SelectedTube;

    public int MinTubeCount;
    public int MinColorCount;
    public int MaxTubeCount;
    public int MaxColorCount;

    public int AddedTubePerLevel;
    public int AddedColorPerLevel;

    public int CurrentTubeCount;
    public int CurrentColorCount;

    public int MinLineCount;
    public float MinCameraSize = 9f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 rayPos = new Vector2(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y);
            RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero, 0f);

            if (hit)
            {
                if (hit.transform.tag == "Player")
                {
                    if (SelectedTube == null)
                    {
                        
                        TubeAgent tube = hit.transform.GetComponent<TubeAgent>();
                        tube.SelectBall();
                        SelectedTube = tube;
                    }
                    else
                    {
                        TubeAgent tube = hit.transform.GetComponent<TubeAgent>();
                        if (tube.BallCount < 4)
                        {
                            BallAgent ball = SelectedTube.PopSelectedBall();
                            tube.PushBall(ball);

                            SelectedTube = null;
                            
                            //tube.CheckBalls(); if returns true play success animation particle etc.

                            CheckForWin();
                        }
                    }
                }
                
            }
            else
            {
                if (SelectedTube != null)
                {
                    
                    SelectedTube.DeSelectBall();
                    SelectedTube = null;
                }
            }
        }
    }

    public void CheckForWin()
    {
        bool isWon = true;
        foreach (TubeAgent tube in Tubes)
        {
            if (tube.BallCount > 0)
            {
                isWon &= tube.CheckBalls();
                
            }
        }

        if (isWon == true)
        {
            IsGameRunning = false;
            LevelId += 1;
            UI.OpenNextLevel();
            //level completed
        }
    }

    public void StartGame()
    {
        UI.OpenInGameUI();
        GenerateLevel();
    }

    public void ClearLevel()
    {
        foreach(TubeAgent tube in Tubes)
        {
            Destroy(tube.gameObject);
        }

        Tubes.Clear();
    }

    public void GenerateLevel()
    {
        CurrentTubeCount = MinTubeCount + ((LevelId / AddedTubePerLevel) * 2);
        CurrentColorCount = MinColorCount + (LevelId / AddedColorPerLevel);

        if (CurrentColorCount > MaxColorCount)
            CurrentColorCount = MaxColorCount;

        if (CurrentTubeCount > MaxTubeCount)
            CurrentTubeCount = MaxTubeCount;

        ClearLevel();

        int colorAddCount = 0;
        int lineCountDivider = MinLineCount;

        int tubeLeft = CurrentTubeCount;
        int lineCount = CurrentTubeCount / lineCountDivider;
        while (lineCount > 1)
        {
            lineCountDivider += 1;
            lineCount = CurrentTubeCount / lineCountDivider;
            Camera.main.orthographicSize = MinCameraSize + ((lineCountDivider - MinLineCount) * 2f);
        }

        float lineStart = 0f;

        if (lineCount > 0)
            lineStart = 3f;

        while (tubeLeft > 0)
        {
            int rowCount = CurrentTubeCount / 2;
            //Debug.Log("rowc: " + rowCount.ToString());

            if (rowCount < lineCountDivider)
                rowCount = lineCountDivider;

            if (rowCount > tubeLeft)
                rowCount = tubeLeft;

            //Debug.Log("rowc after: " + rowCount.ToString());

            for (int i = 0; i < rowCount; i++)
            {
                GameObject go = GameObject.Instantiate(TubePrefab, new Vector3((-rowCount + 1) + (i * 2f), lineStart, 0f), Quaternion.identity);
                go.transform.SetParent(transform);
                TubeAgent tube = go.GetComponent<TubeAgent>();
                Tubes.Add(tube);                
                tubeLeft -= 1;

                if (tubeLeft > 1)
                {
                    int ballId = colorAddCount;
                    if (ballId >= CurrentColorCount)
                    {
                        ballId = Random.Range(0, CurrentColorCount);
                    }

                    tube.AddBall(ballId);
                    tube.AddBall(ballId);
                    tube.AddBall(ballId);
                    tube.AddBall(ballId);

                    colorAddCount += 1;
                }

            }

            lineStart = lineStart * -1f;
        }

        RandomizeBallsInTubes(100);


    }

    public void RandomizeBallsInTubes(int randAmount)
    {
        for(int i=0; i < randAmount; i++)
        {
            int pickTubeIdA = Random.Range(0, Tubes.Count);
            TubeAgent pickTubeA = Tubes[pickTubeIdA];
            while (pickTubeA.BallCount == 0)
            {
                pickTubeIdA = Random.Range(0, Tubes.Count);
                pickTubeA = Tubes[pickTubeIdA];
            }

            int pickTubeIdB = Random.Range(0, Tubes.Count);
            while (pickTubeIdB == pickTubeIdA)
            {
                pickTubeIdB = Random.Range(0, Tubes.Count);
            }
            

            TubeAgent pickTubeB = Tubes[pickTubeIdB];
            while (pickTubeB.BallCount == 4)
            {
                pickTubeIdB = Random.Range(0, Tubes.Count);
                while (pickTubeIdB == pickTubeIdA)
                {
                    pickTubeIdB = Random.Range(0, Tubes.Count);
                }
                pickTubeB = Tubes[pickTubeIdB];
            }

            pickTubeB.PushBall(pickTubeA.PopLastBall());
        }

        for (int i = Tubes.Count - 2; i < Tubes.Count; i++)
        {
            TubeAgent tube = Tubes[i];

            //for(int n = 0; n < tube.BallCount; n++)
            while (tube.BallCount > 0)
            {
                TubeAgent pickTube = Tubes[Random.Range(0, Tubes.Count - 2)];
                while (pickTube.BallCount == 4)
                {
                    pickTube = Tubes[Random.Range(0, Tubes.Count - 2)];
                }

                pickTube.PushBall(tube.PopLastBall());
            }
        }
    }
}

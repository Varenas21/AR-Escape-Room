using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [SerializeField] private LevelData level;
    [SerializeField] private Pipe cellPrefab;

    private bool hasGameFinished;
    private Pipe[,] pipes;
    private List<Pipe> startPipes;


    private void Awake()
    {
        Instance = this;
        hasGameFinished = false;
        SpawnLevel();
    }

    private void SpawnLevel()
    {
        pipes = new Pipe[level.Row, level.Column];
        startPipes = new List<Pipe>();

        for (int i = 0; i < level.Row; i++)
        {
            for (int j = 0; j < level.Column; j++)
            {
                Vector2 spawnPos = new Vector2(j + 0.5f, i + 0.5f);
                Pipe tempPiper = Instantiate(cellPrefab);
                tempPiper.transform.position = spawnPos;
                tempPiper.Init(level.Data[i * level.Column + j]);
                pipes[i, j] = tempPiper;

                if (tempPiper.PipeType == 1)
                {
                    startPipes.Add(tempPiper);
                }
            }
        }

        Camera.main.orthographicSize = Mathf.Max(level.Row, level.Column);
        Vector3 cameraPos = Camera.main.transform.position;
        cameraPos.x = level.Column * 0.5f;
        cameraPos.y = level.Column * 0.5f;
        Camera.main.transform.position = cameraPos;
    }


    private void Update()
    {
        if (!hasGameFinished) return;

        Vector3 MousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        int row = Mathf.FloorToInt(MousePosition.x);
        int column = Mathf.FloorToInt(MousePosition.y);

        if (row < 0 || column < 0) return;
        if (row >= level.Row) return;
        if (column >= level.Row) return;

        if (Input.GetMouseButtonDown(0))
        {
            pipes[row, column].UpdateInput();
            StartCoroutine(ShowHint());
        }
    }

    private IEnumerator ShowHint()
    {
        yield return new WaitForSeconds(0.1f);
        CheckFill();
        CheckWin();
    }

    private void CheckFill()
    {
        for (int i = 0; i <level.Row; i++)
        {
            for (int j = 0; j <level.Column; j++)
            {
                Pipe temPipe = pipes[i, j];
                if (temPipe.PipeType != 0)
                {
                    temPipe.isFilled = false;

                }
            }
        }

        Queue<Pipe> check = new Queue<Pipe>();
        HashSet<Pipe> finished = new HashSet<Pipe>();

        foreach(var pipe in startPipes)
        {
            check.Enqueue(pipe);

        }

        while(check.Count > 0)
        {
            Pipe pipe = check.Dequeue();
            finished.Add(pipe);
            List<Pipe> connected = pipe.ConnectedPipes();
            foreach (var connectedPipe in connected)
            {
                if (!finished.Contains(connectedPipe))
                {
                    check.Enqueue(connectedPipe);
                }
            }


        }

        foreach(var filled in finished)
        {
            filled.isFilled = true;
        }

        for (int i = 0; i < level.Row; i++)
        {
            for (int j = 0; j < level.Column; j ++)
            {
                Pipe temPipe = pipes[i, j];
                temPipe.UpdateFilled();
            }
        }
    }

    private void CheckWin()
    {
        for (int i = 0; i < level.Row; i++)
        {
            for (int j = 0; j < level.Column; j++)
            {
                if (!pipes[i, j].isFilled) return;
            }
        }

        hasGameFinished = true;
        StartCoroutine(GameFinished());
    }

    private IEnumerator GameFinished()
    {
        yield return new WaitForSeconds(2f);
        // TODO ADD THE START OF THE NUMBER GENERATOR
    }


}

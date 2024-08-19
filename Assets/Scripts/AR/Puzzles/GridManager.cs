using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public GameObject pipePrefab;
    public int gridSize = 2;
    public float spacing = 0.09f;
    public float cellSize = 0.09f;
    
    private Pipe[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
        transform.Rotate(0, -90, 0);
    }

    void CreateGrid()
    {
        grid = new Pipe[gridSize, gridSize];
        Vector3 gridStartPosition = new Vector3(1.23000002f, 0.646000028f, -0.721000016f);

        for (int x = 0; x < gridSize; x++) 
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = gridStartPosition + new Vector3(x * cellSize, y * cellSize, 0);
                GameObject pipeObject = Instantiate(pipePrefab, position, Quaternion.identity, transform);
                Pipe pipe = pipeObject.GetComponent<Pipe>();

                // Random rotation
                float randAngle = Random.Range(0f, 360f);
                pipe.transform.Rotate(0,0,randAngle);


                grid[x,y] = pipe;
            }
        }
    }
}

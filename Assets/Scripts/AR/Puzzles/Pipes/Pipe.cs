using System.Collections.Generic;
using UnityEngine;

public class Pipe : MonoBehaviour
{
    [HideInInspector] public bool isFilled;
    [HideInInspector] public int PipeType;

    [SerializeField] private Transform[] _pipePrefabs;

    private Transform currentPipe;
    private int rotation;
    private SpriteRenderer emptySprite;
    private SpriteRenderer filledSprite;
    private List<Transform> connected;

    private const int minRotation = 0;
    private const int maxRotation = 3;
    private const int rotationMultiplier = 90;

    private bool hasGameFinished;


    public void Init(int pipe)
    {
        PipeType = pipe % 10;
        currentPipe = Instantiate(_pipePrefabs[PipeType], transform);
        currentPipe.transform.localPosition = Vector3.zero;

        if(PipeType == 1 || PipeType == 2) rotation = pipe / 10;
        else rotation = Random.Range(minRotation,maxRotation);


        if (PipeType == 0) return;

        currentPipe.transform.eulerAngles = new Vector3(0,0,rotation * rotationMultiplier);

        emptySprite = currentPipe.GetChild(0).GetComponent<SpriteRenderer>();
        filledSprite = currentPipe.GetChild(1).GetComponent<SpriteRenderer>();
        emptySprite.gameObject.SetActive(!isFilled);
        filledSprite.gameObject.SetActive(isFilled);

        connected = new List<Transform>();
        for (int i = 0; i < currentPipe.childCount; i++)
        {
            connected.Add(currentPipe.GetChild(i));
        }
    }

    public void UpdateInput()
    {
        if (PipeType == 0 || PipeType == 1 || PipeType == 2) 
        {
            return;
        }

        rotation = (rotation + 1) % (maxRotation + 1);
        currentPipe.transform.eulerAngles = new Vector3(0,0,rotation * rotationMultiplier);

    }

    public void UpdateFilled()
    {
        if (PipeType == 0 ) return;
        emptySprite.gameObject.SetActive(!isFilled);
        filledSprite.gameObject.SetActive(isFilled);
    }

    public List<Pipe> ConnectedPipes()
    {
        List<Pipe> result = new List<Pipe>();

        foreach (var item in connected)
        {
            RaycastHit2D[] hit = Physics2D.RaycastAll(item.transform.position, Vector2.zero, 0.1f);
            for (int i = 0; i <= hit.Length; i++)
            {
                result.Add(hit[i].collider.transform.parent.parent.GetComponent<Pipe>());

            }

        }

        return result;
    }


}

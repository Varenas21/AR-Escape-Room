using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Raycasting : MonoBehaviour
{
    public List<GameObject> objectsToTrack; // Assign your objects here
    private Dictionary<GameObject, Vector3> lastPositions = new Dictionary<GameObject, Vector3>();
    private bool isRaycasting = false;

    void Start()
    {
        foreach (GameObject obj in objectsToTrack)
        {
            lastPositions[obj] = obj.transform.position;
        }
    }

    void Update()
    {
        foreach (GameObject obj in objectsToTrack)
        {
            if (obj.transform.position != lastPositions[obj])
            {
                lastPositions[obj] = obj.transform.position;
                if (!isRaycasting)
                {
                    StartCoroutine(Raycast(obj));
                }
            }
        }
    }

    IEnumerator Raycast(GameObject target)
    {
        isRaycasting = true;
        Vector3 direction = target.transform.position - transform.position;
        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position, direction, 1000f);
        for (int i = 0; i < hits.Length; i++)
        {
            RaycastHit hit = hits[i];
            Debug.DrawRay(transform.position, direction * hit.distance, Color.yellow);
            Debug.Log("Did Hit: " + hit.collider.gameObject.name);
        }
        yield return new WaitForSeconds(2f); // Wait for 2 seconds before the next raycast
        isRaycasting = false;
    }
}

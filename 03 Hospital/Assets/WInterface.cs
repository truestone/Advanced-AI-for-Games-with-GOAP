using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WInterface : MonoBehaviour
{
    public GameObject newResourcePrefab;
    public NavMeshSurface surface;
    public GameObject hospital;

    GameObject focusObj;
    Vector3 goalPos;

    Vector3 clickOffset = Vector3.zero;
    bool offsetCalc = false;
    bool deleteResource = false;

    void Start()
    {
        
    }

    public void MouseOnHoverTrash()
    {
        deleteResource = true;
    }

    public void MosueOutHoverTrash()
    {
        deleteResource = false;
    }

    void Update()
    {
        if (!focusObj && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            offsetCalc = false;
            clickOffset = Vector3.zero;

            if (hit.transform.tag == "Toilet")
            {
                focusObj = hit.transform.gameObject;
            }
            else
            {
                goalPos = hit.point;
                focusObj = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
            }
            focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (focusObj && Input.GetMouseButton(0))
        {
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(rayMove, out hitMove))
            {
                return;
            }

            if (!offsetCalc)
            {
                clickOffset = hitMove.point - focusObj.transform.position;
                offsetCalc = true;
            }
            goalPos = hitMove.point - clickOffset;
            focusObj.transform.position = goalPos;
        }    
        else if (focusObj && Input.GetMouseButtonUp(0))
        {
            if (deleteResource)
            {
                GWorld.Instance.GetQueue("toilets").RemoveResource(focusObj);
                GWorld.Instance.GetWorld().ModifyState("FreeToilet", -1);
                Destroy(focusObj);
            }
            else
            {
                focusObj.transform.parent = hospital.transform;
                GWorld.Instance.GetQueue("toilets").AddResource(focusObj);
                GWorld.Instance.GetWorld().ModifyState("FreeToilet", 1);
                focusObj.GetComponent<Collider>().enabled = true;     
            }
            surface.BuildNavMesh();
            focusObj = null;
        }

        if (focusObj && (Input.GetKeyDown(KeyCode.Less) || Input.GetKeyDown(KeyCode.Comma)))
        {
            focusObj.transform.Rotate(0, -90, 0);
        }

        if (focusObj && (Input.GetKeyDown(KeyCode.Greater) || Input.GetKeyDown(KeyCode.Period)))
        {
            focusObj.transform.Rotate(0, 90, 0);
        }
    }
}

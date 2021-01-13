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

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            goalPos = hit.point;
            focusObj = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
        }
        else if (focusObj && Input.GetMouseButton(0))
        {
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(rayMove, out hitMove))
            {
                return;
            }

            goalPos = hitMove.point;
            focusObj.transform.position = goalPos;
        }    
        else if (focusObj && Input.GetMouseButtonUp(0))
        {
            focusObj.transform.parent = hospital.transform;
            surface.BuildNavMesh();
            GWorld.Instance.GetQueue("toilets").AddResource(focusObj);
            GWorld.Instance.GetWorld().ModifyState("FreeToilet", 1);
            focusObj = null;
        }
    }
}

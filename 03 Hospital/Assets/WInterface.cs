using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WInterface : MonoBehaviour
{
    public GameObject newResourcePrefab;

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
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class WInterface : MonoBehaviour
{
    public GameObject[] allResources;
    GameObject newResourcePrefab;

    public NavMeshSurface surface;
    public GameObject hospital;

    GameObject focusObj;
    ResourceData foData;
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

    public void ActivateToilet()
    {
        newResourcePrefab = allResources[0];
    }

    public void ActivateCubicle()
    {
        newResourcePrefab = allResources[1];
    }

    void Update()
    {
        if (!focusObj && Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(ray, out hit))
            {
                return;
            }

            offsetCalc = false;
            clickOffset = Vector3.zero;

            Resource r = hit.transform.GetComponent<Resource>();

            if (r != null)
            {
                focusObj = hit.transform.gameObject;
                foData = r.info;
                GWorld.Instance.GetQueue(foData.resourceQueue).RemoveResource(focusObj);
                GWorld.Instance.GetWorld().ModifyState(foData.resourceState, -1);                
            }
            else if (newResourcePrefab != null)
            {
                goalPos = hit.point;
                focusObj = Instantiate(newResourcePrefab, goalPos, newResourcePrefab.transform.rotation);
                foData = focusObj.GetComponent<Resource>().info;
            }
            if (focusObj)
                focusObj.GetComponent<Collider>().enabled = false;
        }
        else if (focusObj && Input.GetMouseButton(0))
        {
            int layerMask = 1 << LayerMask.NameToLayer("Floor");
            RaycastHit hitMove;
            Ray rayMove = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (!Physics.Raycast(rayMove, out hitMove, Mathf.Infinity, layerMask))
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
                Destroy(focusObj);
            }
            else
            {
                focusObj.transform.parent = hospital.transform;
                GWorld.Instance.GetQueue(foData.resourceQueue).AddResource(focusObj);
                GWorld.Instance.GetWorld().ModifyState(foData.resourceState, 1);
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

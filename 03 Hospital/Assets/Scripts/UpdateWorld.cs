using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateWorld : MonoBehaviour
{
    Text states;

    void Awake() {
        states = GetComponent<Text>();
    }

    void LateUpdate()
    {
        Dictionary<string, int> worldStates = GWorld.Instance.GetWorld().GetStates();
        states.text = "";
        foreach (KeyValuePair<string, int> s in worldStates)
        {
            states.text += s.Key + ", " + s.Value + "\n";
        }
    }
}

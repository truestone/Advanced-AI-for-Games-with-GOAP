using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Dictionary<string, int> myDict;
    public List<string> myList;
    public string[] myArray;

    // Start is called before the first frame update
    void Start()
    {
        List<string> strings = new List<string> {"abc", "def"};

        strings.Remove("def");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

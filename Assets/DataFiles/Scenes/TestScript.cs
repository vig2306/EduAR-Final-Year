using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    int modelIndex = 0;

    // Start is called before the first frame update


    void Start()
    {
        Debug.Log(ChangeScreen.clickedIndex);
        Debug.Log("Inside ARP");
        Debug.Log(modelIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

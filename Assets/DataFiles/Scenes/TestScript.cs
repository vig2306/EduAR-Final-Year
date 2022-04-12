using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    int modelIndex = 0;

    // Start is called before the first frame update

    private string[] btnText;

    void Start()
    {
        Debug.Log(ChangeScreen.clickedIndex);
        Debug.Log("Inside ARP");
        Debug.Log(modelIndex);
        btnText = new string[26]{ "Apple", "Ball", "Cat", "Dog", "Elephant","Fish", "Giraffe", "Hat", "Ice-Cream","Jellyfish","Kangaroo","Lion","Monkey","Newspaper","Owl","Penguin","Queen","Rat","Ship","Tree","Umbrella","Violin","Watch","Xylophone","YoYo", "Zebra"};
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(btnText[0]);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class HomeButtonGeography : MonoBehaviour
{
    public void backbtn()
    {
        SceneManager.LoadScene("StartSCreen");
    }
    public void solar()
    {
        SceneManager.LoadScene("SolarSystem");
    }
    public void learn()
    {
        SceneManager.LoadScene("main");
    }
    public void quiz()
    {
        SceneManager.LoadScene("Geographyquiz");
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System.Text;
public class ChangeScreen : MonoBehaviour
{
    public static int clickedIndex = 0;

    public void onpress(){
        string name = EventSystem.current.currentSelectedGameObject.name;
        byte[] ASCIIvalues = Encoding.ASCII.GetBytes(name);
        clickedIndex = ASCIIvalues[0]-65;
        SceneManager.LoadScene("AlphabetScene");
                
    }
    public void backbtn(){
        SceneManager.LoadScene("HomeScene");
    }
    public void restartbtn(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
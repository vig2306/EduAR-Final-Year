using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ARPlacement : MonoBehaviour
{

    public GameObject UIArrows;

   // public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public Text objText;
    public GameObject[] arModels;
    public AudioSource[] audioPlay;
    
    int modelIndex = 0;

    List<string> btnText = new List<string>(new string[]{ "Apple", "Ball", "Cat", "Dog", "Elephant","Fish", "Giraffe", "Hat", "Ice-Cream","Jellyfish","Kangaroo","Lion","Monkey","Newspaper","Owl","Penguin","Queen","Rat","Ship","Tree","Umbrella","Violin","Watch","Xylophone","YoYo", "Zebra"});


    private ChangeScreen changeScreen;



    void Start()
    {
        modelIndex = ChangeScreen.clickedIndex;
        Debug.Log("Inside ARP");
        Debug.Log(modelIndex);
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        UIArrows.SetActive(false);
        

    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if (spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
           
            ARPlaceObject(modelIndex);
            UIArrows.SetActive(true);
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }
    void UpdatePlacementIndicator()
    {
        if (spawnedObject == null && placementPoseIsValid)
        {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(PlacementPose.position, PlacementPose.rotation);
        }
        else
        {
            placementIndicator.SetActive(false);
        }
    }

    void UpdatePlacementPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        aRRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid && spawnedObject == null)
        {
            PlacementPose = hits[0].pose;
        }
    }

    void ARPlaceObject(int id)
    {
        for(int i = 0; i < arModels.Length; i++)
        {
            if(i == id)
            {
                GameObject clearUp = GameObject.FindGameObjectWithTag("ARMultiModel");
                Destroy(clearUp);
                objText.text = btnText[i];
                spawnedObject = Instantiate(arModels[i], PlacementPose.position, PlacementPose.rotation); 
            }
        }

       
    }

    public void ModelChangeRight()
    {
        if (modelIndex < arModels.Length - 1)
            modelIndex++;
        else
            modelIndex = 0;

        ARPlaceObject(modelIndex);
    }
    public void ModelChangeLeft()
    {
        if (modelIndex > 0)
            modelIndex--;
        else
            modelIndex = arModels.Length - 1;

        ARPlaceObject(modelIndex);
    }

    public void restartbtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void backbtn()
    {
        if(SceneManager.GetActiveScene().name == "QuizScene")
        {
            SceneManager.LoadScene("HomeScene");
        }
        else
        {
            SceneManager.LoadScene("Scroll");
        }
        
    }

    public void playAudio()
    {
         for(int i = 0; i < audioPlay.Length; i++)
        {
            if(i == modelIndex)
            {
                audioPlay[i].PlayDelayed(1);
            }
        }
    }



}



using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.EventSystems;

public class ARQuizPlacement : MonoBehaviour
{

    public GameObject Sun;
    public GameObject Earth;
    public GameObject Mars;
    public GameObject Mercury;
    public GameObject Jupiter;
    public GameObject Neptune;
    public GameObject Venus;
    public GameObject Uranus;
    public GameObject Saturn;

    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    public Hashtable questionAnswers = new Hashtable();
    string temp,tempGO;
    int index;
    public Text questionText;
    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;


    List<string> answers = new List<string>(new string[] {"Sun", "Earth", "Mars", "Mercury", "Jupiter", "Neptune", "Venus", "Uranus", "Saturn"  });
    List<string> options = new List<string>(new string[] { "A", "B", "C"});

    


    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();


        questionAnswers.Add("Sun","First Question");
        questionAnswers.Add("Earth","Second Question");
        questionAnswers.Add("Mars","Third Question");
        questionAnswers.Add("Mercury","Fourth Question");
        questionAnswers.Add("Jupiter","Fifth Question");
        questionAnswers.Add("Neptune","Sixth Question");
        questionAnswers.Add("Venus","Seventh Question");
        questionAnswers.Add("Uranus","Eigth Question");
        questionAnswers.Add("Saturn","Ninth Question");

        createQuestion();
    }

    // need to update placement indicator, placement pose and spawn 
    void Update()
    {
        if(spawnedObject == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();


    }
    void UpdatePlacementIndicator()
    {
        if(spawnedObject == null && placementPoseIsValid)
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
        if(placementPoseIsValid)
        {
            PlacementPose = hits[0].pose;
            Debug.Log("Hiiiiiii");
        }
    }

    void ARPlaceObject()
    {
        Vector3 newpose =  new Vector3(0f,0.3f,0f);
        Vector3 newpose2 =  new Vector3(0f,0.3f,0f);
        spawnedObject = Instantiate(Sun, PlacementPose.position, PlacementPose.rotation);
        Instantiate(Earth, PlacementPose.position + newpose, PlacementPose.rotation);
        Instantiate(Mars, PlacementPose.position - newpose2, PlacementPose.rotation);
        
    }

    
    void createQuestion()
    {
        index = Random.Range(0,9);
        temp = answers[index];

        int op1 = index;
        int op2;
        int op3;

        do {
            op2 = Random.Range (0, 9);
        } while (op2 == op1);
       
       do {
            op3 = Random.Range (0, 9);
        } while (op3 == op1 || op3 == op2); 

        options[0] = answers[index];
        options[1] = answers[op2];
        options[2] = answers[op3];
        
         for (int i = 0; i < 3; i++) {
             int rnd = Random.Range(0, 3);
             tempGO = options[rnd];
             options[rnd] = options[i];
             options[i] = tempGO;
         }     

        questionText.text = (string)questionAnswers[temp];
        optionOne.text = options[0];
        optionTwo.text = options[1];
        optionThree.text = options[2];

    }

     public void checkAnswer() 
    {
        string clickedValue = GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().GetComponentInChildren<Text>().text;

        if(clickedValue == temp)
        {
             Debug.Log("GG");
             createQuestion();
        }
        else
        {
            Debug.Log(":(");
        }


       
    }


}



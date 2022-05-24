using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System.Security.Cryptography;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;

public class GeographyQuizLogic : MonoBehaviour
{
    List<string> answers = new List<string>(new string[] {"Sun", "Earth", "Mars", "Mercury", "Jupiter", "Neptune", "Venus", "Uranus", "Saturn"  });
    List<string> options = new List<string>(new string[] { "A", "B", "C"});

    public Hashtable questionAnswers = new Hashtable();
    string temp,tempGO;
    GameObject temp3D;
    int index;
    int score = 0;
    public Text questionText;
    public Text optionOne;
    public Text optionTwo;
    public Text optionThree;
    public Text scoreText;
    public Text finalScoreText;

    public GameObject pan1;
    public GameObject pan2;
    public GameObject background;

    public GameObject arCamera;

    public GameObject[] firstOption;
    public GameObject[] secondOption;
    public GameObject[] thirdOption;

    public GameObject[] objectOption;


    public GameObject arObjectToSpawn;
    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;
    public GameObject spawnedObject1;
    public GameObject spawnedObject2;
    public GameObject spawnedObject3;


    // Start is called before the first frame update
    void Start()
    {
        background.SetActive(false);

        questionAnswers.Add("Sun","Which object is at the center of the solar system?");
        questionAnswers.Add("Earth","Only planet which has life on it is ?");
        questionAnswers.Add("Mars"," This planet is known as the red planet ");
        questionAnswers.Add("Mercury","Closest Planet to the Sun");
        questionAnswers.Add("Jupiter","Largest planet in the solar system");
        questionAnswers.Add("Neptune"," Fartherst planet from the sun");
        questionAnswers.Add("Venus"," Hottest planet in the solar system");
        questionAnswers.Add("Uranus","Which planet has recorded the lowest temperature");
        questionAnswers.Add("Saturn","Which planet has rings ?");

        pan1.SetActive(false);
        pan2.SetActive(false);

        createQuestion();

        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(spawnedObject1 == null && placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            ARPlaceObject();
        }


        UpdatePlacementPose();
        UpdatePlacementIndicator();
    
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

        // firstOption[answers.IndexOf(options[0])].SetActive(true);
        // secondOption[answers.IndexOf(options[1])].SetActive(true);
        // thirdOption[answers.IndexOf(options[2])].SetActive(true);
        Debug.Log(answers.IndexOf(options[0]));
        Debug.Log(answers.IndexOf(options[1]));
        Debug.Log(answers.IndexOf(options[2]));
        Debug.Log(firstOption[answers.IndexOf(options[0])]);

    }

    public void checkAnswer() 
    {
        string clickedValue = GameObject.Find(EventSystem.current.currentSelectedGameObject.name).GetComponent<Button>().GetComponentInChildren<Text>().text;
        

        if(clickedValue == temp)
        {
            Debug.Log("GG");
            score = score + 1;
            var dummy = "Score : " + score.ToString();
            scoreText.text = dummy;
            Destroy(spawnedObject1);
            Destroy(spawnedObject2);
            Destroy(spawnedObject3);
            createQuestion();

            Vector3 newpose =  new Vector3(0.4f,0f,0f);
            spawnedObject1 = Instantiate(firstOption[answers.IndexOf(options[0])], PlacementPose.position - newpose, PlacementPose.rotation);
            spawnedObject2 = Instantiate(secondOption[answers.IndexOf(options[1])], PlacementPose.position, PlacementPose.rotation);
            spawnedObject3 = Instantiate(thirdOption[answers.IndexOf(options[2])], PlacementPose.position + newpose, PlacementPose.rotation);


        }
        else
        {
            var dummy = "Score : " + score.ToString();
            finalScoreText.text = dummy;
            background.SetActive(true);
            pan1.SetActive(false);
            pan2.SetActive(false);
            arCamera.SetActive(false);
            Debug.Log(":(");
            Debug.Log("Quiz");
        }


       
    }

    public void restartbtn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void backbtn()
    {
        SceneManager.LoadScene("GeographyHome");
    }
    
    void UpdatePlacementIndicator()
    {
        if(spawnedObject1 == null && placementPoseIsValid)
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
        }
    }

    void ARPlaceObject()
    {
        Vector3 newpose =  new Vector3(0.4f,0f,0f);

        pan1.SetActive(true);
        pan2.SetActive(true);

        spawnedObject1 = (GameObject)Instantiate(firstOption[answers.IndexOf(options[0])], PlacementPose.position - newpose, PlacementPose.rotation);
        spawnedObject2 = (GameObject)Instantiate(secondOption[answers.IndexOf(options[1])], PlacementPose.position, PlacementPose.rotation);
        spawnedObject3 = (GameObject)Instantiate(thirdOption[answers.IndexOf(options[2])], PlacementPose.position + newpose, PlacementPose.rotation);
    }
        
        
}

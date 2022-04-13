using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AlphaQuiz : MonoBehaviour
{
    
    public GameObject[] alphabets;
    public GameObject[] arObjects;

    int index,op2,op3;
    GameObject answer,tempGO;
    public GameObject[] options;
    public GameObject f;
    public GameObject o;
    public GameObject r;
    GameObject alpha;

    public GameObject placementIndicator;
    private GameObject spawnedObject;
    private Pose PlacementPose;
    private ARRaycastManager aRRaycastManager;
    private bool placementPoseIsValid = false;

    public AudioSource clapping;

    void Start()
    {
        aRRaycastManager = FindObjectOfType<ARRaycastManager>();
        Debug.Log("Hiiiiii");
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
        checkAnswer();
        Debug.Log(index);
        Debug.Log(op2);
        Debug.Log(op3);

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
        }
    }

    void ARPlaceObject()
    {
        // Debug.Log(index);
        // Debug.Log(op2);
        // Debug.Log(op3);

        Vector3 newpose =  new Vector3(0.4f,0f,0f);

        Vector3 newpose2 =  new Vector3(0f,0.3f,0f);
        Vector3 newpose3 =  new Vector3(0.1f,0.3f,0f);
        Vector3 newpose4 =  new Vector3(0.2f,0.3f,0f);
        Vector3 newpose5 =  new Vector3(0.3f,0.3f,0f);


        Debug.Log(options[0]);
        Debug.Log(options[1]);
        Debug.Log(options[2]);
        Debug.Log(alphabets[index]);

        spawnedObject = Instantiate(options[0], PlacementPose.position - newpose, PlacementPose.rotation);
        spawnedObject = Instantiate(options[1], PlacementPose.position, PlacementPose.rotation);
        spawnedObject = Instantiate(options[2], PlacementPose.position + newpose, PlacementPose.rotation);

        spawnedObject = Instantiate(alphabets[index], PlacementPose.position + newpose2, PlacementPose.rotation);
        spawnedObject = Instantiate(f, PlacementPose.position + newpose3, PlacementPose.rotation);
        spawnedObject = Instantiate(o, PlacementPose.position + newpose4, PlacementPose.rotation);
        spawnedObject = Instantiate(r, PlacementPose.position + newpose5, PlacementPose.rotation);


    }

    void createQuestion()
    {
        index =  Random.Range(0,arObjects.Length);
        alpha = alphabets[index];
        answer = arObjects[index];
       
        options[0] = answer;

        do {
            op2 = Random.Range (0, arObjects.Length);
        } while (op2 == index);
       
       do {
            op3 = Random.Range (0, arObjects.Length);
        } while (op3 == index|| op3 == op2); 


        options[1] = arObjects[op2];
        options[2] = arObjects[op3];

        for (int i = 0; i < 3; i++) {
             int rnd = Random.Range(0, 3);
             tempGO = options[rnd];
             options[rnd] = options[i];
             options[i] = tempGO;
         } 


    }

    IEnumerator Winner()
    {
        clapping.Play();
        yield return new WaitForSeconds(3);
    }

     IEnumerator ScaleObject(GameObject G)
    {
        float scaleDuration = 1.5f;                                
        Vector3 actualScale = G.transform.localScale;             
        Vector3 targetScale = new Vector3 (0f,0f,0f);   

        Debug.Log(G);

        for(float t = 0; t < 1; t += Time.deltaTime / scaleDuration )
        {
            Debug.Log(G);
            G.transform.localScale = Vector3.Lerp(actualScale ,targetScale ,t);
            yield return null;
        }
    }

    void checkAnswer()
    {

        Debug.Log("Hello check");


       if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Debug.Log("Aaraha hai kya");
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
            // Debug.Log("Nicee",Physics.Raycast(ray, out hit) );
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log("Hello Inside check");
                if (hit.collider.gameObject.name == arObjects[index].name)
                {
                    Debug.Log("Correct answer");
                    StartCoroutine(Winner());
                }
                else if(hit.collider.gameObject.name == arObjects[op2].name)
                {
                    Debug.Log("Wrong answer");
                    StartCoroutine(ScaleObject(arObjects[op2]));
                }
                else if(hit.collider.gameObject.name == arObjects[op3].name)
                {
                    Debug.Log("Wrong answer");
                    StartCoroutine(ScaleObject(arObjects[op3]));
                }
            }
        }
    }


}




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.SceneManagement;
using Vuforia;

public class AlphabetQuiz : MonoBehaviour
{
    public GameObject[] alphabets;
    public GameObject[] questions;
    public GameObject clap;
    public GameObject AH;
    public GameObject IQ;
    public GameObject RZ;
    public AudioSource clapping;
    private int ansValue;
    // Start is called before the first frame update
    void Start()
    {
        for(int i=0;i<26;i++)
        {
            alphabets[i].SetActive(false);
            questions[i].SetActive(false);
            // clap.SetActive(false);

        }
        ansValue = Random.Range(0,26);
        Debug.Log(ansValue);
        while(ansValue==19)
        {
            ansValue = Random.Range(0,26);
        }
        int val1=0,val2=0;
        alphabets[ansValue].SetActive(true);
        questions[ansValue].SetActive(true);
        if(ansValue<8)
        {
            val1 = Random.Range(8,17);
            val2 = Random.Range(17,26);
            while(val2==19)
            {
                val2 = Random.Range(17,26);
            }
        }
        else if(ansValue>=8 && ansValue<17 )
        {
            val1 = Random.Range(0,8);
            val2 = Random.Range(17,26);
            while(val2==19)
            {
                val2 = Random.Range(17,26);
            }
        }
        else if (ansValue>=17 && ansValue<26 )
        {
            val1 = Random.Range(0,8);
            val2 = Random.Range(8,17);
        }
        alphabets[val1].SetActive(true);
        alphabets[val2].SetActive(true);

    }
    IEnumerator Winner()
    {
        // clap.SetActive(true);
        // clapping.Play();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("AQuiz");
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

    // Update is called once per frame
    void Update()
    {
        if(Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.touches[0].position);
            RaycastHit hit;
                 if(Physics.Raycast(ray, out hit))
                {
                    if(ansValue<8 && hit.collider.gameObject.name == "A-H")
                    {
                        StartCoroutine(Winner());
                    }
                    else if(ansValue>=8 && ansValue<17 && hit.collider.gameObject.name == "I-Q")
                    {
                        StartCoroutine(Winner());
                    }
                    else if(ansValue>=17 && ansValue<26 && hit.collider.gameObject.name == "R-Z")
                    {
                        StartCoroutine(Winner());
                    }
                    else if(ansValue>8 && hit.collider.gameObject.name == "A-H")
                    {
                        StartCoroutine(ScaleObject(AH));
                    }
                    else if((ansValue<8 || ansValue>=17) && hit.collider.gameObject.name == "I-Q")
                    {
                        StartCoroutine(ScaleObject(IQ));
                    }
                    else if(ansValue<=17 && hit.collider.gameObject.name == "R-Z")
                    {
                        StartCoroutine(ScaleObject(RZ));
                    }
                
                }
        }
        #if UNITY_EDITOR
        if(Input.GetMouseButtonDown(0))
        {
            Debug.Log("hi");
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit = new RaycastHit();
        
            if(Physics.Raycast(ray, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);
                if(ansValue<8 && hit.collider.gameObject.name == "A-H")
                {
                    StartCoroutine(Winner());
                }
                else if(ansValue>=8 && ansValue<17 && hit.collider.gameObject.name == "I-Q")
                {
                    StartCoroutine(Winner());
                }
                else if(ansValue>=17 && ansValue<26 && hit.collider.gameObject.name == "R-Z")
                {
                    StartCoroutine(Winner());
                }
                else if(ansValue>8 && hit.collider.gameObject.name == "A-H")
                {
                    StartCoroutine(ScaleObject(AH));
                }
                else if((ansValue<8 || ansValue>=17) && hit.collider.gameObject.name == "I-Q")
                {
                    StartCoroutine(ScaleObject(IQ));
                }
                else if(ansValue<=17 && hit.collider.gameObject.name == "R-Z")
                {
                    StartCoroutine(ScaleObject(RZ));
                }
                
            }
        }
        #endif
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
}

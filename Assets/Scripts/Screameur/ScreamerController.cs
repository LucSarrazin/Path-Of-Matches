using System.Collections.Generic;
using UnityEngine;

public class ScreamerController : MonoBehaviour
{
    
    public ScreamerData[] screamerData;
    public GameObject[] screamerPoints;
    public bool[] screamerPointsFree;
    [SerializeField] private PlayerReferences _playerReferences;
    [SerializeField] private int insanityLvl;
    [SerializeField] private float nextScreamerTime = 0f;
    [SerializeField] private float screamerDelayMin = 15f;
    [SerializeField] private float screamerDelayMax = 45f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        insanityLvl = _playerReferences.PlayerInsanity.InsanityLvl;
        if (Time.time >= nextScreamerTime)
        {
            float screamerDelay = Random.Range(screamerDelayMin, screamerDelayMax);
            if (insanityLvl > 70 && insanityLvl < 100)
            {
                int random = Random.Range(0, 3);
                Vector3 position = Vector3.zero;
                List<GameObject> availablePoints = new();
                switch (screamerData[random].spawnpointName)
                {
                    // Front = 0, Right = 1, Left = 2, Back = 3
                        case "Front":
                            if (screamerPoints[0].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[0].transform.position;
                            }
                            break;
                        
                        case "Back":
                            if (screamerPoints[3].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[3].transform.position;
                            }
                            break;
                        
                        case "Right":
                            if (screamerPoints[1].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[1].transform.position;
                            }
                            
                            break;
                        
                        case "Left":
                            if (screamerPoints[2].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[2].transform.position;
                            }
                            
                            break;
                        
                        default:
                            foreach (GameObject point in screamerPoints)
                            {
                                if (point.GetComponent<ScreamerPoints>().collided)
                                    availablePoints.Add(point);
                            }

                            if (availablePoints.Count > 0)
                            {
                                int randomPoint = Random.Range(0, availablePoints.Count);
                                position = availablePoints[randomPoint].transform.position;
                            }
                            break;
                }
                GameObject screamer = Instantiate(screamerData[random].screamerPrefab, position, Quaternion.identity);
                screamer.GetComponent<ScreamerBehaviour>().Execute();
                Debug.Log("Spawn d'un screameur (Niveau 0-3) " + screamerData[random].screamerName);
                Destroy(screamer, screamerData[random].destroyAfterSeconds);
            }   
            if (insanityLvl >= 100 && insanityLvl <= 150)
            {
                int random = Random.Range(3, 6);
                Vector3 position = Vector3.zero;
                List<GameObject> availablePoints = new();
                switch (screamerData[random].spawnpointName)
                {
                    // Front = 0, Right = 1, Left = 2, Back = 3
                        case "Front":
                            if (screamerPoints[0].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[0].transform.position;
                            }
                            break;
                        
                        case "Back":
                            if (screamerPoints[3].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[3].transform.position;
                            }
                            break;
                        
                        case "Right":
                            if (screamerPoints[1].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[1].transform.position;
                            }
                            
                            break;
                        
                        case "Left":
                            if (screamerPoints[2].GetComponent<ScreamerPoints>().collided)
                            { 
                                position = screamerPoints[2].transform.position;
                            }
                            
                            break;
                        
                        default:
                            foreach (GameObject point in screamerPoints)
                            {
                                if (point.GetComponent<ScreamerPoints>().collided)
                                    availablePoints.Add(point);
                            }

                            if (availablePoints.Count > 0)
                            {
                                int randomPoint = Random.Range(0, availablePoints.Count);
                                position = availablePoints[randomPoint].transform.position;
                            }
                            break;
                }
                GameObject screamer = Instantiate(screamerData[random].screamerPrefab, position, Quaternion.identity);
                screamer.GetComponent<ScreamerBehaviour>().Execute();
                Debug.Log("Spawn d'un screameur (Niveau 3-5) " + screamerData[random].screamerName);
                Destroy(screamer, screamerData[random].destroyAfterSeconds);
            }  
            
            nextScreamerTime = Time.time + screamerDelay;
        }

        // if (insanityLvl <= 70)
        // {
        //     for (int i = 0; i < screamerData.Length; i++)
        //     {
        //         screamerData[i].
        //     }
        // }
    }
}

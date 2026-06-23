using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class DistanceScreameur : ScreamerBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Camera cam;
    [SerializeField] private float playerDistance = 15f;
    
    public override void Execute()
    {
        Debug.Log("Le joueur voit le monstre derrière lui");
    }

    private void Awake()
    {
        target = gameObject;
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(cam.transform);
        gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        
        float distance1 = Vector3.Distance(transform.position, cam.transform.position);
        if (distance1 <= 3f)
        {
            Destroy(gameObject);
        }
    }
}
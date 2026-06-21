using UnityEngine;

public class ScreamerPoints : MonoBehaviour
{
    private RaycastHit hit;

    public bool collided;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            //Debug.Log("Did Hit");
            collided = true;
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * 1000, Color.white);
            //Debug.Log("Did not Hit");
            collided = false;
        }
    }
}

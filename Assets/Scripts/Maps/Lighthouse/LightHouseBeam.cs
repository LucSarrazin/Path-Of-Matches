using UnityEngine;

public class LightHouseBeam : MonoBehaviour
{
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, Speed * Time.deltaTime);
    }
}

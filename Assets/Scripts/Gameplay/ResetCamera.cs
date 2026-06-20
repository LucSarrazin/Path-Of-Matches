using UnityEngine;
using UnityEngine.PlayerLoop;

public class ResetCamera : MonoBehaviour
{
    [SerializeField] private GameObject gameObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetCameraPosition()
    {
        //gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.rotation = new Quaternion(0,0,0,0);
    }
}

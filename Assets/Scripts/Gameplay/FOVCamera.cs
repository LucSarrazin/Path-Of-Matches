using UnityEngine;
using System.Linq;

public class FOVCamera : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Camera cam;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsVisible())
        {
            target.GetComponent<Renderer>().material.color = Color.green;
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return planes.All(plane => plane.GetDistanceToPoint(target.transform.position) >= 0);
    }
}

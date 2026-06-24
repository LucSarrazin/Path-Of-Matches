using UnityEngine;

public class ObjectsFollowCamera : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private MeshRenderer boiteAllumette;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.LookAt(transform.position + camera.transform.forward);

        if ( !boiteAllumette.enabled)
        {
            gameObject.SetActive(false);
        }
    }
}

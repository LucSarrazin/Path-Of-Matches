using System;
using UnityEngine;
using System.Linq;
using Random = UnityEngine.Random;

public class WalkingBehind : ScreamerBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera cam;
    [SerializeField] private float speed;
    [SerializeField] private float speedRun;
    [SerializeField] private AudioClip[] footstepsClips;
    [SerializeField] private AudioSource footstepsAudioSource;
    [SerializeField] private float footstepDelay = 0.5f;
    [SerializeField] private float nextFootstepTime = 0f;
    [SerializeField] private float playerDistance = 15f;
    
    public override void Execute()
    {
        Debug.Log("Le joueur voit le monstre derrière lui");
    }

    private void Awake()
    {
        target = gameObject;
        rb = target.GetComponent<Rigidbody>();
        cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.LookAt(cam.transform);
        gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        if (IsVisible())
        {
            target.GetComponent<Renderer>().material.color = Color.green;
            Debug.Log("See");
            footstepsAudioSource.Stop();
        }
        else
        {
            target.GetComponent<Renderer>().material.color = Color.red;
            Debug.Log("Don't See");
            float distance = Vector3.Distance(transform.position, cam.transform.position);
            if (distance > playerDistance)
            {
                transform.position = Vector3.Lerp(transform.position, cam.transform.position, speedRun * Time.deltaTime);
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, cam.transform.position, speed * Time.deltaTime);
            }
            PlayFootstep();
        }
    }

    private bool IsVisible()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(cam);
        return planes.All(plane => plane.GetDistanceToPoint(target.transform.position) >= 0);
    }
    
    

    #region FootSteps
    public void PlayFootstep()
    {
        if (Time.time >= nextFootstepTime)
        {
            footstepsAudioSource.pitch = Random.Range(0.95f, 1.05f); // Switch pitch to randomise footsteps
            int i = Random.Range(0, footstepsClips.Length);
            footstepsAudioSource.clip = footstepsClips[i];
            footstepsAudioSource.PlayOneShot(footstepsAudioSource.clip);
        
            nextFootstepTime = Time.time + footstepDelay;
        }
    }
    #endregion
}
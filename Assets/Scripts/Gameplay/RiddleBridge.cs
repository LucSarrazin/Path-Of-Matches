using UnityEngine;

public class RiddleBridge : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _fallingBridge; 

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }

    private void Update()
    {
        if (rope == null)
        {
            _audioSource.PlayOneShot(_fallingBridge);

            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime
            );
        }
    }
}
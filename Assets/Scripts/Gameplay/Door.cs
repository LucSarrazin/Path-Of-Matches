using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Vector3 _doorPositionStart;
    [SerializeField] private Vector3 _doorPositionEnd;
    [SerializeField] private float speed;
    [SerializeField] private AudioSource _audioSource;
    private bool doorOpened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _doorPositionStart = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (doorOpened)
        {
            transform.position = Vector3.MoveTowards(transform.position, _doorPositionEnd, speed * Time.deltaTime);
        }
    }

    public void OpenDoor()
    {
        if (!doorOpened)
        {
            doorOpened = true;
            _audioSource.Play();
        }
    }
}

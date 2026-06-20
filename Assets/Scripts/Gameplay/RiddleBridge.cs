using System;
using UnityEngine;

public class RiddleBridge : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private Transform _plankPos; 
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private AudioSource _audioSource; 
    [SerializeField] private AudioClip _fallingBridge;

    private bool _hadFall = false; 

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);

        _hadFall = false; 
    }

    private void Update()
    {
        if (rope == null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime );
            //_audioSource.Play();
            //Debug.Log("Try Play sound smash");
        }

        if (!_hadFall && _plankPos.rotation.eulerAngles.z < 3)
        {
            _hadFall = true;
            Debug.Log($"Plank had fall ");
            _audioSource.Play();
        }
    }
}
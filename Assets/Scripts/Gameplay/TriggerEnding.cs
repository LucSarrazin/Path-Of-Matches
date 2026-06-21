using System;
using UnityEngine;

public class TriggerEnding : MonoBehaviour
{
    private bool oneTime = false;
    [SerializeField] private Animator blinkingEyes;
    [SerializeField] private PlayerReferences _playerReferences;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (oneTime == false)
            {
                oneTime = true;
                blinkingEyes.SetBool("Close", true);
                _playerReferences.PlayerMovements.CanMove(false);
                _playerReferences.PlayerMovements.CanLook(false);
            }
        }
    }
}

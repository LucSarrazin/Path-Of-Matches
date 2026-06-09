using UnityEngine;

public class StartingEvent : MonoBehaviour
{
    [SerializeField] private Animator _animatorEye;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _animatorEye.SetBool("Start", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

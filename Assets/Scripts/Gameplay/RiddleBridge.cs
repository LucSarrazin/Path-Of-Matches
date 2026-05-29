using UnityEngine;

public class RiddleBridge : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private float rotationSpeed = 100f;

    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, 0);
    }

    private void Update()
    {
        if (rope == null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime
            );
        }
    }
}
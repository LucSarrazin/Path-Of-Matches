using UnityEngine;
using UnityEngine.UIElements;

public class RiddleBridge : MonoBehaviour
{
    [SerializeField] private GameObject rope;
    [SerializeField] private float rotationReturnSpeed = 0.0005f;
    private Quaternion targetRotation;

    private void Start()
    {
        targetRotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
    }

    private void Update()
    {
        if (rope == null)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationReturnSpeed);
            if (rotationReturnSpeed < 10f)
            {
                rotationReturnSpeed = rotationReturnSpeed * 1.01f;
            }
        }
    }
}

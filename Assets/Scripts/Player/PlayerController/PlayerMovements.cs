using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
    private float _testSpeed = 5f; 


    public void MovePlayer(/*float speed*/)
    {
        Vector3 moveLR = PlayerReferences.Instance.Controls.MoveInputs.x * transform.right; 
        Vector3 moveFB = PlayerReferences.Instance.Controls.MoveInputs.y * transform.forward;

        Vector3 velocity = moveLR + moveFB;

        velocity.Normalize();
        velocity *= _testSpeed;
        velocity.y = PlayerReferences.Instance.Rigidbody.linearVelocity.y;
        PlayerReferences.Instance.Rigidbody.linearVelocity = velocity;


    }
}

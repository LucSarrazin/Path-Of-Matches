using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private float _distance;
    [SerializeField] private LayerMask _currentGround;

    private int GetCurrentGroundLayer()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, _distance, _currentGround);
        if (hit.collider != null)
        {
            return hit.collider.gameObject.layer;
        }
        return -1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _distance);
    }
}

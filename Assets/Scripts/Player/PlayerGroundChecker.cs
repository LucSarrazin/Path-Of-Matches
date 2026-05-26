using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private float _checkDistance = 0.5f;
    private LayerMask _layerMask;

    public int GetGroundLayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector2.down, out hit, _checkDistance, _layerMask))
        {
            return hit.collider.gameObject.layer;
        }
        return -1;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector2.down * _checkDistance);
    }

}

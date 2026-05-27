using UnityEngine;

public class PlayerGroundChecker : MonoBehaviour
{
    [SerializeField] private float _checkDistance = 0.5f;
    [SerializeField] private LayerMask _layerMask;

    public int GetGroundLayer()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, _checkDistance, _layerMask))
        {
            return hit.collider.gameObject.layer;
        }
        return -1;
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, Vector3.down * _checkDistance);
    }

}

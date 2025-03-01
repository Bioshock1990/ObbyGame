using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float distanceToCheck = 0.5f;
    [SerializeField] public bool isGrounded { get; private set; }
    [SerializeField] private LayerMask _layerMask;

    private void Update()
    {
        //Debug.DrawRay(transform.position, Vector2.down, Color.red, 0.1f);
        Ray ray = new Ray(transform.position, Vector2.down);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, distanceToCheck, _layerMask))
        {
            //Debug.Log("aaaaaa");
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
        }
    }
}

using UnityEngine;

public class Raycast : MonoBehaviour
{
    [SerializeField] private float raycastLength;
    // Start is called before the first frame update
    void Start()
    {
        if (Physics.Raycast(transform.position, Vector3.right, raycastLength, 1 << LayerMask.NameToLayer("Ground")))
            Debug.Log("Hit!");
        else
            Debug.Log("No Hit!");
    }

    // Update is called once per frame
    void Update()
    {        
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, transform.position + (Vector3.right * raycastLength));
    }
}

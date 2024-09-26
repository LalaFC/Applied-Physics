using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AimingMechExp : MonoBehaviour
{
    [SerializeField] GameObject target;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private Vector3 crossProduct;
    private bool canRotate = false;
    void Update()
    {
        
        if (Input.GetKey(KeyCode.A))
        {
            crossProduct = Vector3.Cross(Vector3.forward, (target.transform.position - transform.position));
            Debug.Log(crossProduct);
            float angle = Vector3.Angle(Vector3.forward, (target.transform.position - transform.position));
            transform.Rotate(crossProduct, angle * Time.deltaTime, Space.World);
        }
    }
}

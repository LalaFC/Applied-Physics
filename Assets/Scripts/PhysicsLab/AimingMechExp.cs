using Cinemachine.Utility;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class AimingMechExp : MonoBehaviour
{
    [SerializeField] GameObject target;
    [SerializeField] float range, closeRange;
    [SerializeField] float force;
    private Rigidbody rb;
    private bool canShoot = false, isAiming = false, inRange = false, reload = false, isCharging = true;
    private Vector3 crossProduct, dotProduct, initPos; 
    private Quaternion initRot;
    private float chargeTime;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        crossProduct = Vector3.Cross(Vector3.up, (target.transform.position - transform.position));
        initPos = new Vector3(0, 8.81f, 0);
        initRot = transform.rotation;
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Enemy");
    }

    // Update is called once per frame

    void Update()
    {
        if (target == null)
            target = GameObject.FindGameObjectWithTag("Enemy");
        if (isCharging)
        {
            crossProduct = Vector3.Cross(Vector3.up, (target.transform.position - transform.position));
            chargeTime += Time.deltaTime;
            transform.localScale = reload ? Vector3.zero : Vector3.Lerp(Vector3.zero, Vector3.one, chargeTime / 2);
            if (transform.localScale.x == 1) isCharging = false;
        }

        Vector3 direction = (target.transform.position - initPos).normalized;
        Vector3 startPt = new Vector3(initPos.x, (initPos.y - (initPos.y - target.transform.position.y)), initPos.z);
        Vector3 modifiedDir = new Vector3(direction.x, 0, direction.z);
        RaycastHit hit;
        if (Physics.Raycast(startPt, modifiedDir, out hit, closeRange))
        {
            //Debug.Log("Ray hit: " + hit.collider.name);
        }
        Debug.DrawRay(startPt, modifiedDir*closeRange, Color.red);

        /*        if (Input.GetKey(KeyCode.A))
                {
                    crossProduct = Vector3.Cross(Vector3.forward, (target.transform.position - transform.position));
                    Debug.Log(crossProduct);
                    float angle = Vector3.Angle(Vector3.forward, (target.transform.position - transform.position));
                    transform.Rotate(crossProduct, angle * Time.deltaTime, Space.World);
                }*/

        inRange = ((target.transform.position - transform.position).magnitude <= range && !reload) ? true : false;
        isAiming = (inRange && crossProduct.magnitude > 0.5f &&!isCharging) ? true : false;
        canShoot = (inRange && !isAiming && !isCharging) ? true : false;

        if (inRange && isAiming)
        {
            crossProduct = Vector3.Cross(transform.up, (target.transform.position - transform.position));
            float angle = Vector3.Angle(transform.up, (target.transform.position - transform.position));
            transform.Rotate(crossProduct, angle * Time.deltaTime, Space.World);

        }
        if (Vector3.Dot(Vector3.down, -transform.up) < -Mathf.Cos(35 * (Mathf.PI / 180))) { canShoot = false; isAiming = true; }
        Debug.Log(Vector3.Dot(Vector3.down, -transform.up));
    }
    private void FixedUpdate()
    {
        if(inRange && canShoot)
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.isKinematic = false;
            rb.velocity = transform.up * force;
            canShoot = false;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Hit!");
            StartCoroutine(Reload());
        }

    }
    IEnumerator Reload()
    {
        reload = true;
        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints.None;
        transform.GetChild(0).gameObject.SetActive(false);
        Debug.Log(initPos);
        yield return new WaitForSeconds(2f);
        transform.localScale = Vector3.zero;
        transform.localPosition = new Vector3(0,8.81f,0);
        transform.rotation = initRot;
        transform.GetChild(0).gameObject.SetActive(true);
        reload = false;
        isCharging = true;
        chargeTime = 0;
    }
    void OnDrawGizmos()
    {
        Vector3 direction = (target.transform.position - initPos).normalized;
        Vector3 startPt = new Vector3(initPos.x, (initPos.y - (initPos.y - target.transform.position.y)), initPos.z);
        Vector3 modifiedDir = new Vector3(direction.x, 0, direction.z);
        Gizmos.DrawRay(startPt, modifiedDir * closeRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position,direction*10);
    }
}

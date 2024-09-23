using UnityEngine;
using UnityEngine.UI;


public class Trajectory : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject target;
    [SerializeField] private Slider vertAngleSlider;

    [SerializeField] private float horizontalVelX, horizontalVelZ, verticalVel, TotalTime, angle;
    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            SetDirection();
            //Upward velocity = sqrt of 2*g*h
            verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * (getDistancePerAxis('y')));

            //From distance = change in velocity/time; change in velocity = (final-initial)/2
            //initial velocity = 0;
            GetTime();
            horizontalVelX = ((2 * (getDistancePerAxis('x') * TotalTime)));

            horizontalVelZ = ((2 * (getDistancePerAxis('z') * TotalTime)));

            rb.velocity = new Vector3(horizontalVelX, verticalVel, horizontalVelZ);
        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetDirection();
            float hypotenuse = (target.transform.position - transform.position).magnitude;

            verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * ((Mathf.Sin(angle) * hypotenuse)));

            Debug.Log((Mathf.Sin(angle)) + "\n" + hypotenuse + "\n" + direction);

            GetTime();
            horizontalVelX = ((2 * (Mathf.Cos(angle) * hypotenuse) * direction.x * TotalTime));

            horizontalVelZ = ((2 * (Mathf.Cos(angle) * hypotenuse) * direction.z * TotalTime));
            Debug.Log((Mathf.Cos(angle) * hypotenuse) * direction.x + "\n" + (Mathf.Cos(angle) * hypotenuse) * direction.z);


            rb.velocity = new Vector3(horizontalVelX, verticalVel, horizontalVelZ);
        }
    }
    void SetDirection()
    {
        direction = (target.transform.position - transform.position).normalized;
    }
    float getDistancePerAxis(char axis)
    {
        switch (axis)
        {
            case 'x':
                return target.transform.position.x - transform.position.x;

            case 'y':
                return target.transform.position.y - transform.position.y;

            case 'z':
                return target.transform.position.z - transform.position.z;

            default:
                return 0;

        }
    }
    void GetTime()
    {
        //final velocity = initial velocity + acceration*time
        float ascendTime = verticalVel / Physics.gravity.magnitude;
        //height = difference of higher to lower;   free fall => d = 1/2 gt^2
        float descendTime = Mathf.Sqrt(getDistancePerAxis('y') / (0.5f * Physics2D.gravity.magnitude));
        TotalTime = ascendTime + descendTime;
    }
    public void ChangeAngle()
    {
        angle = vertAngleSlider.value * (Mathf.PI / 180);
    }
}

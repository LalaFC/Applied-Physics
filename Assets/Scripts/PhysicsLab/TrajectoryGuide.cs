using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TrajectoryGuide : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private GameObject target;
    [SerializeField] private Slider vertAngleSlider;

    [SerializeField] private float horizontalVelX, horizontalVelZ, verticalVel, TotalTime, angle;
    private Vector3 direction;
    private float initAngle;
    bool canLaunch=true;
    Vector3 initPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initAngle = Mathf.Atan(Mathf.Abs(getDistancePerAxis('y') / getDistancePerAxis('x'))) * (180/Mathf.PI);
        vertAngleSlider.value = initAngle;
        initPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(canLaunch)
        StartCoroutine(Launch());
        if (Input.GetKey(KeyCode.Backspace))
        {
            rb.velocity = Vector3.zero;
            transform.position = initPos;
            canLaunch = true;
        }

    }
    IEnumerator Launch()
    {
        canLaunch = false;
        if (Input.GetKeyDown(KeyCode.F))
        {
            SetDirection(target.transform.position, transform.position);
            //Upward velocity = sqrt of 2*g*h
            verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * (getDistancePerAxis('y')));

            //From distance = change in velocity/time; change in velocity = (final-initial)/2
            //initial velocity = 0;
            GetTime();
            horizontalVelX = ((2 * (getDistancePerAxis('x') * TotalTime)));
            horizontalVelZ = ((2 * (getDistancePerAxis('z') * TotalTime)));

            rb.velocity = new Vector3(horizontalVelX, verticalVel, horizontalVelZ);
        }

        else if (Input.GetKeyDown(KeyCode.G))
        {
            Debug.Log(getDistancePerAxis('x') + " + " + getDistancePerAxis('y'));
            //Adds slider value to default angle;
            float NewAngle = ((Mathf.Atan(Mathf.Abs(getDistancePerAxis('y') / getDistancePerAxis('x'))) * (180 / Mathf.PI)) + vertAngleSlider.value) * (Mathf.PI / 180);
            float MaxHeight = Mathf.Tan(NewAngle) * Mathf.Abs(getDistancePerAxis('x'));
            float fallHeight = (MaxHeight - Mathf.Abs(getDistancePerAxis('y')));
            //freefall =  d = 1/2 gt^2
            if (fallHeight < 0)
            {
                TotalTime = verticalVel / Physics.gravity.magnitude;
                verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * (getDistancePerAxis('y')));
            }
            else
            {
                TotalTime = (Mathf.Sqrt((2 * fallHeight) / Physics.gravity.magnitude) + Mathf.Sqrt((2 * MaxHeight) / Physics.gravity.magnitude));
                verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * MaxHeight);
            }

            horizontalVelX = (getDistancePerAxis('x') / TotalTime);
            horizontalVelZ = (getDistancePerAxis('z') / TotalTime);

            rb.velocity = new Vector3(horizontalVelX, verticalVel, horizontalVelZ);
            Debug.Log("NewAngle = " + NewAngle * (180 / Mathf.PI) + "\nMax Height = " + MaxHeight + "\nFall Height = " + fallHeight + "\nTime of Flight = " + TotalTime
    + "\nvelocity = " + new Vector3(horizontalVelX, verticalVel, horizontalVelZ));

        }

        else if (Input.GetKeyDown(KeyCode.C))
        {
            SetDirection(target.transform.position, transform.position);
            float hypotenuse = (target.transform.position - transform.position).magnitude;
            //float force = TotalTime * Physics.gravity.magnitude / Mathf.Sin(angle);
            verticalVel = Mathf.Sqrt(2 * Physics.gravity.magnitude * (Mathf.Sin(angle) * hypotenuse));

            Debug.Log((Mathf.Sin(angle)) + "\n" + hypotenuse + "\n" + direction + "\n" + angle);
            Debug.Log(Mathf.Sqrt(2 * Physics.gravity.magnitude * (Mathf.Sin(angle) * hypotenuse)));

            GetTime();
            horizontalVelX = ((2 * (Mathf.Cos(angle) * hypotenuse) * direction.x * TotalTime));

            horizontalVelZ = ((2 * (Mathf.Cos(angle) * hypotenuse) * direction.z * TotalTime));
            Debug.Log((Mathf.Cos(angle) * hypotenuse) * direction.x + "\n" + (Mathf.Cos(angle) * hypotenuse) * direction.z);


            rb.velocity = new Vector3(horizontalVelX, verticalVel, horizontalVelZ);
        }
        yield return new WaitForSeconds(0.2f);
        canLaunch = true;
    }
    void SetDirection(Vector3 targetPos, Vector3 startPos)
    {
        direction = (targetPos - startPos).normalized;
    }
    float getDistancePerAxis(char axis)
    {
        switch (axis)
        {
            case'x':
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

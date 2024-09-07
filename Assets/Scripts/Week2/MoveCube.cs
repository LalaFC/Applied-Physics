using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCube : MonoBehaviour
{
    [SerializeField] private GameObject cube;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private float endTime;
    [SerializeField] private DistanceCalculator distanceCalculator;

    [SerializeField] private float AccelerationMultiplier;
    [SerializeField] private float AccelerationDuration;
    private Vector3 newVel;

    private Rigidbody cubeRb;
    private Vector3 initialPos;
    private float time, accTime;
    private bool move=false, accelerate = false;

    // Start is called before the first frame update
    void Start()
    {
        cubeRb = cube.GetComponent<Rigidbody>();
        initialPos = cube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            distance = distanceCalculator.GetDistance();
        }
        if (Input.GetKey(KeyCode.Space))
        {
            if(distance != 0 && endTime != 0)
            {
                Move(speed);
                move = true;
            }
            else if (speed != 0 && endTime != 0)
            {
                Move(speed);
                distance = speed * endTime;
                move = true;
            }
            else if (speed != 0 && endTime != 0)
            {
                Move(speed);
                endTime = distance / speed;
                move = true;
            }
        }
        if (Input.GetKey(KeyCode.Backspace))
        {
            cubeRb.velocity = Vector3.zero;
            cube.transform.position = initialPos;
        }
        if (Input.GetKey(KeyCode.A))
        {
            accelerate = true;
            newVel = cubeRb.velocity * AccelerationMultiplier;
        }
    }
    private void FixedUpdate()
    {
        if (move == true && time < endTime)
        {
            time += Time.fixedDeltaTime;
            speed = cubeRb.velocity.magnitude;
        }

        else
        {
            move = false;
            cubeRb.velocity = Vector3.zero;
            time = 0;
        }

        if (accelerate == true && accTime <= AccelerationDuration)
        {
            accTime += Time.fixedDeltaTime;
            speed = cubeRb.velocity.magnitude;
            Accelerate();
        }
        else if(accelerate == true && accTime > AccelerationDuration)
        {
            accelerate = false;
            time = 0;
            endTime = distanceCalculator.GetDistance(cube)/speed;
            move = true;
        }

    }

    private void Move(float speed)
    {
        if (speed != 0)
        {
            cubeRb.velocity = new Vector3(speed, 0, 0);
        }
        else
            cubeRb.velocity = new Vector3(distance / endTime, 0, 0);
    }
    private void Accelerate()
    {
        move = false;
        Vector3 initialVel = cubeRb.velocity;
        cubeRb.velocity = Vector3.Lerp(initialVel, newVel, accTime / AccelerationDuration);
    }

}

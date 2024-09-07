using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceCalculator : MonoBehaviour
{
    [SerializeField] private GameObject StartPt;
    [SerializeField] private GameObject EndPt;
    private float distance;
    private Vector3 start, end;

    // Start is called before the first frame update
    void Start()
    {
        start = StartPt.transform.position;
        end = EndPt.transform.position;
        distance = Vector3.Distance(start, end);
    }

    // Update is called once per frame
    void Update()
    {
        start = StartPt.transform.position;
        end = EndPt.transform.position;
        if (StartPt.transform.position != start || EndPt.transform.position != end)
        {
            start = StartPt.transform.position;
            end = EndPt.transform.position;

            distance = Vector3.Distance(start, end);
        }
    }
    public float GetDistance()
    {
        return distance;
    }
    public float GetDistance(GameObject cube)
    {
        return Vector3.Distance(cube.transform.position, end);
    }
}

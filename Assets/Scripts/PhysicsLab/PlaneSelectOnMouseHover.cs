using UnityEngine;

public class PlaneSelectOnMouseHover : MonoBehaviour
{
    private Transform previous;
    // Update is called once per frame
    void Update()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(camRay,out hit, Mathf.Infinity, LayerMask.GetMask("Floor")))
        {
            Material hitMat = hit.transform.GetComponent<Renderer>().material;
            if (hit.transform != previous && hitMat != null)
            {
                if (previous != null) previous.GetComponent<Renderer>().material.DisableKeyword("_EMISSION");
                hitMat.EnableKeyword("_EMISSION");
                hitMat.SetColor("_EmissionColor", Color.gray * 0.5f);
                previous = hit.transform;
            }
            if (Input.GetMouseButtonDown(0) && hit.transform != null)
            {
                hit.transform.GetComponent<TowerSpawner>().SpawnTower();
            }
        }
    }
}

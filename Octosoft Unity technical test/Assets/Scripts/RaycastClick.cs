using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Camera))]
public class RaycastClick : MonoBehaviour
{
    public float distance = 1000f;
    public LayerMask layerTarget;

    public delegate void OnHitDelegate();
    public OnHitDelegate onHitDelegate;

    private Camera cam;
    private Ray ray;
    private RaycastHit hit;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = new Ray(
                cam.ScreenToWorldPoint(Input.mousePosition),
                cam.transform.forward);

            if (Physics.Raycast(ray, out hit, distance, layerTarget))
            {
                onHitDelegate.Invoke();
            }
        }
    }
}

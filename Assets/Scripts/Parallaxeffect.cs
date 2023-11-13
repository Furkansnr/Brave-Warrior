using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxeffect : MonoBehaviour
{
    public Camera cam;
    public Transform fallowtarget;
    private Vector2 startpos;
    private float startingZ;
    private Vector2 camMoveSinceStart => (Vector2) cam.transform.position - startpos;

    private float zdistanceFromTarget => transform.position.z - fallowtarget.transform.position.z;

    private float clippingPlane =>
        (cam.transform.position.z + (zdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    private float parallaxFactor => Mathf.Abs(zdistanceFromTarget) / clippingPlane;
    void Start()
    {
        startpos = transform.position;
        startingZ = transform.position.z;
    }

   
    void Update()
    {
        Vector2 newPosition = startpos + camMoveSinceStart * parallaxFactor;

        transform.position = new Vector3(newPosition.x, newPosition.y, startingZ);
    }
}

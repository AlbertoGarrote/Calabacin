using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public Transform[] views;
    public float transitionSpeed = 1;
    Transform currentView;

    void Start()
    {
        currentView = transform;
    }

    public void CambiarCamara(int i)
    {
        currentView = views[i];
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, currentView.position, Time.deltaTime * transitionSpeed);
    }
}

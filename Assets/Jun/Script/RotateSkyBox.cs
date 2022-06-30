using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSkyBox : MonoBehaviour
{
    public float rotateSpeed = 30f;

    // Update is called once per frame
    void Update()
    {
        RenderSettings.skybox.SetFloat("_Roatation", Time.deltaTime * rotateSpeed);
    }
}

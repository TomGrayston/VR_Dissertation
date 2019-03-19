using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class UserCameraMatch : MonoBehaviour
{
    public GameObject Body;
    public GameObject VR_cam;

    // Update is called once per frame
    void Update()
    {
        Quaternion replacementQuaternion = Quaternion.Euler(Vector3.zero);

        Body.transform.position = VR_cam.transform.localPosition;;  

        Body.transform.rotation = replacementQuaternion;
        
    }

    void LateUpdat()
    {
        
        Valve.VR.OpenVR.System.ResetSeatedZeroPose();
    }
}

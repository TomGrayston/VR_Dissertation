using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserCameraMatch : MonoBehaviour
{
    public GameObject Body;
    public GameObject VR_cam;

    // Update is called once per frame
    void Update()
    {
        Quaternion replacementQuaternion = Quaternion.Euler(Vector3.zero);

        Body.transform.position = VR_cam.transform.localPosition;
        //Body.transform.rotation = VR_cam.transform.rotation;
        VR_cam.transform.position = new Vector3(0, 0, 0);
        VR_cam.transform.rotation = Body.transform.rotation;
        Body.transform.rotation = replacementQuaternion;
        
    }
}

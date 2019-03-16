using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AmplificationCoSign : MonoBehaviour
{
    public Vector3 StartPos = Vector3.forward;      //Get the Starting Point
    public Quaternion LastFramesPos = Quaternion.identity;       //get the last Quaternion
    public Camera HMD;
    private float amplificationFactor;

    // Update is called once per frame
    void Update()
    {
        HMD.transform.position = Vector3.zero;
        //get both the HMD current Pos and the VR_cam current Pos
        Quaternion CurrentPos_HMD  = HMD.transform.rotation;
        Quaternion CurrentPos_VR_cam = this.transform.rotation;

        //convert to Vector3 to get the XYZ's
        Vector3 LastFrameXYZ = LastFramesPos.eulerAngles;
        Vector3 CurrentXYZ_HMD = CurrentPos_HMD.eulerAngles;
        Vector3 CurrentXYZ_VR_cam = CurrentPos_VR_cam.eulerAngles;

        //get the diffrence between the neutral forward and the HMD current pos
        Vector3 currDirr = CurrentPos_HMD * Vector3.forward;
        float Diff = GetDiff(StartPos, currDirr);

        //get the Amplification Factor by using 2 - cos(Diff)
        float AF = GetAF(Diff);

        Vector3 difInDegrees = GetXYZ_Diff(LastFrameXYZ, CurrentXYZ_HMD);

        //Use the Amplification on the VR_cam
        Vector3 AmpedXYZ_VR_cam = amplify(AF, difInDegrees);

        //Make the new Amplified Quaternion
        Quaternion AmpRotation = Quaternion.Euler(AmpedXYZ_VR_cam);

        //Push the amplification to the VR_cam
        transform.rotation = transform.rotation * AmpRotation;

        //UPDATE THE LAST FRAME POS
        LastFramesPos = HMD.transform.rotation;

        /* Function used to get the XYZ diffrence between the 
        nuetral forward Pos and the current real world HMD Pos*/
        float GetDiff(Vector3 NeutralForward, Vector3 HMD_Pos)
        {

            float diff = Vector3.Angle(NeutralForward, HMD_Pos);

            return diff;
        }

        /*This function is used to get the Amplification Factor
        using the XYZ diffrence and creates an Amplifaction Factor
        using 2 - cos(XYZ Diffrence) */
        float GetAF(float d)
        {
            float af;

            af = (float)Math.Cos(Mathf.Deg2Rad * d);
            af = 2 - af;

            print(af);

            return af;
        }

        /* Amplifies the in game VR camera using the Amplification Factor*/
        Vector3 amplify(float af, Vector3 VR_cam)
        {
            Vector3 output;

            output.x = VR_cam.x * AF;
            output.y = VR_cam.y * AF;
            output.z = VR_cam.z * AF;

            return output;
        }

        Vector3 GetXYZ_Diff(Vector3 lastXYZ, Vector3 currentXYZ)
        {
            Vector3 output;

            output.x = (float) Math.Atan2(Math.Sin(currentXYZ.x-lastXYZ.x), Math.Cos(currentXYZ.x-lastXYZ.x));
            output.y = (float) Math.Atan2(Math.Sin(currentXYZ.y-lastXYZ.y), Math.Cos(currentXYZ.y-lastXYZ.y));
            output.z = (float) Math.Atan2(Math.Sin(currentXYZ.z-lastXYZ.z), Math.Cos(currentXYZ.z-lastXYZ.z));
            return output;
        }
    }

    void LateUpdate()
    {
        HMD.transform.position = Vector3.zero;
    }
}

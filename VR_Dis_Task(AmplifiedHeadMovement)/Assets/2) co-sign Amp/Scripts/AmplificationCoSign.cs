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

    int i = 1;


    // Update is called once per frame
    void Update()
    {
        // print("FRAME_" + i);
        HMD.transform.position = Vector3.zero;
        //get both the HMD current Pos and the VR_cam current Pos
        Quaternion CurrentPos_HMD  = HMD.transform.rotation;
        Quaternion CurrentPos_VR_cam = this.transform.rotation;

        // print("GETTING_Quarternions");
        // print("Last Frame Pos = " + LastFramesPos);
        // print("HMD Pos = " + CurrentPos_HMD);
        // print("VR_cam Pos = " + CurrentPos_VR_cam);

        //convert to Vector3 to get the XYZ's
        Vector3 LastFrameXYZ = LastFramesPos.eulerAngles;
        Vector3 CurrentXYZ_HMD = CurrentPos_HMD.eulerAngles;
        Vector3 CurrentXYZ_VR_cam = CurrentPos_VR_cam.eulerAngles;

        // print("GETTING_XYZ's");
        // print("Start Pos = " + StartPos);
        // print("Last Frames XYZ = " + LastFrameXYZ);
        // print("HMD's XYZ = " + CurrentXYZ_HMD);
        // print("VR_cam XYZ = " + CurrentXYZ_VR_cam);

        //get the diffrence between the neutral forward and the HMD current pos
        Vector3 currDirr = CurrentPos_HMD * Vector3.forward;

       // print(currDirr);

        float Diff = GetDiff(StartPos, currDirr);
       // print("Diffrence = " + Diff);

        //get the Amplification Factor by using 2 - cos(Diff)
        float AF = GetAF(Diff);
        //print("Amplification Factor = " + AF);

        Vector3 difInDegrees = GetXYZ_Diff(LastFrameXYZ, CurrentXYZ_HMD);
        //print("x: " + difInDegrees.x + " y: " + difInDegrees.y + " z: " + difInDegrees.z);

        //Use the Amplification on the VR_cam
        Vector3 AmpedXYZ_VR_cam = amplify(AF, difInDegrees);


        //print("AF: " + AF +  " x: " + AmpedXYZ_VR_cam.x + " y: " + AmpedXYZ_VR_cam.y + " z: " + AmpedXYZ_VR_cam.z);
        //Make the new Amplified Quaternion
        Quaternion AmpRotation = Quaternion.Euler(AmpedXYZ_VR_cam);

        //Push the amplification to the VR_cam
        //print(AmpRotation);
        
        transform.rotation = transform.rotation * AmpRotation;

        //UPDATE THE LAST FRAME POS
        LastFramesPos = HMD.transform.rotation;

        i++;

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
            //output.x = currentXYZ.x - lastXYZ.x;
            //output.y = currentXYZ.y - lastXYZ.y;
           // output.z = currentXYZ.z - lastXYZ.z;



            if(output.x > 100f || output.y > 100f ||output.z > 100f)
            {
                print("Dif in Degrees: " + output);
            }
            

            return output;
        }
        /* 
        //get users current Position
        Quaternion CurrentPosRealWorld  = HMD.transform.localRotation;
        Quaternion CurrentPosVirtualWorld = HMD.transform.rotation;

        //Get the XYZ of both Current Position and the Starting Position
        //Vector3 StartXYZ = GetXYZ(StartPos);
        Vector3 LastFrameXYZ = GetXYZ(lastRotation);
        Vector3 CurrentXYZRealWorld = GetXYZ(CurrentPosRealWorld);
        Vector3 CurrentXYZVirtualWorld = GetXYZ(CurrentPosVirtualWorld);

        //get the diffrence in degrees
        Vector3 difInDegrees = GetXYZ_Diff(CurrentXYZRealWorld, StartPos);

        //Calulate the Diffrence
        float Diff = GetDiff(CurrentXYZRealWorld, LastFrameXYZ);

        //Get AF
        amplificationFactor = GetAF(Diff);
        print(amplificationFactor);

        //Maths it
        Vector3 AmpVals = amplify(amplificationFactor, difInDegrees);

        //Convert to Quaternion
        Quaternion AmpedPos = Quaternion.Euler(AmpVals);

        //THIS WAY DOESNT WORK
        transform.rotation = transform.rotation * AmpedPos;

        //Make the current Rotation the Last rotation
        lastRotation = HMD.transform.localRotation;

        Vector3 GetXYZ(Quaternion lRot)
        {
            Vector3 xyz = lRot.eulerAngles;
            return xyz;
        }

        float GetDiff(Vector3 Start, Vector3 CurrentPoint)
        {

            float diff = Vector3.Angle(Start, CurrentPoint);

            return diff;
        }

        float GetAF(float diff)
        {
            float AF;

            AF = (float)Math.Cos(Mathf.Deg2Rad * diff);
            AF = AF - 2;

            return AF;
        }
        */


        /* 
        
        Vector3 amplify(float AF, Vector3 diffrences)
        {   
            Vector3 output = diffrences;

            output.x = output.x * AF;
            if(output.x > 360)
            {
                output.x = output.x - 360;
            }
            output.y = output.y * AF;
            if(output.y > 360)
            {
                output.y = output.y - 360;
            }
            output.z = output.z * AF;
            if(output.z > 360)
            {
                output.z = output.z - 360;
            }
            print("Amplified Dif in Degrees: " + output);

            return output;
        }

        */
    }

    void LateUpdate()
    {
        HMD.transform.position = Vector3.zero;
    }
}

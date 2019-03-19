using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;
using System;


public class EyeTrackingExample : MonoBehaviour
{
    // Start is called before the first frame update

    float Zone1 = 10;
    float Zone2 = 20;
    float Zone3 = 30;
    public Quaternion lastRotation = Quaternion.identity;       //get the last Quaternion
    public Camera HMD;



    void Start()
    {
        Tracking_Data_Manager.OnNewGazeData += DetectGaze;
    }

    public void DetectGaze(IVRGazeData newGazeData)
    {
        //Checks if there are any eyes to record
        if(newGazeData.CombinedGazeRayWorldValid == true)
        {
            Vector3 GazeDirection = newGazeData.CombinedGazeRayWorld.direction.normalized;
            Vector3 HeadDirection =  newGazeData.Pose.Rotation * Vector3.forward;
            HeadDirection = HeadDirection.normalized;

            //get the current Quaternion
            Quaternion currentRotation = HMD.transform.localRotation;

            //Get the XYZ of the last frame
            Vector3 LastFrameXYZ = GetXYZ(lastRotation);
            Vector3 ThisFrameXYZ = GetXYZ(currentRotation);

            //get the diffrence between where the eyes are looking and center of the screen
            float AngleDiff = Vector3.Angle(HeadDirection, GazeDirection);

            //Create the Amplifcation factor
            float AF = createAF(AngleDiff);
            print(AF);

            //get the diffrence in degrees
            Vector3 difInDegrees = GetXYZ_Diff(LastFrameXYZ, ThisFrameXYZ);

            //Get the Amplified xyz values
            Vector3 AmpVals = amplify(AF, difInDegrees);

            //Make the new Amplified Quaternion
            Quaternion AmpRotation = Quaternion.Euler(AmpVals);

            //Update the cameras rotataion with the new quarterion
            transform.rotation = transform.rotation * AmpRotation;

            //Make the current Rotation the Last rotation
            lastRotation = HMD.transform.localRotation;

        }
        else{
            print("NO EYES DETECTED"); 
        }

        float createAF(float AngleDiff)
        {
            if(AngleDiff < Zone1)
            {
                return 1f;
            }
            else if(AngleDiff < Zone2 && AngleDiff > Zone1)
            {
                return 1.5f;
            }
            else if(AngleDiff < Zone3 && (AngleDiff > Zone2 && AngleDiff > Zone1))
            {
                return 2f;
            }
            else{
                return 2f;
            }
        }

        Vector3 amplify(float AF, Vector3 diffrences)
        {   
            Vector3 output;

            output.x = diffrences.x * AF;
            output.y = diffrences.y * AF;
            output.z = diffrences.z * AF;
            return output;
        }

        Vector3 GetXYZ_Diff(Vector3 lastXYZ, Vector3 currentXYZ)
        {
            Vector3 output;

            output.x = (float) Math.Atan2(Math.Sin(currentXYZ.x-lastXYZ.x), Math.Cos(currentXYZ.x-lastXYZ.x));
            if(output.x >= 360f)
            {
                output.x = output.x - 360;
            }
            output.y = (float) Math.Atan2(Math.Sin(currentXYZ.y-lastXYZ.y), Math.Cos(currentXYZ.y-lastXYZ.y));
            if(output.y >= 360f)
            {
                output.y = output.y - 360;
            }
            output.z = (float) Math.Atan2(Math.Sin(currentXYZ.z-lastXYZ.z), Math.Cos(currentXYZ.z-lastXYZ.z));
            if(output.z >= 360f)
            {
                output.z = output.z - 360;
            }
            return output;
        }

        Vector3 GetXYZ(Quaternion lRot)
        {
            Vector3 xyz = lRot.eulerAngles;
            return xyz;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.Research.Unity;
using System;


public class EyeTrackingCOS_GAZE : MonoBehaviour
{
    // Start is called before the first frame update
    public Quaternion lastFrameQ = Quaternion.identity;       //get the last frame Quaternion
    public Camera HMD;
    private float amplificationFactor;



    void Start()
    {
        Tracking_Data_Manager.OnNewGazeData += DetectGaze;
    }

    public void DetectGaze(IVRGazeData newGazeData)
    {
        /*Get the values to be amplified later */

        //Users head location IRL
        Quaternion CurrentFrameQ_HMD  = HMD.transform.localRotation;

        //get the diffrence between last frame and the current frame
        Vector3 Diff = GetXYZ_Diff(lastFrameQ.eulerAngles, CurrentFrameQ_HMD.eulerAngles);

        //Checks if the camera can detect the users eyes
        if(newGazeData.CombinedGazeRayWorldValid == true)
        {
            /* Get the Amplification Factor */
            //get the center of the camera
            Vector3 HeadDirection =  newGazeData.Pose.Rotation * Vector3.forward;
            HeadDirection = HeadDirection.normalized;

            //get where the user is current looking
            Vector3 GazeDirection = newGazeData.CombinedGazeRayWorld.direction.normalized;

            //get the diffrence between where the eyes are looking and center of the screen
            float AngleDiff = Vector3.Angle(HeadDirection, GazeDirection);
            print(AngleDiff);

            //Create the Amplifcation factor
            float AF = createAF(AngleDiff);

            //Amplify
            Vector3 AmpedHead = amplify(AF, Diff);

            //convert to a Quarternion
            Quaternion AmpedQ = Quaternion.Euler(AmpedHead);

            //Push the amplification to the VR_cam
            transform.rotation = transform.rotation * AmpedQ;

            //Make the current Rotation the Last rotation
            lastFrameQ = HMD.transform.localRotation;

        }
        else{
            print("NO EYES DETECTED"); 
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

        float createAF(float AD)
        {
            float af;

            af = (float)Math.Cos(Mathf.Deg2Rad * AD);
            af = 2 - af;

            //print(af);

            return af;
        }
    }
}

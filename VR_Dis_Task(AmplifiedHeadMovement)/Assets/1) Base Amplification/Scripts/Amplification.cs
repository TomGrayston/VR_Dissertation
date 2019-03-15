using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplification : MonoBehaviour
{
    [SerializeField] float amplificationFactor;
    public Quaternion lastRotation = Quaternion.identity;       //get the last Quaternion
    public Camera HMD;

    // Update is called once per frame
    void Update()
    {
        print("NEW_FRAME");
        print("Last: " + lastRotation);
        //get the current Quaternion
        Quaternion currentRotation = HMD.transform.localRotation;
        print("Current: " + currentRotation);

        //Get the XYZ of the last frame
        Vector3 LastFrameXYZ = GetXYZ(lastRotation);
        Vector3 ThisFrameXYZ = GetXYZ(currentRotation);

        //get the diffrence in degrees
        Vector3 difInDegrees = GetXYZ_Diff(LastFrameXYZ, ThisFrameXYZ);

        //Get the Amplified xyz values
        Vector3 AmpVals = amplify(amplificationFactor, difInDegrees);

        //Make the new Amplified Quaternion
        Quaternion AmpRotation = Quaternion.Euler(AmpVals);

        //THIS WAY DOESNT WORK
        transform.rotation = transform.rotation * AmpRotation;
        
        //Make the current Rotation the Last rotation
        lastRotation = HMD.transform.localRotation;

        Vector3 GetXYZ(Quaternion lRot)
        {
            Vector3 xyz = lRot.eulerAngles;
            return xyz;
        }

        Vector3 GetXYZ_Diff(Vector3 lastXYZ, Vector3 currentXYZ)
        {
            Vector3 output;

            output.x = currentXYZ.x - lastXYZ.x;
            output.y = currentXYZ.y - lastXYZ.y;
            output.z = currentXYZ.z - lastXYZ.z;
            print("Dif in Degrees: " + output);

            return output;
        }

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
    }
}

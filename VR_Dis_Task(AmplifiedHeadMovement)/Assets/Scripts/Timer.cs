using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class Timer : MonoBehaviour
{
    public string File_Name = "";
    float t = 0;

    // Update is called once per frame
    void Update()
    {
        
        t = t + Time.deltaTime;
        WriteFile(t);
        
    }

    void WriteFile(float T)
    {
        string path = "Assets/Outputs/"+File_Name+".txt";

        StreamWriter w = new StreamWriter(path, true);
        w.WriteLine(T);
        w.Close();
    }
}

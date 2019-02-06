using System;
using UnityEngine;

public class InteractiveItem : MonoBehaviour
{
    public event Action gazeOver;

    public event Action gazeNotOver;

    bool user_isOver;

    public bool IsOver
    {
        get{ return user_isOver; }
    }

    public void Over()
    {
        user_isOver = true;

        if(gazeOver !=  null)
        {
            gazeOver();
        }
    }

    public void notOver()
    {
        user_isOver = false;

        if(gazeNotOver != null)
        {
            gazeNotOver();
        }
    }
}
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using VRStandardAssets.Utils;
using System.Collections.Generic;
using System.Collections;

namespace VRStandardAssets.Examples
{
    // This script is a simple example of how an interactive item can
    // be used to change things on gameobjects by handling events.
    public class ExampleInteractiveItem_Start : MonoBehaviour
    {
        [SerializeField] private Material m_NormalMaterial;                
        [SerializeField] private Material m_OverMaterial;                           
        [SerializeField] private VRInteractiveItem m_InteractiveItem;
        [SerializeField] private Renderer m_Renderer;
        [SerializeField] public GameObject Button;
        [SerializeField] public GameObject targets;

        [SerializeField]public Image timerImg;
        public float totalTime = 2;
        bool status;
        public float timer;


        private void Awake ()
        {
            m_Renderer.material = m_NormalMaterial;
        }


        private void OnEnable()
        {
            m_InteractiveItem.OnOver += HandleOver;
            m_InteractiveItem.OnOut += HandleOut;
        }


        private void OnDisable()
        {
            m_InteractiveItem.OnOver -= HandleOver;
            m_InteractiveItem.OnOut -= HandleOut;
        }

        //Handle the Over event
        private void HandleOver()
        {
            Debug.Log("Show over state");
            m_Renderer.material = m_OverMaterial;


            /* status = true;

            if(status == true)
            {
                timer += Time.deltaTime;
                timerImg.fillAmount = timer/totalTime;
            }
            if(timer > totalTime)
            {
                targets.SetActive(true);
                Destroy(Button);
            }*/
        }


        //Handle the Out event
        private void HandleOut()
        {
            Debug.Log("Show out state");
            m_Renderer.material = m_NormalMaterial;

            /*

            status = false;
            timer = 0;
            timerImg.fillAmount = 0;

            targets.SetActive(false);
            */
        }
    }

}
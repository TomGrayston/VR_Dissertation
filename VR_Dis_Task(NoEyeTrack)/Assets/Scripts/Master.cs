using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VRStandardAssets.Examples
{
    public class Master : MonoBehaviour
    {
        [SerializeField] public GameObject Test;
        [SerializeField] public GameObject StartButton;
        [SerializeField] public GameObject Targets;

        int children;

        bool status;

        public float totalTime = 2;

        public float timer;

        // Start is called before the first frame update
        void Start()
        {
            Test.transform.Fi

        }

        // Update is called once per frame
        void Update()
        {

            if(StartButton.activeSelf)
            {
                Debug.Log("IT WORKED??");
                Targets.SetActive(true);
            }
            else
            {
                StartButton.SetActive(false);
            }
            
        }
    }
}

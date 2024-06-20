using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class ResetCameraPosition : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            InputTracking.Recenter();
        }

    }
}

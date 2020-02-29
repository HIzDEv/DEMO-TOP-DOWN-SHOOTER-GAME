using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCam : MonoBehaviour
{
    private Transform Cam;

    public bool Viseur;
    public Transform ViseurPos;
    
	// Update is called once per frame
	void Update ()
    {
        if (Viseur)
        {
            transform.LookAt(ViseurPos);
            return;
        }



        if (Cam == null)
        {
            try
            {
                Cam = Camera.main.transform;
            }
            catch (System.Exception ex)
            {
                Debug.Log("Main Camera Disabled ?!");
            }
        }
        else
        {
            
            transform.LookAt(Cam);
        }
	}
}

using UnityEngine;
using System.Collections;

public class EnvironmentMovement : MonoBehaviour 
{
    public Transform CurrentEnvironment;
    //public Transform NextEnvironment;

    public Global globals;

    public void Update()
    {
        if(CurrentEnvironment != null)
            CurrentEnvironment.transform.Translate(new Vector3(-globals.ScrollSpeed * Time.deltaTime, 0, 0));
        //if (CurrentEnvironment != null)
        //    NextEnvironment.transform.Translate(new Vector3(-globals.ScrollSpeed * Time.deltaTime, 0, 0));
    }
}

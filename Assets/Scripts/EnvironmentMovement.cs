using UnityEngine;
using System.Collections;

public class EnvironmentMovement : MonoBehaviour 
{
    public Transform CurrentEnvironment;
    public Transform NextEnvironment;

    public float ScrollSpeed;

    public void Update()
    {
        if(CurrentEnvironment != null)
            CurrentEnvironment.transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0, 0));
        if (CurrentEnvironment != null)
            NextEnvironment.transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0, 0));
    }
}

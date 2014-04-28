using UnityEngine;
using System.Collections;

public class EnvironmentMovement : MonoBehaviour 
{
    public Transform CurrentEnvironment;
    public Transform NextEnvironment;

    public float ScrollSpeed;

    public void Update()
    {
        CurrentEnvironment.transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0, 0));
        NextEnvironment.transform.Translate(new Vector3(-ScrollSpeed * Time.deltaTime, 0, 0));
    }
}

using UnityEngine;
using System;
using System.Collections.Generic;

public class EnvironmentMovement : MonoBehaviour 
{
    public Transform PreviousEnvironment;
    public Transform CurrentEnvironment;
    public Transform NextEnvironment;

    public List<Transform> EnvironmentPool;
    public float EnvironmentWidth;

    public Global globals;

    public void Start()
    {
        if (CurrentEnvironment == null)
            throw new Exception("WHAAAAAT!?!? Punk, need current environment");

        if (NextEnvironment == null)
            RollNextEnvironment();

        environmentOffset = 0;
    }

    public void Update()
    {
        environmentOffset += globals.ScrollSpeed * Time.deltaTime;

        if (CurrentEnvironment != null)
            CurrentEnvironment.transform.Translate(globals.LeftTranslate);
        if (NextEnvironment != null)
            NextEnvironment.transform.Translate(globals.LeftTranslate);
        if (PreviousEnvironment != null)
            PreviousEnvironment.transform.Translate(globals.LeftTranslate);

        if (environmentOffset > EnvironmentWidth)
        {
            environmentOffset -= EnvironmentWidth;
            PreviousEnvironment = CurrentEnvironment;
            CurrentEnvironment = NextEnvironment;
            RollNextEnvironment();
        }

        // HACK: Need to wait longer to clear the previous environment
        if (PreviousEnvironment != null && environmentOffset > EnvironmentWidth / 2.0f)
        {
            DestroyPreviousEnvironment();
        }
    }

    public void RollNextEnvironment()
    {
        NextEnvironment = (Transform)Instantiate(EnvironmentPool[UnityEngine.Random.Range(0, EnvironmentPool.Count - 1)]);
        NextEnvironment.transform.position = new Vector3(environmentOffset + EnvironmentWidth, 0, 0);
    }

    public void DestroyPreviousEnvironment()
    {
        Destroy(PreviousEnvironment.gameObject);
        PreviousEnvironment = null;
    }

    private float environmentOffset;
}

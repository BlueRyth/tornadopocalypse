using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour 
{
    public float ScrollSpeed;

	public Vector3 RightTranslate { get { return new Vector3(ScrollSpeed * Time.deltaTime, 0f, 0f); } }
	public Vector3 LeftTranslate  { get { return new Vector3(-ScrollSpeed * Time.deltaTime, 0f, 0f); } }
}

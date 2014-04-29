﻿using UnityEngine;
using System.Collections;

public class Global : MonoBehaviour 
{
    public float ScrollSpeed;

	public Vector3 RightTranslate { get { return new Vector3(ScrollSpeed * Time.deltaTime, 0f, 0f); } }
	public Vector3 LeftTranslate  { get { return new Vector3(-ScrollSpeed * Time.deltaTime, 0f, 0f); } }

	// Tag Names
	public string tag_KillPlane = "KillPlane";
	public string tag_Player = "Player";
	public string tag_Ground = "Ground";
	public string tag_Environment = "Environment";
	public string tag_PowerUp = "PowerUp";
}

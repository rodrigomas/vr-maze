﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SignPost : MonoBehaviour
{	
	public void ResetScene() 
	{
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
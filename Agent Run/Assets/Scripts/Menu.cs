using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

	public SceneController sceneController;
	public GameObject ui;
	public GameObject settingsUI;

	public void Play ()
	{
		ui.SetActive (false);
		sceneController.LoadLevel ("SelectLevel");
	}

	public void Settings ()
	{
		ui.SetActive (false);
		settingsUI.SetActive (true);
	}

	public void Quit ()
	{
		Application.Quit ();
	}
}

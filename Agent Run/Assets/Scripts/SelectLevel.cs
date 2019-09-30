using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectLevel : MonoBehaviour
{

	public SceneController sceneController;
	public GameObject levelsUI;
	public Button[] levelButtons;

	void Start ()
	{
		int reachedLevel = PlayerPrefs.GetInt ("ReachedLevel", 1);
		for (int i = 0; i < levelButtons.Length; i++) {
			if (i < reachedLevel)
				levelButtons [i].interactable = true;
		}
	}

	public void LoadLevel (string name)
	{
		levelsUI.SetActive (false);
		sceneController.LoadLevel (name);
	}

	public void ToMenu ()
	{
		levelsUI.SetActive (false);
		sceneController.LoadLevel ("MainMenu");
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public string nextLevelName;
	public int reachedLevel = 2;
	
	[Header ("Game objects")]
	public GameObject gameOverUI;
	public GameObject pauseUI;
	public GameObject levelWonUI;
	public Button nexLevelButton;
	public GameObject shootingUI;

	[Header ("State")]
	public bool gameEnd = false;
	public bool pause = false;

	[Header ("Script")]
	public SceneController sceneController;
	public Enemy[] enemies;

	[Header("Sound")]
	public AudioSource playerDeath;
	public AudioSource playerWon;

	void Start(){
		Cursor.visible = false;
	}

	void Update ()
	{
		if (gameEnd) {
			if (Cursor.visible == false)
				Cursor.visible = true;
			return;
		}

		if (Input.GetKeyDown (KeyCode.Escape) || Input.GetKeyDown ("p")) {
			if (pauseUI.activeSelf) {
				pauseUI.SetActive (false);
				Cursor.visible = false;
				Time.timeScale = 1f;
				pause = false;

				if(shootingUI!=null)
				shootingUI.SetActive (true);
			} else {
				Time.timeScale = 0f;
				pause = true;

				if(shootingUI!=null)
				shootingUI.SetActive (false);

				Cursor.visible = true;
				pauseUI.SetActive (true);
			}
		}
	}

	void StopEnemies(){
		foreach (Enemy e in enemies)
			e.SetGameOver (true);
	}

	public void GameOver ()
	{
		gameEnd = true;

		if(shootingUI!=null)
		shootingUI.SetActive (false);

		gameOverUI.SetActive (true);
	}

	public void LevelWon ()
	{
		playerWon.Play ();
		pause = true;
		gameEnd = true;

		if(shootingUI!=null)
		shootingUI.SetActive (false);
		
		Time.timeScale = 0f;

		if (reachedLevel > PlayerPrefs.GetInt ("ReachedLevel", 1))
			PlayerPrefs.SetInt ("ReachedLevel", reachedLevel);

		levelWonUI.SetActive (true);

		if (nextLevelName == "")
			nexLevelButton.interactable = false;
	}

	// button functions

	public void Menu ()
	{
		StopEnemies ();
		pauseUI.SetActive (false);
		gameOverUI.SetActive (false);

		sceneController.LoadLevel ("MainMenu");
	}

	public void Retry ()
	{
		StopEnemies ();
		pauseUI.SetActive (false);
		gameOverUI.SetActive (false);

		sceneController.LoadLevel (SceneManager.GetActiveScene ().name);
	}

	public void Continue ()
	{
		pause = false;
		pauseUI.SetActive (false);

		if(shootingUI!=null)
		shootingUI.SetActive (true);

		Time.timeScale = 1f;
	}

	// loads next level
	public void NextLevel ()
	{
		StopEnemies ();
		levelWonUI.SetActive (false);
		sceneController.LoadLevel (nextLevelName);
	}
}
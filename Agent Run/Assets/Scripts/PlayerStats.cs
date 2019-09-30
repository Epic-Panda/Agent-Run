using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class PlayerStats : MonoBehaviour
{
	[Header ("Atributes")]
	public int health = 100;
	public float speed = 10;
	public float rotataionSpeed = 30;
	public int shootDamage = 20;

	[Header ("Player setup")]
	public bool canShoot = false;
	public Transform finish;
	public GameManager gameManager;
	public GameObject cam;
	public Text hpText;
	public GameObject gun;
	public AudioSource gunShot;

	[Header ("Player mouse setup")]
	public float mouseSensitivity = 4f;
	public float maxAngle=25f;
	public float minAngle=-25f;


	void Start ()
	{
		if (hpText != null)
			hpText.text = "HP " + health;

		mouseSensitivity = PlayerPrefs.GetFloat ("mouseSensitivity", 4f);
	}

	public void DealDamage (int amount)
	{
		// in case player is not destroyed in this frame
		if (health <= 0)
			return;
		
		health -= amount;
		Mathf.Clamp (health, 0, Mathf.Infinity);

		if (hpText != null)
			hpText.text = "HP " + health;

		if (health <= 0) {
			gameManager.GameOver ();
			gameManager.playerDeath.Play ();
			Destroy (gameObject);
			return;
		}
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovements : MonoBehaviour
{
	public PlayerStats playerStats;

	private Vector2 rotate = Vector2.zero;
	// Update is called once per frame
	void Update ()
	{
		if (playerStats.gameManager.pause)
			return;
		
		if (Input.GetButtonDown ("Fire1")) {
			if (playerStats.canShoot)
				Shoot ();
		}

		if (playerStats.gameManager.gameEnd)
			return;
		
		if (Vector3.Distance (new Vector3 (playerStats.finish.position.x, transform.position.y, playerStats.finish.position.z), transform.position) <= 1) {
			playerStats.gameManager.LevelWon ();
			return;
		}

		RotatePlayer ();
		MovePlayer ();
	}

	void RotatePlayer ()
	{
		rotate.x += Input.GetAxis ("Mouse X") * playerStats.mouseSensitivity;
		rotate.y -= Input.GetAxis ("Mouse Y") * playerStats.mouseSensitivity;

		rotate.x = Mathf.Repeat (rotate.x, 360);
		rotate.y = Mathf.Clamp (rotate.y, playerStats.minAngle, playerStats.maxAngle);

		transform.rotation = Quaternion.Euler (0, rotate.x, 0);

		if (playerStats.cam != null) {
			playerStats.cam.transform.rotation = Quaternion.Euler (rotate.y, 0, 0);
			playerStats.gun.transform.rotation = Quaternion.Euler (rotate.y, playerStats.gun.transform.rotation.eulerAngles.y, playerStats.gun.transform.rotation.eulerAngles.z);
		}
	}

	void MovePlayer ()
	{
		if (Input.GetKey ("a"))
			transform.position -= transform.right * playerStats.speed * Time.deltaTime;
		else if (Input.GetKey ("d"))
			transform.position += transform.right * playerStats.speed * Time.deltaTime;

		if (Input.GetKey ("w"))
			transform.position += transform.forward * playerStats.speed * Time.deltaTime;
		else if (Input.GetKey ("s"))
			transform.position -= transform.forward * playerStats.speed * Time.deltaTime;
	}

	void Shoot ()
	{
		playerStats.gunShot.Play ();
		RaycastHit hit;

		if (Physics.Raycast (transform.position, transform.forward, out hit)) {
			Enemy enemy = hit.transform.GetComponent<Enemy> ();

			if (enemy != null)
				enemy.TakeDamage (playerStats.shootDamage);
		}
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	public GameObject player;
	public Vector3 positionOffset;

	void Update ()
	{
		if (player == null)
			return;
		
		transform.position = player.transform.position + new Vector3 (positionOffset.x, positionOffset.y) + player.transform.forward * positionOffset.z;
		transform.rotation = Quaternion.Euler (transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y, player.transform.rotation.eulerAngles.z);
	}
}

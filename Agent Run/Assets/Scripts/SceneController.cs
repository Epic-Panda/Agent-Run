using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{

	public Image image;
	public AnimationCurve curve;

	void Start ()
	{
		StartCoroutine (FadeIn ());
	}

	IEnumerator FadeIn ()
	{
		float t = 1f;

		while (t > 0f) {
			t -= Time.deltaTime;

			float alpha = curve.Evaluate (t);
			image.color = new Color (0, 0, 0, alpha);
			yield return new WaitForEndOfFrame ();
		}
	}

	IEnumerator FadeToScene (string name)
	{
		float t = 0f;

		while (t < 1f) {
			t += Time.deltaTime;

			float alpha = curve.Evaluate (t);
			image.color = new Color (0, 0, 0, alpha);
			yield return new WaitForEndOfFrame ();
		}

		SceneManager.LoadScene (name);
	}

	public void LoadLevel (string name)
	{
		if (Time.timeScale != 1f)
			Time.timeScale = 1f;
		
		StartCoroutine (FadeToScene (name));
	}
}

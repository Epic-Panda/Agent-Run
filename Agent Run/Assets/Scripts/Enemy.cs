using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{

	public GameObject player;
	public NavMeshAgent agent;
	public int health = 50;
	public Image hpBar;
	private int maxHealth;
	public AudioSource enemyDeath;
	public AudioSource gunShot;

	[Header ("Enemy type")]
	public bool isPatrolling = false;
	public bool isShooting = false;
	public bool isChasing = false;


	[Header ("Patrolling")]
	public Transform[] path;
	private int destinationIndex = 0;
	public GameObject destination;

	[Header ("Shooting")]
	public int damage = 10;
	public float rateOfFire = 1.2f;
	private float cooldown = 0;
	public float range = 20f;
	public float rotationSpeed = 120f;

	private Vector3 dir;
	private PlayerStats playerStats;
	private RaycastHit hit;
	private bool gameEnd = false;

	// Use this for initialization
	void Start ()
	{
		maxHealth = health;

		playerStats = player.GetComponent<PlayerStats> ();

		if (!isPatrolling)
			return;
		
		path = destination.GetComponent<Destinations> ().destinations;
		agent.SetDestination (path [0].position);
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameEnd) {
			if (agent.hasPath)
				agent.ResetPath ();
			return;
		}

		if (player == null)
			return;

		dir = player.transform.position - transform.position;
		
		if (isShooting) {
			Shoot ();
			return;
		}

		if (isPatrolling) {
			Patroll ();
			return;
		}

		Chase ();
	}

	public void TakeDamage(int amount){
		health -= amount;

		hpBar.fillAmount = (float)health / maxHealth;

		if (health <= 0) {
			enemyDeath.Play ();
			Destroy (gameObject);
		}
	}

	public void SetGameOver(bool v){
		gameEnd = v;
	}

	void Shoot ()
	{
		if (cooldown > 0)
			cooldown -= Time.deltaTime;
		
		if (dir.magnitude > range)
			return;

		if (Physics.Raycast (transform.position, dir, out hit, range))
		if (hit.collider.tag == "Player") {
			LookAtPlayer ();
			if (cooldown <= 0) {
				gunShot.Play ();
				playerStats.DealDamage (damage);
				cooldown = 1f / rateOfFire;
			}
		}
	}

	void LookAtPlayer(){
		Quaternion lookAt = Quaternion.LookRotation (dir);
		Vector3 rotation = Quaternion.Lerp (transform.rotation, lookAt, rotationSpeed * Time.deltaTime).eulerAngles;
		transform.rotation = Quaternion.Euler (0, rotation.y, 0);
	}

	void Patroll ()
	{ 
		// if enemy sees player while patrolling he starts chasing him
		if (dir.magnitude <= range) {
		
			if (Physics.Raycast (transform.position, dir, out hit, range))
			if (hit.collider.tag == "Player") {
				isPatrolling = false;
				isChasing = true;
				return;
			}
		}

		if (agent.remainingDistance > agent.stoppingDistance)
			return;
		
		destinationIndex++;

		if (destinationIndex == path.Length)
			destinationIndex = 0;

		agent.SetDestination (path [destinationIndex].position);
	}

	void Chase ()
	{
		// if lost sight of player return to patroll
		if (dir.magnitude > range && path.Length > 0 || player == null && path.Length > 0) {
			isChasing = false;
			isPatrolling = true;
			agent.SetDestination (path [destinationIndex].position);
			return;
		}
			
		
		agent.SetDestination (player.transform.position);

		if (dir.magnitude > 1f)
			return;
		
		playerStats.DealDamage (playerStats.health);
	}

	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere (transform.position, range);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof (NavMeshAgent))]
[RequireComponent (typeof (AudioSource))]
[RequireComponent (typeof (Animator))]
public class NavEnemy : MonoBehaviour {
	public Transform[] pointsWay;
	private int indx = 0;

	public Transform target;
	private NavMeshAgent agente;
	private AudioSource   audio;
	private Animator       anim;
	public AudioClip  audioLose;

	public float     rangoEnemigo;
	public bool      enemigoActivo;

	void Start() {
		agente = GetComponent<NavMeshAgent>();
		audio = GetComponent<AudioSource>();
		StartCoroutine(MoverPoint()); // ! INCIAIAMOS LA COROUTINE
	}

	IEnumerator MoverPoint(){ // Coroutine del movimiento del enemigo
		while (true) {
			if ( Vector3.Distance(transform.position, target.position) <= rangoEnemigo )
				agente.SetDestination(target.position);
			else {
				agente.SetDestination(pointsWay[indx].position);
				if (Vector3.Distance(transform.position, pointsWay[indx].position) < 1f) {
					transform.LookAt(pointsWay[indx].position);
					indx++;
					if ( indx >=  pointsWay.Length) indx = 0;
				}
			}

			yield return new WaitForSeconds(0.1f);
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Player"))
			audio.PlayOneShot(audioLose);
	}

	private void OnDrawGizmos()	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rangoEnemigo);
	}
}
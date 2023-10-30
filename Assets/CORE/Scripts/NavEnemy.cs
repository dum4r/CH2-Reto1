using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent (typeof (NavMeshAgent))]
public class NavEnemy : MonoBehaviour {
	public Transform[] pointsWay;
	private int indx = 0;

	public Transform target;
	NavMeshAgent agente;

	public float     rangoEnemigo;
	public bool      enemigoActivo;

	void Start() {
		agente = GetComponent<NavMeshAgent>();
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

	private void OnDrawGizmos()	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, rangoEnemigo);
	}
}
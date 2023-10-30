using UnityEngine;
using UnityEngine.Events;

public class Interactuable : MonoBehaviour {
	public UnityEvent onInteract; // Unity Event que se disparará al interactuar con el objeto.

	public void Interactuar()	{
		// Ejecuta el Unity Event cuando se interactúa con el objeto.
		onInteract.Invoke();
	}
	private void OnTriggerEnter(Collider other)	{
		// if (!GetComponent<Collider>().isTrigger ) return;
		// Verifica si el objeto que entra en el Trigger tiene un componente "Player".
		if (other.CompareTag("Player"))
			// Llama al método Interactuar cuando el jugador entra en el Trigger.
			Interactuar();
	}

}

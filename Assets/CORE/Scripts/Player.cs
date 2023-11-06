using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	private Animator            anim;
	private CharacterController controller;


	public GameObject PanelDead;
	public GameObject PanelGame;

	public AudioSource efect;
	private int indxStep = 0;
	public List<AudioClip> ListSoundSteps = new List<AudioClip>();


	public float  speed = 5.0f;
	public float  mouseSensitivity = 2.0f;

	public float  alcance = 5f;
	private float verticalRotation = 0f;

	private bool isDead = false;
	
	void Start() {
		anim       = GetComponent<Animator>();    // Animator
		controller = GetComponent<CharacterController>();
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()	{
		// Rotación de la cámara (movimiento del ratón)
		float mouseX =  Input.GetAxis("Mouse X") * mouseSensitivity;
		float mouseY = -Input.GetAxis("Mouse Y") * mouseSensitivity;

		verticalRotation += mouseY;
		verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
		transform.Rotate(Vector3.up * mouseX);
		Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

		if (isDead) return ;

		// Movimiento del personaje
		float horizontalMovement = Input.GetAxis("Horizontal") * speed;
		float verticalMovement   = Input.GetAxis("Vertical")   * speed;
		Vector3 movement = transform.forward * verticalMovement + transform.right * horizontalMovement;
		anim.Play(movement.magnitude < 1 ? "Player_idle" : "Player_run");
		controller.Move(movement * Time.deltaTime);

		if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.E)) {
			// Lanzar un rayo desde la posición de la cámara en la dirección de la mira.
			Ray rayo = new Ray(transform.position, transform.forward);
			RaycastHit hitInfo; // Almacenará información sobre el objeto golpeado por el rayo.

			// Verificar si el rayo golpea un objeto interactuable.
			if (Physics.Raycast(rayo, out hitInfo, alcance)) {
				// Verificar si el objeto golpeado tiene un componente interactuable.
				Interactuable interactuable = hitInfo.collider.GetComponent<Interactuable>();

				if (interactuable != null) // Realizar la interacción con el objeto interactuable. 
					interactuable.Interactuar();
			}
		}
	}

	private void OnTriggerEnter(Collider other) {
		if (other.CompareTag("Enemy")) {
			PanelGame.SetActive(false);
			isDead = true;
			anim.Play("Player_dead");
			Invoke("ActivePanelDead", 1.8f);
		}
	}

	private void ActivePanelDead() {
		PanelDead.SetActive(true);
	}

	private void playSoundStep(){
		efect.PlayOneShot(ListSoundSteps[indxStep]);
		indxStep++;
		if (indxStep == ListSoundSteps.Count) indxStep = 0;
	}
}

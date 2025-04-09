using System;
using UnityEngine;

public class hands : MonoBehaviour
{
	// Define the controller and virtual hand (GameObject) in the Inspector
	public Transform vrController;
	public Transform hand;

	// Define the minimum high (y) possible for the virtual hand and float to store the values
	private float minimumY = 0.80235f;
	private float finalRotationX;



	void Update() {

		// Get th eEuler angles from the Controlller's rotation
		Vector3 eulerRotation = vrController.rotation.eulerAngles;
		// Store only the X-axis rotation
		finalRotationX = eulerRotation.x;

	}


	void LateUpdate() {
		
		if (vrController != null && hand != null) {

			// Get current rotation of the attached object (elbow) 
			Quaternion currentRotation = transform.rotation;

			// Define a new retation only changing the X-axis dependent of the Controller
			Quaternion newRotation = Quaternion.Euler (
				finalRotationX, currentRotation.eulerAngles.y, currentRotation.eulerAngles.z);

			// Temporarally apply new rotation to check the final hand position
			transform.rotation = newRotation;

			// If the hand y position is lower than the threshold (minimumY), the rotation of the elbow returns/stays to the
			// current rotation values
			if (hand.position.y < minimumY) {
				transform.rotation = currentRotation;
			}

		}

	}

}
using System;
using UnityEngine;

public class SyncHeadWithCamera : MonoBehaviour
	{
	
	public Transform vrCamera;

	void LateUpdate() {
		if (vrCamera != null) {
			transform.rotation = vrCamera.rotation;
			//transform.position = vrCamera.position;
		}
	}
}


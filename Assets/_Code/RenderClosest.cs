using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RenderClosest : MonoBehaviour {
	
	public GameObject[] lightFrame;
	public GameObject currentlyRendered;


	void Start () {		
		StartCoroutine(DisableRendering());
	}
	
	IEnumerator DisableRendering () {
		yield return new WaitForSeconds(.55f);
		lightFrame = GameObject.FindGameObjectsWithTag("LightFrame");

		foreach (GameObject l in lightFrame)
		{
			l.SetActive (false);
		}

		currentlyRendered = lightFrame [0];
	}


	void Update(){
		RenderClosestOne(lightFrame);
		//UpdateCameraRotation ();
	}

	void RenderClosestOne(GameObject[] frame){
		GameObject tMin = null;
		float minDist = Mathf.Infinity;
		Vector3 currentPos = transform.position;
		foreach (GameObject t in frame)
		{
			float dist = Vector3.Distance(t.transform.position, currentPos);
			if (dist < minDist)
			{
				tMin = t.gameObject;
				minDist = dist;
			}
		}
		if (tMin != currentlyRendered) {
			currentlyRendered.SetActive (false);
			currentlyRendered = tMin;
			currentlyRendered.SetActive (true);
		}
	}

	void UpdateCameraRotation(){
		if(currentlyRendered!=null)		Camera.main.transform.LookAt (currentlyRendered.transform.GetChild(1).transform);
	}
}

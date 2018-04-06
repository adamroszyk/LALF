using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRenderer : MonoBehaviour {

	public int id;
	public Texture2D color;
	public Texture2D depth;
	public Material mat;


	public void initializeDepth(){
		mat = new Material(Shader.Find("Standard"));
		mat.mainTexture = color;
		GameObject d = new GameObject("depthmap");
		d.transform.SetParent (gameObject.transform);
		d.transform.localPosition=Vector3.zero;
		d.transform.localRotation = transform.parent.localRotation;
		d.AddComponent<DepthmapRenderer> ();
		d.GetComponent<DepthmapRenderer> ().heightMap = depth;
		d.GetComponent<DepthmapRenderer> ().material = mat;

	}

}

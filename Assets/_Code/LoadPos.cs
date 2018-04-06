using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadPos : MonoBehaviour {

	public GameObject parent;
	public List<Vector3> rotation = new List<Vector3>();
	public List<Vector3> position = new List<Vector3>();
	public int limit = 500;


	void Start () {
		Debug.Log ("Load Postion");
		loadPositionAndRotationFromFile ();
		sumUpPositionAndRotation ();
		StartCoroutine(visualizePoints());
	}

	void loadPositionAndRotationFromFile ()
	{
		string[] rotFiles;
		string[] posFiles;

		#if UNITY_IOS//UNITY_STANDALONE_OSX//
		try{	
			rotFiles = Directory.GetFiles (Application.temporaryCachePath+"/proj", "*rot.txt", SearchOption.TopDirectoryOnly);
			posFiles = Directory.GetFiles (Application.temporaryCachePath+"/proj", "*pos.txt", SearchOption.TopDirectoryOnly);
		}catch{}
		#endif
		#if UNITY_EDITOR//UNITY_IOS//
			rotFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*rot.txt", SearchOption.TopDirectoryOnly);
			posFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*pos.txt", SearchOption.TopDirectoryOnly);
		#elif
			#if UNITY_STANDALONE_OSX
				rotFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*rot.txt", SearchOption.TopDirectoryOnly);
				posFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*pos.txt", SearchOption.TopDirectoryOnly);
			#endif
		#endif
		print (rotFiles.Length);

		foreach (string f in rotFiles) {
			if (rotation.Count < limit) {
				var source = new StreamReader (f);
				var fileContents = source.ReadToEnd ();
				source.Close ();
				var r = fileContents.Split (" " [0]);
				rotation.Add (new Vector3 (float.Parse (r [0].Substring(0,12)) , float.Parse (r [1].Substring(0,12)) , float.Parse (r [2].Substring(0,12)) )); /// EULER KURWA !!!
				//Debug.Log(float.Parse (r [1].Substring(0,12)));
			}else break;
		}
		foreach (string f in posFiles) {
			if (position.Count < limit) {
				var source = new StreamReader (f);
				var fileContents = source.ReadToEnd ();
				source.Close ();
				var r = fileContents.Split (" " [0]);
				position.Add (new Vector3 (float.Parse (r [0])/75, float.Parse (r [1])/75, float.Parse (r [2])/75));
			} else
				break;
		}
	}
	/// <summary>
	/// Acummulating rotations from each depthmap - Cloud gives relatives positions and rotations. We need global.
	/// </summary>
	void sumUpPositionAndRotation(){
		var cnt = 0;
		foreach (Vector3 p in position){
			if (cnt > 0) {
				position[cnt]=position[cnt]+position[cnt-1];
				//rotation[cnt]=rotation[cnt]+rotation[cnt-1];
			}
			cnt++;
		}
	}
		
	IEnumerator visualizePoints(){
		yield return new WaitForSeconds(.51f);
		var cnt = 0;
		foreach (Vector3 p in position) {
			if (cnt < limit) {
				GameObject cube = GameObject.CreatePrimitive (PrimitiveType.Cube);
				GameObject cubeSmall = GameObject.CreatePrimitive (PrimitiveType.Cube);					
				cubeSmall.name = "lightfieldPointer" + cnt;
				cubeSmall.transform.parent = cube.transform;
				cubeSmall.transform.localScale = new Vector3 (.6f, .6f, .6f);
				cubeSmall.transform.Translate (Vector3.forward*-0.5f );
				cube.name = "lightfield_" + cnt;
				cube.transform.position = p;
				cube.transform.parent = parent.transform;
				cube.transform.localScale = new Vector3 (.6f, .6f, .6f);
				cube.transform.rotation = Quaternion.Euler (rotation [cnt]);
				cube.AddComponent<ObjectRenderer> ();
				cube.GetComponent<ObjectRenderer> ().id = cnt;
				cube.GetComponent<ObjectRenderer> ().color = GetComponent<LoadTextures> ().color [cnt];
				cube.GetComponent<ObjectRenderer> ().depth = GetComponent<LoadTextures> ().depth [cnt];
				cube.GetComponent<ObjectRenderer> ().initializeDepth ();
				cube.tag = "LightFrame";
				cnt++;
			} else
				break;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class LoadTextures : MonoBehaviour {

	public List<Texture2D> depth = new List<Texture2D>();
	public List<Texture2D> color = new List<Texture2D>();
	public int limit = 50;


	// Use this for initialization
	void Start () {
		Debug.Log ("Load Textures");

		string[] depthFiles;
		string[] colorFiles;
		#if UNITY_IOS
		try{
			depthFiles = Directory.GetFiles (Application.temporaryCachePath+"/proj", "*depth.jpg", SearchOption.TopDirectoryOnly);
			colorFiles = Directory.GetFiles (Application.temporaryCachePath+"/proj", "*color.jpg", SearchOption.TopDirectoryOnly);
		}catch{}
		#endif
		#if UNITY_EDITOR
			depthFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*depth.jpg", SearchOption.TopDirectoryOnly);
			colorFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*color.jpg", SearchOption.TopDirectoryOnly);
		#elif
			#if UNITY_STANDALONE_OSX
				depthFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*depth.jpg", SearchOption.TopDirectoryOnly);
				colorFiles = Directory.GetFiles ("/Users/adam/Desktop/proj", "*color.jpg", SearchOption.TopDirectoryOnly);
			#endif
		#endif

		foreach (string f in depthFiles){
			if (depth.Count < limit) {
				depth.Add (LoadPNG (f));
			}
		}
		foreach (string f in colorFiles){
			if (color.Count < limit) {
				color.Add (LoadPNG (f));
			}
		}
	}


	public static Texture2D LoadPNG(string filePath) {
		Texture2D tex = null;
		byte[] fileData;
		if (File.Exists(filePath))     {
			fileData = File.ReadAllBytes(filePath);
			tex = new Texture2D(2, 2);
			tex.LoadImage(fileData); //..this will auto-resize the texture dimensions.
		}
		return tex;
	}

}

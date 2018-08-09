using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float ly = 6.5f; // y-component of lantern
		float sy = 2f; // y-component of stand
		int r = 10; // radius of lantern spread

		GameObject cam = GameObject.Find ("Main Camera");
		cam.transform.position = new Vector3 (0f, 5.5f, -5f);

		GameObject stand = Resources.Load("Stand") as GameObject;
		stand = Instantiate (stand, new Vector3 (0f, sy, 0f), Quaternion.identity);
		stand.name = "Stand";
		stand.transform.rotation = Quaternion.Euler(-90f, 0f, 0f);
		Renderer rendStand = stand.GetComponent<Renderer> ();
		rendStand.material = Resources.Load("StoneSurface") as Material;


		GameObject lantern = Resources.Load("Lantern") as GameObject;
		lantern = Instantiate (lantern, new Vector3 (0f, ly, 0f), Quaternion.identity);
		lantern.name = "Lantern";

		GameObject paper = GameObject.Find ("/Lantern/Paper");

		GameObject rim = GameObject.Find ("/Lantern/Rim");

		GameObject wick = GameObject.Find ("/Lantern/Wick");

		GameObject wire1 = GameObject.Find ("/Lantern/Wire1");
		GameObject wire2 = GameObject.Find ("/Lantern/Wire2");
		GameObject wire3 = GameObject.Find ("/Lantern/Wire3");
		GameObject wire4 = GameObject.Find ("/Lantern/Wire4");
		GameObject[] wires = { wire1, wire2, wire3, wire4 };

		Material paperMat = new Material (Shader.Find ("Standard"));
		paperMat.color = new Color32 (255, 250, 226, 255);
		paperMat.SetFloat("_Mode", 3);
		Renderer rendPaper = paper.GetComponent<Renderer> ();
		rendPaper.material = paperMat;

		Material rimMat = new Material (Shader.Find ("Standard"));
		rimMat.color = new Color32 (255, 255, 255, 255);
		Renderer rendRim = rim.GetComponent<Renderer> ();
		rendRim.material = rimMat;

		Material wickMat = new Material (Shader.Find ("Standard"));
		wickMat.color = new Color32 (178, 95, 0, 255);
		Renderer rendWick = wick.GetComponent<Renderer> ();
		rendWick.material = wickMat;

		Material wireMat = new Material (Shader.Find ("Standard"));
		wireMat.color = new Color32 (0, 0, 0, 255);
		for (int i = 0; i < 4; i++) {
			Renderer rendWire = wires [i].GetComponent<Renderer> ();
			rendWire.material = wireMat;
		}

		// places all lanterns inside a circle with radius of r lanterns
		int c = 0;
		for (int a = -r; a <= r; a++) {
			for (int b = -r; b <= r; b++) {
				if (a == 0 && b == 0) { continue; }
				else {
					if (a * a + b * b <= r * r) {
						GameObject lClone = Instantiate (lantern, new Vector3 ((float) 10*a, ly, (float) 10*b), Quaternion.identity);
						GameObject sClone = Instantiate (stand, new Vector3 ((float) 10*a, sy, (float) 10*b), Quaternion.Euler (-90f, 0f, 0f));
						lClone.name = "LanternClone" + c.ToString ("000");
						sClone.name = "StandClone" + c.ToString ("000");
						c++;
					}
				}
			}
		}



//		// places all lanterns in square with side length 15
//		for (int i = 0; i < 2; i++) {
//			for (int j = 0; j < 112; j++) {
//				float n = 0f;
//				int k = j;
//				if (i == 0) { 
//					n = 1f;
//					k = j + 1;
//				}
//				if (i == 1) { 
//					n = -1f; 
//					k = j + 113;
//				}
//				float x = n*(-70+10*((Mathf.Floor(j/15))));
//				float z = n*(-70+10*((j%15)));
//				GameObject lClone = Instantiate(lantern, new Vector3 (x, ly, z), Quaternion.identity);
//				GameObject sClone = Instantiate(stand, new Vector3 (x, sy, z), Quaternion.Euler(-90f, 0f, 0f));
//				if (n > 0) {
//					lClone.name = "LanternClone" + k.ToString ("000");
//					sClone.name = "StandClone" + k.ToString ("000");
//				}
//				if (n < 0) {
//					lClone.name = "LanternClone" + k.ToString ("000");
//					sClone.name = "StandClone" + k.ToString ("000");
//				}
//
//			}
//		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

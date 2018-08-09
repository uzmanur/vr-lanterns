using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lantern : MonoBehaviour {

	// Use this for initialization
	void Start () {

		float ly = 6.5f; // y-component of lantern
		float sy = 2f; // y-component of stand
		int r = 10; // radius of lantern spread

		randomSkybox ();

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
		Material paperMat = new Material (Shader.Find ("Standard"));
		paperMat.color = new Color32 (255, 250, 226, 255);
		paperMat.SetFloat("_Mode", 3);
		Renderer rendPaper = paper.GetComponent<Renderer> ();
		rendPaper.material = paperMat;

		GameObject rim = GameObject.Find ("/Lantern/Rim");
		Material rimMat = new Material (Shader.Find ("Standard"));
		rimMat.color = new Color32 (255, 255, 255, 255);
		Renderer rendRim = rim.GetComponent<Renderer> ();
		rendRim.material = rimMat;

		GameObject wick = GameObject.Find ("/Lantern/Wick");
		Material wickMat = new Material (Shader.Find ("Standard"));
		wickMat.color = new Color32 (178, 95, 0, 255);
		Renderer rendWick = wick.GetComponent<Renderer> ();
		rendWick.material = wickMat;

		GameObject wire1 = GameObject.Find ("/Lantern/Wire1");
		GameObject wire2 = GameObject.Find ("/Lantern/Wire2");
		GameObject wire3 = GameObject.Find ("/Lantern/Wire3");
		GameObject wire4 = GameObject.Find ("/Lantern/Wire4");
		GameObject[] wires = { wire1, wire2, wire3, wire4 };
		Material wireMat = new Material (Shader.Find ("Standard"));
		wireMat.color = new Color32 (0, 0, 0, 255);
		for (int i = 0; i < 4; i++) {
			Renderer rendWire = wires [i].GetComponent<Renderer> ();
			rendWire.material = wireMat;
		}

		ParticleSystem flame = wick.AddComponent<ParticleSystem>() as ParticleSystem;
		// accessing main module of particle system
		var fm = flame.main;
		fm.startSpeed = 1f;
		fm.startSize = 1.5f;
		fm.startRotation = (float) Math.PI;
		// accessing shape module of particle system
		var fs = flame.shape;
		fs.angle = 0;
		fs.radius = 0.25f * (float) Math.Sqrt (2);
		// accessing size over lifetime module of particle system
		var fsz = flame.sizeOverLifetime;
		fsz.enabled = true;
		AnimationCurve curve = new AnimationCurve();
		curve.AddKey (0f, 0.5f);
		curve.AddKey (0.05f, 0.45f);
		curve.AddKey (0.125f, 0.25f);
		curve.AddKey (0.20f, 0.05f);
		curve.AddKey (0.25f, 0f);
		fsz.size = new ParticleSystem.MinMaxCurve(1.0f, curve);
		// accessing lights module of particle system
		var fl = flame.lights;
		fl.enabled = true;
		fl.ratio = 1f;
		GameObject fire = new GameObject ("Fire");
		Light fireLight = fire.AddComponent<Light> ();
		fire.SetActive (false);
		fireLight.type = LightType.Point;
		fireLight.range = 7f;
		fireLight.color = new Color32 (255, 174, 0, 255);
		fl.light = fireLight;
		fl.useRandomDistribution = false;
		fl.useParticleColor = false;
		fl.maxLights = 100;
		// accessing renderer module of particle system
		ParticleSystemRenderer rendFlame = flame.GetComponent<ParticleSystemRenderer> ();
		rendFlame.material = Resources.Load("EffectExamples/FireExplosionEffects/Materials/FlameRoundYellowParticle") as Material;

		// places all lanterns inside a circle with radius of r lanterns
		int c = 0;
		for (int a = -r; a <= r; a++) {
			for (int b = -r; b <= r; b++) {
				if (a == 0 && b == 0) { continue; }
				else {
					if (a * a + b * b <= r * r) {
						GameObject lClone = Instantiate (lantern, new Vector3 ((float) 10*a, ly, (float) 10*b), Quaternion.identity);
						GameObject sClone = Instantiate (stand, new Vector3 ((float) 10*a, sy, (float) 10*b), Quaternion.Euler (-90f, 0f, 0f));
						c++;
						lClone.name = "LanternClone" + c.ToString ("000");
						sClone.name = "StandClone" + c.ToString ("000");
					}
				}
			}
		}
	}

	void randomSkybox() {

		Material sky1 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material sky2 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material sky3 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material sky4 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material sky5 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material sky6 = new Material (Shader.Find ("RenderFX/Skybox"));
		Material[] sky = { sky1, sky2, sky3, sky4, sky5, sky6 };

		Texture star1 = Resources.Load ("StarSkyBox04/StarSkyBox041") as Texture;
		Texture star2 = Resources.Load ("StarSkyBox04/StarSkyBox042") as Texture;
		Texture star3 = Resources.Load ("StarSkyBox04/StarSkyBox043") as Texture;
		Texture star4 = Resources.Load ("StarSkyBox04/StarSkyBox044") as Texture;
		Texture star5 = Resources.Load ("StarSkyBox04/StarSkyBox045") as Texture;
		Texture star6 = Resources.Load ("StarSkyBox04/StarSkyBox046") as Texture;

		Texture[] star = { star1, star2, star3, star4, star5, star6 };

		for (int i = 0; i < 6; i++) {
			sky [i].SetTexture ("_FrontTex", star [(i + 0) % 6]);
			sky [i].SetTexture ("_BackTex", star [(i + 1) % 6]);
			sky [i].SetTexture ("_LeftTex", star [(i + 2) % 6]);
			sky [i].SetTexture ("_RightTex", star [(i + 3) % 6]);
			sky [i].SetTexture ("_UpTex", star [(i + 4) % 6]);
			sky [i].SetTexture ("_DownTex", star [(i + 5) % 6]);
		}

		System.Random rand = new System.Random ();
		int n = rand.Next(0,6);
		RenderSettings.skybox = sky [n];
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

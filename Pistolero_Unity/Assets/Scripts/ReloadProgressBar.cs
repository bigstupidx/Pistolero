using UnityEngine;
using System.Collections;

public class ReloadProgressBar : MonoBehaviour {
	public tk2dUIProgressBar progressBar {get; private set;}

	public tk2dBaseSprite[] sprites;

	void Start () {
		progressBar = GetComponent<tk2dUIProgressBar>();
	}

	public void Show() {
		foreach (tk2dBaseSprite s in sprites) {
			Color c = s.color;
			c.a = 1;
			s.color = c;
		}
	}

	public void Hide() {
		foreach (tk2dBaseSprite s in sprites) {
			Color c = s.color;
			c.a = 0;
			s.color = c;
		}
	}
	
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class Helper : MonoBehaviour {
	public static Helper instance;

	public tk2dCamera gameCam;
	public tk2dCameraAnchor gameCamAnchorLowerLeft;
	public tk2dCameraAnchor gameCamAnchorLowerRight;
	public tk2dCameraAnchor gameCamAnchorUpperRight;
	public tk2dCameraAnchor gameCamAnchorUpperLeft;

	// Use this for initialization
	void Awake () {
		instance = this;
	}

	public Rect GetScreenRectInWorldSpace() {
		Rect r = new Rect();
		r.x = gameCamAnchorLowerLeft.transform.position.x;
		r.y = gameCamAnchorLowerLeft.transform.position.y;
		r.width = gameCamAnchorLowerRight.transform.position.x - gameCamAnchorLowerLeft.transform.position.x;
		r.height = gameCamAnchorUpperRight.transform.position.y - gameCamAnchorLowerRight.transform.position.y;
		return r;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

using UnityEngine;
using System.Collections;

public class TouchZoneInitializer : MonoBehaviour {
	public tk2dUICamera uiCamera;
	public BoxCollider leftTouchZone;
	public BoxCollider rightTouchZone;

	// Use this for initialization
	void Awake () {
		Vector3 ls = leftTouchZone.size;
		Vector3 lc = leftTouchZone.center;

		Vector3 rs = rightTouchZone.size;
		Vector3 rc = rightTouchZone.center;

		float aspectRatio = (float)Screen.width / (float)Screen.height;
		float height = uiCamera.camera.orthographicSize * 2;
		float width = height * aspectRatio;

		ls.x = width / 2f;
		ls.y = height;

		lc.x = ls.x / 2f;
		lc.y = 0;

		rs.x = width / 2f;
		rs.y = height;

		rc.x = -rs.x / 2f;
		rc.y = 0;

		leftTouchZone.size = ls;
		leftTouchZone.center = lc;

		rightTouchZone.size = rs;
		rightTouchZone.center = rc;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}

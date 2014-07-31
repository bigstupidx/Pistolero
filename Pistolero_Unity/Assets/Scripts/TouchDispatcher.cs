using UnityEngine;
using System.Collections;
using System;

public class TouchDispatcher : MonoBehaviour {
	public TouchZone leftTouchZone;
	public TouchZone rightTouchZone;

	public Action<TouchSide, TouchType> SignalTouch;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable() {
		leftTouchZone.SignalTouch += HandleTouch;
		rightTouchZone.SignalTouch += HandleTouch;
	}

	void OnDisable() {
		leftTouchZone.SignalTouch -= HandleTouch;
		rightTouchZone.SignalTouch -= HandleTouch;
	}

	public bool GetIsPressed(TouchSide touchSide) {
		if (touchSide == TouchSide.Left) return leftTouchZone.uiItem.IsPressed;
		if (touchSide == TouchSide.Right) return rightTouchZone.uiItem.IsPressed;
		Debug.LogWarning("invalid touch zone type");
		return false;
	}

	void HandleTouch(TouchSide touchSide, TouchType touchType) {
		if (SignalTouch != null) SignalTouch(touchSide, touchType);
	}
}

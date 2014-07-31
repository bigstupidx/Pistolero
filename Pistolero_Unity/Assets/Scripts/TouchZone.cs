using UnityEngine;
using System.Collections;
using System;

public enum TouchSide {
	Left,
	Right
}

public enum TouchType {
	OnDown,
	OnUp,
	OnClick,
	OnRelease
}

public class TouchZone : MonoBehaviour {
	public TouchSide touchSide = TouchSide.Left;

	public Action<TouchSide, TouchType> SignalTouch;

	public tk2dUIItem uiItem {get; private set;}

	void Awake() {
		uiItem = GetComponent<tk2dUIItem>();
	}

	void OnEnable() {
		uiItem.OnDown += HandleOnDown;
		uiItem.OnUp += HandleOnUp;
		uiItem.OnClick += HandleOnClick;
		uiItem.OnRelease += HandleOnRelease;
	}

	void OnDisable() {
		uiItem.OnDown -= HandleOnDown;
		uiItem.OnUp -= HandleOnUp;
		uiItem.OnClick -= HandleOnClick;
		uiItem.OnRelease -= HandleOnRelease;
	}

	void HandleOnDown() {
		if (SignalTouch != null) SignalTouch(touchSide, TouchType.OnDown);
	}

	void HandleOnUp() {
		if (SignalTouch != null) SignalTouch(touchSide, TouchType.OnUp);
	}

	void HandleOnClick() {
		if (SignalTouch != null) SignalTouch(touchSide, TouchType.OnClick);
	}

	void HandleOnRelease() {
		if (SignalTouch != null) SignalTouch(touchSide, TouchType.OnRelease);
	}

	void Update() {

	}
}

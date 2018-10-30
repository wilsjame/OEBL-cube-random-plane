using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Examples.InteractiveElements;

public class DistanceSlider : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// Get slider value, called by slider's event update
	public void getSlider()
	{

		// Get the slider's current value 
		GameObject slider = GameObject.Find("Distance_Slider"); // Grab distance slider from scene
		SliderGestureControl sliderScript = slider.GetComponent<SliderGestureControl>(); // Grab script off of slider
		float sliderValue = sliderScript.GetSliderValue ();

		// Get the height slider's value
		GameObject height_slider = GameObject.Find("Height_Slider"); // Grab height slider from scene
		SliderGestureControl height_sliderScript = height_slider.GetComponent<SliderGestureControl>();
		float height_sliderValue = height_sliderScript.GetSliderValue ();

		// Move the task's object collection according to the slider value
		GameObject task_objects = GameObject.Find("SpawnHotSpots"); // Grab the task objects from scene
		task_objects.transform.position = new Vector3(0.0f, height_sliderValue, sliderValue);
	}

}

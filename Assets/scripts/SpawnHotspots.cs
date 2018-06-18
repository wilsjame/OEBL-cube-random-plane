using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpawnHotspots : MonoBehaviour {

	/* Trigger point prefab */
	public Transform trigger_point;

	/* Global struct to encapsulate coordinates */
	public struct CoOrds 
	{
		public float x, y, z;

		/* Constructor to initialize x, y, and z coordinates */ 
		public CoOrds(float x_coOrd, float y_coOrd, float z_coOrd)
		{
			x = x_coOrd;
			y = y_coOrd;
			z = z_coOrd;
		}
	}

	/* Another global variable (oh no!) to keep track of list iterations */
	public int itr = 0;

	/* Use this for initialization */
	void Start () {

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();
	
	}

	/* Update is called once per frame */
	void Update () {
	}
		
	/* This function is called from Hotspot.cs after Start () */
	public void HotSpotTriggerInstantiate ()
	{

		/* Useful variable and data structure definitions */
		CoOrds coords_temp = new CoOrds (); 					/* Create a temporary CoOrds struct */
		List<CoOrds> coOrds_collection = new List<CoOrds> (); 	/* Use a list to handle data points */

		/* Create all the data points and add them to the list */
		/* z = 0 frame				 (x, y, z) */
		CoOrds coords_1 = new CoOrds (0, 0, 0);
		coOrds_collection.Add (coords_1);
		CoOrds coords_2 = new CoOrds (0.3f, 0, 0);
		coOrds_collection.Add (coords_2);
		CoOrds coords_3 = new CoOrds (0.6f, 0, 0);
		coOrds_collection.Add (coords_3);
		CoOrds coords_4 = new CoOrds (0, 0.3f, 0);
		coOrds_collection.Add (coords_4);
		CoOrds coords_5 = new CoOrds (0.3f, 0.3f, 0);
		coOrds_collection.Add (coords_5);
		CoOrds coords_6 = new CoOrds (0.6f, 0.3f, 0);
		coOrds_collection.Add (coords_6);
		CoOrds coords_7 = new CoOrds (0, 0.6f, 0);
		coOrds_collection.Add (coords_7);
		CoOrds coords_8 = new CoOrds (0.3f, 0.6f, 0);
		coOrds_collection.Add (coords_8);
		CoOrds coords_9 = new CoOrds (0.6f, 0.6f, 0);
		coOrds_collection.Add (coords_9);

		/* z = 0.3 frame			  (x, y, z) */
		CoOrds coords_10 = new CoOrds (0, 0, 0.3f);
		coOrds_collection.Add (coords_10);
		CoOrds coords_11 = new CoOrds (0.3f, 0, 0.3f);
		coOrds_collection.Add (coords_11);
		CoOrds coords_12 = new CoOrds (0.6f, 0, 0.3f);
		coOrds_collection.Add (coords_12);
		CoOrds coords_13 = new CoOrds (0, 0.3f, 0.3f);
		coOrds_collection.Add (coords_13);
		CoOrds coords_14 = new CoOrds (0.3f, 0.3f, 0.3f);
		coOrds_collection.Add (coords_14);
		CoOrds coords_15 = new CoOrds (0.6f, 0.3f, 0.3f);
		coOrds_collection.Add (coords_15);
		CoOrds coords_16 = new CoOrds (0, 0.6f, 0.3f);
		coOrds_collection.Add (coords_16);
		CoOrds coords_17 = new CoOrds (0.3f, 0.6f, 0.3f);
		coOrds_collection.Add (coords_17);
		CoOrds coords_18 = new CoOrds (0.6f, 0.6f, 0.3f);
		coOrds_collection.Add (coords_18);

		/* z = 0.6 frame			  (x, y, z) */
		CoOrds coords_19 = new CoOrds (0, 0, 0.6f);
		coOrds_collection.Add (coords_19);
		CoOrds coords_20 = new CoOrds (0.3f, 0, 0.6f);
		coOrds_collection.Add (coords_20);
		CoOrds coords_21 = new CoOrds (0.6f, 0, 0.6f);
		coOrds_collection.Add (coords_21);
		CoOrds coords_22 = new CoOrds (0, 0.3f, 0.6f);
		coOrds_collection.Add (coords_22);
		CoOrds coords_23 = new CoOrds (0.3f, 0.3f, 0.6f);
		coOrds_collection.Add (coords_23);
		CoOrds coords_24 = new CoOrds (0.6f, 0.3f, 0.6f);
		coOrds_collection.Add (coords_24);
		CoOrds coords_25 = new CoOrds (0, 0.6f, 0.6f);
		coOrds_collection.Add (coords_25);
		CoOrds coords_26 = new CoOrds (0.3f, 0.6f, 0.6f);
		coOrds_collection.Add (coords_26);
		CoOrds coords_27 = new CoOrds (0.6f, 0.6f, 0.6f);
		coOrds_collection.Add (coords_27);

		/* Do only once, Fisher Yates shuffle list to randomize spawn order */
		if (itr == 0) {

			/* Debugging */ 
			Debug.Log ("Shuffling hotspot spawn points!");

			int random_placeholder;

			for (int i = 0; i < coOrds_collection.Count; i++) {
				random_placeholder = i + Random.Range (0, coOrds_collection.Count - i);

				/* Swap */
				coords_temp = coOrds_collection [i];
				coOrds_collection [i] = coOrds_collection [random_placeholder];
				coOrds_collection [random_placeholder] = coords_temp;
			}

		}
			
		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
			
			/* Debugging */
			Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			
			/* Copy the next coordinate in the list to the temp variable */
			coords_temp = coOrds_collection [itr];
			itr++;

			/* Spawn the point */ 
			Instantiate (trigger_point, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity);

			/* Debugging and hack fix */
			if (itr == coOrds_collection.Count) {
				
				Instantiate (trigger_point, new Vector3 (0, 0, 0), Quaternion.identity); // Bug fix: coordinate (0, 0, 0) gets skipped above, spawn manually here.
				
				Debug.Log ("Entire Coords_Collection spawned!");
				Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			}

		}

	}

}

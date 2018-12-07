using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Diagnostics;
using System.Threading;

public class SpawnHotspots : MonoBehaviour {

	/* Prefabs */
	public Transform trigger_point;
	public Transform trial_counter;
	public Transform cubes;

	/* Global variables */
	/* Encapsulate coordinates */
	public struct CoOrds 
	{
		public float x, y, z;
		public string plane;

		/* Constructor to initialize x, y, and z coordinates */ 
		public CoOrds(float x_coOrd, float y_coOrd, float z_coOrd, string p)
		{
			x = x_coOrd;
			y = y_coOrd;
			z = z_coOrd;
			plane = p;
		}
	}

	List<CoOrds> coOrds_collection = new List<CoOrds> (); 	/* Entire point collection */
	List<CoOrds> coOrds_collection_1 = new List<CoOrds> ();	/* z = 0.0 frame points */
	List<CoOrds> coOrds_collection_2 = new List<CoOrds> ();	/* z = 0.3 frame points */
	List<CoOrds> coOrds_collection_3 = new List<CoOrds> ();	/* z = 0.6 frame points */
	List<CoOrds> counter_collection = new List<CoOrds> ();	/* Trial counter coordinates */
	public int itr = 0;					/* Keep track of list iterations */
	public int trial = 0;					/* Keep track of completed trials */

	public string fileName = "cube_random_plane_task_time_";
	public string path;

	public Stopwatch trial_stopwatch = new Stopwatch();
	public Stopwatch plane_stopwatch = new Stopwatch();
	public System.TimeSpan trial_time; 
	public System.TimeSpan plane_1_time;
	public System.TimeSpan plane_2_time;
	public System.TimeSpan plane_3_time;
	
	/* Use this for initialization */
	void Start () 
	{

		// Create unique out file 
		fileName = fileName + System.DateTime.Now + ".txt";
		fileName = fileName.Replace("/","-");
		fileName = fileName.Replace(":",";");
		path = Path.Combine(Application.persistentDataPath, fileName);
		UnityEngine.Debug.Log(fileName);
		UnityEngine.Debug.Log(Application.persistentDataPath);

		initializeCoordinates (ref coOrds_collection, ref counter_collection, ref coOrds_collection_1, ref coOrds_collection_2, ref coOrds_collection_3);

		/* Call function once on startup to create initial hotspot */
		HotSpotTriggerInstantiate ();

		/* Spawn new cube collection */
		Instantiate (cubes, new Vector3 (-0.3f, 0.3f, 0.3f), Quaternion.identity, this.transform); // Make this gameObject the parent
	}

	/* Populate point collection and counter collection coordinate lists.
	 * Parameters are global list variables referencing the coordinate collections.
	 * Initially they are empty and after this function finishes they're filled with 
	 * the trigger point coordinates randomized by plane (i.e. Each plane is contained for example:
	 * [randomized coordinates in front plane][randomized coordinates in mid plane][randomized coordinates in back plane])
	 * and the trial counter coordinates. */
	public void initializeCoordinates (ref List<CoOrds> coOrds_collection, ref List<CoOrds> counter_collection, ref List<CoOrds> coOrds_collection_1, ref List<CoOrds> coOrds_collection_2, ref List<CoOrds> coOrds_collection_3)
	{

		/* Create all the data points and add them to the list */
		/* z = 0 frame				 (x, y, z) */
		CoOrds coords_1 = new CoOrds (0, 0, 0, "front");
		coOrds_collection_1.Add (coords_1);
		CoOrds coords_2 = new CoOrds (0.3f, 0, 0, "front");
		coOrds_collection_1.Add (coords_2);
		CoOrds coords_3 = new CoOrds (0.6f, 0, 0, "front");
		coOrds_collection_1.Add (coords_3);
		CoOrds coords_4 = new CoOrds (0, 0.3f, 0, "front");
		coOrds_collection_1.Add (coords_4);
		CoOrds coords_5 = new CoOrds (0.3f, 0.3f, 0, "front");
		coOrds_collection_1.Add (coords_5);
		CoOrds coords_6 = new CoOrds (0.6f, 0.3f, 0, "front");
		coOrds_collection_1.Add (coords_6);
		CoOrds coords_7 = new CoOrds (0, 0.6f, 0, "front");
		coOrds_collection_1.Add (coords_7);
		CoOrds coords_8 = new CoOrds (0.3f, 0.6f, 0, "front");
		coOrds_collection_1.Add (coords_8);
		CoOrds coords_9 = new CoOrds (0.6f, 0.6f, 0, "front");
		coOrds_collection_1.Add (coords_9);

		/* z = 0.3 frame			  (x, y, z) */
		CoOrds coords_10 = new CoOrds (0, 0, 0.3f, "middle");
		coOrds_collection_2.Add (coords_10);
		CoOrds coords_11 = new CoOrds (0.3f, 0, 0.3f, "middle");
		coOrds_collection_2.Add (coords_11);
		CoOrds coords_12 = new CoOrds (0.6f, 0, 0.3f, "middle");
		coOrds_collection_2.Add (coords_12);
		CoOrds coords_13 = new CoOrds (0, 0.3f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_13);
		CoOrds coords_14 = new CoOrds (0.3f, 0.3f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_14);
		CoOrds coords_15 = new CoOrds (0.6f, 0.3f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_15);
		CoOrds coords_16 = new CoOrds (0, 0.6f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_16);
		CoOrds coords_17 = new CoOrds (0.3f, 0.6f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_17);
		CoOrds coords_18 = new CoOrds (0.6f, 0.6f, 0.3f, "middle");
		coOrds_collection_2.Add (coords_18);

		/* z = 0.6 frame			  (x, y, z) */
		CoOrds coords_19 = new CoOrds (0, 0, 0.6f, "back");
		coOrds_collection_3.Add (coords_19);
		CoOrds coords_20 = new CoOrds (0.3f, 0, 0.6f, "back");
		coOrds_collection_3.Add (coords_20);
		CoOrds coords_21 = new CoOrds (0.6f, 0, 0.6f, "back");
		coOrds_collection_3.Add (coords_21);
		CoOrds coords_22 = new CoOrds (0, 0.3f, 0.6f, "back");
		coOrds_collection_3.Add (coords_22);
		CoOrds coords_23 = new CoOrds (0.3f, 0.3f, 0.6f, "back");
		coOrds_collection_3.Add (coords_23);
		CoOrds coords_24 = new CoOrds (0.6f, 0.3f, 0.6f, "back");
		coOrds_collection_3.Add (coords_24);
		CoOrds coords_25 = new CoOrds (0, 0.6f, 0.6f, "back");
		coOrds_collection_3.Add (coords_25);
		CoOrds coords_26 = new CoOrds (0.3f, 0.6f, 0.6f, "back");
		coOrds_collection_3.Add (coords_26);
		CoOrds coords_27 = new CoOrds (0.6f, 0.6f, 0.6f, "back");
		coOrds_collection_3.Add (coords_27);

		//UnityEngine.Debug.Log ("Shuffling spawn points within planes...");
		shuffle (ref coOrds_collection, ref coOrds_collection_1, ref coOrds_collection_2, ref coOrds_collection_3);

		/* Trial counters */
		CoOrds counter_1 = new CoOrds (-0.43f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_1);
		CoOrds counter_2 = new CoOrds (-0.37f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_2);
		CoOrds counter_3 = new CoOrds (-0.31f, 0.57f, 0.3f, null);
		counter_collection.Add (counter_3);
	}

	/* Shuffle coordinate list by plane and shuffle plane order */
	public void shuffle (ref List<CoOrds> coOrds_collection, ref List<CoOrds> coOrds_collection_1, ref List<CoOrds> coOrds_collection_2, ref List<CoOrds> coOrds_collection_3)
	{
		int i;
		int temp;
		int random_placeholder;
		int[] order = {1, 2, 3};
		CoOrds coords_temp = new CoOrds ();

		/* Fisher Yates shuffle coordinate lists to randomize spawn order */
		//UnityEngine.Debug.Log ("Shuffling spawn points within planes...");

		/* z = 0 frame */ 
		for (i = 0; i < coOrds_collection_1.Count; i++) {
			random_placeholder = i + Random.Range (0, coOrds_collection_1.Count - i);

			/* Swap */
			coords_temp = coOrds_collection_1 [i];
			coOrds_collection_1 [i] = coOrds_collection_1 [random_placeholder];
			coOrds_collection_1 [random_placeholder] = coords_temp;
		}
		
		/* z = 0.3 frame */
		for (i = 0; i < coOrds_collection_2.Count; i++) {
			random_placeholder = i + Random.Range (0, coOrds_collection_2.Count - i);

			/* Swap */
			coords_temp = coOrds_collection_2 [i];
			coOrds_collection_2 [i] = coOrds_collection_2 [random_placeholder];
			coOrds_collection_2 [random_placeholder] = coords_temp;
		}
		
		/* z = 0.6 frame */
		for (i = 0; i < coOrds_collection_3.Count; i++) {
			random_placeholder = i + Random.Range (0, coOrds_collection_3.Count - i);

			/* Swap */
			coords_temp = coOrds_collection_3 [i];
			coOrds_collection_3 [i] = coOrds_collection_3 [random_placeholder];
			coOrds_collection_3 [random_placeholder] = coords_temp;
		}
		
		/* Randomize the plane order */ 
		//UnityEngine.Debug.Log("Plane order before shuffle: " + order[0] + order[1] + order[2]);
		
		for (i = 0; i < 3; i++) {
			random_placeholder = i + Random.Range (0, 3 - i);

			/* Swap */
			temp = order[i];
			order[i] = order[random_placeholder];
			order[random_placeholder] = temp;
		}
		
		//UnityEngine.Debug.Log("Plane order after shuffle: " + order[0] + order[1] + order[2]);
		
		/* Randomly add each shuffled plane into the coOrds_collection list */
		coOrds_collection.Clear();

		for(i = 0; i < 3; i++) {
				
			switch (order[i]) {
				case 1:
					coOrds_collection.AddRange(coOrds_collection_1);
					break;
				case 2:
					coOrds_collection.AddRange(coOrds_collection_2);
					break;
				case 3:
					coOrds_collection.AddRange(coOrds_collection_3);
					break;
			}

		}

	}

	/* Reset the task for a new trial by 
	 * destroying all the triggered cubes,
	 * spawning a new cube collection,
	 * resetting itr, and shuffling the trigger
	 * point spawn coordinates. */
	public void reset ()
	{

		/* Destroy triggered cubes */
		GameObject[] triggeredCubes = GameObject.FindGameObjectsWithTag ("triggered");

		for(var i = 0; i < triggeredCubes.Length; i++) {
			Destroy(triggeredCubes[i]);
		}

		/* Spawn new cube collection */
		Transform local_cubes = Instantiate (cubes, new Vector3 (-0.3f, 0.3f, 0.3f), Quaternion.identity, this.transform); // Make this gameObject the parent
		local_cubes.localPosition = new Vector3 (-0.3f, 0.3f, 0.3f); // Spawn position relative to parent

		itr = 0;

		shuffle (ref coOrds_collection, ref coOrds_collection_1, ref coOrds_collection_2, ref coOrds_collection_3);
		HotSpotTriggerInstantiate ();
	}

	/* This function is called from Hotspot.cs after Start ().
	 * Instead of spawning all the points at random like OEBL-Cube,
	 * the three frontal planes are randomized and then points within
	 * the current plane are spawned at random until the plane is filled. Then
	 * the next plane's points are filled until all three planes are complete. */
	public void HotSpotTriggerInstantiate ()
	{

		/* check if user has tapped first point */
		if (itr == 1) {
			// Begin trial and plane 1 timing
			UnityEngine.Debug.Log( "Start a trial and plane 1 timing." );
			trial_stopwatch.Start();
			plane_stopwatch.Start();
		}
		else if (itr == 10) {
			// Begin plane 2 timing
			UnityEngine.Debug.Log( "Start plane 2 timing." );
			plane_stopwatch.Start();
		}
		else if (itr == 19) {
			// Begin plane 3 timing
			UnityEngine.Debug.Log( "Start plane 3 timing." );
			plane_stopwatch.Start();
		}

		CoOrds coords_temp = new CoOrds (); 				
		
		/* Begin spawning */
		if (itr < coOrds_collection.Count) {
		//UnityEngine.Debug.Log ("coOrds_collection count: " + coOrds_collection.Count + " itr: " + itr);
			
			/* Copy the next coordinate in the list to the temp variable */
			coords_temp = coOrds_collection [itr];

			/* Spawn the point */ 
			Transform local_trigger_point = Instantiate (trigger_point, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity, this.transform); // Make this gameObject the parent
			local_trigger_point.localPosition = new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z); // Spawn position relative to parent

			switch (coords_temp.plane) {
				case "front":
					local_trigger_point.tag = "front";
					break;
				case "middle":
					local_trigger_point.tag = "middle";
					break;
				case "back":
					local_trigger_point.tag = "back";
					break;
			}

			// Plane timing
			if (itr == 9) {
				plane_1_time = plane_stopwatch.Elapsed; 
				plane_stopwatch.Reset();
				coords_temp = coOrds_collection [itr - 1]; // Copy the previous coordinate in the list to determine last plane
				UnityEngine.Debug.Log("Plane 1: " + coords_temp.plane + " " + plane_1_time);

				// Write time to file
				File.AppendAllText(@path, "Plane 1" + " : ");
				File.AppendAllText(@path, plane_1_time.ToString() + " " + coords_temp.plane);
				File.AppendAllText(@path, "\r\n");
			}
			else if (itr == 18) {
				plane_2_time = plane_stopwatch.Elapsed; 
				plane_stopwatch.Reset();
				coords_temp = coOrds_collection [itr - 1]; // Copy the previous coordinate in the list to determine last plane
				UnityEngine.Debug.Log("Plane 2: " + coords_temp.plane + " " + plane_2_time);

				// Write time to file
				File.AppendAllText(@path, "Plane 2" + " : ");
				File.AppendAllText(@path, plane_2_time.ToString() + " " + coords_temp.plane);
				File.AppendAllText(@path, "\r\n");
			}
				
			itr++;
		}
		/* Start new trial and update counter */
		else {
		//UnityEngine.Debug.Log( "Starting a new trial!" );

			trial++;

			// Plane and trial timing
			trial_time = trial_stopwatch.Elapsed;
			trial_stopwatch.Reset();
			plane_3_time = plane_stopwatch.Elapsed; 
			plane_stopwatch.Reset();
			coords_temp = coOrds_collection [itr - 1]; // Copy the previous coordinate in the list to determine last plane
			UnityEngine.Debug.Log("Plane 3: " + coords_temp.plane + " " + plane_3_time);
			UnityEngine.Debug.Log("Trial " + trial + ": " + trial_time);
			trial_stopwatch.Reset();

			// Write plane time to file
			File.AppendAllText(@path, "Plane 3" + " : ");
			File.AppendAllText(@path, plane_3_time.ToString() + " " + coords_temp.plane);
			File.AppendAllText(@path, "\r\n");

			// Write trial time to file
			File.AppendAllText(@path, "Trial " + trial + " : ");
			File.AppendAllText(@path, trial_time.ToString());
			File.AppendAllText(@path, "\r\n");

			/* Copy counter location coordinates */
			coords_temp = counter_collection [trial - 1];

			/* Spawn counter */
			Transform local_trial_counter = Instantiate (trial_counter, new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z), Quaternion.identity, this.transform); // Make this gameObject the parent
			local_trial_counter.localPosition = new Vector3 (coords_temp.x, coords_temp.y, coords_temp.z); // Spawn position relative to parent
			UnityEngine.Debug.Log( "Trial " + trial + " completed!");

			if (trial < 3) {
				reset();
			}

		}

		return;

	}

}

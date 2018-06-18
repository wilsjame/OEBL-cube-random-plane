/* Attach to game objects intended to be trigger points (hotspots). 
 * This script works in conjuction with HotSpotColorChanger.cs. */

using UnityEngine;

public class Hotspot : MonoBehaviour
{
    public delegate void HotspotEntered(GameObject someCube);
    public static event HotspotEntered OnEntered;

    public delegate void HotspotExited(GameObject someCube);
    public static event HotspotExited OnExited;

    private void OnTriggerEnter(Collider other)
    {
  
        if (OnEntered != null)
        {
            OnEntered(other.gameObject);

			/* Call test function */
			GameObject.Find ("GameObject_SpawnHotSpots").GetComponent<SpawnHotspots> ().HotSpotTriggerInstantiate ();

			/* Remove this hotspot when triggered */
			Destroy (this.gameObject);
        }

    }

    private void OnTriggerExit(Collider other)
    {

        if (OnExited != null)
        {
            OnExited(other.gameObject);
        }

    }

}

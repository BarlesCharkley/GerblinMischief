using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI : MonoBehaviour
{
    private Transform target;
    private CameraControl camControl;
    private bool isAlreadyPOI;

	void Awake ()
    {
        camControl = FindObjectOfType<CameraControl>();
        target = camControl.POITarget;
	}
	
	void Update ()
    {
        TrackPlayer();
	}

    void TrackPlayer()
    {
        float dist = Vector3.Distance(transform.position, target.position);

        if (dist <= camControl.minFocusDistance)
        {
            PointOfInterest(true);
        }

        if (isAlreadyPOI && dist > camControl.minFocusDistance)
        {
            PointOfInterest(false);
        }
    }

    public void PointOfInterest(bool POI)
    {
        if (POI && !isAlreadyPOI)
        {
            camControl.camTargets.Add(gameObject.transform);
            isAlreadyPOI = true;
        }

        if (isAlreadyPOI && !POI)
        {
            camControl.camTargets.Remove(gameObject.transform);
            isAlreadyPOI = false;
        }
    }

    private void OnDisable()
    {
        PointOfInterest(false);
    }

    private void OnDestroy()
    {
        PointOfInterest(false);
    }
}

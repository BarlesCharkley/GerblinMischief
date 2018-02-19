//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Klak.Motion;

//public class GoodieScript : MonoBehaviour
//{
//    private SnailMove snailMove;
//    private Collect collectScript;
//    private CameraControl camControl;
//    private CinematicsHandler cinematics;

//    private SmoothFollow followScript;
//    private BrownianMotion noiseScript;
//    private POI poiScript;
//    private bool isCollected;
//    private bool hasSpawned;

//    void Start ()
//    {
//        snailMove = FindObjectOfType<SnailMove>();
//        collectScript = FindObjectOfType<Collect>();
//        camControl = FindObjectOfType<CameraControl>();
//        cinematics = FindObjectOfType<CinematicsHandler>();

//        followScript = GetComponent<SmoothFollow>();
//        noiseScript = GetComponent<BrownianMotion>();
//        poiScript = GetComponent<POI>();

//        followScript.enabled = false;
//        noiseScript.enabled = true;
//        poiScript.enabled = false;
//        hasSpawned = false;
//    }

//    void FixedUpdate()
//    {
//        if (!isCollected)
//        {
//            GoodieFollow();

//            if (Input.GetKeyDown(cinematics.endTestButton) && snailMove.canControl)
//            {
//                GoodieCollected();
//            }
//        }

//        if (snailMove.isInCinematic && !hasSpawned)
//        {
//            SpawnCinematicLeaf();
//        }
//    }

//    public void GoodieFollow()
//    {
//        float playerDist = Vector3.Distance(transform.position, collectScript.inventoryTarget.position);

//        if (playerDist <= collectScript.collectDistance && !cinematics.titleIsUp)
//        {
//            GoodieCollected();
//        }
//    }

//    private void GoodieCollected()
//    {
//        followScript.enabled = true;
//        noiseScript.enabled = false;
//        poiScript.PointOfInterest(false);
//        poiScript.enabled = false;
//        followScript._target = collectScript.inventoryTarget;
//        collectScript.AddToGoodies(gameObject);
//        gameObject.layer = LayerMask.NameToLayer(collectScript.collectedLayer);

//        snailMove.StartBoost();

//        int goodieCoint = collectScript.collectedGoodies.Count;

//        followScript._positionSpeed =
//            collectScript.baseLeafFollowSpeed - Random.Range(0f, collectScript.followIncrement);

//        //followScript._positionSpeed =
//        //    collectScript.followSpeeds[goodieCoint - 1];

//        transform.parent = collectScript.collectedParent;
//        isCollected = true;
//    }

//    void SpawnCinematicLeaf()
//    {
//        transform.parent = null;
//        GameObject cinematicLeaf = Instantiate(cinematics.leafPrefab, transform.position, transform.rotation);
//        hasSpawned = true;
//        gameObject.SetActive(false);
//    }
//}

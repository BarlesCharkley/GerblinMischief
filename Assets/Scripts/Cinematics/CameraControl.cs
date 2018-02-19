using UnityEngine;
using System.Collections.Generic;
using Klak.Motion;

public class CameraControl : MonoBehaviour
{
    [Header("Camera Focus")]
    [SerializeField] public float m_DampTime = 0.8f;                 // Approximate time for the camera to refocus.
    [SerializeField] private float dampMult = 1.3f;
    [SerializeField] public float m_ScreenEdgeBuffer = 7f;           // Space between the top/bottom most target and the screen edge.
    [SerializeField] public float m_MinOrthoSize = 12f;                  // The smallest orthographic size the camera can be.
    public float minFocusDistance;
    [SerializeField] public Transform POITarget;
    [SerializeField] public Transform[] playerTargets; //FIX THIS TO WORK WITH TWO PLAYERS
    [SerializeField] private float movingLookDistance; //INPUT NORMALIZED MAGNITUDE OF PLAYER DIRECTION VECTOR
    [SerializeField] public float restingLookDistance;
    public List<Transform> camTargets;

    //private SnailMove snailMove;
    private Camera m_Camera;                                          // Used for referencing the camera.
    private float m_ZoomSpeed;                                        // Reference speed for the smooth damping of the orthographic size.
    private Vector3 m_MoveVelocity;                                   // Reference velocity for the smooth damping of the position.
    private Vector3 m_DesiredPosition;                                // The position the camera is moving towards.
    private Vector3 poiTargetPos;
    //private BrownianMotion camNoise;
    [Space]
    private float updatedDamp;


    private void Awake()
    {
        //snailMove = FindObjectOfType<SnailMove>();
        m_Camera = GetComponentInChildren<Camera>();
        //camNoise = GetComponentInChildren<BrownianMotion>();
        //camTargets.Add(playerTarget);
    }

    private void FixedUpdate()
    {
        Move(); // Move the camera towards a desired position.
        Zoom(); // Change the size of the camera based.
        UpdatePlayerTarget();
    }

    private void Move()
    {
        // Find the average position of the targets.
        FindAveragePosition();

        // Smoothly transition to that position.
        transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, updatedDamp);
        POITarget.position = Vector3.SmoothDamp(POITarget.position, poiTargetPos, ref m_MoveVelocity, updatedDamp);
    }

    void UpdatePlayerTarget()
    {
        //if (snailMove.isInCinematic)
        //{
        //    movingLookDistance = 0f;
        //    restingLookDistance = 0f;
        //    CamNoise(false);
        //}

        if (camTargets.Count == 2) // if only one target
        {
            updatedDamp = m_DampTime;

            //if (snailMove.updatedSpeed > 0.2f) // and player is moving forward
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, movingLookDistance), updatedDamp);
            //}

            //else if (snailMove.updatedSpeed < -0.1f) // and player is moving backwards
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, movingLookDistance * -0.5f), updatedDamp);
            //}

            //else // and player is still
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, restingLookDistance), updatedDamp / 2);
            //}
        }

        if (camTargets.Count > 2) // if more than one target
        {
            updatedDamp = m_DampTime * dampMult;

            //if (snailMove.updatedSpeed > 0.2f) // and player is moving forward
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, movingLookDistance * 0.9f), updatedDamp);
            //}

            //else if (snailMove.updatedSpeed < -0.1f) // and player is moving backwards
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, movingLookDistance * -0.5f), updatedDamp);
            //}

            //else // and player is still
            //{
            //    playerTarget.localPosition = Vector3.Lerp(playerTarget.localPosition, new Vector3(0, 0, restingLookDistance), updatedDamp / 2);
            //}
        }
    }

    private void FindAveragePosition()
    {
        Vector3 averagePos = new Vector3();
        Vector3 averagePlayerPos = new Vector3();
        int numTargets = 0;
        int numPlayers = 0;

        // Go through all the targets and add their positions together.
        for (int i = 0; i < camTargets.Count; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!camTargets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePos += camTargets[i].position;
            numTargets++;
        }

        for (int i = 0; i < 2; i++)
        {
            // If the target isn't active, go on to the next one.
            if (!camTargets[i].gameObject.activeSelf)
                continue;

            // Add to the average and increment the number of targets in the average.
            averagePlayerPos += camTargets[i].position;
            numPlayers++;
        }

        // If there are targets divide the sum of the positions by the number of them to find the average.
        if (numTargets > 0)
            averagePos /= numTargets;

        if (numPlayers > 0)
            averagePlayerPos /= numPlayers;

        // Keep the same y value.
        averagePos.y = transform.position.y;
        averagePlayerPos.y = transform.position.y;

        // The desired position is the average position;
        m_DesiredPosition = averagePos;
        poiTargetPos = averagePlayerPos;
    }

    private void Zoom()
    {
        // Find the required size based on the desired position and smoothly transition to that size.
        float requiredSize = FindRequiredSize();
        m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, updatedDamp);
    }

    private float FindRequiredSize()
    {
        // Find the position the camera rig is moving towards in its local space.
        Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

        // Start the camera's size calculation at zero.
        float size = 0f;

        // Go through all the targets...
        for (int i = 0; i < camTargets.Count; i++)
        {
            // ... and if they aren't active continue on to the next target.
            if (!camTargets[i].gameObject.activeSelf)
                continue;

            // Otherwise, find the position of the target in the camera's local space.
            Vector3 targetLocalPos = transform.InverseTransformPoint(camTargets[i].position);

            // Find the position of the target from the desired position of the camera's local space.
            Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

            // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

            // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
            size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
        }

        // Add the edge buffer to the size.
        size += m_ScreenEdgeBuffer;

        // Make sure the camera's size isn't below the minimum.
        size = Mathf.Max(size, m_MinOrthoSize);

        return size;
    }

    public void SetStartPositionAndSize()
    {
        // Find the desired position.
        FindAveragePosition();

        // Set the camera's position to the desired position without damping.
        transform.position = m_DesiredPosition;

        // Find and set the required size of the camera.
        m_Camera.orthographicSize = FindRequiredSize();
    }

    //public void CamNoise(bool isNoisy)
    //{
    //    if (isNoisy)
    //    {
    //        camNoise.enabled = true;
    //    }

    //    if (!isNoisy)
    //    {
    //        camNoise.enabled = false;
    //    }
    //}
}

//using UnityEngine;
//using System.Collections;
//using Klak.Motion;

//public class SnailMove : MonoBehaviour
//{
//    #region Editable Fields

//    //[SerializeField] private int m_PlayerNumber = 1;              

//    [Space]
//    [Header("Handling")]
//    [SerializeField] private float m_Speed = 18f;                 // How fast the snail moves forward and back.
//    [SerializeField] private float slowdown = 0.5f;
//    [SerializeField] private float m_TurnSpeed = 250f;            // How fast the snail turns in degrees per second.
//    [SerializeField] private KeyCode handbrake = KeyCode.Space;
//    [SerializeField] private float powerslideAmount;
//    public float updatedSpeed;

//    [Space]
//    [Header("Boost")]
//    [SerializeField] private float boostMultiplier = 1.2f;
//    [SerializeField] private float boostTimer = 1f;

//    [Space]
//    [Header("Animation")]
//    [SerializeField] private Transform skellington;
//    [SerializeField] private float animSpeed = 1.5f;
//    [SerializeField] private float tiltSpeed = 10f;
//    [SerializeField] private float maximumTiltAngle;

//    [Space]
//    [Header("Teapot")]
//    [SerializeField] private GameObject teapot;
//    [SerializeField] private float teapotTiltFreq;
//    [SerializeField] private float teapotTiltAmp;

//    [Space]
//    [Header("Sounds")]
//    [SerializeField] private AudioSource backupSound;
//    [SerializeField] private float backupSoundVolume = 0.4f;

//    #endregion

//    #region Private Fields

//    [HideInInspector] public float moveMult = 1f;
//    private float initMoveMult;

//    private string m_MovementAxisName;          // The name of the input axis for moving forward and back.
//    private string m_TurnAxisName;              // The name of the input axis for turning.
//    private Rigidbody m_Rigidbody;              // Reference used to move the snail.
//    private float m_MovementInputValue;         // The current value of the movement input.
//    private float m_TurnInputValue;             // The current value of the turn input.
//    private bool isBoosting;
//    private BrownianMotion teapotNoise;
//    private BrownianMotion slimeNoise;

//    private Animator snailAnim;
//    private Quaternion skelInitialQuat;
//    private Vector3 skelInitialEuler;
//    private Animator teapotAnim;
//    private ParticleSystem[] m_particleSystems; // References to all the particles systems used by the snail

//    private CameraControl camControl;
//    private CinematicsHandler cinematics;
//    [HideInInspector] public bool isInCinematic;
//    [HideInInspector] public bool canControl = true;

//    #endregion

//    #region Trigger Functions

//    private void Awake()
//    {
//        m_Rigidbody = GetComponent<Rigidbody>();
//    }

//    private void OnEnable()
//    {
//        m_Rigidbody.isKinematic = false;
//        m_MovementInputValue = 0f;
//        m_TurnInputValue = 0f;
//        isBoosting = false;

//        snailAnim = GetComponentInChildren<Animator>();
//        teapotAnim = teapot.GetComponentInChildren<Animator>();
//        teapotNoise = teapot.GetComponent<BrownianMotion>();
//        slimeNoise = GameObject.Find("Slime").GetComponent<BrownianMotion>();
//        camControl = FindObjectOfType<CameraControl>();
//        cinematics = FindObjectOfType<CinematicsHandler>();
//        skelInitialQuat = skellington.localRotation;
//        skelInitialEuler = skellington.localEulerAngles;
//        initMoveMult = moveMult;

//        m_particleSystems = GetComponentsInChildren<ParticleSystem>();
//        for (int i = 0; i < m_particleSystems.Length; ++i)
//        {
//            m_particleSystems[i].Play();
//        }

//        //Invoke("TitleUp", titleDelay);
//    }

//    private void OnDisable()
//    {
//        m_Rigidbody.isKinematic = true;

//        for (int i = 0; i < m_particleSystems.Length; ++i)
//        {
//            m_particleSystems[i].Stop();
//        }
//    }

//    private void Start()
//    {
//        m_MovementAxisName = "Vertical";
//        m_TurnAxisName = "Horizontal";
//    }

//    private void Update()
//    {
//        m_MovementInputValue = Input.GetAxis(m_MovementAxisName);
//        m_TurnInputValue = Input.GetAxis(m_TurnAxisName);

//        if (cinematics.titleIsUp && canControl)
//        {
//            if (Mathf.Abs(m_MovementInputValue) > 0f || Mathf.Abs(m_TurnInputValue) > 0)
//            {
//                cinematics.TitleDown();
//            }
//        }
//    }

//    //private void EngineAudio ()
//    //{
//    //    // If there is no input (the tank is stationary)...
//    //    if (Mathf.Abs (m_MovementInputValue) < 0.1f && Mathf.Abs (m_TurnInputValue) < 0.1f)
//    //    {
//    //        // ... and if the audio source is currently playing the driving clip...
//    //        if (m_MovementAudio.clip == m_EngineDriving)
//    //        {
//    //            // ... change the clip to idling and play it.
//    //            m_MovementAudio.clip = m_EngineIdling;
//    //            m_MovementAudio.pitch = Random.Range (m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
//    //            m_MovementAudio.Play ();
//    //        }
//    //    }
//    //    else
//    //    {
//    //        // Otherwise if the tank is moving and if the idling clip is currently playing...
//    //        if (m_MovementAudio.clip == m_EngineIdling)
//    //        {
//    //            // ... change the clip to driving and play.
//    //            m_MovementAudio.clip = m_EngineDriving;
//    //            m_MovementAudio.pitch = Random.Range(m_OriginalPitch - m_PitchRange, m_OriginalPitch + m_PitchRange);
//    //            m_MovementAudio.Play();
//    //        }
//    //    }
//    //}


//    private void FixedUpdate()
//    {
//        if (canControl)
//        {
//            Move();
//            Turn();
//            UpdateAnim();
//        }

//        if (isInCinematic)
//        {
//            m_Speed = Mathf.Lerp(m_Speed, 0, (1 / slowdown) * Time.fixedDeltaTime);
//            m_TurnSpeed = Mathf.Lerp(m_Speed, 0, (1 / slowdown) * Time.fixedDeltaTime);
//            maximumTiltAngle = 0;
//        }
//    }
//    #endregion

//    #region Movement Functions

//    private void Move()
//    {
//        bool isReversing;

//        updatedSpeed = m_MovementInputValue * m_Speed * moveMult * Time.deltaTime;
//        //teapotAnim.SetFloat("SettleSpeed", m_MovementInputValue);

//        if (m_MovementInputValue < 0 )
//        {
//            updatedSpeed *= slowdown;
//            isReversing = true;
//        }
//        else
//        {
//            isReversing = false;
//        }

//        //if (updatedSpeed >= 0.3f)
//        //{
//        //    camControl.CamNoise(true);
//        //    slimeNoise.enabled = true;
//        //}
//        //else
//        //{
//        //    camControl.CamNoise(false);
//        //    slimeNoise.enabled = false;
//        //}

//        if (isReversing && !isInCinematic)
//        {
//            backupSound.volume = backupSoundVolume;
//        }
//        else
//            backupSound.volume = 0f;

//        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
//        Vector3 movement = transform.forward * updatedSpeed;

//        // Apply this movement to the rigidbody's position.
//        m_Rigidbody.MovePosition(m_Rigidbody.position + movement);

//    }

//    private void Turn()
//    {
//        // Determine the number of degrees to be turned based on the input, speed and time between frames.
//        float turn = m_TurnInputValue * m_TurnSpeed * Time.deltaTime;
//        bool isHandbraking;

//        if (m_MovementInputValue < 0.1f)
//        {
//            turn *= slowdown;
//        }

//        if (Input.GetKey(handbrake) && m_MovementInputValue > 0.1f)
//        {
//            turn *= powerslideAmount;
//            isHandbraking = true;
//        }

//        else
//            isHandbraking = false;

//        if (!isHandbraking)
//        {
//            TiltTeapot((turn * teapotTiltFreq), (turn * teapotTiltAmp));
//        }

//        else
//            TiltTeapot((turn / 1.5f * teapotTiltFreq), (turn / 1.5f * teapotTiltAmp));

//        TiltSnail();

//        // Make this into a rotation in the y axis.
//        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);

//        // Apply this rotation to the rigidbody's rotation.
//        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * turnRotation);
//    }
//    #endregion

//    #region Animation Functions

//    private void TiltSnail()
//    {
//        float maxTilt = maximumTiltAngle;

//        if (m_MovementInputValue < 0.1f)
//        {
//            maxTilt *= slowdown;
//        }
//        else
//            maxTilt = maximumTiltAngle;


//        if (m_TurnInputValue > 0.1f && Mathf.Abs(skellington.localEulerAngles.x) < Mathf.Abs(maxTilt + skelInitialEuler.x))
//        {
//            Vector3 tiltRight = new Vector3(0, tiltSpeed, 0) * Time.fixedDeltaTime;
//            skellington.Rotate(tiltRight);
//        }
//        else if (m_TurnInputValue < -0.1f && Mathf.Abs(skellington.localEulerAngles.x) < Mathf.Abs(maxTilt + skelInitialEuler.x))
//        {
//            Vector3 tiltLeft = new Vector3(0, -tiltSpeed, 0) * Time.fixedDeltaTime;
//            skellington.Rotate(tiltLeft);
//        }
//        else if (m_TurnInputValue < 0.1f && m_TurnInputValue > -0.1f || Mathf.Abs(skellington.localEulerAngles.x) > Mathf.Abs(maxTilt + skelInitialEuler.x))
//        {
//            skellington.localRotation = Quaternion.Lerp(skellington.localRotation, skelInitialQuat, tiltSpeed * slowdown * Time.fixedDeltaTime);
//        }
//    }

//    private void TiltTeapot(float tiltFreq, float tiltAmplitude)
//    {
//        teapotNoise._rotationAmplitude = Mathf.Lerp(teapotNoise._rotationAmplitude, tiltAmplitude, teapotTiltAmp * Time.fixedDeltaTime);
//        teapotNoise._rotationFrequency = Mathf.Lerp(teapotNoise._rotationFrequency, tiltFreq, teapotTiltAmp * Time.fixedDeltaTime);
//    }

//    public void TeapotSettle()
//    {
//        teapotAnim.SetTrigger("Settle");
//        //Debug.Log(teapotAnim.GetFloat("SettleSpeed"));
//    }

//    public void MirrorIdle()
//    {
//        //bool mirror = snailAnim.GetBool("IdleMirrored");
//        //snailAnim.SetBool("IdleMirrored", !mirror);
//        float idleSpeed = snailAnim.GetFloat("IdleSpeed");
//        snailAnim.SetFloat("IdleSpeed", idleSpeed * -1);
//    }

//    public void GiveControl()
//    {
//        canControl = true;
//        Debug.Log("Can control now!");
//    }

//    public void StartBoost()
//    {
//        if (!isBoosting)
//        {
//            StartCoroutine(BoostTime());
//        }
//    }

//    private IEnumerator BoostTime()
//    {
//        isBoosting = true;
//        moveMult = boostMultiplier;
//        yield return new WaitForSeconds(boostTimer);
//        moveMult = initMoveMult;
//        isBoosting = false;
//    }

//    private void UpdateAnim()
//    {
//        float updatedAnimSpeed = animSpeed * m_MovementInputValue;

//        if (isInCinematic)
//        {
//            updatedAnimSpeed = 0f;
//        }

//        snailAnim.SetFloat("SnailSpeed", updatedAnimSpeed);
//    }
//    #endregion
//}
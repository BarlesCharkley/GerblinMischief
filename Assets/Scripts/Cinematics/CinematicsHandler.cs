//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Timeline;
//using UnityEngine.Playables;
//using Klak.Motion;

//public class CinematicsHandler : MonoBehaviour
//{
    
//    #region Title Screen Variables

//    [Header("Title")]
//    [SerializeField] public KeyCode skipIntroButton;
//    [SerializeField] private GameObject startPrompt;
//    [SerializeField] private float startPromptDelay;
//    [SerializeField] private GameObject titleCard;
//    [SerializeField] private float titleDelay;
//    //[SerializeField] public float snailShockDuration = 4f;
//    [Space]

//    [SerializeField] private float startCamHeight = 3f;
//    [SerializeField] private float startEdgeBuffer = 3f;
//    [SerializeField] private float titleEdgeBuffer = 6f;
//    [SerializeField] private float titleCamDepth = -30f;
//    [SerializeField] private float titleDampTime = 0.5f;
//    [Space]

//    [SerializeField] private float startPromptFadeOutTime;
//    [SerializeField] private float titleFadeInTime;
//    [SerializeField] private float titleFadeOutTime;
//    [SerializeField] private float titleDisableDelay;
//    [Space]

//    [SerializeField] private FadeAlpha[] titleTextFade;
//    [SerializeField] private Color titleColor;
//    [Space]

//    [SerializeField] private FadeAlpha[] titleTextBGFade;
//    [SerializeField] private Color titleBGColor;
//    [Space]

//    [SerializeField] private FadeAlpha[] bylineTextFade;
//    [SerializeField] private Color bylineColor;
//    [Space]

//    [SerializeField] private FadeAlpha[] startPromptFadeScripts;
//    [Space]

//    public bool startPromptIsUp = false;
//    public bool titleIsUp = false;
//    public bool introDone;

//    private float initRestingLookDistance;
//    private float initDampTime;
//    private float initEdgeBuffer;
//    private float initStartCamHeight;
//    private bool skipIntro;

//    #endregion
//    ////////////////////////////////////////////////////////
//    #region End Cinematic Variables

//    [Space]
//    [Header("End Cinematic")]

//    [SerializeField] private TimelineAsset endCinematic;
//    [SerializeField] public KeyCode endTestButton;
//    [Space]

//    [Header("Snail")]
//    [SerializeField] private float turnPause;
//    [SerializeField] private float turnSpeed = 2f;
//    [SerializeField] private Quaternion lookAngle;
//    [Space]

//    [Header("Camera")]
//    [SerializeField] private float zoomLevel = 5f;
//    [SerializeField] private float endCamHeight = 1;
//    [SerializeField] private float camSpeed = 2f;
//    [SerializeField] private float cinematicClipPlane = 75f;
//    [SerializeField] private float cinematicClipPlaneSpeed = 2f;
//    [Space]

//    [Header("Leaves")]
//    [SerializeField] public GameObject leafPrefab;
//    [SerializeField] public Transform teaTarget;
//    [SerializeField] private float leavesPause;
//    [SerializeField] public float leafDistance;
//    [SerializeField] public float gatherSpeed;
//    public List<GameObject> leaves;

//    private PlayableDirector endCinematicDirector;
//    private bool isEnding;
//    private bool isAnimating;
//    private bool isTurning;
//    #endregion

//    private Transform player;
//    private SnailMove snailMoveScript;
//    private CameraControl camControl;
//    private ReactionManager reactionManager;

//    void Start ()
//    {
//        endCinematicDirector = GetComponent<PlayableDirector>();
//        player = GameObject.FindGameObjectWithTag("Player").transform;
//        snailMoveScript = player.GetComponent<SnailMove>();
//        camControl = FindObjectOfType<CameraControl>();
//        reactionManager = FindObjectOfType<ReactionManager>();

//        initRestingLookDistance = camControl.restingLookDistance;
//        initDampTime = camControl.m_DampTime;
//        initEdgeBuffer = camControl.m_ScreenEdgeBuffer;
//        initStartCamHeight = startCamHeight;
//        camControl.m_ScreenEdgeBuffer = startEdgeBuffer;
//        Invoke("ShowStartPrompt", startPromptDelay);
//        PrepTitleTextFade();
//        startCamHeight = initStartCamHeight + 3f;
//    }

//    private void Update()
//    {
//        if (Input.GetKey(skipIntroButton))
//        {
//            skipIntro = true;
//            //reactionManager.Unreact(startPrompt);

//            for (int i = 0; i < startPromptFadeScripts.Length; i++)
//            {
//                startPromptFadeScripts[i].BeginFadeOut();
//            }

//            TitleDown();
//            startCamHeight = initStartCamHeight + 3f;
//        }

//        if (startPromptIsUp && Input.anyKeyDown)
//        {
//            StartCoroutine(BeginTitleSequence());
//        }
//    }

//    private void FixedUpdate()
//    {
//        PlayerFaceCamera();
//        AdjustCam();
//    }

//    #region Title Screen Functions

//    public void ShowStartPrompt()
//    {
//        if (!skipIntro)
//        {
//            reactionManager.React(startPrompt, false, 1f);
//            startPromptIsUp = true;
//            startCamHeight = startCamHeight + 2f;
//        }
//    }

//    public IEnumerator BeginTitleSequence()
//    {
//        //reactionManager.Unreact(startPrompt);
//        yield return new WaitForSeconds(titleDelay);
//        TitleUp();
//    }

//    public void TitleUp()
//    {
//        if (!titleIsUp && !skipIntro && !introDone)
//        {
//            titleCard.SetActive(true);
//            titleIsUp = true;
//            camControl.restingLookDistance = titleCamDepth;
//            camControl.m_DampTime = titleDampTime;
//            camControl.m_ScreenEdgeBuffer = titleEdgeBuffer;
//            titleCard.GetComponentInChildren<POI>().enabled = true;

//            FadeTitleTextIn();
//            reactionManager.React(reactionManager.shock, false, 1f);
//        }
//    }

//    public void TitleDown()
//    {
//        if (titleIsUp || skipIntro)
//        {
//            if (!snailMoveScript.canControl)
//            {
//                snailMoveScript.GiveControl();
//            }

//            titleIsUp = false;
//            camControl.restingLookDistance = initRestingLookDistance;
//            camControl.m_DampTime = initDampTime;
//            camControl.m_ScreenEdgeBuffer = initEdgeBuffer;

//            //titleCard.GetComponentInChildren<Animator>().SetTrigger("TitleExit");
//            titleCard.GetComponent<SmoothFollow>().enabled = false;
//            titleCard.GetComponentInChildren<POI>().enabled = false;

//            FadeTitleTextOut();
//            ActivateLeafPOI();
//            reactionManager.Unreact(reactionManager.shock);
//        }
//    }

//    private void PrepTitleTextFade()
//    {
//        titleCard.SetActive(false);

//        for (int i = 0; i < titleTextFade.Length; i++)
//        {
//            titleTextFade[i].fadeTime = titleFadeInTime;
//            titleTextFade[i].disableDelay = titleDisableDelay;
//            titleTextFade[i].materialColor = titleColor;
//        }

//        for (int i = 0; i < titleTextBGFade.Length; i++)
//        {
//            titleTextBGFade[i].fadeTime = titleFadeInTime;
//            titleTextBGFade[i].disableDelay = titleDisableDelay;
//            titleTextBGFade[i].materialColor = titleBGColor;
//        }

//        for (int i = 0; i < bylineTextFade.Length; i++)
//        {
//            bylineTextFade[i].fadeTime = titleFadeInTime;
//            bylineTextFade[i].disableDelay = titleDisableDelay;
//            bylineTextFade[i].materialColor = bylineColor;
//        }

//        for (int i = 0; i < startPromptFadeScripts.Length; i++)
//        {
//            startPromptFadeScripts[i].fadeTime = startPromptFadeOutTime;
//            startPromptFadeScripts[i].disableDelay = titleDisableDelay;
//            startPromptFadeScripts[i].materialColor = titleColor;
//        }
//    }

//    private void FadeTitleTextIn()
//    {
//        if (!introDone)
//        {
//            startCamHeight = initStartCamHeight;

//            for (int i = 0; i < titleTextFade.Length; i++)
//            {
//                titleTextFade[i].BeginFadeIn();
//            }

//            for (int i = 0; i < titleTextBGFade.Length; i++)
//            {
//                titleTextBGFade[i].BeginFadeIn();
//            }

//            for (int i = 0; i < bylineTextFade.Length; i++)
//            {
//                bylineTextFade[i].BeginFadeIn();
//            }

//            startPromptIsUp = false;
//            for (int i = 0; i < startPromptFadeScripts.Length; i++)
//            {
//                startPromptFadeScripts[i].BeginFadeOut();
//            }
//        } 
//    }

//    private void FadeTitleTextOut()
//    {
//        introDone = true;
//        startCamHeight = initStartCamHeight;

//        for (int i = 0; i < titleTextFade.Length; i++)
//        {
//            titleTextFade[i].fadeTime = titleFadeOutTime;
//            titleTextFade[i].BeginFadeOut();
//        }

//        for (int i = 0; i < titleTextBGFade.Length; i++)
//        {
//            titleTextBGFade[i].fadeTime = titleFadeOutTime;
//            titleTextBGFade[i].BeginFadeOut();
//        }

//        for (int i = 0; i < bylineTextFade.Length; i++)
//        {
//            bylineTextFade[i].fadeTime = titleFadeOutTime;
//            bylineTextFade[i].BeginFadeOut();
//        }
//    }

//    private void ActivateLeafPOI()
//    {
//        GameObject[] leaves = GameObject.FindGameObjectsWithTag("Leaf");

//        for (int i = 0; i < leaves.Length; i++)
//        {
//            leaves[i].GetComponent<POI>().enabled = true;
//        }
//    }

//    #endregion

//    #region End Cinematic Functions

//    public void TheEnd()
//    {
//        snailMoveScript.isInCinematic = true;
//        player.GetComponent<Animator>().Play("Idle");

//        isEnding = true;

//        StartCoroutine(PlayEndCinematic());
//    }

//    IEnumerator PlayEndCinematic()
//    {
//        yield return new WaitForSeconds(turnPause);
//        isTurning = true;

//        yield return new WaitForSeconds(leavesPause);
//        endCinematicDirector.Play(endCinematic);
//        isAnimating = true;
//        GatherLeaves();
//    }

//    private void PlayerFaceCamera()
//    {
//        if (isTurning)
//        {
//            float updatedTurnSpeed = Mathf.Lerp(0, turnSpeed, turnSpeed * Time.fixedDeltaTime);
//            player.rotation = Quaternion.Lerp(player.rotation, lookAngle, updatedTurnSpeed * Time.fixedDeltaTime);
//        }
//    }

//    private void AdjustCam()
//    {
//        if (isEnding)
//        {
//            camControl.m_MinOrthoSize = zoomLevel;
//            camControl.m_ScreenEdgeBuffer = 0;

//            Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
//                new Vector3(Camera.main.transform.position.x,
//                endCamHeight + initStartCamHeight, Camera.main.transform.position.z),
//                camSpeed * Time.fixedDeltaTime);

//            Camera.main.nearClipPlane =
//                Mathf.Lerp(Camera.main.nearClipPlane, cinematicClipPlane, cinematicClipPlaneSpeed * Time.fixedDeltaTime);
//        }

//        else
//        {
//              Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position,
//              new Vector3(Camera.main.transform.position.x,
//              startCamHeight, Camera.main.transform.position.z),
//              camSpeed * Time.fixedDeltaTime);
//        }
//    }

//    private void GatherLeaves()
//    {
//        foreach (GameObject leaf in leaves)
//        {
//            leaf.GetComponent<CinematicLeaf>().GatherAnim();
//        }
//    }

//    #endregion
    
//}

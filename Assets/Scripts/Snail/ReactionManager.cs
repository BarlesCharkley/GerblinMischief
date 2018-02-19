//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ReactionManager : MonoBehaviour
//{
//    [SerializeField] private float manualReactTime = 3f;
//    [SerializeField] public Quaternion rotationOffset;
//    [Space]

//    [SerializeField] public GameObject love;
//    [SerializeField] private KeyCode loveTestButton = KeyCode.F;
//    [Space]

//    [SerializeField] public GameObject confuse;
//    [SerializeField] private KeyCode confuseTestButton = KeyCode.Q;
//    [Space]

//    [SerializeField] public GameObject surprise;
//    [SerializeField] private KeyCode surpriseTestButton = KeyCode.E;
//    [Space]

//    [SerializeField] public GameObject shock;
//    [SerializeField] private KeyCode shockTestButton = KeyCode.Alpha3;
//    [Space]

//    [SerializeField] public KeyCode clearReactions = KeyCode.Backslash;

//    private SnailMove snailMove;
//    private CinematicsHandler cinematics;
//    [HideInInspector] public bool isReacting;
//    [HideInInspector] public bool timedReaction;
//    [HideInInspector] public float reactTime = 3f;

//    private void Start()
//    {
//        snailMove = FindObjectOfType<SnailMove>();
//        cinematics = FindObjectOfType<CinematicsHandler>();
//    }

//    public void React(GameObject emotion, bool timed, float timerDuration)
//    {
//        if (!snailMove.isInCinematic && !isReacting)
//        {
//            timedReaction = timed;
//            reactTime = timerDuration;
//            emotion.SetActive(true);
//            Debug.Log("He " + emotion.name + "!!!");
//        }
//    }

//    public void Unreact(GameObject emotion)
//    {
//        if (isReacting)
//        {
//            emotion.GetComponent<Reaction>().Unreact();
//        }
//    }

//    private void Update()
//    {
//        if (snailMove.canControl && !cinematics.titleIsUp)
//        {
//            if (Input.GetKeyDown(loveTestButton))
//            {
//                React(love, true, manualReactTime);
//            }

//            if (Input.GetKeyDown(confuseTestButton))
//            {
//                React(confuse, true, manualReactTime);
//            }

//            if (Input.GetKeyDown(surpriseTestButton))
//            {
//                React(surprise, true, manualReactTime);
//            }

//            if (Input.GetKeyDown(shockTestButton))
//            {
//                React(shock, true, manualReactTime);
//            }
//        }
//    }
//}

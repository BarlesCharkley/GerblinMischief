//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Reaction : MonoBehaviour
//{
//    private Animator anim;
//    private ReactionManager manager;
//    private SnailMove snailMove;
//    private Transform rotator;

//    private void Awake()
//    {
//        anim = GetComponent<Animator>();
//        manager = FindObjectOfType<ReactionManager>();
//        snailMove = FindObjectOfType<SnailMove>();
//        rotator = GameObject.Find(gameObject.name + "Rotator").transform;
//    }

//    private void OnEnable()
//    {
//        anim.SetBool("React", true);

//        if (!snailMove.isInCinematic)
//        {
//            manager.isReacting = true;

//            if (manager.timedReaction)
//            {
//                StartCoroutine(DelayedUnreact());
//            }
//        }
//    }

//    private void Update()
//    {
//        if (Input.GetKeyDown(manager.clearReactions))
//        {
//            Unreact();
//        }
//    }

//    private void FixedUpdate()
//    {
//        FaceCamera();

//        if (snailMove.isInCinematic && manager.isReacting)
//        {
//            Unreact();
//        }
//    }

//    private void FaceCamera()
//    {
//        rotator.rotation = manager.rotationOffset;
//    }

//    public void DisableSelf()
//    {
//        manager.isReacting = false;
//        gameObject.SetActive(false);
//    }

//    private IEnumerator DelayedUnreact()
//    {
//        yield return new WaitForSeconds(manager.reactTime);
//        Unreact();
//    }

//    public void Unreact()
//    {
//        anim.SetBool("React", false);
//    }
//}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Serialization;

//public class GrassFollow : MonoBehaviour
//{
//    [SerializeField] private Vector3 offset;
//    [SerializeField] private float grassAngleOffset;

//    private SnailMove snail;

//    public float grassAngle;
//    public float grassSpeed;

//    private void Start()
//    {
//        snail = FindObjectOfType<SnailMove>();
//    }

//    private void FixedUpdate()
//    {
//        Follow();
//        UpdateGrass();
//    }

//    private void Follow()
//    {
//        Vector3 targetPos = snail.transform.position;
//        gameObject.transform.position = new Vector3((targetPos.x + offset.x), (targetPos.y + offset.y), (targetPos.z + offset.z));
//    }

//    private void UpdateGrass()
//    {
//        grassAngle = snail.transform.eulerAngles.y + grassAngleOffset;
//        grassSpeed = -snail.updatedSpeed;
//    }
//}

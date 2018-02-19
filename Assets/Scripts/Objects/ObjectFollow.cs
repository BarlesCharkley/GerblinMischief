using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFollow : MonoBehaviour
{
    [SerializeField]Transform target;
    [SerializeField] private Vector3 offset;

    void FixedUpdate()
    {
        Follow();
    }

    void Follow()
    {
        Vector3 targetPos = target.position;
        gameObject.transform.position = new Vector3((targetPos.x + offset.x), (targetPos.y + offset.y), (targetPos.z + offset.z));
    }
}

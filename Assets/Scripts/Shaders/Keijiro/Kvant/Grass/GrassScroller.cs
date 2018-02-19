//
// Scroller script for Grass
//
using UnityEngine;
using UnityEngine.Serialization;

namespace Kvant
{
    [RequireComponent(typeof(Grass))]
    [AddComponentMenu("Kvant/Grass Scroller")]
    public class GrassScroller : MonoBehaviour
    {
        //private GrassFollow grassFollow;

        # region GrassScroll Parameters
        [SerializeField, FormerlySerializedAs("yawAngle")]
        float _yawAngle;

        public float yawAngle
        {
            get { return _yawAngle; }
            set { _yawAngle = value; }
        }

        [SerializeField, FormerlySerializedAs("speed")]
        float _speed;

        public float speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
#endregion

        //void Start()
        //{
        //    grassFollow = GetComponent<GrassFollow>();
        //}

        void Update()
        {
            RegularScroll();
            //SnailScroll();
        }

        void RegularScroll()
        {
            var r = _yawAngle * Mathf.Deg2Rad;
            var dir = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
            GetComponent<Grass>().offset += dir * _speed * Time.deltaTime;
        }

        //void SnailScroll()
        //{
        //    var r = grassFollow.grassAngle * Mathf.Deg2Rad;
        //    var dir = new Vector2(Mathf.Cos(r), Mathf.Sin(r));
        //    GetComponent<Grass>().offset += dir * grassFollow.grassSpeed;
        //}
    }
}

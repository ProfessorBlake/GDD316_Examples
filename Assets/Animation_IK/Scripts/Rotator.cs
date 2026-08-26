using UnityEngine;

namespace Game.AnimationIK
{
    public class Rotator : MonoBehaviour
    {
        [SerializeField] private Vector3 angularVelocity;

        void Update()
        {
            transform.Rotate(angularVelocity * Time.deltaTime);
        }
    }
}

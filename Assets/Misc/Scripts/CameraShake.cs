using Game.AnimationLayers;
using UnityEngine;

namespace Game
{
    public class CameraShake : MonoBehaviour
    {
        [SerializeField] private float shakeDistance;
        [SerializeField] private float shakeSpeed;
        [SerializeField] private float shakeDampen;

        [SerializeField] private float shake;
        private Vector3 shakeVel;

		private void LateUpdate()
		{
            if (shake > 0f)
            {
                transform.localPosition =
                    Vector3.SmoothDamp(
                        transform.localPosition,
                        Random.onUnitSphere * Mathf.Pow(shake, shakeDistance),
                        ref shakeVel,
                        shakeSpeed);

                shake = Mathf.Lerp(shake, 0, shakeDampen * Time.deltaTime);
                if (shake <= 0.01f)
                {   
                    shake = 0;
                }
            }
            else
            {
                transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, Time.deltaTime);
			}
        }
	
        public void Shake(float amnt)
        {
            shake = amnt;
        }
    }
}

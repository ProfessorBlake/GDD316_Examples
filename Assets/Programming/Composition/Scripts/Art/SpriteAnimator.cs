using UnityEngine;

namespace Game
{
	[RequireComponent (typeof (SpriteRenderer))]
    public class SpriteAnimator : MonoBehaviour
    {
        [SerializeField] private Sprite[] sprites;
        [SerializeField] private float frameDelay;

		private int frameIndex;
		private SpriteRenderer sprRend;
		private float delay;

		private void Awake()
		{
			sprRend = GetComponent<SpriteRenderer> ();
		}

		private void Update()
		{
			delay -= Time.deltaTime;
			if (delay < 0)
			{
				delay = frameDelay;
				frameIndex++;
				if(frameIndex >= sprites.Length) frameIndex = 0;
				sprRend.sprite = sprites[frameIndex];
			}
		}
	}
}

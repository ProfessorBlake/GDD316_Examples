using System;
using UnityEngine;

namespace Game.Programming
{
	[RequireComponent(typeof(Rigidbody))]
    public class Ball : MonoBehaviour
    {
		public static Ball Instance => instance;

		public Action OnReset;
		public Action<Vector3> OnMoved;
		public Action<int> OnTriggerCheckpoint;

        [SerializeField] private float power;
		[SerializeField] private Transform cam;
		[SerializeField] private float killHeight = -100;

		[Header("Audio")]
		[SerializeField] private AudioSource audioscource;
		[SerializeField] private AudioClip sfxCheckpoint;
		[SerializeField] private AudioClip sfxFall;

		private static Ball instance;

		private Rigidbody rb;
        private Vector2 input;
		private Vector3 camOffset;
		private Vector3 spawnPoint;

		private void Awake()
		{
			if(instance != null)
			{
				Debug.LogError("Ball:: Instance already exists");
				DestroyImmediate(this.gameObject);
				return;
			}
			instance = this;

			rb = GetComponent<Rigidbody>();
			camOffset = cam.position - transform.position;
			spawnPoint = transform.position;
			OnMoved = null;
		}

		private void Update()
		{
			input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
		}

		private void LateUpdate()
		{
			cam.position = Vector3.Lerp(cam.position, transform.position + camOffset, 10f * Time.deltaTime); 
			OnMoved?.Invoke(rb.position);
		}

		private void FixedUpdate()
		{
			if(transform.position.y < killHeight)
			{
				AudioSource.PlayClipAtPoint(sfxFall, spawnPoint);
				rb.linearVelocity = Vector3.zero;
				rb.angularVelocity = Vector3.zero;
				transform.position = spawnPoint;
				OnReset?.Invoke();
			}

			Vector3 fwd = (transform.position - cam.position).normalized;
			fwd.y = 0;
			Vector3 right = Vector3.Cross(Vector3.up, fwd).normalized;
			rb.AddTorque(((right * input.y) + (fwd * -input.x)).normalized * power);

			Vector3 xzVel = rb.linearVelocity * 0.1f;
			xzVel.y = 0;
			audioscource.volume = Mathf.Lerp(0f, 0.3f,xzVel.sqrMagnitude);
		}

		private void OnTriggerEnter(Collider other)
		{
			if(other.TryGetComponent<Checkpoint>(out Checkpoint checkpoint))
			{
				AudioSource.PlayClipAtPoint(sfxCheckpoint, transform.position);
				spawnPoint = other.transform.position;
				OnTriggerCheckpoint?.Invoke(checkpoint.Id);
				other.enabled = false;
			}
		}

		private void OnDestroy()
		{
			OnMoved = null;
			OnReset = null;
		}
	}
}

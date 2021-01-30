using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempPlayerControl : MonoBehaviour
{

	public float speed;
	public float jump;
	Rigidbody rb;
	public SphereCollider col;
	public LayerMask groundLayers;

	// Use this for initialization
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		col = GetComponent<SphereCollider>();
	}

	///CHECK_IF_GROUNDED
	private bool IsGrounded()
	{
		return Physics.CheckCapsule(col.bounds.center, new Vector3(col.bounds.center.x, col.bounds.min.y, col.bounds.center.z), col.radius * .9f, groundLayers);

	}
	/// ////////////
	// Update is called once per frame
	void Update()
	{

		if (Input.GetKey(KeyCode.W))
		{
			rb.AddForce(Vector3.forward * speed * Time.deltaTime * 50, ForceMode.Acceleration);
		}
		if (Input.GetKey(KeyCode.A))
		{
			rb.AddForce(Vector3.left * speed * Time.deltaTime * 50, ForceMode.Acceleration);
		}
		if (Input.GetKey(KeyCode.S))
		{
			rb.AddForce(Vector3.back * speed * Time.deltaTime * 50, ForceMode.Acceleration);
		}
		if (Input.GetKey(KeyCode.D))
		{
			rb.AddForce(Vector3.right * speed * Time.deltaTime * 50, ForceMode.Acceleration);
		}
		if (IsGrounded() && Input.GetKeyDown(KeyCode.Space))
		{
			rb.AddForce(Vector3.up * jump * Time.deltaTime * 50, ForceMode.Impulse);
		}
	}
}
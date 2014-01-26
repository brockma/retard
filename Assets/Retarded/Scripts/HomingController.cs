using UnityEngine;
using System.Collections;

public class HomingController : MonoBehaviour {
	public GameObject wayPoint;
	public GameObject explosion;
	public GameObject playerExplosion;
	
	public float targetConeWidth = 30;
	public float thrustMaxPower = 3f;
	public float thrustMaxSeconds = 1.0f;
	public float turnMaxPower = 3f;
	public float turnMaxSeconds = 1.0f;


	private float thrustMaxSpeed;
	private float turnMaxSpeed;
	private float lifeTime = 0.0f;	
	public float maxLifeTime = 7.0f;
	
		
//	private float nextFire;

	void Start() {
		thrustMaxSpeed = thrustMaxPower * thrustMaxSeconds;
		turnMaxSpeed = turnMaxPower * turnMaxSeconds;
	}

	void Update() {
	
		lifeTime += Time.deltaTime;
		if (lifeTime > maxLifeTime) {
			Instantiate(explosion, transform.position, transform.rotation);
			Destroy (gameObject);
		}
//		if (Input.GetButton("Fire1") && Time.time > nextFire) {
//			nextFire = Time.time + fireRate;
//			Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
//		}
	}

	void FixedUpdate() {
		// Snap the object to the plane		
//		transform.position.Set(transform.position.x, -5, transform.position.z);
		
		if (wayPoint == null) {
			// No target set. Just accelerate forward
			rigidbody.AddRelativeForce(Vector3.forward * thrustMaxPower);
			return;
		}
	
		// First calculate where we would like to go
		Vector3 targetVec = getTargetVector().normalized * thrustMaxSpeed;
		
		// Compensate for movement not aligned with where we want to go
		Vector3 alignedVec = Vector3.Project(rigidbody.velocity,targetVec);
		Vector3 unalignedVec = rigidbody.velocity - alignedVec;
		targetVec -= unalignedVec;

		// Find the angle to the desired direction and find out whether it is forward or back, left or right
		float targetAngle = Vector3.Angle(rigidbody.transform.forward, targetVec);
		float dotProduct = Vector3.Dot(transform.forward, targetVec);
		float dotRight = Vector3.Dot(transform.right, targetVec);

		// Calculate how hard we should be moving forward and rotating
		float thrustSetting = 0;
		float turnSetting = 0;		
		if (targetAngle < targetConeWidth && dotProduct > 0) {
			// The ship points roughly towards the target. Stop turning and thrust
			turnSetting = targetAngle / targetConeWidth;
			thrustSetting = (1 - targetAngle / targetConeWidth);
		} else {
			// The ship points away from the target. Stop thrusting and turn
			turnSetting = 1;
			thrustSetting = 0;
		}

		// In case turning right would take longer to get to the desired direction, turn the other way
		if (dotRight < 0) {
			turnSetting *= -1;
		}
		
		// Calculate the desired velocity
		Vector3 desiredForwardVelocity = transform.forward * thrustSetting * thrustMaxSpeed;
		// Let's now take the existing thrust into account, and figure out how much we should be thrusting actually
		Vector3 actualForwardVelocity = Vector3.Project(rigidbody.velocity, transform.forward);
		Vector3 desiredForwardVelocityDiff = desiredForwardVelocity - actualForwardVelocity;

		// Accelerate only if we are facing the desired direction
		if (Vector3.Dot(desiredForwardVelocityDiff,transform.forward) > 0) {
			rigidbody.AddRelativeForce(Vector3.forward * thrustMaxPower);
		}
		
		// Calculate the desired turning speed
		Vector3 desiredTurnSpeed = transform.up * turnSetting * turnMaxSpeed;
		// Next take the current turning speed into account
		Vector3 desiredTurnSpeedDiff = desiredTurnSpeed - rigidbody.angularVelocity;
		// Finally turn the ship so it tries to match the desired turning speed
		Vector3 torque = desiredTurnSpeedDiff.normalized * turnMaxPower;
		rigidbody.AddRelativeTorque(torque);
		

	}

	Vector3 getTargetVector() {
		return wayPoint.transform.position - rigidbody.position;
	}
	
	void OnTriggerEnter (Collider other)
	{
		if(lifeTime < 1.0f) return;
		
		Debug.Log("MISSILE TRIGGER)");
		if (other.tag == "Boundary")
		{
			return;
		}
		
		if (other.tag == "Player") {
			if(playerExplosion != null) Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
		} else if(other.tag == "Enemy") {
			if(explosion != null) Instantiate(explosion, other.transform.position, other.transform.rotation);
		}
		
		if(other.tag == "Enemy" || other.tag == "Player") {
			Destroy (other.gameObject);
			Destroy (gameObject);
		}
	}
}

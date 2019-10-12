using UnityEngine;
using UnityEngine.AI;

public class Tower : MonoBehaviour
{
	public enum TurnMode
	{
		AllAxis,
		YawOnly
	}

	[Header("Target Prediction:")]
	public bool useTargetPrediction = true;
	public float targetPredictionStrength = 1.0f;

	[Header("Target Selection:")]
	public string targetTag = "Enemy";
	public float attackRange = 3.0f;            // The maximum attack range before engaging a target.

	[Header("Aiming:")]
	public TurnMode turnMode = TurnMode.AllAxis;
	public float maxTurnRate = 360.0f;      	// max. rotation change in degrees per second.
	public float minFiringAngle = 5.0f;			// min. angle towards target within which we may open fire.
	public float scanInterval = 0.1f;       	// Every how many seconds to scan for targets to shoot at.

	[Header("Firing:")]
	public float projectileSpeed = 5.0f;        // Starting speed of projectiles fired at the target, in meters per second.
	public float fireInterval = 0.5f;           // min. time interval between consecutive shots fired, in seconds.

	private GameObject target = null;
	private NavMeshAgent targetAgent = null;
	private float currentAngle = 0.0f;
	private float targetAngle = 0.0f;
	private float lastScanTime = -1.0f;
	private float lastFireTime = -1.0f;

	public Projectile projectilePrefab = null;
	public Damage damage = Damage.Standard; 	// The damage output per shot.

	public GameObject CurrentTarget => target;

	void Start()
    {
		//...
    }

	void Update()
    {
		// Scan and select targets every so many seconds:
		if(Time.time > lastScanTime + scanInterval)
		{
			lastScanTime = Time.time;

			DetectTargets();
		}

		// Return if no target is available:
		if (target == null) return;

		// Align self with an ideal firing direction:
		RotateTowardsTarget();

		// Fire at the target:
		if (currentAngle < minFiringAngle && Time.time > lastFireTime + fireInterval)
		{
			lastFireTime = Time.time;

			Vector3 velocity = transform.forward * projectileSpeed;
			CombatSystem.Fire(transform.position, velocity, projectilePrefab, damage);
		}
	}

	private void DetectTargets()
	{
		// Find any potential targets in the scene:
		GameObject[] targets = GameObject.FindGameObjectsWithTag(targetTag);
		if(targets == null || targets.Length == 0)
		{
			target = null;
			return;
		}

		// Select the closest enemy to self:
		float attackRangeSq = attackRange * attackRange;
		float bestDistSq = 1.0e+8f;
		GameObject bestTargetGO = null;
		foreach (GameObject go in targets)
		{
			float distSq = Vector3.SqrMagnitude(go.transform.position - transform.position);
			if (distSq < attackRangeSq && distSq < bestDistSq)
			{
				bestTargetGO = go;
				bestDistSq = distSq;
			}
		}

		// Set the selected object as our new target:
		if (target != bestTargetGO) targetAgent = targetAgent.GetComponent<NavMeshAgent>();
		target = bestTargetGO;
	}

	private void RotateTowardsTarget()
	{
		// Get target position and do some basic target prediction based on target's speed:
		Vector3 targetPos = target.transform.position;
		if (useTargetPrediction && targetAgent != null)
		{
			float targetDist = Vector3.Distance(targetPos, transform.position);
			float flightTime = targetDist / projectileSpeed * targetPredictionStrength;

			Vector3 targetOffset = targetAgent.velocity * flightTime;
			targetPos += targetOffset;
		}

		// Get direction to target:
		Vector3 targetDir = (targetPos - transform.position).normalized;
		float maxAngleDiff = maxTurnRate * Time.deltaTime;

		if (turnMode == TurnMode.AllAxis)
		{
			// Calculate and apply rotations along all axis:
			Quaternion targetRotation = Quaternion.LookRotation(targetDir);
			Quaternion prevRotation = transform.rotation;
			transform.rotation = Quaternion.RotateTowards(prevRotation, targetRotation, maxAngleDiff);
			currentAngle = Vector3.Angle(transform.forward, targetDir);
		}
		else if (turnMode == TurnMode.YawOnly)
		{
			// Calculate target angle towards current target:
			targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90;

			// Rotate towards target angle:
			float previousAngle = currentAngle;
			currentAngle = Mathf.MoveTowardsAngle(previousAngle, targetAngle, maxAngleDiff);

			// Apply rotation to transform:
			transform.eulerAngles = new Vector3(0, currentAngle, 0);
		}
	}
}

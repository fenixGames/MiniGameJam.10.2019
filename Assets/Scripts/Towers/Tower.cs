using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
	public string targetTag = "Enemy";
	public float attackRange = 3.0f;			// The maximum attack range before engaging a target.
	public float maxTurnRate = 360.0f;      	// max. rotation change in degrees per second.
	public float minFiringAngle = 5.0f;			// min. angle towards target within which we may open fire.
	public float scanInterval = 0.1f;       	// Every how many seconds to scan for targets to shoot at.
	public float projectileSpeed = 5.0f;		// Starting speed of projectiles fired at the target, in meters per second.

	private GameObject target = null;
	private float currentAngle = 0.0f;
	private float targetAngle = 0.0f;
	private float lastScanTime = -1.0f;

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
			DetectTargets();
			lastScanTime = Time.time;
		}

		// Return if no target is available:
		if (target == null) return;

		// Align self with an ideal firing direction:
		RotateTowardsTarget();

		// Fire at the target:
		if (currentAngle < minFiringAngle)
		{
			Vector3 velocity = transform.right * projectileSpeed;
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

		//Debug.Log($"Found targets: {targets.Length}");

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
		target = bestTargetGO;
	}

	private void RotateTowardsTarget()
	{
		// Calculate target angle towards current target:
		Vector3 targetDir = (target.transform.position - transform.position).normalized;
		targetAngle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg + 90;

		// Rotate towards target angle:
		float previousAngle = currentAngle;
		float maxAngleDiff = maxTurnRate * Time.deltaTime;
		currentAngle = Mathf.MoveTowardsAngle(previousAngle, targetAngle, maxAngleDiff);

		// Apply rotation to transform:
		transform.eulerAngles = new Vector3(0, currentAngle, 0);
	}
}

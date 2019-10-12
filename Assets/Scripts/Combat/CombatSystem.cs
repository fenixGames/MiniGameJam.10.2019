using System;
using UnityEngine;

public static class CombatSystem
{
	public static readonly string damageMessageName = "ApplyDamage";

	public static void Fire(Vector3 origin, Vector3 velocity, Projectile projectilePrefab, Damage damage)
	{
		if(projectilePrefab == null)
		{
			Debug.LogError("Error! Projectile prefab may not be null!");
			return;
		}

		// Spawn a new projectile from prefab:
		Projectile projectile = UnityEngine.Object.Instantiate(projectilePrefab, origin, Quaternion.identity);

		// Transmit firing parameters to newly spawned projectile:
		projectile.Launch(velocity, damage);
	}
}

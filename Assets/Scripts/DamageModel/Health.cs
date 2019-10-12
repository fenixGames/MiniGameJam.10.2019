using System;
using UnityEngine;

public class Health : MonoBehaviour
{
	public float hitpoints = 1.0f;
	public DamageType damageResistances = 0;
	public DamageType damageImmunities = 0;
	public DamageType damageWeaknesses = 0;

	public const float resistanceDamageModifier = 0.5f;
	public const float weaknessDamageModifier = 2.0f;

	public void ApplyDamage(Damage damage)
	{
		// Deal no damage if the target is immune:
		if ((damage.type & damageImmunities) != 0) return;

		float amount = damage.amount;
		// Do half damage if target has resistence against this damage type:
		if ((damage.type & damageResistances) != 0)
		{
			amount *= resistanceDamageModifier;
		}
		// Do double damage if target is weak against this damage type:
		if ((damage.type & damageWeaknesses) != 0)
		{
			amount *= weaknessDamageModifier;
		}
		hitpoints -= amount;

		if (hitpoints <= 0)
		{
			// TODO: Update score/mana/resource logic here...

			gameObject.SetActive(false);
			Destroy(gameObject);
		}
	}
}

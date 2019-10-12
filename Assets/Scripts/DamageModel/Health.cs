using System;

[Serializable]
public class Health
{
	public Health(float _hitpoints, DamageType _dmgResistances, DamageType _dmgImmunities)
	{
		hitpoints = _hitpoints;
		damageResistances = _dmgResistances;
		damageImmunities = _dmgImmunities;
	}

	public float hitpoints = 1.0f;
	public DamageType damageResistances = DamageType.None;
	public DamageType damageImmunities = DamageType.None;

	public const float resistanceDamageModifier = 0.5f;

	public void ApplyDamage(Damage damage)
	{
		bool isImmune = (damage.type & damageImmunities) != 0;
		if (isImmune)
		{
			return;
		}

		bool isResistant = (damage.type & damageResistances) != 0;
		float amount = isResistant ? damage.amount * resistanceDamageModifier : damage.amount;
		hitpoints -= amount;

		if (hitpoints <= 0)
		{
			// TODO: Either destroy the gameObject directly or call some callback on the host type and let it do the killing.
		}
	}
}

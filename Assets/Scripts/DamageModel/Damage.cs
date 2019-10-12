using System;

[Serializable]
public struct Damage
{
	public Damage(float _amount, DamageType _type = DamageType.Normal)
	{
		type = _type;
		amount = _amount;
	}

	public DamageType type;
	public float amount;

	public static Damage Standard => new Damage(1.0f, DamageType.Normal);
}

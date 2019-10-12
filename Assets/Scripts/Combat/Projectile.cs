using UnityEngine;

public class Projectile : MonoBehaviour
{
	public Damage damage = Damage.Standard;
	public Rigidbody rig = null;
	public GameObject impactFx = null;

	public void Launch(Vector3 _velocity, Damage _damage)
	{
		// Set the damage:
		damage = _damage;

		// Align projectile with launch velocity:
		transform.rotation = Quaternion.LookRotation(_velocity);

		// Apply launch velocity to rigidbody:
		if (rig == null) rig = GetComponent<Rigidbody>();
		rig.velocity = _velocity;
	}

	private void OnCollisionEnter(Collision collision)
	{
		// Deal damage to the target:
		collision.gameObject.SendMessage(CombatSystem.damageMessageName, damage, SendMessageOptions.DontRequireReceiver);

		// Optionally spawn a fancy impact FX at the first point of contact:
		if (impactFx != null)
		{
			ContactPoint contact = collision.contacts[0];
			Instantiate<GameObject>(impactFx, contact.point, Quaternion.LookRotation(contact.normal));
		}

		// Wipe self from existence:
		Destroy(gameObject);
	}
}

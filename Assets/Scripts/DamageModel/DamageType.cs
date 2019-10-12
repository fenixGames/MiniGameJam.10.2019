using System;
using UnityEngine;

[Flags]
public enum DamageType
{
    Normal		= 0x01,		// Standard physical damage type.

	// TODO: Add more damage types once they've been decided upon by game design folks.

	None		= 0
}

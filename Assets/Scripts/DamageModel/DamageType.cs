using System;
using UnityEngine;

[Flags]
public enum DamageType
{
    Normal		= 0x01,		// Standard physical damage type.

	Lightning	= 0x02,

	// TODO: Add more damage types once they've been decided upon by game design folks.
}

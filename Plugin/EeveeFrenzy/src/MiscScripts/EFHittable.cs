using GameNetcodeStuff;
using Unity.Netcode;
using UnityEngine;

namespace EeveeFrenzy.src.MiscScripts;
public abstract class EFHittable : NetworkBehaviour
{
	public abstract bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null!,
							 bool playHitSFX = false,
							 int hitID = -1);
}
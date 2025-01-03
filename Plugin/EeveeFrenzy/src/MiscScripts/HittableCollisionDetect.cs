using GameNetcodeStuff;
using UnityEngine;

namespace EeveeFrenzy.src.MiscScripts;
public class HittableCollisionDetect : MonoBehaviour, IHittable
{
	[SerializeField]
	private EFHittable _mainScript = null!;
	
	public bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null!, bool playHitSFX = false,
					int hitID = -1) {
		return _mainScript.Hit(force, hitDirection, playerWhoHit, playHitSFX, hitID);
	}
}
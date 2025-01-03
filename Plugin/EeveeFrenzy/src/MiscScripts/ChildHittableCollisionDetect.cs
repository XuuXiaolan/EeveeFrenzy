using EeveeFrenzy.src.Content.Items;
using GameNetcodeStuff;
using UnityEngine;

namespace EeveeFrenzy.src.MiscScripts;
public class ChildHittableCollisionDetect : MonoBehaviour, IHittable
{
	[SerializeField]
	private ChildEnemyAI _mainScript = null!;
	
	public bool Hit(int force, Vector3 hitDirection, PlayerControllerB playerWhoHit = null!, bool playHitSFX = false,
					int hitID = -1) {
		return _mainScript.Hit(force, hitDirection, playerWhoHit, playHitSFX, hitID);
	}
}
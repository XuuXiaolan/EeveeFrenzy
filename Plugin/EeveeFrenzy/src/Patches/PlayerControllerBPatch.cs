using System.Collections.Generic;
using EeveeFrenzy.src.Content.Enemies;
using EeveeFrenzy.src.Content.Items;
using EeveeFrenzy.src.MiscScripts;
using GameNetcodeStuff;
using UnityEngine;

namespace EeveeFrenzy.src.Patches;
static class PlayerControllerBPatch
{
    public static List<SmartAgentNavigator> smartAgentNavigators = new();

    public static void Init()
    {
        On.GameNetcodeStuff.PlayerControllerB.TeleportPlayer += PlayerControllerB_TeleportPlayer;
        On.GameNetcodeStuff.PlayerControllerB.DamagePlayer += PlayerControllerB_DamagePlayer;
    }

    private static void PlayerControllerB_DamagePlayer(On.GameNetcodeStuff.PlayerControllerB.orig_DamagePlayer orig, PlayerControllerB self, int damageNumber, bool hasDamageSFX, bool callRPC, CauseOfDeath causeOfDeath, int deathAnimation, bool fallDamage, Vector3 force)
    {
        orig(self, damageNumber, hasDamageSFX, callRPC, causeOfDeath, deathAnimation, fallDamage, force);
        Plugin.ExtendedLogging($"PlayerControllerB_DamagePlayer called on client: {self.playerUsername} with caller: {self.playerUsername}");
        if (self.currentlyHeldObjectServer is ChildEnemyAI childEnemyAI && self == GameNetworkManager.Instance.localPlayerController)
        {
            self.StartCoroutine(self.waitToEndOfFrameToDiscard());
            if (childEnemyAI.mommyAlive && childEnemyAI.parentEevee != null)
            {
                childEnemyAI.parentEevee.HandleStateAnimationSpeedChangesServerRpc((int)ParentEnemyAI.State.Guarding);
                childEnemyAI.parentEevee.SetTargetServerRpc(-1);
            }
        }
    }

    private static void PlayerControllerB_TeleportPlayer(On.GameNetcodeStuff.PlayerControllerB.orig_TeleportPlayer orig, PlayerControllerB self, Vector3 pos, bool withRotation, float rot, bool allowInteractTrigger, bool enableController)
    {
        foreach (var navigator in smartAgentNavigators)
        {
            Plugin.ExtendedLogging($"Setting SmartAgentNavigator.positionsOfPlayersBeforeTeleport[self] to {self.transform.position}");
            navigator.positionsOfPlayersBeforeTeleport[self] = self.transform.position;
        }
        orig(self, pos, withRotation, rot, allowInteractTrigger, enableController);
    }
}
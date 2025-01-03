using HarmonyLib;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using EeveeFrenzy.src.Util;
using EeveeFrenzy.src.Util.Extensions;

namespace EeveeFrenzy.src.Patches;
[HarmonyPatch(typeof(StartOfRound))]
static class StartOfRoundPatch
{
	[HarmonyPatch(nameof(StartOfRound.Awake))]
	[HarmonyPostfix]
	public static void StartOfRound_Awake(ref StartOfRound __instance)
	{
		Plugin.ExtendedLogging("StartOfRound.Awake");
		__instance.NetworkObject.OnSpawn(CreateNetworkManager);
	}
	
	private static void CreateNetworkManager()
	{
		if (StartOfRound.Instance.IsServer || StartOfRound.Instance.IsHost)
		{
			if (EeveeFrenzyUtils.Instance == null)
			{
				GameObject utilsInstance = GameObject.Instantiate(Plugin.Assets.UtilsPrefab);
				SceneManager.MoveGameObjectToScene(utilsInstance, StartOfRound.Instance.gameObject.scene);
				utilsInstance.GetComponent<NetworkObject>().Spawn();
				Plugin.ExtendedLogging($"Created EeveeFrenzyUtils. Scene is: '{utilsInstance.scene.name}'");
			}
			else
			{
				Plugin.Logger.LogWarning("EeveeFrenzyUtils already exists?");
			}
		}
	}
}
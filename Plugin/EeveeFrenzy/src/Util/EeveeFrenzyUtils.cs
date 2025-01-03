using System;
using System.Collections;
using EeveeFrenzy.src.Content.Enemies;
using Unity.Netcode;
using UnityEngine;
using Random = System.Random;
using EeveeFrenzy.src.Util.Extensions;

namespace EeveeFrenzy.src.Util;
internal class EeveeFrenzyUtils : NetworkBehaviour
{
    private static Random random = null!;
    internal static EeveeFrenzyUtils Instance { get; private set; } = null!;

    private void Awake()
    {
        Instance = this;
    }

    [ServerRpc(RequireOwnership = false)]
    public void SpawnScrapServerRpc(string itemName, Vector3 position, bool defaultRotation = true, int valueIncrease = 0)
    {
        if (itemName == string.Empty)
        {
            return;
        }
        Plugin.samplePrefabs.TryGetValue(itemName, out Item item);
        if (item == null)
        {
            // throw for stacktrace
            Plugin.Logger.LogError($"'{itemName}' either isn't a EeveeFrenzy scrap or not registered! This method only handles EeveeFrenzy scrap!");
            return;
        }
        SpawnScrap(item, position, defaultRotation, valueIncrease);
    }

    public NetworkObjectReference SpawnScrap(Item item, Vector3 position, bool defaultRotation, int valueIncrease)
    {
        if (StartOfRound.Instance == null)
        {
            return default;
        }
        
        random ??= new Random(StartOfRound.Instance.randomMapSeed + 85);

        Transform? parent = null;
        if (parent == null)
        {
            parent = StartOfRound.Instance.propsContainer;
        }
        GameObject go = Instantiate(item.spawnPrefab, position + Vector3.up * 0.2f, defaultRotation == true ? Quaternion.Euler(item.restingRotation) : Quaternion.identity, parent);
        go.GetComponent<NetworkObject>().Spawn();
        int value = (int)(random.NextInt(item.minValue, item.maxValue) * RoundManager.Instance.scrapValueMultiplier) + valueIncrease;
        var scanNode = go.GetComponentInChildren<ScanNodeProperties>();
        scanNode.scrapValue = value;
        scanNode.subText = $"Value: ${value}";
        go.GetComponent<GrabbableObject>().scrapValue = value;
        UpdateScanNodeClientRpc(new NetworkObjectReference(go), value);
        return new NetworkObjectReference(go);
    }

    [ClientRpc]
    public void UpdateScanNodeClientRpc(NetworkObjectReference go, int value)
    {
        go.TryGet(out NetworkObject netObj);
        if(netObj != null)
        {
            if (netObj.gameObject.TryGetComponent(out GrabbableObject grabbableObject))
            {
                grabbableObject.SetScrapValue(value);
                Plugin.ExtendedLogging($"Scrap Value: {value}");
            }
        }
    }
}
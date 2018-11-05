using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkRift.Client.Unity;
using DarkRift;
using DarkRift.Client;

public class PlayerSpawner : MonoBehaviour
{
    const byte SPAWN_TAG = 0;

    [SerializeField]
    [Tooltip("The DarkRift client to communicate on.")]
    UnityClient client;

    [SerializeField]
    [Tooltip("The controllable player prefab.")]
    GameObject controllablePrefab;

    [SerializeField]
    [Tooltip("The network controllable player prefab.")]
    GameObject networkPrefab;

	[SerializeField]
	[Tooltip("The network player manager.")]
	NetworkPlayerManager networkPlayerManager;

    void Awake()
    {
        if (client == null)
        {
            Debug.LogError("Client unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (controllablePrefab == null)
        {
            Debug.LogError("Controllable Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        if (networkPrefab == null)
        {
            Debug.LogError("Network Prefab unassigned in PlayerSpawner.");
            Application.Quit();
        }

        client.MessageReceived += MessageReceived;
    }

	void SpawnPlayer(object sender, MessageReceivedEventArgs e)
	{
		using (Message message = e.GetMessage())
		using (DarkRiftReader reader = message.GetReader())
		{
			if (message.Tag == Tags.SpawnPlayerTag)
			{
				if (reader.Length % 14 != 0)
				{
					Debug.LogWarning("Received malformed spawn packet.");
					return;
				}

				while (reader.Position < reader.Length)
				{
					ushort id = reader.ReadUInt16();
					Vector3 position = new Vector3(reader.ReadSingle(), reader.ReadSingle(), reader.ReadSingle());

					GameObject obj;
					if (id == client.ID)
					{
						obj = Instantiate(controllablePrefab, position, Quaternion.identity) as GameObject;

						Player player = obj.GetComponent<Player>();
						player.Client = client;
					}
					else
					{
						obj = Instantiate(networkPrefab, position, Quaternion.identity) as GameObject;
					}

					PlayerObject playerObj = obj.GetComponent<PlayerObject>();
					networkPlayerManager.Add(id, playerObj);
				}
			}
		}
	}

	void MessageReceived(object sender, MessageReceivedEventArgs e)
	{
		using (Message message = e.GetMessage() as Message)
		{
			if (message.Tag == Tags.SpawnPlayerTag)
				SpawnPlayer(sender, e);
			else if (message.Tag == Tags.DespawnPlayerTag)
				DespawnPlayer(sender, e);
		}
	}

	void DespawnPlayer(object sender, MessageReceivedEventArgs e)
	{
		using (Message message = e.GetMessage())
		using (DarkRiftReader reader = message.GetReader())
			networkPlayerManager.DestroyPlayer(reader.ReadUInt16());
	}
}

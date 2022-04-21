using RiptideNetworking;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Dictionary<ushort, Player> list = new Dictionary<ushort, Player>();

    public ushort Id { get; private set; }
    public string Username { get; private set; }

    private void OnDestroy()
    {
        list.Remove(Id);
        
    }

    public static void Spawn(ushort id, string username)
    {
        Player player = Instantiate(GameLogic.Singelton.PlayerPrefab, new Vector3(0f, 1f, 0f), Quaternion.identity).GetComponent<Player>();
        player.name = $"Player {id}({(string.IsNullOrEmpty(username) ? "Guest" : username)}";
        player.Id = id;
        player.Username = string.IsNullOrEmpty(username)? $"Guest { id}" : username;
        Debug.Log("Player Spawned");
        list.Add(id, player);
    }



    [MessageHandler((ushort)CliendToServerId.name)]

    public static void Name(ushort fromClientId, Message message)
    {
        Spawn(fromClientId, message.GetString());
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

/// <summary>
/// Gameシーンに切り替わったタイミングでプレイヤーを生成する
/// </summary>
public class Game : NetworkBehaviour
{
    //プレイヤーのプレハブ
    [SerializeField] private NetworkObject m_playerPrefab;

    public override void OnNetworkSpawn()
    {
        //ホスト以外の場合
        if (IsHost == false) { return; }

        //クライアント接続時
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;

        //すでに存在するクライアント用に関数呼び出す
        foreach (var client in NetworkManager.Singleton.ConnectedClientsList)
        {
            OnClientConnected(client.ClientId);
        }
    }

    public void OnClientConnected(ulong clientId) //OnClientConnectedの引数はクライアントのＩＤ
    {
        //プレイヤーオブジェクト生成
        var generatePos = new Vector3(0, 1, -8);
        generatePos.x = -5 + 5 * (NetworkManager.Singleton.ConnectedClients.Count % 3);
        NetworkObject playerObject = Instantiate(m_playerPrefab, generatePos, Quaternion.identity);
        //接続クライアントをOwnerにしてPlayerObjectとしてスポーン
        playerObject.SpawnAsPlayerObject(clientId);
    }
}

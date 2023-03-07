using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class CoinManager : NetworkBehaviour
{
    //コインのプレハブ
    [SerializeField] private NetworkObject m_coinPrefab;
    //生成したコインのリスト
    private List<GameObject> m_coinObjects = new List<GameObject>();

    //スポーンされたとき
    public override void OnNetworkSpawn()
    {
        //ホストの場合
        if (IsHost)
        {
            GenerateCoin();
        }
    }

    // コイン生成
    public void GenerateCoin()
    {
        //コイン生成
        for (int x = 0; x < 10; x++)
        {
            NetworkObject coin = Instantiate(m_coinPrefab);
            int posX = UnityEngine.Random.Range(0, 10) - 5;
            int posZ = UnityEngine.Random.Range(0, 10) - 5;
            coin.transform.position = new Vector3(posX, 0, posZ);
            coin.Spawn();
            m_coinObjects.Add(coin.gameObject);
        }
    }

    // コイン削除（ホストから呼び出される）
    public void DeleteCoin(GameObject coinObj)
    {
        var network = coinObj.GetComponent<NetworkObject>();
        network.Despawn();
        m_coinObjects.Remove(coinObj);
        //全部消えたらコイン再生成
        if (m_coinObjects.Count == 0)
        {
            GenerateCoin();
        }
    }

    //簡易的なシングルトン
    private static CoinManager instance;
    public static CoinManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = (CoinManager)FindObjectOfType(typeof(CoinManager));
            }

            return instance;
        }
    }
}

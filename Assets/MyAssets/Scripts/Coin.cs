using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Coin : NetworkBehaviour
{
    private void Update()
    {
        if (IsServer)
        {
            transform.Rotate(Vector3.up, Space.World);
        }
    }
}

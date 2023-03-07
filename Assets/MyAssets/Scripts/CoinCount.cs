using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CoinCount : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;
    private Transform m_target;

    private void Update()
    {
        //ターゲットがいなくなっていたら削除する
        if (m_target == null)
        {
            Destroy(gameObject);
            return;
        }

        //ワールド座標をスクリーン座標に変換
        transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, m_target.position + Vector3.up);
    }

    //数値設定
    public void SetNumber(int count)
    {
        m_text.text = count.ToString();
    }


    //表示する対象（プレイヤー）を設定
    public void SetTarget(Transform target)
    {
        m_target = target;
    }
}


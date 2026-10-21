using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<KeyType> keyItems = new();     //  取得済みの鍵

    private void OnEnable()
    {
        //  GameObjectが有効になったらKeyクラスのイベントを登録
        Key.OnKeyCollected += AddKey;
    }

    private void OnDisable()
    {
        //  GameObjectが無効になったらKeyクラスのイベントを削除
        Key.OnKeyCollected -= AddKey;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //  鍵の取得
    public void AddKey(KeyType key)
    {
        keyItems.Add(key);
        Debug.Log("Key added: " + key);
    }

    //  該当する種類の鍵を持っているか調べる
    public bool HasKey(KeyType key)
    {
        return keyItems.Contains(key);
    }
}

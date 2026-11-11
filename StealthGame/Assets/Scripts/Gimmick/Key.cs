using System;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private KeyType keyType;

    //  イベント宣言
    public static event Action<KeyType> OnKeyCollected;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        //  接触したGameObjectがPlayerである場合、Keyを消す
        if (other.CompareTag("Player"))
        {
            OnKeyCollected?.Invoke(keyType);    //  イベントの呼び出し
            Destroy(gameObject);
        }
    }
}

using System;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private KeyType keyType;

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            var inventory = other.gameObject.GetComponent<PlayerInventory>();
            if (inventory.HasKey(keyType))
            {
                Open();
            }
        }
    }

    //  î‡ÇäJÇØÇÈ
    private void Open()
    {
        Destroy(gameObject);    //  ç°âÒÇÕè¡Ç∑ÇÃÇ›
    }
}

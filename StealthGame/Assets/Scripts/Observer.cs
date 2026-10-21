using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StealthGame
{
    public class Observer : MonoBehaviour
    {
        private Transform player;
        private bool m_IsPlayerInRange;

        private void Start()
        {
            //  シーン上にいるプレイヤーを探して登録する
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            m_IsPlayerInRange = false;
        }

        private void Update()
        {
            //  プレイヤーが索敵範囲内にいる
            if (m_IsPlayerInRange)
            {
                //  索敵用Raycastを準備
                Vector3 direction = player.position - transform.position + Vector3.up;  //  基準点より上からRayを飛ばす（＋Vector3.Up）
                Ray ray = new Ray(transform.position, direction);
                RaycastHit raycastHit;

                //  直線上にPlayerがいる場合、Playerを発見したことにする
                if (Physics.Raycast(ray, out raycastHit))
                {
                    if (raycastHit.collider.CompareTag("Player"))
                    {
                        Debug.Log("Playerを発見！");
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //  索敵範囲内にPlayerが入った
            if (other.CompareTag("Player"))
            {
                m_IsPlayerInRange = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            //  索敵範囲内からPlayerが出た
            if (other.CompareTag("Player"))
            {
                m_IsPlayerInRange = false;
            }
        }

    }
}
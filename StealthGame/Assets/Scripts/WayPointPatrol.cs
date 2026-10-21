using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace StealthGame
{
    public class WaypointPatrol : MonoBehaviour
    {
        [SerializeField] private float moveSpeed;               //  移動速度(unit/sec)
        [SerializeField] private List<Transform> waypoints;     //  WayPointリスト

        private Rigidbody m_RigidBody;      //  物理演算
        int m_CurrentWaypointIndex;         //  巡回中のWayPointIndex

        void Start()
        {
            m_RigidBody = GetComponent<Rigidbody>();

            //  開始位置をWayPointリストの最初のWayPointに設定し、次の巡回先を[1]に設定する
            transform.position = waypoints[0].position;
            m_CurrentWaypointIndex = 1;
        }

        //  物理演算による移動なので、[FixedUpdate]で処理する
        void FixedUpdate()
        {
            Transform currentWaypoint = waypoints[m_CurrentWaypointIndex];              //  現在向かっているWaypoint
            Vector3 currentToTarget = currentWaypoint.position - m_RigidBody.position;  //  現時点の移動ベクトル

            //  WayPointに近づいた場合、次のWayPointへ移動する
            if (currentToTarget.magnitude < 0.1f)
            {
                m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Count;
            }

            //  向かっているWaypointに向く
            Quaternion forwardRotation = Quaternion.LookRotation(currentToTarget);
            m_RigidBody.MoveRotation(forwardRotation);

            //  Waypointに向かって移動する
            m_RigidBody.MovePosition(m_RigidBody.position + currentToTarget.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
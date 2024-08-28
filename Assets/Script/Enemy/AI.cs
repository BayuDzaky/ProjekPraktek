using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    public GameObject player;
    public Transform[] waypoints;
    public float deg;
    public float jarakBerhenti = 2.0f; // Jarak di mana AI berhenti mengejar
    private NavMeshAgent agent;
    private int currentWaypointIndex = 0;
    private bool sedangMengejar = false;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        PergiKeWaypointBerikutnya();
    }

    void Update()
    {
        Vector3 arah = player.transform.position - transform.position;
        float sudut = Vector3.Angle(transform.forward, arah);

        // Periksa apakah pemain dalam garis pandang
        bool pemainDalamPandangan = PeriksaGarisPandang();

        // Jika AI dapat melihat pemain dan dalam sudut yang ditentukan, kejar pemain
        if (pemainDalamPandangan && Mathf.Abs(sudut) < deg)
        {
            float jarakKePemain = Vector3.Distance(transform.position, player.transform.position);

            if (jarakKePemain > jarakBerhenti)
            {
                sedangMengejar = true;
                agent.SetDestination(player.transform.position);
            }
            else
            {
                Debug.Log("Anjing Lu");
                agent.ResetPath(); // Berhenti jika sudah dekat
            }
        }
        else if (sedangMengejar)
        {
            // Berhenti mengejar jika pemain keluar dari jangkauan atau tidak terlihat
            sedangMengejar = false;
            PergiKeWaypointBerikutnya();
        }

        // Periksa apakah AI sudah mencapai tujuan saat ini
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            if (!sedangMengejar)
            {
                PergiKeWaypointBerikutnya();
            }
        }
    }

    void PergiKeWaypointBerikutnya()
    {
        if (waypoints.Length == 0)
            return;

        agent.SetDestination(waypoints[currentWaypointIndex].position);
        currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
    }

    bool PeriksaGarisPandang()
    {
        RaycastHit hit;
        Vector3 arahKePemain = player.transform.position - transform.position;

        if (Physics.Raycast(transform.position, arahKePemain, out hit))
        {
            // Periksa apakah raycast mengenai pemain
            if (hit.transform == player.transform)
            {
                return true; // Pemain terlihat
            }
        }

        return false; // Pemain tidak terlihat
    }
}

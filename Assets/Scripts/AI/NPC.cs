using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour
{
    private NavMeshAgent m_Agent;
    public Transform m_target;

    public EnvQuerySys m_EnvQuerySys;

    public List<Vector3> points = new List<Vector3>();

    [Header("State Machine Data")]
    [SerializeField] private SM_Settings SM_Info;

    // Start is called before the first frame update
    void Start()
    {
        m_Agent = GetComponent<NavMeshAgent>();



        Dictionary<Vector3, bool> tempQuery = m_EnvQuerySys.HardQueryPosts();


        foreach (var q in tempQuery)
        {
            if (q.Value)
            {
                points.Add(q.Key);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_Agent.hasPath)
            ChooseLocation();

        if (m_Agent.remainingDistance < 2.0f)
            ChooseLocation();

    }

    void ChooseLocation()
    {
        //points = m_PostSelector.QueryWalkablePosts();

        print("Count: " + points.Count);
        int rnd = Random.Range(0, points.Count - 1);
        print("Rnd Value: " + rnd);
        //m_Agent.destination = points[rnd];
        m_Agent.SetDestination(points[rnd]);
    }


    void EQS()
    {

    }
}

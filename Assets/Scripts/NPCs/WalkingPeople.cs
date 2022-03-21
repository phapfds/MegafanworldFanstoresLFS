using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class WalkingPeople : MonoBehaviour
{
    [SerializeField] protected List<Transform> destinations;
    [SerializeField] protected Transform currentDes;
    [SerializeField] protected NavMeshAgent navMeshAgent;
    private float timeToChangeDes;
    private void Awake()
    {
        OnAwake();
    }
    public virtual void OnAwake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        navMeshAgent.SetDestination(currentDes.position);
    }
    private void Update()
    {
        timeToChangeDes += Time.deltaTime;
        OnUpdate();
        if (navMeshAgent.remainingDistance < 1f && timeToChangeDes >= 5f)
        {
            currentDes = currentDes == destinations[0] ? destinations[1] : destinations[0];
            navMeshAgent.SetDestination(currentDes.position);
            timeToChangeDes = 0;
        }
    }
    public virtual void OnUpdate()
    {

    }
    private void OnEnable()
    {
        _OnEnable();
    }
    public virtual void _OnEnable()
    {

    }
}

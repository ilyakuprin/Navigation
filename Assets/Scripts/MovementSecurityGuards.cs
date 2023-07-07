using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovementSecurityGuards : MonoBehaviour
{
    [SerializeField] private GameObject[] _visitors;

    private System.Random _random;
    private NavMeshAgent _npcSecurityGuards;
    private Vector3 _target;
    private float _completionDistance = 6f;

    private void Start()
    {
        _random = new System.Random();
        _npcSecurityGuards = GetComponent<NavMeshAgent>();
        StartCoroutine(LocationSelection());
    }

    private IEnumerator LocationSelection()
    {
        int index = _random.Next(0, VisitorsController.Visitors.Count);
        _target = VisitorsController.Visitors[index].transform.position;

        yield return _npcSecurityGuards.destination = _target;

        while ((_target.x - transform.position.x) * (_target.x - transform.position.x) + 
            (_target.z - transform.position.z) * (_target.z - transform.position.z) >
            _completionDistance * _completionDistance)
        {
            _npcSecurityGuards.destination = _target;
            _target = VisitorsController.Visitors[index].transform.position;
            yield return null;
        }

        yield return StartCoroutine(LocationSelection());
    }
}

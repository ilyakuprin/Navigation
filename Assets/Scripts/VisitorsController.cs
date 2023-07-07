using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorsController : MonoBehaviour
{
    public static List<GameObject> Visitors = new List<GameObject>();

    [SerializeField] private GameObject[] _visitors;
    private int _countVisitors;
    private float _occurrenceInterval = 2f;

    private void Start()
    {
        StartCoroutine(CreateVisitor());

        _countVisitors = _visitors.Length;
    }

    private IEnumerator CreateVisitor()
    {
        _visitors[_countVisitors].SetActive(true);
        Visitors.Add(_visitors[_countVisitors]);
        yield return new WaitForSeconds(_occurrenceInterval);

        _countVisitors--;

        if (_countVisitors > -1)
        {
            StartCoroutine(CreateVisitor());
        }

        yield return null;
    }
}

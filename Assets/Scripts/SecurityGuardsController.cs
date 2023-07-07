using System.Collections;
using UnityEngine;

public class SecurityGuardsController : MonoBehaviour
{
    [SerializeField] private GameObject[] _securityGuards;
    private int _countSecurityGuards;
    private float _occurrenceInterval = 3f;

    private void Start()
    {
        StartCoroutine(CreateVisitor());

        _countSecurityGuards = _securityGuards.Length - 1;
    }

    private IEnumerator CreateVisitor()
    {
        yield return new WaitForSeconds(_occurrenceInterval);

        _securityGuards[_countSecurityGuards].SetActive(true);

        _countSecurityGuards--;

        if (_countSecurityGuards > -1)
        {
            StartCoroutine(CreateVisitor());
        }

        yield return null;
    }
}

using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class MovementNPCVisitor : MonoBehaviour
{
    [SerializeField] private Bar _bar;

    private NavMeshAgent _npcVisitor;
    private Vector3 _target;
    private Animator _animator;

    private System.Random _random;
    private int[] _locationsNumbering;
    private int _timeFrom = 1;
    private int _timeTo = 8;
    private int _currentLocationIndex;

    private void Start()
    {
        _npcVisitor = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
        int nmberOfLocations = _bar.NumberOfLocations;
        _random = new System.Random();
        _currentLocationIndex = _random.Next(0, nmberOfLocations);
        _locationsNumbering = new int[nmberOfLocations - 1];
        int counnter = 0;
        for (int i = 0; i < nmberOfLocations; i++)
        {
            if (i != _currentLocationIndex)
            {
                _locationsNumbering[counnter] = i;
                counnter++;
            }
        }

        StartCoroutine(LocationSelection());
    }

    private IEnumerator LocationSelection()
    {
        switch (_currentLocationIndex)
        {
            case 0:
                //Debug.Log("бар");
                int indexBarSeat = _random.Next(0, Bar.BarSeats.Count);
                _target = Bar.BarSeats[indexBarSeat];
                Bar.BarSeats.RemoveAt(indexBarSeat);
                break;
            case 1:
                //Debug.Log("лаундж");
                int indexLoungeSeat = _random.Next(0, Bar.LoungeSeats.Count);
                _target = Bar.LoungeSeats[indexLoungeSeat];
                Bar.LoungeSeats.RemoveAt(indexLoungeSeat);
                break;
            case 2:
                //Debug.Log("танцпол");
                double randomX = _random.NextDouble() * (_bar.EndX - _bar.BeginX) + _bar.BeginX;
                double randomZ = _random.NextDouble() * (_bar.EndZ - _bar.BeginZ) + _bar.BeginZ;
                _target = new Vector3((float)randomX, 0, (float)randomZ);
                break;
            default:
                _target = Vector3.zero;
                break;
        }

        yield return _npcVisitor.destination = _target;

        while ((_target.x - transform.position.x) * (_target.x - transform.position.x) + (_target.z - transform.position.z) * (_target.z - transform.position.z) > 1f)
        { 
            yield return null; 
        }

        int _waiting = _random.Next(_timeFrom, _timeTo + 1);

        if (_currentLocationIndex == 2)
        {
            _animator.SetTrigger("Dance");
            float timeAnim = 0.25f;
            float time = 0;
            while (time < _waiting)
            {
                time += timeAnim;
                yield return new WaitForSeconds(timeAnim);
            }
            _animator.SetTrigger("Go");
        }
        else
        {
            yield return new WaitForSeconds(_waiting);
        }

        switch (_currentLocationIndex)
        {
            case 0:
                Bar.BarSeats.Add(_target);
                break;
            case 1:
                Bar.LoungeSeats.Add(_target);
                break;
            default:
                break;
        }

        int variableForCurrentLocation = _currentLocationIndex;
        int randomIndex = _random.Next(0, _locationsNumbering.Length);
        _currentLocationIndex = _locationsNumbering[randomIndex];
        _locationsNumbering[randomIndex] = variableForCurrentLocation;

        yield return StartCoroutine(LocationSelection());
    }
}

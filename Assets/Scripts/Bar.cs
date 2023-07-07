using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    static public List<Vector3> BarSeats = new List<Vector3>();
    static public List<Vector3> LoungeSeats = new List<Vector3>();

    [HideInInspector] public int NumberOfLocations = 3;
    [HideInInspector] public float BeginX;
    [HideInInspector] public float EndX;
    [HideInInspector] public float BeginZ;
    [HideInInspector] public float EndZ;

    [SerializeField] private Transform _danceFloorPoint1;
    [SerializeField] private Transform _danceFloorPoint2;
    [SerializeField] private GameObject _chair;
    [SerializeField] private GameObject _lounge;

    private string _nameLayerLounge = "Seating";

    private void Awake()
    {
        int index = LayerMask.NameToLayer(_nameLayerLounge);

        foreach (Transform child in _chair.transform)
        {
            BarSeats.Add(child.position);
        }

        foreach (Transform child in _lounge.GetComponentsInChildren<Transform>())
        {
            if (child.gameObject.layer == index)
            {
                LoungeSeats.Add(child.transform.position);
            }
        }

        if (_danceFloorPoint1.position.x > _danceFloorPoint2.position.x)
        {
            BeginX = _danceFloorPoint2.position.x;
            EndX = _danceFloorPoint1.position.x;
        }
        else
        {
            BeginX = _danceFloorPoint1.position.x;
            EndX = _danceFloorPoint2.position.x;
        }

        if (_danceFloorPoint1.position.z > _danceFloorPoint2.position.z)
        {
            BeginZ = _danceFloorPoint2.position.z;
            EndZ = _danceFloorPoint1.position.z;
        }
        else
        {
            BeginZ = _danceFloorPoint1.position.z;
            EndZ = _danceFloorPoint2.position.z;
        }
    }
}

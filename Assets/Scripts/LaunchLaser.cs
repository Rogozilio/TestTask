using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class LaunchLaser : MonoBehaviour
    {
        public float power;
        public float beamRange;

        private LineRenderer _lineRenderer;
        private byte _index;
        private float _startPower;

        private void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
        }

        private void Update()
        {
            _index = 0;
            _startPower = power;
            SetPosition(transform.position);
            Raycast(transform.position, transform.up);
        }

        private void Raycast(Vector3 start, Vector3 end)
        {
            RaycastHit hit;
            var isBeamHit = Physics.Raycast(start, end, out hit);
            var wallPower = (isBeamHit)
                ? hit.transform.GetComponent<AbsorptionCoefficient>().value
                : _startPower;
            if (isBeamHit && wallPower < _startPower)
            {
                end = Vector3.Reflect((hit.point - start).normalized, hit.normal);
                start = hit.point;
                _startPower -= wallPower;
                SetPosition(hit.point);
                Raycast(start, end);
            }
            else
            {
                if(Physics.Raycast(start, end, out hit, beamRange))
                    SetPosition(hit.point);
                else
                    SetPosition(start + end * beamRange);
                _lineRenderer.positionCount = _index;
            }
        }

        private void SetPosition(Vector3 pos)
        {
            _lineRenderer.positionCount++;
            _lineRenderer.SetPosition(_index++, pos);
        }
    }
}
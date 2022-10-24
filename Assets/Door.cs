using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : Triggerable
{
    public float moveSpeed = 5f;
    public Vector3 moveOffset;

    private Vector3 _startPosition;
    private Vector3 _endPosition;
    private Vector3 _targetPosition;
    private Coroutine _update;
    private Rigidbody _rigidBody;
    
    void Awake() {
        _rigidBody = GetComponent<Rigidbody>();
        _rigidBody.isKinematic = true;

        Debug.Log(_rigidBody);
        Vector3 offsetLocal = transform.TransformVector(moveOffset);
        _startPosition = transform.position;
        _endPosition = _startPosition + offsetLocal;
    }

    public override void Trigger (TriggerableAction action) {
        Debug.Log(action);
        if (action == TriggerableAction.Toggle) {
            if (_targetPosition == _endPosition) {
                _targetPosition = _startPosition;
            } else {
                _targetPosition = _endPosition;
            }
        } else {
            if (action == TriggerableAction.Deacttivate) {
                _targetPosition = _startPosition;
            } else {
                _targetPosition = _endPosition;
            }
        }

        if (_update != null) {
            StopCoroutine(_update);
            _update = null;
        }
        _update = StartCoroutine(MoveToTarget());
    }

    IEnumerator MoveToTarget() {
        while (true) {
            Vector3 offset = _targetPosition - transform.position;
            float distance = offset.magnitude;
            float moveDistance = moveSpeed * Time.deltaTime;

            if(moveDistance < distance) {
                Vector3 move = offset.normalized * moveDistance;
                Debug.Log(move);
                _rigidBody.MovePosition(transform.position + move);
                yield return null;
            }
            else {
                break;
            }
        }
        _rigidBody.MovePosition(_targetPosition);
        _update = null;
    }


    void OnDrawGizmosSelected() {
        Gizmos.matrix = transform.localToWorldMatrix;
        MeshFilter mf = GetComponent<MeshFilter>();
        if (mf != null) {
            Gizmos.DrawWireMesh(mf.sharedMesh, moveOffset);
        }
    }
}

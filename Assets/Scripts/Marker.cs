using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// マーカーを動かす処理と、座標を送り処理
/// </summary>
public class Marker : MonoBehaviour
{
    MeshTransformation _meshTransformation;
    private Vector3 _originPotision;
    private Vector3 _currentPosition;
    private Camera _mainCamera;
    private Vector3 offset;
    private float zCoord;
    void Start()
    {
        _meshTransformation = GetComponentInParent<MeshTransformation>();
        _originPotision = transform.position;
        _mainCamera = Camera.main;
    }
    void OnMouseDown()
    {
        zCoord = _mainCamera.WorldToScreenPoint(transform.position).z;
        offset = transform.position - GetMouseWorldPos();
        _meshTransformation.SelectVertice(RelativePosition(transform.position));
    }

    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + offset;
    }
    private void OnMouseUp()
    {
        _meshTransformation.UpdateMesh(RelativePosition(transform.position));
    }

    // マウスのスクリーン座標をワールド座標に変換
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = zCoord;
        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }
    private Vector3 RelativePosition(Vector3 position)
    {
        return transform.position - _meshTransformation.gameObject.transform.position;
    }
}

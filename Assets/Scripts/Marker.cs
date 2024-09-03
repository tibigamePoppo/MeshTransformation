using UnityEngine;

/// <summary>
/// マーカーを動かす処理と、座標を送り処理
/// </summary>
public class Marker : MonoBehaviour
{
    MeshTransformation _meshTransformation;
    [SerializeField]
    private Material _uselected;
    [SerializeField]
    private Material _un_uselected;
    private Renderer _render;
    private Camera _mainCamera;
    private Vector3 _offset;
    private float _zCoord;
    void Start()
    {
        _render = GetComponent<Renderer>();
        _meshTransformation = GetComponentInParent<MeshTransformation>();
        _mainCamera = Camera.main;
    }

    // マウスを押した時の処理
    void OnMouseDown()
    {
        _render.material = _uselected;
        _zCoord = _mainCamera.WorldToScreenPoint(transform.position).z;
        _offset = transform.position - GetMouseWorldPos();
        _meshTransformation.SelectVertice(RelativePosition(transform.position));
    }

    // マウスをドラッグしている時の処理
    void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + _offset;
    }

    // マウスを離した時の処理
    private void OnMouseUp()
    {
        _render.material = _un_uselected;
        _meshTransformation.UpdateMesh(RelativePosition(transform.position));
    }

    // マウスのスクリーン座標をワールド座標に変換
    Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = _zCoord;

        return _mainCamera.ScreenToWorldPoint(mousePoint);
    }
    private Vector3 RelativePosition(Vector3 position)
    {
        return transform.position - _meshTransformation.gameObject.transform.position;
    }
}

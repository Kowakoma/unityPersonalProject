using UnityEngine;

public class MainCameraMovement : MonoBehaviour
{
    [SerializeField] private float _smoothSpeed;
    private Vector3 _menuCameraPos = new Vector3(0, 11, -30);
    private Vector3 _gameCameraPos = new Vector3(0, 11, -12);
    private bool _cameraShouldMoveToGamePos;
    private bool _cameraShouldMoveToMenuPos;

    void LateUpdate()
    {
        if (_cameraShouldMoveToGamePos)
        {
            transform.position = Vector3.Lerp(transform.position, _gameCameraPos,
                _smoothSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _gameCameraPos) < 0.1f)
                _cameraShouldMoveToGamePos = false;
        }
        if (_cameraShouldMoveToMenuPos)
        {
            transform.position = Vector3.Lerp(transform.position, _menuCameraPos,
                _smoothSpeed * Time.deltaTime);
            if (Vector3.Distance(transform.position, _menuCameraPos) < 0.1f)
                _cameraShouldMoveToMenuPos = false;
        }
    }

    public void ToGamePosition()
    {
        _cameraShouldMoveToGamePos = true;
    }

    public void ToMenuPosition()
    {
        _cameraShouldMoveToMenuPos = true;
    }
}

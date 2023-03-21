using Cinemachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;

    [SerializeField] private float zoomSpeed = .5f;
    private float orthographicSize = 3.5f;

    private void Start()
    {
        AudioManager.Instance.StopMusic();
    }

    private void Update()
    {
        ZoomInOut();
    }

    private void ZoomInOut()
    {
        orthographicSize -= Input.mouseScrollDelta.y * zoomSpeed;
        orthographicSize = Mathf.Clamp(orthographicSize, 3f, 7f);
        cam.m_Lens.OrthographicSize = orthographicSize;
    }
}

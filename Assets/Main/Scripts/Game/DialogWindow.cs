using UnityEngine;

public class DialogWindow : MonoBehaviour
{
    [SerializeField] private GameObject _dialogPanel;
    [SerializeField] private float _rotationSpeed = 5f;
    [SerializeField] private float _fadeDuration = 0.3f;
    [SerializeField] private PlayerTrigger playerTrigger;
    [SerializeField] private Transform _player;

    private bool _isVisible;

    private void Awake()
    {
        HidePanel();
        playerTrigger.OnPlayerEntered.AddListener(ShowPanel);
        playerTrigger.OnPlayerExited.AddListener(HidePanel);
    }

    private void Update()
    {
        if (_isVisible && _player != null)
        {
            // ѕоворачиваем панель к игроку на плоскости Y
            Vector3 lookDirection = _player.position - transform.position;
            lookDirection.y = 0; // »гнорируем вертикальную ось

            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
            transform.rotation = Quaternion.Slerp(
                transform.rotation,
                targetRotation,
                _rotationSpeed * Time.deltaTime
            );
        }
    }

    private void ShowPanel()
    {
        _isVisible = true;
        _dialogPanel.SetActive(_isVisible);
    }

    private void HidePanel()
    {
        _isVisible = false;
        _dialogPanel.SetActive(_isVisible);
    }
}
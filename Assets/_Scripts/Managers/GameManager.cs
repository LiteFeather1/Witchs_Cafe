using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public static Camera Camera { get; private set; }

    [Header("Money")]
    private float _money = 0f;

    [Header("Time")]
    [SerializeField] private float _startTime;
    [SerializeField] private float _endTime;

    [Header("Managers")]
    [SerializeField] private MouseManager _mouseManager;
    [SerializeField] private ClientManager _clientManager;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private HoverInfoManager _hoverInfoManager;
    [SerializeField] private AudioManager _audioManager;

    public static ManagerInput InputManager { get; private set; }

    public MouseManager MouseManager => _mouseManager;
    public HoverInfoManager HoverInfoManager => _hoverInfoManager;
    public AudioManager AudioManager => _audioManager;

    private void Awake()
    {
        Instance = this;
        InputManager = new();
        Camera = Camera.main;
    }

    private void OnEnable()
    {
        _clientManager.OnNewClient += AppearDialogue;
        _clientManager.OnClientServed += OnClieantServed;
        _clientManager.OnAllClientsServed += _uiManager.OnAllClientsServed;

        InputManager.PlayerInputs.LeftClick.performed += StartGame;
        InputManager.PlayerInputs.RightClick.performed += StartGame;

        InputManager.PlayerInputs.PauseUnPause.performed += PauseUnpause;
    }

    private void OnDisable()
    {
        _clientManager.OnNewClient -= AppearDialogue;
        _clientManager.OnClientServed -= OnClieantServed;
        _clientManager.OnAllClientsServed -= _uiManager.OnAllClientsServed;

        InputManager.PlayerInputs.PauseUnPause.performed -= PauseUnpause;
    }

    public IEnumerator StartGameCO()
    {
        InputManager.PlayerInputs.LeftClick.performed -= StartGame;
        InputManager.PlayerInputs.RightClick.performed -= StartGame;
        yield return _uiManager.IntroFade();
        _clientManager.AppearClient();
    }

    private void StartGame(InputAction.CallbackContext ctx) => StartCoroutine(StartGameCO());

    public static Vector2 MousePosition() => Camera.ScreenToWorldPoint(Input.mousePosition);

    private void AppearDialogue(Client client)
    {
        _uiManager.PopUpDialogue(client.Dialogue);
        _uiManager.SetCurrentClient(client);
    }

    private void OnClieantServed(CoffeeComparisonResults results, float clientPatience)
    {
        _uiManager.CoffeeDelivered(results, clientPatience);
        StartCoroutine(_uiManager.StepMoney(_money, results.Money));
        if (_clientManager.CurrentClientIndex == _clientManager.ClientLength)
            return;

        int index = Mathf.Max(1, _clientManager.CurrentClientIndex - 1);
        float increment = (_endTime + 24f - _startTime) % 24f / index;
        float time = _startTime + (increment * _clientManager.CurrentClientIndex);
        _uiManager.SetTime(time);

        _money += results.Money;
    }

    // Also called by a button
    public void PauseUnpause()
    {
        bool paused = Time.timeScale == 0f;
        Time.timeScale = paused ? 1f : 0f;
        _uiManager.PauseOverlay(!paused);
    }

    private void PauseUnpause(InputAction.CallbackContext ctx) => PauseUnpause();

    public class ManagerInput
    {
        private readonly InputMaps _inputMaps;

        public ManagerInput()
        {
            _inputMaps = new();
            _inputMaps.Player.Enable();
        }

        public InputMaps.PlayerActions PlayerInputs => _inputMaps.Player;
    }
}


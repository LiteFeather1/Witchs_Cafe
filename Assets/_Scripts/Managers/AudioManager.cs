using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{

    private static float _prevAudioVolume = .5f;
    private void Awake()
    {
        AudioListener.volume = _prevAudioVolume;
    }

    private void OnEnable()
    {
        GameManager.InputManager.PlayerInputs.IncreaseVolume.performed += IncreaseListenerVol;
        GameManager.InputManager.PlayerInputs.IncreaseVolume.performed += IncreaseListenerVol;
        GameManager.InputManager.PlayerInputs.MuteUnmute.performed += MuteUnmute;
    }

    private void OnDisable()
    {
        GameManager.InputManager.PlayerInputs.IncreaseVolume.performed -= IncreaseListenerVol;
        GameManager.InputManager.PlayerInputs.DecreaseVolume.performed -= DecreaseListenerVol;
        GameManager.InputManager.PlayerInputs.MuteUnmute.performed -= MuteUnmute;
    }

    private void IncreaseListenerVol(InputAction.CallbackContext ctx)
    {
        if (AudioListener.volume == 1f)
            return;

        _prevAudioVolume = AudioListener.volume += 0.1f;
    }

    private void DecreaseListenerVol(InputAction.CallbackContext ctx)
    {
        if (AudioListener.volume == 0f)
            return;

        _prevAudioVolume = AudioListener.volume -= 0.1f;
    }

    private void MuteUnmute(InputAction.CallbackContext ctx) => MuteUnmute();

    // Also Called by button
    public void MuteUnmute()
    {
        AudioListener.volume = AudioListener.volume == 0f ? _prevAudioVolume : 0f;
    }
}

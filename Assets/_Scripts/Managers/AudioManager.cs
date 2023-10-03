using UnityEngine;
using UnityEngine.InputSystem;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private Vector2 _pitchRange;

    private static float _prevAudioVolume = .5f;

    private void Awake()
    {
        AudioListener.volume = _prevAudioVolume;
    }

    private void OnEnable()
    {
        GameManager.InputManager.PlayerInputs.IncreaseVolume.performed += IncreaseListenerVol;
        GameManager.InputManager.PlayerInputs.DecreaseVolume.performed += DecreaseListenerVol;
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
        _prevAudioVolume += .1f;
        if (AudioListener.volume >= .95f)
            _prevAudioVolume = 1f;

         AudioListener.volume = _prevAudioVolume;
    }

    private void DecreaseListenerVol(InputAction.CallbackContext ctx)
    {
        _prevAudioVolume -= .1f;
        if (AudioListener.volume <= 0.05f)
            _prevAudioVolume = 0f;

        AudioListener.volume = _prevAudioVolume;
    }

    private void MuteUnmute(InputAction.CallbackContext ctx) => MuteUnmute();

    // Also Called by button
    public void MuteUnmute()
    {
        AudioListener.volume = AudioListener.volume == 0f ? _prevAudioVolume : 0f;
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        _sfxSource.PlayOneShot(clip);
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        _sfxSource.pitch = Random.Range(_pitchRange.x, _pitchRange.y);
        _sfxSource.PlayOneShot(clip, volume);
    }
}

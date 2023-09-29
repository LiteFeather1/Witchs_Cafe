using UnityEngine;

public class PlaySoundOnHit : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip[] _hitAudio;
    [SerializeField] private bool _canPlay = true;
    [SerializeField, Range(0f, 1f)] private float _volume = .5f;

    public void SetCanPlay(bool canPlay) => _canPlay = canPlay;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!_canPlay)
            return;

        int r = Random.Range(0, _hitAudio.Length);
        GameManager.Instance.AudioManager.PlaySFX(_hitAudio[r], _volume);
    }
}

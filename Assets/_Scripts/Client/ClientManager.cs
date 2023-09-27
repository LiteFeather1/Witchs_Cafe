using System;
using System.Collections;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] _clients;
    private int _currentClientIndex;

    [Header("Audio")]
    [SerializeField] private AudioClip _audioCustomerArrive;

    private readonly static YieldInstruction _delayBeforeShowDialogue = new WaitForSeconds(.5f);

    public Action<Client> OnNewClient { get; set; }
    public Action OnAllClientsServed { get; set; }
    public Action<CoffeeComparisonResults, float> OnClientServed { get; set; }

    public int ClientLength => _clients.Length;
    public int CurrentClientIndex => _currentClientIndex;

    private void OnEnable()
    {
        for (int i = 0; i < _clients.Length; i++)
            _clients[i].OnCoffeeDelivered += ClientServed;
    }

    private void OnDisable()
    {
        for (int i = 0; i < _clients.Length; i++)
            _clients[i].OnCoffeeDelivered -= ClientServed;
    }

    public void AppearClient()
    {
        StartCoroutine(AppearClientCO());
    }

    public IEnumerator AppearClientCO()
    {
        if (_currentClientIndex == _clients.Length - 1)
            OnAllClientsServed?.Invoke();

        var client = _clients[_currentClientIndex];
        client.gameObject.SetActive(true);
        GameManager.Instance.AudioManager.PlaySFX(_audioCustomerArrive);
        yield return client.PopAnimation();
        yield return _delayBeforeShowDialogue;
        OnNewClient?.Invoke(client);
    }

    public void ClientServed(CoffeeComparisonResults results, float patience)
    {
        _currentClientIndex++;
        OnClientServed?.Invoke(results, patience);
    }
}


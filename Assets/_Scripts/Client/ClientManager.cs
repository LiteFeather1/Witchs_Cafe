using System;
using System.Collections;
using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] _clients;
    private int _currentClientIndex;

    private readonly static YieldInstruction _delayBeforeShowDialogue = new WaitForSeconds(.5f);

    public Action<Client> OnNewClient { get; set; }
    public Action<CoffeeComparisonResults, float> OnClientServed { get; set; }
    public Action OnAllClientsServed { get; set; }

    private void OnEnable()
    {
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].OnCoffeeDelivered += ClientServed;
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < _clients.Length; i++)
        {
            _clients[i].OnCoffeeDelivered -= ClientServed;
        }
    }

    public void AppearClient()
    {
        StartCoroutine(AppearClientCO());
    }

    public IEnumerator AppearClientCO()
    {
        var client = _clients[_currentClientIndex];
        client.gameObject.SetActive(true);
        yield return client.PopAnimation();
        yield return _delayBeforeShowDialogue;
        OnNewClient?.Invoke(client);
    }

    public void DisappearCurrentClient()
    {
        _currentClientIndex++;
        if (_currentClientIndex < _clients.Length)
            return;

        print("All Clients Served");
        OnAllClientsServed?.Invoke();
    }

    public void ClientServed(CoffeeComparisonResults results, float patience)
    {
        OnClientServed?.Invoke(results, patience);
        DisappearCurrentClient();
    }
}


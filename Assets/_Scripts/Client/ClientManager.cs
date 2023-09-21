using UnityEngine;

public class ClientManager : MonoBehaviour
{
    [SerializeField] private Client[] _clients;
    private int _currentClient;

    public void AppearClient()
    {
        _clients[_currentClient].gameObject.SetActive(true);
    }

    public void DisappearCurrentClient()
    {
        _clients[_currentClient].gameObject.SetActive(false);

        _currentClient++;
        if (_currentClient >= _clients.Length)
            print("All Clients Served");
    }

    public void NextClient()
    {
        _clients[_currentClient].gameObject.SetActive(true);
    }
}


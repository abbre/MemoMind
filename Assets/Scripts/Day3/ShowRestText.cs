using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRestText : MonoBehaviour
{
    private Collider _collider;
    [SerializeField] private GameObject _playerText;

    void Start()
    {
        _playerText.SetActive(false);
    }
    // Update is called once per frame
    private void ShowPlayerText()
    {
        _playerText.SetActive(true);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ShowPlayerText();
        }
    }
}

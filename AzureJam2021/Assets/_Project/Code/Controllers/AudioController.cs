using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] private GameSound _gameSoundPrefab;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip[] _sheepSounds;

    private void Awake()
    {
        GameController.OnGameOver += HandleGameOver;
        Health.OnDeath += HandleDeath;
        Health.OnDamageTaken += HandleDamageTaken;
        ItemHolder.OnItemEquiped += HandleItemEqupied;
    }

    private void OnDestroy()
    {
        GameController.OnGameOver -= HandleGameOver;
        Health.OnDeath -= HandleDeath;
        Health.OnDamageTaken -= HandleDamageTaken;
        ItemHolder.OnItemEquiped -= HandleItemEqupied;
    }

    private void HandleGameOver()
    {
        GameSound gameSound = Instantiate(_gameSoundPrefab, transform.position, Quaternion.identity);
        gameSound.Initialize(_gameOverSound);
    }
    private void HandleDeath(GameObject deadObject)
    {
        CheckForSheep(deadObject);
    }
    private void HandleDamageTaken(GameObject damagedObject)
    {
        CheckForSheep(damagedObject);
    }
    private void HandleItemEqupied(Item item)
    {
        GameSound gameSound = Instantiate(_gameSoundPrefab, transform.position, Quaternion.identity);
        gameSound.Initialize(item.ItemSound);
    }

    private void CheckForSheep(GameObject testObject)
    {
        Sheep sheep = testObject.GetComponent<Sheep>();
        if (sheep == null) return;
        GameSound gameSound = Instantiate(_gameSoundPrefab, testObject.transform.position, Quaternion.identity);
        gameSound.Initialize(PickSound());
    }

    private AudioClip PickSound()
    {
        int index = UnityEngine.Random.Range(0, _sheepSounds.Length);
        return _sheepSounds[index];
    }
}

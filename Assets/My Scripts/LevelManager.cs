﻿using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public AudioSource playerAudio;                             // Reference to the AudioSource component.
    public AudioClip dying;
    public AudioClip playerRespawn;
    public AudioClip enemyDeath;
    public AudioClip bulletImpact;

    public GameObject currentCheckpoint;

    public GameObject RespawnParticle;
    public GameObject DeathParticleEffect;

    public GameObject Player;

    public GameObject BulletSpawnPoint;

    private Player player;

    public float respawnDelay;

    KillPlayer playerDeathsCount;

    public static float playerDeaths = 0f;

    // Use this for initialization
    void Start ()
    {
        player = FindObjectOfType<Player>();
        playerAudio = GetComponent<AudioSource>();
    }

    public void RespawnPlayer()
    {
        StartCoroutine("RespawnPlayerCo");
    }

    public void EnemyDeath()
    {
        playerAudio.clip = enemyDeath;
        playerAudio.Play();
    }

    public void BulletImpact()
    {
        playerAudio.clip = bulletImpact;
        playerAudio.Play();
    }

    public IEnumerator RespawnPlayerCo()
    {
        playerDeaths = 1f;

        playerAudio.clip = dying;
        playerAudio.Play();

        Instantiate(DeathParticleEffect, player.transform.position, player.transform.rotation);

        player.enabled = false;
        player.GetComponent<Renderer>().enabled = false;

        Player.SetActive(false);

        BulletSpawnPoint.SetActive(false);

        yield return new WaitForSeconds(respawnDelay);

        playerAudio.clip = playerRespawn;
        playerAudio.Play();

        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.GetComponent<Renderer>().enabled = true;

        Player.SetActive(true);

        BulletSpawnPoint.SetActive(true);

        Instantiate(RespawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
    }

}

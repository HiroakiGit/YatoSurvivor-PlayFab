﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapEdgeEnemySpawner : Spawner
{
    public EnemySpawnerManager _EnemySpawnerManager;
    public Player _player;
    public GameObject enemyPrefab;
    public Transform enemySpawnPoint;

    private float timeSinceLastSpawn;
    public MapManager _MapManager;

    public override void Spawn()
    {
        Vector2 spawnPosition = GetRandomEdgePosition();
        GameObject enemy = Instantiate(enemyPrefab, spawnPosition, Quaternion.identity, enemySpawnPoint);
        enemy.GetComponent<Enemy>().InitializeEnemyType(_EnemySpawnerManager.GetRandomEnemyType(), _EnemySpawnerManager.EnemyDamageRATIO);
        enemy.GetComponent<Enemy>()._player = _player;
    }
    
    private void Update()
    {
        //開始まで待つ
        if (!GameManager.Instance.IsGameStarted()) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= _EnemySpawnerManager.MapEdgeSpawnInterval)
        {
            Spawn();
            timeSinceLastSpawn = 0f;
        }
    }

    private Vector2 GetRandomEdgePosition()
    {
        float x, y;
        int edge = Random.Range(0, 4);

        switch (edge)
        {
            case 0: // 上辺
                x = Random.Range(_MapManager.edgeSizeMin.x, _MapManager.edgeSizeMax.x);
                y = _MapManager.edgeSizeMax.y;
                break;
            case 1: // 下辺
                x = Random.Range(_MapManager.edgeSizeMin.x, _MapManager.edgeSizeMax.x);
                y = _MapManager.edgeSizeMin.y;
                break;
            case 2: // 左辺
                x = _MapManager.edgeSizeMin.x;
                y = Random.Range(_MapManager.edgeSizeMin.y, _MapManager.edgeSizeMax.y);
                break;
            case 3: // 右辺
                x = _MapManager.edgeSizeMax.x;
                y = Random.Range(_MapManager.edgeSizeMin.y, _MapManager.edgeSizeMax.y);
                break;
            default:
                x = 0;
                y = 0;
                break;
        }

        return new Vector2(x, y);
    }
}

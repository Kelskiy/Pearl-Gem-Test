using UnityEngine;
using System;
using System.Collections.Generic;

public class SpawnManager : Singleton<SpawnManager>
{
    [Header("Planet settings")]
    public int totalBalls = 500;
    public float planetRadius = 5f;
    public int numberOfZones = 5;

    public BallContoller ballPrefab;

    public Dictionary<int, List<GameObject>> zoneDict = new Dictionary<int, List<GameObject>>();

    public List<BallContoller> balls = new List<BallContoller>();

    public BallColor[] zoneColors;

    public List<BallColor> currentBallColors = new List<BallColor>();

    void Awake()
    {
        InitializeZoneColors();
        GeneratePlanet();
    }


    void InitializeZoneColors()
    {
        BallColor[] availableColors = (BallColor[])Enum.GetValues(typeof(BallColor));
        if (zoneColors == null || zoneColors.Length < numberOfZones)
        {
            zoneColors = new BallColor[numberOfZones];
            for (int i = 0; i < numberOfZones; i++)
            {
                zoneColors[i] = availableColors[i % availableColors.Length];
            }
        }
    }

    void GeneratePlanet()
    {
        float goldenAngle = Mathf.PI * (3 - Mathf.Sqrt(5));
        for (int i = 0; i < totalBalls; i++)
        {
            float y = 1 - (i / (float)(totalBalls - 1)) * 2;
            float r = Mathf.Sqrt(1 - y * y);
            float theta = i * goldenAngle;
            float x = r * Mathf.Cos(theta);
            float z = r * Mathf.Sin(theta);
            Vector3 pos = new Vector3(x, y, z) * planetRadius + transform.position;

            BallContoller spawnedBall = Instantiate(ballPrefab, pos, Quaternion.identity, transform);

            float azimuth = Mathf.Atan2(z, x) * Mathf.Rad2Deg;
            if (azimuth < 0) azimuth += 360f;
            int zoneIndex = Mathf.FloorToInt(azimuth / (360f / numberOfZones));
            zoneIndex = Mathf.Clamp(zoneIndex, 0, numberOfZones - 1);

            spawnedBall.InitializedBall(zoneIndex);

            balls.Add(spawnedBall);

            if (!zoneDict.ContainsKey(zoneIndex))
            {
                zoneDict[zoneIndex] = new List<GameObject>();
            }
            zoneDict[zoneIndex].Add(spawnedBall.gameObject);
        }
    }

    public void DestroySegment(int zoneIndex, BallColor color)
    {
        if (zoneDict.ContainsKey(zoneIndex))
        {
            RemoveColor(color);

            foreach (GameObject sphere in zoneDict[zoneIndex])
            {
                Rigidbody rb = sphere.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = sphere.AddComponent<Rigidbody>();
                }
                rb.useGravity = true;
                rb.isKinematic = false;
                Destroy(sphere, 3f);
            }
            zoneDict.Remove(zoneIndex);
        }
    }

    public void AddColor(BallColor color)
    {
        if (currentBallColors.Contains(color))
            return;

        currentBallColors.Add(color);
    }

    private void RemoveColor(BallColor color)
    {
        currentBallColors.Remove(color);
    }

    public BallColor GetRandomColor()
    {
        int colorID = 0;

        colorID = UnityEngine.Random.Range(0, currentBallColors.Count);

        if (currentBallColors.Count > 0)
            return currentBallColors[colorID];
        else
            return BallColor.None;
    }
}
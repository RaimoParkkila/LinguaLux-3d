using UnityEngine;

public class LaserMovement : MonoBehaviour
{
    public Transform[] waypoints; // Tässä määritellään seinän reunojen sijainnit
    private int currentWaypointIndex = 0;
    public float movementSpeed = 1f;
    public float pauseDuration = 1f;
    private float nextMoveTime = 0f;

    void Update()
    {
        // Tarkistetaan, onko kulunut riittävästi aikaa seuraavan siirtymisen tekemiseen
        if (Time.time >= nextMoveTime)
        {
            MoveToNextWaypoint();
        }
    }

    void MoveToNextWaypoint()
    {
        // Jos olemme saavuttaneet kaikki waypointit, asetetaan indeksi takaisin alkuun
        if (currentWaypointIndex >= waypoints.Length)
        {
            currentWaypointIndex = 0;
            nextMoveTime = Time.time + pauseDuration; // Tauko ennen seuraavaa kierrosta
            return;
        }

        // Siirry seuraavaan waypointiin
        Transform targetWaypoint = waypoints[currentWaypointIndex];
        float step = movementSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

        // Tarkista, olemmeko saavuttaneet waypointin
        if (transform.position == targetWaypoint.position)
        {
            // Siirry seuraavaan waypointiin
            currentWaypointIndex++;
            nextMoveTime = Time.time + pauseDuration; // Tauko ennen seuraavaa siirtymistä
        }
    }
}

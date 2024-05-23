using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private float speed = 5f;
    [SerializeField] private bool debugEnable = false;

    [SerializeField] private EmptyAction shootMomentEvent;
    [SerializeField] private ActionChannel<Transform> shootPointEvent;

    private List<Bullet> activeBullets = new();
    private List<Bullet> poolBullets = new();

    private void OnEnable()
    {
        if (shootMomentEvent)
            shootMomentEvent.Subscribe(HandleShoot);

        if (shootPointEvent)
            shootPointEvent.Subscribe(HandleSetShootPoint);
    }

    private void OnDisable()
    {
        if (shootMomentEvent)
            shootMomentEvent.Unsubscribe(HandleShoot);

        if (shootPointEvent)
            shootPointEvent.Unsubscribe(HandleSetShootPoint);
    }

    private void OnGUI()
    {
        if (debugEnable)
        {
            GUI.Label(new Rect(10, 10, 200, 30), $"Total Pool Size: {activeBullets.Count + poolBullets.Count}");
            GUI.Label(new Rect(10, 30, 200, 30), $"Active Objects: {activeBullets.Count}");
        }
    }

    private void HandleSetShootPoint(Transform pointShoot)
    {
        shootPoint = pointShoot;
    }

    private void HandleShoot()
    {
        Bullet temp = SelectBullet();
        temp.transform.position = shootPoint.position;

        if (poolBullets.Contains(temp))
            poolBullets.Remove(temp);

        activeBullets.Add(temp);
        temp.Shoot(shootPoint.position, shootPoint.forward, speed);
    }

    private Bullet SelectBullet()
    {
        if (poolBullets.Count == 0)
        {
            Bullet temp = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
            temp.onDisable += HandleDesactiveBullet;
            return temp;
        }

        return poolBullets[0];
    }

    private void HandleDesactiveBullet(Bullet bullet)
    {
        if (activeBullets.Contains(bullet))
        {
            activeBullets.Remove(bullet);
            poolBullets.Add(bullet);
        }
    }
}
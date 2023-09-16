using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    public float speed = 5f;

    public bool canRotate = false;

    public GameObject vfxFail;

    public GameObject vfxSuccess;

    int Id;

    private void OnMouseDown()
    {
        canRotate = true;
    }

    private void Awake()
    {
        Id = GetInstanceID();
    }

    private void Update()
    {
        if (!canRotate) return;
        transform.Rotate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Rotator")) {
            GameObject vfxOnTounch = Instantiate(vfxFail, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfxOnTounch, 1f);
            canRotate = false;
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else if (collision != null && collision.gameObject.CompareTag("Goal"))
        {
            GameObject vfxOnTounch = Instantiate(vfxSuccess, transform.position, Quaternion.identity) as GameObject;
            Destroy(vfxOnTounch, 1f);
            GameManager.Instance.levels[GameManager.Instance.GetCurrentIndex()].gameObjects.Remove(gameObject);
            GameManager.Instance.CheckLevelUp();
            Destroy(gameObject);
        }
    }
}

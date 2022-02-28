using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    [SerializeField]
    private Collider2D[] cameraTargets;

    private Camera cam;
    private EdgeCollider2D edgeCollider;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Cam: " + -(cam.rect.width / 2));
        //Debug.Log("Char: " + cameraTargets[0].transform.position.x);

        for (int i = 0; i < cameraTargets.Length; i++)
        {
            if (-(cam.rect.width/2) < cameraTargets[i].transform.position.x)
            {
                Debug.Log("past");
                float distToMove = cam.rect.xMin - (cameraTargets[i].transform.position.x);

                transform.Translate(-distToMove, 0, 0);
            }
            else if((cam.rect.width / 2) > cameraTargets[i].transform.position.x)
            {
                Debug.Log("past");
                float distToMove = cam.rect.xMax + (cameraTargets[i].transform.position.x);

                transform.Translate(distToMove, 0, 0);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Character")
        {
            if (cam.WorldToScreenPoint(collision.transform.position).x > Screen.width / 2)
            {
                transform.Translate(0.1f, 0, 0);
            }
            else if (cam.WorldToScreenPoint(collision.transform.position).x < Screen.width / 2)
            {
                transform.Translate(-0.1f, 0, 0);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            if (cam.WorldToScreenPoint(collision.transform.position).x > Screen.width / 2)
            {
                transform.Translate(collision.GetComponent<CharController>().groundSpeed * Time.deltaTime, 0, 0);
            }
            else if (cam.WorldToScreenPoint(collision.transform.position).x < Screen.width / 2)
            {
                transform.Translate(-collision.GetComponent<CharController>().groundSpeed * Time.deltaTime, 0, 0);
            }
        }
    }
}

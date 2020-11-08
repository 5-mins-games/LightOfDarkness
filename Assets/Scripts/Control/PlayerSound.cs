using UnityEngine;

public class PlayerSound : MonoBehaviour
{
    AudioSource audiosrc;

    void Start()
    {
        audiosrc = GetComponentInChildren<AudioSource>();
    }

    //void Update()
    //{
    //    if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) ||
    //        Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
    //    {
    //        audiosrc.loop = true;
    //        audiosrc.Play();
    //    }
    //    else
    //    {
    //        audiosrc.Pause();
    //    }
    //}
}

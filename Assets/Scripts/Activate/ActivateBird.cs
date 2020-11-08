using UnityEngine;

[RequireComponent(typeof(BirdGen))]
public class ActivateBird : MonoBehaviour
{
    public GameObject player;

    [Range(1, 20)]
    public float activateInRange = 15f;
    public bool inverse = false;

    BirdGen installation;
    Light spotlight;
    AudioSource audiosrc;

    private bool currActivate;

    private void Start()
    {
        installation = GetComponent<BirdGen>();
        spotlight = GetComponentInChildren<Light>();
        audiosrc = GetComponentInChildren<AudioSource>();

        spotlight.gameObject.SetActive(inverse);
        installation.animate = inverse;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool activate = inverse ? distance > activateInRange : distance < activateInRange;

        if (activate != currActivate)
        {
            installation.animate = activate;
            spotlight.gameObject.SetActive(activate);

            currActivate = activate;
            audiosrc.Play();
        }
    }
}

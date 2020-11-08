using UnityEngine;

[RequireComponent(typeof(BoidGen))]
public class ActivateStarGen : MonoBehaviour
{
    public GameObject player;

    [Range(1, 20)]
    public float activateInRange = 15f;
    public bool inverse = false;

    BoidGen installation;
    Light spotlight;
    AudioSource audiosrc;
    GameObject star;

    private bool currActivate;

    private void Start()
    {
        installation = GetComponent<BoidGen>();
        spotlight = GetComponentInChildren<Light>();
        audiosrc = GetComponentInChildren<AudioSource>();

        star = GameObject.Find("Star");

        installation.animate = inverse;
        spotlight.gameObject.SetActive(inverse);
        star.SetActive(inverse);
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool activate = inverse ? distance > activateInRange : distance < activateInRange;

        if (activate != currActivate)
        {
            installation.animate = activate;
            spotlight.gameObject.SetActive(activate);
            star.SetActive(activate);

            currActivate = activate;
            audiosrc.Play();
        }
    }
}

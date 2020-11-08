using UnityEngine;

[RequireComponent(typeof(RockGen))]
public class ActivateRock : MonoBehaviour
{
    public GameObject player;

    [Range(1, 20)]
    public float activateInRange = 15f;
    public bool inverse = false;

    Light spotlight;
    AudioSource audiosrc;
    RockGen rock;

    private bool currActivate;

    private void Start()
    {
        spotlight = GetComponentInChildren<Light>();
        audiosrc = GetComponentInChildren<AudioSource>();
        rock = GetComponent<RockGen>();

        spotlight.gameObject.SetActive(inverse);
        rock.animate = inverse;
    }

    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        bool activate = inverse ? distance > activateInRange : distance < activateInRange;

        if (activate != currActivate)
        {
            spotlight.gameObject.SetActive(activate);
            rock.animate = activate;

            currActivate = activate;
            audiosrc.Play();
        }
    }
}

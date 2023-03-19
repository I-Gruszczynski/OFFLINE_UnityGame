using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VigneteScript : MonoBehaviour
{
    RawImage RI;
    public float ON_alpha;
    public Vector3 ON_scale;
    public float alphaspeed;
    public float transformspeed;
    public ParticleSystem ps;
    public float dismiss_speed;
    int strona = 0;
    private float alphasp;
    public float alphadecayspeed;
    public ParticleSystem.Particle[] particles;
    // Start is called before the first frame update
    void Start()
    {
        var emission = ps.emission;
        emission.rateOverTime = 0;
        RI = gameObject.GetComponent<RawImage>();
        transform.localScale = new Vector3(2, 2, 2);

    }

    // Update is called once per frame
    void Update()
    {
        /*   DEBUG
        if (Input.GetKeyDown(KeyCode.U))
        {
            StartCoroutine(ONanimation());
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            StartCoroutine(OFFanimation());
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCoroutine(alphafade());
        }
        */
    }
    public IEnumerator ONanimation()
    {
        strona = 1;
        while (strona == 1)
        {
            if (RI.color.a < ON_alpha)
            {
                RI.color= new Color(RI.color.r, RI.color.g, RI.color.b, RI.color.a+(alphaspeed*Time.deltaTime));
            }

            if (transform.localScale.x > 1)
            {
                float temptransform = transform.localScale.x - (transformspeed * Time.deltaTime);
                if (temptransform < 1)
                {
                    temptransform = 1;
                }
                transform.localScale = new Vector3(temptransform, temptransform, temptransform);
            }
            else
            {
                var main = ps.main;
                main.simulationSpeed = 0.5f;
                var emission = ps.emission;
                emission.rateOverTime = 10;
            }
            yield return null;
        }
        yield return null;
    }
    public IEnumerator OFFanimation()
    {
        
        strona = -1;
        while (strona == -1)
        {
            var main = ps.main;
            main.simulationSpeed = dismiss_speed;
            var emission = ps.emission;
            emission.rateOverTime = 0;
            if (RI.color.a > 0)
            {
                RI.color = new Color(RI.color.r, RI.color.g, RI.color.b, RI.color.a - (alphaspeed * Time.deltaTime));
            }

            if (transform.localScale.x < 2)
            {
                float temptransform = transform.localScale.x + (transformspeed * Time.deltaTime);
                transform.localScale = new Vector3(temptransform, temptransform, temptransform);
            }
            yield return null;
        }
        yield return null;
    }


    public IEnumerator alphafade()
    {
        particles = new ParticleSystem.Particle[ps.main.maxParticles];
        var test = ps.GetParticles(particles);
        alphasp = 1;
        while (alphasp > 0)
        {
            alphasp -= alphadecayspeed * Time.deltaTime;
            for (int i = 0; i < test; i++)
            {
                particles[i].startColor = new Color(1, 1, 1, alphasp);
            }
            ps.SetParticles(particles);
            yield return null;
        }
        yield return null;
    }

}

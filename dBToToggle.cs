using UnityEngine;
using UnityEngine.SceneManagement;

public class dBToToggle : MonoBehaviour
{
    public AudioSource source;
    public float dbActive = -20f;

    private ParticleSystem ps;

    public float db;

    public float[] buffer = new float[128];

    private string oScene;

    // Start is called before the first frame update
    void Start()
    {

        //Get AudioSource and the Particle System
        AudioSource[] msource = FindObjectsOfType<AudioSource>();

        for (int i = 0; i < msource.Length; i++)
        {
            if (msource[i].enabled)
                source = msource[i];
        }

        ps = GetComponent<ParticleSystem>();

        oScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        if (oScene != SceneManager.GetActiveScene().name)
        {
            AudioSource[] msource = FindObjectsOfType<AudioSource>();

            for (int i = 0; i < msource.Length; i++)
            {
                if (msource[i].enabled)
                    source = msource[i];
            }

            oScene = SceneManager.GetActiveScene().name;
        }

        var em = ps.emission;


        //Calculate dB
        int length = (int)source.clip.length;

        buffer = source.GetOutputData(128, 1);

        db = Mathf.Round(AudioPower.ComputeDB(buffer, 0, ref length));

        if (db < dbActive)
        {
            ps.enableEmission = false;
            return;
        }

        em.enabled = true;
    }
}

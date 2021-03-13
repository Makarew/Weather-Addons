using UnityEngine;
using UnityEngine.SceneManagement;

public class spikeToPlay : MonoBehaviour
{
    public AudioSource source;
    public float spikeAmount = 10;

    private ParticleSystem ps;

    public float db;

    public float[] buffer = new float[128];

    private string oScene;

    public float lastdb = 0;
    private bool onActive = false;

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
        lastdb = db;

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


        //Calculate dB
        int length = (int)source.clip.length;

        buffer = source.GetOutputData(128, 1);

        db = Mathf.Round(AudioPower.ComputeDB(buffer, 0, ref length));

        if (db - lastdb > spikeAmount && db < -15 || db - lastdb > spikeAmount / 2 && db > -15 && lastdb > -15 || db - lastdb > spikeAmount / 3 && db > -10 && lastdb > -10 || db - lastdb > spikeAmount / 4 && db > -5 && lastdb > -5)
        {
            ps.Play();
        }
    }
}

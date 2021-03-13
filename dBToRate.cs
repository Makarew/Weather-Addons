using UnityEngine;
using UnityEngine.SceneManagement;

public class dBToRate : MonoBehaviour
{
    public AudioSource source;
    public float dbMin = -20f;
    public float dbMax = 0f;

    private ParticleSystem ps;
    public float rateMin = 0f;
    public float rateMax = 100f;
    private float range;

    public float db;
    private float dif;

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

        //Get the difference between dB min and max
        float nMin = dbMin;

        if (nMin < 0)
        {
            nMin *= -1;
        }

        dif = nMin + dbMax;

        //Get the difference between emission rate min and max
        range = rateMax - rateMin;

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

        if (db < dbMin)
            return;

        //Make dB positive
        float adb = new float();
        if (db < 0)
            adb = db * -1;
        else
        {
            adb = db;

            if (adb > dbMax)
                adb = dbMax;
        }

        //Put dB in range of dif
        float pdb = new float();
        if (adb != dbMax)
        {
            pdb = dbMax + adb;
        } else
        {
            pdb = dif;
        }

        //Convert dB to a percentage
        pdb /= dif;

        pdb -= 1f;
        pdb *= -1;

        if (pdb > 1f)
            pdb = 1f;

        //Use dB percentage to change emission rate starting at minimum rate
        float fillA = range * pdb;
        em.rateOverTime = rateMin + fillA;
    }
}

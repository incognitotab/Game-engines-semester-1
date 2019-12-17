using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundvisualizer : MonoBehaviour
{
     AudioSource source;
    [SerializeField] Material dottedline;
    public Camera backgroundeditor;
    public GameObject instantiator;
    public GameObject instantiatee;
    int counter;
   [SerializeField] List<GameObject> shapes = new List<GameObject>();
    List<Transform> sizes = new List<Transform>();
   [SerializeField] float[] soundsampling = new float[512];
    Transform boxshape;
    Color32 rand;
    Color32 randbehind;
    bool test;
    bool reset;
    bool deathset;
    float samplecode;
    bool finale;
    float measure;
    float measureuse;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        boxshape = GetComponent<Transform>();
        reset = true;
        deathset = false;
        measure = 0;
    }

    // Update is called once per frame
    void Update()
    {
        source.GetSpectrumData(soundsampling, 0, FFTWindow.Triangle);
        if (soundsampling[1] > measure)
        {
            measure = soundsampling[1];
            measureuse = 250 - (measure * 100);
            Debug.Log(measureuse);
        }
            
        Vector3 setdet = new Vector3(soundsampling[1], soundsampling[1], soundsampling[1])*measureuse;
        Vector3 retractor = Vector3.Lerp(boxshape.localScale, setdet, 0.5f);
        Quaternion rotational = Quaternion.Lerp(boxshape.rotation, Quaternion.Euler(soundsampling[1]*1000, soundsampling[2]*1000, soundsampling[3]*1000),3);
        boxshape.localScale = retractor;
        boxshape.localRotation = rotational;

        if (boxshape.localScale.x >= 6 && boxshape.localScale.x <=14 && test == true)
        {
            randbehind = dottedline.color;
            byte rnd1 =(byte) Random.Range(0, 255);
            byte rnd2 =(byte) Random.Range(0,255);
            byte rnd3 = (byte)Random.Range(0,255);
            rand = new Color32(rnd1, rnd2, rnd3,255);

            dottedline.color = rand;

            test = false;
            if (reset == true)
            {
                backgroundselector();
            }
          
        }
       if(boxshape.localScale.x < 20)
        {
            test = true;
        }
        backgroundorganiser();
    }

    void backgroundselector()
    {

        GameObject sphere = GameObject.Instantiate(instantiatee, instantiator.transform);
        sphere.GetComponent<Renderer>().material.color = randbehind;
        shapes.Add(sphere);
        samplecode = soundsampling[1]*1000;
        sizes.Add(sphere.transform);
        for(int i = 0; i<=shapes.Count; i++)
        {
            shapes[i].transform.position = instantiator.transform.position - new Vector3(0, 0, i);
            sizes[i] = shapes[i].transform;
            
        }
        deathset = false;
        reset = false;
    }
    
    void backgroundorganiser()
    {
       
            for (int i = 0; i <= shapes.Count; i++)
            {
            Vector3 expand;
            if (deathset == false)
            {
                expand = Vector3.Lerp(new Vector3(sizes[i].localScale.x, sizes[i].localScale.y, shapes[i].transform.localScale.z), new Vector3(sizes[i].localScale.x + samplecode, sizes[i].localScale.y + samplecode, shapes[i].transform.localScale.z), 3f * Time.deltaTime);
                shapes[i].transform.localScale = expand;
                if (shapes[i].transform.localScale.x >= sizes[i].localScale.x + samplecode)
                {
                    reset = true;
                }
            }
            if (shapes.Count==1  && deathset == true)
            {
                expand = Vector3.Lerp(shapes[i].transform.localScale, Vector3.zero, 5f * Time.deltaTime);
                shapes[i].transform.localScale = expand;
                reset = true;
                if (shapes[i].transform.localScale.x <= 5)
                {
                    finale = true;
                    deathset = false;
                    

                }
            }
            if (finale == true)
            {
                GameObject x = shapes[i];
                shapes.RemoveAt(i);
                sizes.RemoveAt(i);
                Destroy(x);
                finale = false;
            }

            if (shapes[i].transform.localScale.x > 750)
            {
                if (shapes.Count==1)
                {
                    deathset = true;
                }

                if (deathset==false)
                {
                    GameObject x = shapes[i];
                    shapes.RemoveAt(i);
                    sizes.RemoveAt(i);
                    Destroy(x);
                }
            }

            }
        
    }
  
}

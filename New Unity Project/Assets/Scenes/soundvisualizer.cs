using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundvisualizer : MonoBehaviour
{   //all the componenents that make up the script
    AudioSource source;
    [SerializeField] Material dottedline;
    [SerializeField] Material backgroundeditor;
    [SerializeField] GameObject instantiator;
    [SerializeField] GameObject instantiatee;
    [SerializeField] GameObject instantiatortwo;
    [SerializeField] GameObject instantiateetwo;
    //data holders for spawners
    List<GameObject> points = new List<GameObject>(15);
    List<GameObject> shapes = new List<GameObject>();
    List<Transform> sizes = new List<Transform>();
    [SerializeField] float[] soundsampling = new float[512];
    Vector3[] angleset = new Vector3[20];
    //normal variables for components
    Transform boxshape;
    Color32 rand;
    Color32 randbehind;
    bool test;
    bool reset;
    bool deathset;
    bool finale;
    float samplecode;
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
        //getting the sound data and saving it
        source.GetSpectrumData(soundsampling, 0, FFTWindow.Triangle);
        //mediating the data so that spectrum applies to all tracks and rather then the intensity of the sound the main aspect is the bpm
        if (soundsampling[1] > measure)
        {
            measure = soundsampling[1];
            measureuse = 250 - (measure * 100);
            
        }
        // Allowcates the data to size and rotation of the box    
        Vector3 setdet = new Vector3(soundsampling[1], soundsampling[1], soundsampling[1])*measureuse;
        Vector3 retractor = Vector3.Lerp(boxshape.localScale, setdet, 0.5f);
        Quaternion rotational = Quaternion.Lerp(boxshape.rotation, Quaternion.Euler(soundsampling[1]*1000, soundsampling[2]*1000, soundsampling[3]*1000),8*Time.deltaTime);
        boxshape.localScale = retractor;
        boxshape.localRotation = rotational;

        if (boxshape.localScale.x >= 6 && boxshape.localScale.x <=19 && test == true)
        {
            //dependent on the boxs size a plate is created and a randome color is associated
            randbehind = dottedline.color;
            byte rnd1 =(byte) Random.Range(0, 255);
            byte rnd2 =(byte) Random.Range(0,255);
            byte rnd3 = (byte)Random.Range(0,255);
            rand = new Color32(rnd1, rnd2, rnd3,255);

            dottedline.color = rand;
            backgroundeditor.color = rand;
        
            //locks so too many plates do not spawn and animations can play smoothly
            test = false;
            if (reset == true)
            {
                backgroundselector();
            }
          
        }
        //reseting lock so event can play again
       if(boxshape.localScale.x < 14)
        {
            test = true;
        }

        spiral();
        backgroundorganiser();
        
    }

    void backgroundselector()
    {
        // a new sphear is instatiated and added to a list
        GameObject sphere = GameObject.Instantiate(instantiatee, instantiator.transform);
        sphere.GetComponent<Renderer>().material.color = randbehind;
        shapes.Add(sphere);
        samplecode = soundsampling[1]*1000;
        sizes.Add(sphere.transform);
        //the plates are put one in front of the other so the color effect can occur and data logging to keep for use for a different instance
        for(int i = 0; i<=shapes.Count; i++)
        {
            shapes[i].transform.position = instantiator.transform.position - new Vector3(0, 0, i);
            sizes[i] = shapes[i].transform;
            
        }
        //resetting locks to minimise errors
        deathset = false;
        reset = false;
    }
    
    void backgroundorganiser()
    {
       //the plate scale system, each plate is moved with a lerp and the lerp is updated using the sound spectrum until it reaches a certain lenght except the last plate that minimises
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

    void spiral()
    {
       //the specels are instatiated and using trigonometry to make a guiding path that is also based on the sound. it also deletes itself if they go too far but it reinstatiates
        if (boxshape.localScale.x > 0.01 && boxshape.localScale.x < 4) ;
        {
            for (int i = 0; i <= 15; i++)
            {
                if (points.Count != 15)
                {
                    GameObject trangle = GameObject.Instantiate(instantiateetwo, instantiatortwo.transform);
                    
                    points.Add(trangle);
                    float ang = Random.value * 360;
                    Vector3 pos = new Vector3(instantiatortwo.transform.position.x + 5f * Mathf.Sin(ang * Mathf.Deg2Rad), instantiatortwo.transform.position.y + 5f * Mathf.Cos(ang * Mathf.Deg2Rad), instantiatortwo.transform.position.z);
                    angleset[i] = new Vector3(Mathf.Cos(ang), Mathf.Sin(ang), 0);
                    trangle.GetComponent<Transform>().localPosition = pos;
                    
                }
                


            }
            for(int i = 0; i < 15; i++)
                {

                points[i].GetComponent<Transform>().localPosition =Vector3.Lerp(points[i].GetComponent<Transform>().localPosition, points[i].GetComponent<Transform>().localPosition+ angleset[i],6f*Time.deltaTime);
               float blingo =  Vector3.Distance(points[i].GetComponent<Transform>().localPosition, instantiatortwo.transform.localPosition);
                if (blingo > 50)
                {
                   
                    GameObject x = points[i];
                    points.RemoveAt(i);
                    Destroy(x);
                }
                Debug.Log(blingo);
                }
        }

      
    }
  
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFT_Display_Bar : MonoBehaviour
{

    public float[] aveMag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Analyze sample data.
        int numPartitions = 16;
        aveMag = new float[numPartitions];
        float partitionIndx = 0;
        int numDisplayedBins = 512 / 2; //NOTE: we only display half the spectral data because the max displayable frequency is Nyquist (at half the num of bins)

        for (int i = 0; i < numDisplayedBins; i++)
        {
            if (i < numDisplayedBins * (partitionIndx + 1) / numPartitions)
            {
                aveMag[(int)partitionIndx] += FFT_Handler.spectrumData[i] / (512 / numPartitions);
            }
            else
            {
                partitionIndx++;
                i--;
            }
        }

        // scale and bound the average magnitude.
        for (int i = 0; i < numPartitions; i++)
        {
            aveMag[i] = (float)0.5 + aveMag[i] * 100;
            if (aveMag[i] > 100)
            {
                aveMag[i] = 100;
            }
        }

        for (int i = 1; i <= 16; i++)
        {
            if (gameObject.name == "FFTDisplayBar"+i)
            {
                GetComponent<RectTransform>().localScale = new Vector3(GetComponent<RectTransform>().localScale.x, aveMag[i-1], GetComponent<RectTransform>().localScale.z);
            }
        }
    }
}

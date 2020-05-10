using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FFT_Color_Floor : MonoBehaviour
{
    public float[] aveMag;
    public Material floor1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Analyze sample data.
        int numPartitions = 8;
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

        Color FFTColor = new Color();
        FFTColor.r =Mathf.Clamp(aveMag[3],0.6f,1f);
        FFTColor.g = Mathf.Clamp(aveMag[5], 0.6f, 1f);
        FFTColor.b = Mathf.Clamp(aveMag[7], 0.6f, 1f);
        FFTColor.a = 1;


        for (int i = 1; i <= 8; i++)
        {
            floor1.color= FFTColor;
        }
    }
}

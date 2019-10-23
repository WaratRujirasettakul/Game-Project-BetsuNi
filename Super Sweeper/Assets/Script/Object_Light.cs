using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class Object_Light: MonoBehaviour
{
    [Header("Reference")]
    public Light2D Shine;
    public Light2D Emitter;
    [Header("Variable")]
    public float MaxReduction = 0.5f;
    public float MaxIncrease = 0;
    public float RateSpeed = 1f;
    public float RateDamping = 0.1f;
    public float Strength = 300;
    public float Ignite = 0.3f;
    private void Start()
    {
        StartCoroutine(DoFlicker());
    }

    private IEnumerator DoFlicker()
    {
        while (true)
        {
            float Intensity_Target = Mathf.Lerp(Shine.intensity, Random.Range(1 - MaxReduction, 1 + MaxIncrease), Strength * Time.deltaTime);
            Shine.intensity = 0;
            Emitter.intensity = 0;
            yield return new WaitForSeconds(Random.Range(0, RateSpeed));
            Shine.intensity = Random.Range(0, Ignite);
            yield return new WaitForSeconds(Random.Range(0, RateSpeed));
            Shine.intensity = Intensity_Target;
            Emitter.intensity = Intensity_Target;
            yield return new WaitForSeconds(RateDamping);
        }
    }
}

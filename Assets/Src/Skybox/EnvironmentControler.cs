using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnvironmentControler : MonoBehaviour
{

    public TaggedEnvironmentsContainer editor_environments;

    public BottleTrigger bottleTrigger;

    public Smoke smoke;

    public GameObject currentSun;

    private Dictionary<string, Environment> environments;

    private Dictionary<string, string> potionEnvironment;

    public float delayLoadEnvironment;

    private Coroutine coroutine;

    private Coroutine lightCoroutine;

    public Book book;

    public PulseControler pulseControler;

    private Dictionary<string, List<Potion>> categoryPotion;

    public TaggedRecipeCategoryContainer taggedRecipeCategoryContainer;

    private Dictionary<string, List<Potion>> categoryRandomSelect;

    // Start is called before the first frame update

    IEnumerator LightTransition(float time, int steps, Color ambientColor, float reflectionIntensity)
    {
        var stepTime = time / steps;

        Vector4 renderAmbientColor = RenderSettings.ambientLight;
        var ambientColorStep = ((Vector4)ambientColor - renderAmbientColor) / steps;

        float renderReflectionIntensity = RenderSettings.reflectionIntensity;
        var reflectionIntensityStep = (reflectionIntensity - renderReflectionIntensity) / steps;

        for (int step = 0; step < steps; step++)
        {
            renderAmbientColor += ambientColorStep;
            renderReflectionIntensity += reflectionIntensityStep;

            RenderSettings.ambientLight = renderAmbientColor;
            RenderSettings.reflectionIntensity = renderReflectionIntensity;

            yield return new WaitForSeconds(stepTime);
        }
    }

    void LoadEnvironment(string name, float transitionTime = 0.0f)
    {
        if (environments.TryGetValue(name, out Environment env))
        {

            if (env.sun != null)
            {
                if (currentSun != null)
                {
                    Destroy(currentSun);
                    currentSun = null;
                }
                currentSun = Instantiate(env.sun);
            }

            if (env.panoramic != null)
                RenderSettings.skybox = env.panoramic;

            if (lightCoroutine != null)
                StopCoroutine(lightCoroutine);

            var steps = transitionTime == 0.0f ? 1 : 60;

            lightCoroutine = StartCoroutine(LightTransition(transitionTime, steps, env.ambientColor, env.reflectionIntensity));

            if (name != "Default")
                ChangeRecipe();

        }
    }

    void ChangeRecipe()
    {

        string category = "";

        switch (pulseControler.pulseStatus)
        {
            case PulseStatus.LOW: category = "LOW"; break;
            case PulseStatus.NORMAL: category = "NORM"; break;
            case PulseStatus.HIGH: category = "HIGH"; break;
        }

        Potion pot = GetRandomPotion(category);

        book.LoadTexture(pot.PotionName, Side.Left);
        book.LoadTexture(pot.PotionName, Side.Right);
    }

    IEnumerator DelayLoadEnvironment(string environment)
    {
        LoadEnvironment("Default", delayLoadEnvironment / 2);
        yield return new WaitForSeconds(delayLoadEnvironment);
        LoadEnvironment(environment, delayLoadEnvironment / 2);
    }

    void OnBottleThrowed(Potion potion)
    {
        if (potionEnvironment.TryGetValue(potion.PotionName, out string environmentName))
        {
            Color smokeColor = potion.color;
            smokeColor.a = 1.0f;
            // smokeColor.a = smoke.baseColor.a;
            smoke.Play(delayLoadEnvironment, smokeColor);

            if (coroutine != null)
                StopCoroutine(coroutine);

            coroutine = StartCoroutine(DelayLoadEnvironment(environmentName));

            Destroy(potion.gameObject);
        }
    }

    Potion GetRandomPotion(string category)
    {
        List<Potion> potions = categoryRandomSelect[category];

        if (potions.Count == 0)
        {
            potions = new List<Potion>(categoryPotion[category]);
            categoryRandomSelect[category] = potions;
        }

        int randomIndex = UnityEngine.Random.Range(0, potions.Count - 1);
        Potion pot = potions[randomIndex];
        potions.RemoveRange(randomIndex, 1);

        return pot;
    }

    void Start()
    {
        environments = new Dictionary<string, Environment>();
        potionEnvironment = new Dictionary<string, string>();
        foreach (var elem in editor_environments.elements)
        {
            environments[elem.tag] = elem.value;
            Potion pot = elem.value.potion;
            if (pot != null)
                potionEnvironment[pot.PotionName] = elem.tag;
        }

        categoryPotion = new Dictionary<string, List<Potion>>();

        categoryRandomSelect = new Dictionary<string, List<Potion>>();

        foreach (var elem in taggedRecipeCategoryContainer.elements)
        {
            categoryPotion[elem.tag] = elem.value;

            categoryRandomSelect[elem.tag] = new List<Potion>(elem.value);
        }

        bottleTrigger.PotionTriggered += OnBottleThrowed;

        LoadEnvironment("Base_1");

        ChangeRecipe();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DayManager : MonoBehaviour
{
    public static DayManager instance;
    public int DAYCOUNT { get { return dayCount; } }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else if (instance == null)
        {
            instance = this;
        }
    }
    int dayCount = 0;
    public void advanceDay()
    {
        dayCount++;
        Debug.Log("DAY ADVANCED");
        PlantManager.updateplants();

        StartCoroutine(daySequence());
    }
    float t = 0;
    float tmax = 1f;
    [SerializeField]
    Image dayimg;
    [SerializeField]
    Color fullblack;
    [SerializeField]
    Color fadeout;
    IEnumerator daySequence()
    {
        t = 0;
        dayimg.color = fadeout;
        while (t < tmax)
        {
            t += Time.deltaTime;
            dayimg.color = Color.Lerp(fadeout, fullblack, t / tmax);
            yield return null;
        }
        t = 0;
        dayimg.color = fullblack;
        while (t < tmax)
        {
            t += Time.deltaTime;
            dayimg.color = Color.Lerp(fullblack,fadeout, t / tmax);
            yield return null;
        }
        dayimg.color = fadeout;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            advanceDay();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}

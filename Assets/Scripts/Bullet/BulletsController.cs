using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsController : MonoBehaviour
{
    public static BulletsController instance;

    public Sprite[] sprites;
    public GameObject[] bulletsPrefab;
    public List<Bullets> bullets = new List<Bullets>();

    public bool isAddAntiAll,isAddAntiDepressants,isAddAntiHallucinogens,isAddAntiStimulants,isAddAntiMultipleEffect = false;

    [Header("Bullet")]
    public Bullets antiAll;
    public Bullets antiDepressants;
    public Bullets antiHallucinogens;
    public Bullets antiStimulants;
    public Bullets antiMultipleEffect;

    void Start()
    {
        instance = this;

        //Bullets antiAll = new Bullets();
        antiDepressants = new Bullets(Bullets.BulletsType.antiDepressants, "สลายยาเสพติดประเภท 1", 50, 5, "best bullet forever", sprites[0], bulletsPrefab[1]);
        antiHallucinogens = new Bullets(Bullets.BulletsType.antiHallucinogens, "สลายยาเสพติดประเภท 2", 20, 5, "boom", sprites[1], bulletsPrefab[2]);
        //Bullets antiStimulants = new Bullets();
        //Bullets antiMultipleEffect = new Bullets();

        if (isAddAntiAll)
        {
            
        }
        
        if (isAddAntiDepressants)
        {
            bullets.Add(antiDepressants);
        }
        
        if (isAddAntiHallucinogens)
        {
            bullets.Add(antiHallucinogens);
        }
        
        if (isAddAntiStimulants)
        {
            
        }

        if (isAddAntiMultipleEffect)
        {

        }
    }
}

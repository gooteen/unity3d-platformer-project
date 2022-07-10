using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//namespace buttonData
//{
[RequireComponent(typeof(PlayerMotion))]
public class InputProcessing : MonoBehaviour
{
    //private Vector3 direction;
    public bool disabled;
    public bool menuMode;
    public bool endLevelMenuMode;
    public bool endBossLevelMenuMode;
    private PlayerMotion pm;
    private Shooter shooter;
    private SoulprintCounter counter;
    [SerializeField] private AnimationHandler anim;
    [SerializeField] private float movementCap;
    [SerializeField] private MenuController menu;
    [SerializeField] private LevelController level;

    void Start()
    {
        menuMode = false;
        disabled = false;
        endLevelMenuMode = false;
        counter = GetComponent<SoulprintCounter>();
        pm = GetComponent<PlayerMotion>();
        //anim = GetComponent<AnimationHandler>();
        shooter = GetComponent<Shooter>();
    }

    void Update()
    {
        float hAxis = Input.GetAxis(Buttons.HORIZONTAL_AXIS);
        float hAxisRaw = Input.GetAxisRaw(Buttons.HORIZONTAL_AXIS);

        if (disabled)
        {
            hAxis = 0;
            hAxisRaw = 0;
        }
        else if (menuMode)
        {
            hAxis = 0;
            hAxisRaw = 0;
            if (Input.GetButtonDown(Buttons.JUMP_BUTTON) && !disabled)
            {
                menu.Resume();
                menuMode = false;
            }
            if (Input.GetButtonDown(Buttons.SLASH_BUTTON) && !disabled)
            {
                level.Reload();
            }
            if (Input.GetButtonDown(Buttons.FIRE_BUTTON) && !disabled)
            {
                menu.ToMenu();
            }
        }
        else if (endLevelMenuMode)
        {
            hAxis = 0;
            hAxisRaw = 0;
            if (Input.GetButtonDown(Buttons.SLASH_BUTTON) && !disabled)
            {
                level.Reload();
            }
            if (Input.GetButtonDown(Buttons.FIRE_BUTTON) && !disabled)
            {
                menu.ToMenu();
                menu.FileUpdate(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        else if (endBossLevelMenuMode)
        {
            hAxis = 0;
            hAxisRaw = 0;
            if (Input.GetButtonDown(Buttons.FIRE_BUTTON) && !disabled)
            {
                menu.ToMenu();
                menu.FileUpdateBoss(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
        else {

            if (Input.GetButtonDown(Buttons.JUMP_BUTTON) && !disabled)
            {
                pm.Jump();
            }

            if (Input.GetButtonDown(Buttons.FIRE_BUTTON) && !shooter.isCoolingDown && counter.Check() && !disabled)
            {
                anim.Cast();
                counter.SubSoulprint();
                //Debug.Log("Done");
                shooter.isCoolingDown = true;
                shooter.SetStartTime();
            }

            if (Input.GetButtonDown(Buttons.SLASH_BUTTON) && !disabled)
            {
                anim.Slash();
            }

            if (Input.GetButtonDown(Buttons.ESCAPE_BUTTON) && !disabled)
            {
                Debug.Log("Escaped pressed");
                menu.Pause();
                menuMode = true;
            }

            if (Mathf.Abs(hAxis) > movementCap && (!anim.GetSlashing() && !anim.GetCasting()))
                pm.Move(hAxis);
            anim.Flip(hAxisRaw);


            
        }
        anim.Run(hAxisRaw);
    }

        
    }

//}

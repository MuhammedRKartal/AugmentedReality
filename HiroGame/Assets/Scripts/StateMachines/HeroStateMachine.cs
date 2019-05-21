using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroStateMachine : MonoBehaviour
{
    public GameObject HeroCube;

    private BattleStateMachine BSM;
    public BaseHero hero;

    public enum TurnState
    {
        PROCESSING,
        ADDTOLIST,
        WAITING,
        SELECTING,
        ACTION,
        DEAD
    }

    static Animator animat; //To get animator

    public TurnState currentState; //To set states.

    //To use in ProgressBar
    private float curCooldown = 0f;
    private float maxCooldown = 7f;
    private Image ProgressBar;

    //HeroPanel
    private HeroPanelStats stats;
    public GameObject HeroPanel;
    private Transform HPSpacer;
    private Image HealthBar;
    private Image ManaBar;

    //To use in IENumerator
    public GameObject EnemyToAttack;
    public GameObject EnemyToAttackCube;
    private bool actionStarted = false;
    private Vector3 startPosition;
    private float animSpeed = 10f;

    //public GameObject spellButton;
    private int index;
    //Dead
    private bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        //Load the heros info(vitality,str, etc.).
        LoadInfo.LoadAllInfo();
        hero.strength = GameInfo.Strenght;
        hero.vitality = GameInfo.Vitality;
        hero.intelligence = GameInfo.Intelligence;
        hero.toughness = GameInfo.Toughness;

        hero.curATK = hero.strength * 0.6f;
        hero.baseHP += hero.vitality*10;
        hero.curHP += hero.vitality*10;

        //create panel, fill in informations
        HPSpacer = GameObject.Find("Canvas").transform.Find("HeroPanel").transform.Find("HPSpacer");
        CreateHeroPanel();
    
        //curCooldown = Random.Range(0, 2.5f);
        animat = GetComponent<Animator>();
        startPosition = transform.position;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        currentState = TurnState.PROCESSING;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case (TurnState.PROCESSING):
                UpgradeProgressBar();
                break;
            case (TurnState.ADDTOLIST):
                BSM.HeroesToManage.Add(this.gameObject);
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):
                //idle
                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):
                if (!alive)
                {
                    return;
                }
                else
                {
                    //change tag of hero to not add it performlist
                    this.gameObject.tag = "DeadHero";

                    //not attackable by enemy
                    BSM.HeroesInGame.Remove(this.gameObject);

                    //not able to manage this hero
                    BSM.HeroesToManage.Remove(this.gameObject);

                    //reset gui
                    BSM.ActionPanel.SetActive(false);
                    BSM.EnemySelectPanel.SetActive(false);

                    //remove item from performlist
                    if (BSM.HeroesInGame.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                            {
                                BSM.PerformList.Remove(BSM.PerformList[i]);
                            }
                            if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.PerformList[i].AttackersTarget = BSM.HeroesInGame[Random.Range(0, BSM.HeroesInGame.Count)];
                            }
                        }
                    }
                    //play the dead animation
                    animat.Play("Die");

                    //reset battlestates
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
                    

                    alive = false;
                }
                break;
        }
    }

    void UpgradeProgressBar()
    {
        curCooldown += Time.deltaTime;
        float calcCooldown = curCooldown / maxCooldown;
        ProgressBar.transform.localScale = new Vector3(Mathf.Clamp(calcCooldown, 0, 1), 
            ProgressBar.transform.localScale.y,
            ProgressBar.transform.localScale.z); ;
        if(curCooldown >= maxCooldown)
        {
            currentState = TurnState.ADDTOLIST;
        }
    }

    private IEnumerator TimeForAction()
    {
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        //Debug.Log("BSM.isSpell is: " + BSM.isSpell);
        if (BSM.isSpell)
        {
            index = BSM.selectedSpellNumber;
            if(index == 0)
            {
                float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
                animat.Play("Attack");
                EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calcDamage);
            }
            else if(index == 1)
            {
                float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
                animat.Play("Attack2");
                EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calcDamage);
            }
            else if (index == 2)
            {
                float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
                animat.Play("Attack3");
                EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calcDamage);
            }

            
            if(EnemyToAttack.name == "ElfMageGreen")
            {
                hero.SpellsObj[index].gameObject.SetActive(true);
                yield return new WaitForSeconds(4.5f);
                hero.SpellsObj[index].gameObject.SetActive(false);
            }
            else if (EnemyToAttack.name == "ElfMageEmerald")
            {
                hero.SpellsObj[index+3].gameObject.SetActive(true);
                yield return new WaitForSeconds(4.5f);
                hero.SpellsObj[index+3].gameObject.SetActive(false);
            }
            else if (EnemyToAttack.name == "ElfMageRed")
            {
                hero.SpellsObj[index + 6].gameObject.SetActive(true);
                yield return new WaitForSeconds(4.5f);
                hero.SpellsObj[index + 6].gameObject.SetActive(false);
            }
            else if (EnemyToAttack.name == "WarChimera")
            {
                hero.SpellsObj[index].gameObject.SetActive(true);
                yield return new WaitForSeconds(4.5f);
                hero.SpellsObj[index].gameObject.SetActive(false);
            }
            
            
        }
        else
        {
            //animate the enemy near the hero to attack
            //Debug.Log("Enemy to attack is: " + EnemyToAttack.name);

            Vector3 enemyPosition = new Vector3((EnemyToAttackCube.transform.position.x ),
                EnemyToAttackCube.transform.position.y,
                EnemyToAttackCube.transform.position.z);
            if (EnemyToAttack.name == "WarChimera")
            {
                enemyPosition = new Vector3((EnemyToAttackCube.transform.position.x),
                EnemyToAttackCube.transform.position.y ,
                EnemyToAttackCube.transform.position.z);
            }
            //this.transform.LookAt(enemyPosition);
            while (MoveTowardsEnemy(enemyPosition))
            {
                enemyPosition = new Vector3((EnemyToAttackCube.transform.position.x ),
                EnemyToAttackCube.transform.position.y,
                EnemyToAttackCube.transform.position.z);
                if (EnemyToAttack.name == "WarChimera")
                {
                    enemyPosition = new Vector3((EnemyToAttackCube.transform.position.x),
                EnemyToAttackCube.transform.position.y ,
                EnemyToAttackCube.transform.position.z);
                }
                animat.Play("Run");
                yield return null;
            }


            //do damage
            DoDamage();

            //wait abit
            yield return new WaitForSeconds(1.3f);


            startPosition = HeroCube.transform.position;

            //animate back to startposition
            Vector3 firstPosition = startPosition;
            while (MoveBackwardsStart(firstPosition))
            {
                startPosition = HeroCube.transform.position;
                firstPosition = startPosition;
                yield return null;
            }
        }

        BSM.isSpell = false;
        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0);


        //reset bsm -->wait
        if(BSM.battleStates != BattleStateMachine.PerformAction.WIN && BSM.battleStates != BattleStateMachine.PerformAction.LOSE)
        {
            BSM.battleStates = BattleStateMachine.PerformAction.WAIT;
            //reset this enemy state
            curCooldown = 0f;
            currentState = TurnState.PROCESSING;
        }
        else
        {
            currentState = TurnState.WAITING;
        }

        

        //end coroutine
        actionStarted = false;

       
    }
    

    //Burada animasyonu haraket ettirebilirsin
    private bool MoveTowardsEnemy(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    private bool MoveBackwardsStart(Vector3 target)
    {
        return target != (transform.position = Vector3.MoveTowards(transform.position, target, animSpeed * Time.deltaTime));
    }

    public void TakeDamage(float getDamageAmount)
    {
        getDamageAmount = getDamageAmount - hero.toughness*1.05f;
        hero.curHP -= getDamageAmount;
        //Debug.Log("Current HP is : " +hero.curHP);
        animat.Play("Hit");
        if(hero.curHP <= 0)
        {
            hero.curHP = 0;
            currentState=TurnState.DEAD;
            //Debug.Log("Hero died");
            animat.Play("Die");
            animat.GetComponent<Animator>().enabled = false;
        }
        UpdateHeroPanel();
    }

    public void DoDamage()
    {
        float calcDamage = hero.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        animat.Play("Attack");
        EnemyToAttack.GetComponent<EnemyStateMachine>().TakeDamage(calcDamage);
    }

    void CreateHeroPanel()
    {
        HeroPanel = Instantiate(HeroPanel) as GameObject;
        stats = HeroPanel.GetComponent<HeroPanelStats>();
        stats.HeroName.text = hero.theName;
        stats.HeroHP.text = hero.curHP + "/" + hero.baseHP;
        stats.HeroMP.text = hero.curMP + "/" + hero.baseMP;

        ProgressBar = stats.ProgressBar;
        HealthBar = stats.HealthBar;
        ManaBar = stats.ManaBar;
        HeroPanel.transform.SetParent(HPSpacer, false);
    }

    void UpdateHeroPanel()
    {
        stats.HeroHP.text = hero.curHP + "/" + hero.baseHP;
        stats.HeroMP.text = hero.curMP + "/" + hero.baseMP;
        if(hero.curHP == 0)
        {
            stats.HeroHP.text = "Dead";
        }
        HealthBar.fillAmount = hero.curHP / hero.baseHP;
        ManaBar.fillAmount = hero.curMP / hero.baseMP;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyStateMachine : MonoBehaviour
{
    public GameObject HeroCube;
    public GameObject EnemyCube;

    private BattleStateMachine BSM;
    public BaseEnemy enemy;

    public enum TurnState
    {
        PROCESSING,
        CHOOSEACTION,
        WAITING,
        ACTION,
        DEAD
    }

    public TurnState currentState;

    //To use in ProgressBar
    private float curCooldown = 0f;
    private float maxCooldown = 7f;
    private Image ProgressBar;

    //To use in IENumerator
    private bool actionStarted = false;
    public GameObject heroToAttack;
    private Vector3 startposition;//Go back to start
    private float animSpeed = 10f;

    static Animator animat;//To use Animations

    //HeroPanel
    private EnemyPanelStats stats;
    public GameObject EnemyPanel;
    private Transform EPSpacer;
    private Image HealthBar;
    private Image ManaBar;

    //alive
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        //create panel, fill in informations
        EPSpacer = GameObject.Find("Canvas").transform.Find("EnemyPanel").transform.Find("EPSpacer");
        CreateEnemyPanel();

        currentState = TurnState.PROCESSING;
        BSM = GameObject.Find("BattleManager").GetComponent<BattleStateMachine>();
        //startposition = transform.position;
        startposition = EnemyCube.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        switch (currentState)
        {
            case (TurnState.PROCESSING):
                
                UpgradeProgressBar();
                break;
            case (TurnState.CHOOSEACTION):
                ChooseAction();
                currentState = TurnState.WAITING;
                break;
            case (TurnState.WAITING):
                //idle state
                break;
            case (TurnState.ACTION):
                StartCoroutine(TimeForAction());
                break;
            case (TurnState.DEAD):
                //if its not alive return
                if (!alive)
                {
                    return;
                }
                //if its still alive at dead state make it dead
                else
                {
                    //change tag of enemy so it will be attackable
                    this.gameObject.tag = "DeadEnemy";
                    //not attackable by heroes
                    BSM.EnemiesInGame.Remove(this.gameObject);

                    if (BSM.EnemiesInGame.Count > 0)
                    {
                        for (int i = 0; i < BSM.PerformList.Count; i++)
                        {
                            if (BSM.PerformList[i].AttackersGameObject == this.gameObject)
                            {
                                BSM.PerformList.Remove(BSM.PerformList[i]);
                            }
                            if (BSM.PerformList[i].AttackersTarget == this.gameObject)
                            {
                                BSM.PerformList[i].AttackersTarget = BSM.EnemiesInGame[Random.Range(0, BSM.EnemiesInGame.Count)];
                            }
                        }
                    }
                    
                    animat.Play("Die");
                    

                    alive = false;
                    BSM.EnemyButtons(); //reset enemy buttons

                    //check alive
                    BSM.battleStates = BattleStateMachine.PerformAction.CHECKALIVE;
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
        if (curCooldown >= maxCooldown)
        {
            currentState = TurnState.CHOOSEACTION;
        }
    }

    void ChooseAction()
    {
        HandleTurns myAttack = new HandleTurns();
        myAttack.Attacker = enemy.theName;
        myAttack.Type = "Enemy";
        myAttack.AttackersGameObject = this.gameObject;
        myAttack.AttackersTarget = BSM.HeroesInGame[Random.Range(0, BSM.HeroesInGame.Count)];

        int attackNumber = Random.Range(0, enemy.attacks.Count);
        myAttack.choosenAttack = enemy.attacks[attackNumber];
        /*
        Debug.Log(this.gameObject.name + " has choosen " +
            myAttack.choosenAttack.attackName + " and do " + myAttack.choosenAttack.attackDamage +
            " and costs " + myAttack.choosenAttack.manaCost);
        */ 
        BSM.CollectActions(myAttack);
    }

    private IEnumerator TimeForAction()
    {
        animat = BSM.PerformList[0].AttackersGameObject.GetComponent<Animator>();
        
        if (actionStarted)
        {
            yield break;
        }
        actionStarted = true;

        if (BSM.enemyisSpell)
        {
            //Select a random spell and do attack

            if(enemy.theName == "Furion" || enemy.theName == "Resifion" || enemy.theName == "Spilutiel")
            {
                enemy.SpellsObj[0].SetActive(true);
                float calcDamage = enemy.curATK + enemy.Spells[0].attackDamage;
                animat.Play("Attack");
                heroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calcDamage);

                yield return new WaitForSeconds(4f); //Wait 4 secs for the effect. 
                enemy.SpellsObj[0].SetActive(false); //deactive the effect
            }
            if(enemy.theName == "Heroltrazsa")
            {
                enemy.SpellsObj[Random.Range(0, 3)].SetActive(true);
                float calcDamage = enemy.curATK + enemy.Spells[Random.Range(0, 3)].attackDamage;
                animat.Play("Attack");
                heroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calcDamage);

                yield return new WaitForSeconds(6f); //Wait 6 secs for the effect. 
                enemy.SpellsObj[Random.Range(0, 3)].SetActive(false); //deactive the effect
            }
            
        }

        else
        {
            //Find the hero's first position to move it.
            Vector3 heroPosition = new Vector3((HeroCube.transform.position.x),
                HeroCube.transform.position.y,
                HeroCube.transform.position.z);
            //dragons positions a little bit different because its flying
            if (enemy.theName == "Heroltrazsa")
            {
                heroPosition = new Vector3((HeroCube.transform.position.x ),
                    HeroCube.transform.position.y,
                    HeroCube.transform.position.z + 1.5f);
            }
            
            //Checks the position every time to go right position.
            while (MoveTowardsEnemy(heroPosition))
            {
                heroPosition = new Vector3((HeroCube.transform.position.x ),
                HeroCube.transform.position.y,
                HeroCube.transform.position.z);
                if (enemy.theName == "Heroltrazsa")
                {
                    heroPosition = new Vector3((HeroCube.transform.position.x + 1f),
                        HeroCube.transform.position.y,
                        HeroCube.transform.position.z + 1.5f);
                }
                animat.Play("Run");
                yield return null;
            }

            //do damage
            DoDamage();

            //wait abit for the animations
            yield return new WaitForSeconds(1.3f);

            //Set the start position again for Augmented Reality cam movements.
            startposition = EnemyCube.transform.position;

            //animate back to startposition

            //Checks start position to turn back right place, (because of AR.)
            Vector3 firstPosition = startposition;
            while (MoveBackwardsStart(firstPosition))
            {
                startposition = EnemyCube.transform.position;
                firstPosition = startposition;
                yield return null;
            }

        }
        BSM.enemyisSpell = false; //making the enemy's attack type not spell for the next turn.

        //remove this performer from the list in BSM
        BSM.PerformList.RemoveAt(0);

        //reset bsm -->wait
        BSM.battleStates = BattleStateMachine.PerformAction.WAIT;

        //end coroutine
        actionStarted = false;

        //reset this enemy state
        curCooldown = 0f;
        currentState = TurnState.PROCESSING;
    }


    //Do we need to move towards a little bit more?
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
        Animator x = this.gameObject.GetComponent<Animator>();
        enemy.curHP -= getDamageAmount;
        //Debug.Log("Current HP is : " + enemy.curHP);
        x.Play("Hit");
        if (enemy.curHP <= 0)
        {
            enemy.curHP = 0;
            currentState = TurnState.DEAD;

            //Debug.Log("Hero died");
            x.Play("Die");
            x.GetComponent<Animator>().enabled = false;
            
        }
        UpdateEnemyPanel();
    }


    //Do melee attack and give damage.
    void DoDamage()
    {
        float calcDamage = enemy.curATK + BSM.PerformList[0].choosenAttack.attackDamage;
        animat.Play("Attack");
        heroToAttack.GetComponent<HeroStateMachine>().TakeDamage(calcDamage);
        
    }

    //Create enemys health bar etc.
    void CreateEnemyPanel()
    {
        EnemyPanel = Instantiate(EnemyPanel) as GameObject;
        stats = EnemyPanel.GetComponent<EnemyPanelStats>();
        stats.EnemyName.text = enemy.theName;
        stats.EnemyHP.text = enemy.curHP + "/" + enemy.baseHP;
        stats.EnemyMP.text = enemy.curMP + "/" + enemy.baseMP;

        ProgressBar = stats.ProgressBar;
        HealthBar = stats.HealthBar;
        ManaBar = stats.ManaBar;
        EnemyPanel.transform.SetParent(EPSpacer, false);
    }

    //Update the enemys current health etc.
    void UpdateEnemyPanel()
    {
        stats.EnemyHP.text = enemy.curHP + "/" + enemy.baseHP;
        stats.EnemyMP.text = enemy.curMP + "/" + enemy.baseMP;
        if (enemy.curHP == 0)
        {
            stats.EnemyHP.text = "Dead";
        }
        HealthBar.fillAmount = enemy.curHP / enemy.baseHP;
        ManaBar.fillAmount = enemy.curMP / enemy.baseMP;
    }
}
 
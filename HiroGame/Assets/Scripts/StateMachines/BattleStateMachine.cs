using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleStateMachine : MonoBehaviour
{

    public enum PerformAction
    {
        WAIT,
        TAKEACTION,
        PERFORMACTION,
        CHECKALIVE,
        WIN,
        LOSE
    }

    public PerformAction battleStates;

    public List<HandleTurns> PerformList = new List<HandleTurns>();

    public List<GameObject> HeroesInGame = new List<GameObject>();
    public List<GameObject> EnemiesInGame = new List<GameObject>();


    public enum HeroGUI
    {
        ACTIVATE,
        WAITING,
        INPUT1, //basic attack
        INPUT2, //enemy input
        DONE
    }

    public HeroGUI HeroInput;

    public List<GameObject> HeroesToManage = new List<GameObject>();
    private HandleTurns HeroChoice; //

    public GameObject enemyButton;
    public Transform Spacer;

    public GameObject ActionPanel;
    public GameObject EnemySelectPanel;
    public GameObject SpellPanel;

    //Spell Attack
    public Transform actionSpacer;
    public Transform spellSpacer;
    public GameObject actionButton;
    public GameObject spellButton;
    private List<GameObject> atkBtns = new List<GameObject>();
    public bool isSpell = false;
    public bool enemyisSpell = false;
    int randomEnemyAttack;

    public List<GameObject> spells = new List<GameObject>();
    public int selectedSpellNumber;

    private List<GameObject> enemyBtns = new List<GameObject>();
    private string heroname;
    private string youwin;
    private string youlose;

    // Start is called before the first frame update
    void Start()
    {

        foreach (GameObject spell in spells)
        {
            spell.SetActive(false);
        }

        battleStates = PerformAction.WAIT;
        HeroesInGame.AddRange(GameObject.FindGameObjectsWithTag("Hero"));
        heroname = HeroesInGame[0].name;
        youwin = "YouWin" + heroname;
        youlose = "YouLose" + heroname;

        EnemiesInGame.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        HeroInput = HeroGUI.ACTIVATE;

        ActionPanel.SetActive(false);
        EnemySelectPanel.SetActive(false);
        SpellPanel.SetActive(false);

        EnemyButtons();
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (battleStates)
        {
            case (PerformAction.WAIT):
                if(PerformList.Count> 0)
                {
                    battleStates = PerformAction.TAKEACTION;
                }

                break;

            case (PerformAction.TAKEACTION):
                GameObject performer = GameObject.Find(PerformList[0].AttackersGameObject.name);
                //Debug.Log(GameObject.Find(PerformList[0].AttackersGameObject.name));
                if (PerformList[0].Type == "Enemy")
                {
                    //Debug.Log(performer);
                    EnemyStateMachine ESM = performer.GetComponent<EnemyStateMachine>();

                    //Check hero is dead
                    for(int i = 0; i<HeroesInGame.Count; i++) // we don't need a for loop because we have just 1 hero.
                    {
                        //if attackers target is hero and not dead go on.
                        if (PerformList[0].AttackersTarget == HeroesInGame[i]) 
                        {
                            ESM.heroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                            break;
                        }
                        //if the attackers target is hero and dead select another hero and go on
                        //this is not necessary too.
                        else
                        {
                            PerformList[0].AttackersTarget = HeroesInGame[Random.Range(0, HeroesInGame.Count)];
                            ESM.heroToAttack = PerformList[0].AttackersTarget;
                            ESM.currentState = EnemyStateMachine.TurnState.ACTION; 
                        }
                    }

                    ESM.heroToAttack = PerformList[0].AttackersTarget;
                    ESM.currentState = EnemyStateMachine.TurnState.ACTION;
                
                }
                randomEnemyAttack = Random.Range(0, 2);
                //Debug.Log(randomEnemyAttack);
                if (PerformList[0].Type == "Hero")
                {
                   // Debug.Log("Hero is ready");
                    HeroStateMachine HSM = performer.GetComponent<HeroStateMachine>();
                    HSM.EnemyToAttack = PerformList[0].AttackersTarget;
                    HSM.EnemyToAttackCube = PerformList[0].AttackersTargetCube;
                    HSM.currentState = HeroStateMachine.TurnState.ACTION;
                }
                if(PerformList[0].Type == "Enemy")
                {
                    if(randomEnemyAttack == 0)
                    {
                        enemyisSpell = true;
                    }
                    else
                    {
                        enemyisSpell = false;
                    }
                }
                battleStates = PerformAction.PERFORMACTION;

                break;

            case (PerformAction.PERFORMACTION):

                break;
            case (PerformAction.CHECKALIVE):
                if(HeroesInGame.Count < 1)
                {
                    battleStates = PerformAction.LOSE;
                    //Lose the battle
                }
                else if(EnemiesInGame.Count < 1)
                {
                    battleStates = PerformAction.WIN;
                    //Win the battle
                }
                else
                {
                    clearAttackPanel();
                    HeroInput = HeroGUI.ACTIVATE;
                }
                break;

            case (PerformAction.LOSE):
                Debug.Log("You LOSE the battle");
                
                SceneManager.LoadScene(youlose);
                break;

            case (PerformAction.WIN):
                Debug.Log("You win the battle");
                for(int i =0; i<HeroesInGame.Count; i++)
                {
                    HeroesInGame[i].GetComponent<HeroStateMachine>().currentState = HeroStateMachine.TurnState.WAITING;
                }
                SceneManager.LoadScene(youwin);
                break;


        }

        switch (HeroInput)
        {
            case (HeroGUI.ACTIVATE):
                if(HeroesToManage.Count > 0)
                {
                    HeroChoice = new HandleTurns();

                    ActionPanel.SetActive(true);
                    CreateAttackButtons();
                    HeroInput = HeroGUI.WAITING;
                }
                break;
            case (HeroGUI.WAITING):
                //idle
                break;
            case (HeroGUI.DONE):
                HeroInputDone();
                break;
        }
    }

    public void CollectActions(HandleTurns input)
    {
        PerformList.Add(input);
    }

    public void EnemyButtons()
    {
        //cleanup
        foreach(GameObject enemyBtn in enemyBtns)
        {
            Destroy(enemyBtn);
        }
        enemyBtns.Clear();

        foreach(GameObject enemy in EnemiesInGame)
        {
            GameObject newButton = Instantiate(enemyButton) as GameObject;
            EnemySelectButton button = newButton.GetComponent<EnemySelectButton>();

            EnemyStateMachine curEnemy = enemy.GetComponent<EnemyStateMachine>();

            Text buttonText = newButton.GetComponentInChildren<Text>();
            buttonText.text = curEnemy.enemy.theName;

            button.EnemyPrefab = enemy;
            button.EnemyCube = curEnemy.EnemyCube;

            newButton.transform.SetParent(Spacer);
            enemyBtns.Add(newButton);
        }
    }

    //Attack
    public void Input1()
    {
        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.AttackersGameObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";
        HeroChoice.choosenAttack = HeroesToManage[0].GetComponent<HeroStateMachine>().hero.attacks[0]; 

        ActionPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }

    //Enemy Selection
    public void Input2(GameObject choosenEnemy , GameObject choosenEnemyCube)
    {
        HeroChoice.AttackersTarget = choosenEnemy;
        HeroChoice.AttackersTargetCube = choosenEnemyCube;
        HeroInput = HeroGUI.DONE;
    }

    public void Input3()
    {
        isSpell = true;
        ActionPanel.SetActive(false);
        SpellPanel.SetActive(true);
    }

    //Magic attack choose;
    public void Input4(BaseAttack choosenSpell )
    {

        HeroChoice.Attacker = HeroesToManage[0].name;
        HeroChoice.AttackersGameObject = HeroesToManage[0];
        HeroChoice.Type = "Hero";

        HeroChoice.choosenAttack = choosenSpell;

        selectedSpellNumber = choosenSpell.spellNumber; 

        //Debug.Log(choosenSpell);
        SpellPanel.SetActive(false);
        EnemySelectPanel.SetActive(true);
    }



    void HeroInputDone()
    {
        PerformList.Add(HeroChoice);

        //clear the panel
        clearAttackPanel();

        HeroesToManage.RemoveAt(0);
        HeroInput = HeroGUI.ACTIVATE;
    }


    void clearAttackPanel()
    {
        EnemySelectPanel.SetActive(false);
        ActionPanel.SetActive(false);
        SpellPanel.SetActive(false);
        foreach (GameObject atkBtn in atkBtns)
        {
            Destroy(atkBtn);
        }
        atkBtns.Clear();
    }

    void CreateAttackButtons()
    {
        GameObject AttackButton = Instantiate(actionButton) as GameObject;
        Text AttackButtonText = AttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
        AttackButtonText.text = "Attack";
        AttackButton.GetComponent<Button>().onClick.AddListener(() => Input1());
        AttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(AttackButton);

        GameObject SpellAttackButton = Instantiate(actionButton) as GameObject;
        Text SpellAttackButtonText = SpellAttackButton.transform.Find("Text").gameObject.GetComponent<Text>();
        SpellAttackButtonText.text = "Spell";
        SpellAttackButton.GetComponent<Button>().onClick.AddListener(() => Input3());
        SpellAttackButton.transform.SetParent(actionSpacer, false);
        atkBtns.Add(SpellAttackButton);

        if (HeroesToManage[0].GetComponent<HeroStateMachine>().hero.Spells.Count > 0)
        {
            foreach (BaseAttack spell in HeroesToManage[0].GetComponent<HeroStateMachine>().hero.Spells) 
            {
                GameObject SpellButton = Instantiate(spellButton) as GameObject;
                Text MagicButtonText = SpellButton.transform.Find("Text").gameObject.GetComponent<Text>();
                MagicButtonText.text = spell.attackName;
                AttackButton1 ATB = SpellButton.GetComponent<AttackButton1>();
                ATB.spellAttackToPerform = spell;
                //ATB.spellIndexToPerform = spell.spellNumber;
                SpellButton.transform.SetParent(spellSpacer, false);
                atkBtns.Add(SpellButton);
            }
        }
        else
        {
            SpellAttackButton.GetComponent<Button>().interactable = false;
        }
    }

}

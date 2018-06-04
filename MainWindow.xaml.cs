using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Media;

namespace Final_Project
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string ver = "0.15";
        string appName = "ATKOS Map Generator";
        bool gameStarted = false;
        bool devCheats = false;
        // define player
        int playerX = 7;
        int playerY = 7;
        int pCon;
        int pStr;
        int pAgi;
        int pInt;
        int pDef;
        int pCha;
        int pHealth = 100;
        int pHealthMax = 100;
        int pStamina = 100;
        int pStaminaMax = 100;
        int pMagic = 100;
        int pMagicMax = 100;
        int pExp = 0;
        int nextExp = 100;
        int pLvl = 0;
        int statPoints = 0;
        int gold;
        string pClass = "Adventurer";
        int inventoryUsed = 0;
        int inventoryMax = 100;
        bool nameInput = false;
        public string playerName = "";
        string returnType = "Back";
        // define name generation lists
        string[] enchantments = {
            "pierce",
            "flame",
            "shock",
            "frost",
            "toxic",
            "vampire",
            "exhaustion",
        };
        string[] bladeCondition = {
            "broken",
            "cracking",
            "worn",
            "dull",
            "new",
            "agile",
            "sharp",
            "deadly",
            "masterwork",
        };
        string[] rangedCondition = {
            "malformed",
            "shoddy",
            "damaged",
            "worn",
            "basic",
            "nimble",
            "forceful",
            "superior",
            "legendary",
        };
        string[] qualityList = {
            "terrible",
            "average",
            "decent",
            "good",
            "exceptional",
            "powerful",
            "mighty",
            "valiant",
            "supreme",
        };
        // define icons
        BitmapImage[] iconset = {
            new BitmapImage(new Uri("./images/player.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/plains.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/ocean.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/mountain.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/desert.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/valley.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/forest.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/hill.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/island.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/camp.png", UriKind.Relative)),
            new BitmapImage(new Uri("./images/town.png", UriKind.Relative)),
        };
        const int terrainSize = 15;
        String[,] terrain = new String[terrainSize, terrainSize];
        Town[,] towns = new Town[terrainSize, terrainSize];
        // define monsters
        bool[,] monsterSpawn = new bool[terrainSize, terrainSize];
        bool[,] monsterAlive = new bool[terrainSize, terrainSize];
        int[,] monsterDeath = new int[terrainSize, terrainSize];
        Entities[] monsters = {
            new Entities(1,1,1,1,1,1,1,"Slime", new List<Skill>()),
        };
        // define skills
        List<Skill> learnedSkills = new List<Skill>();
        List<Skill> emptySkills = new List<Skill>();
        Skill Heal = new Skill("holy", "support", 1);
        Skill Fireball = new Skill("fire", "offensive", 1);
        Skill Shock = new Skill("lightning", "offensive", 1);
        Skill Frost = new Skill("ice", "offensive", 1);
        // define items
        List<Item>[,] droppedItems = new List<Item>[terrainSize,terrainSize];
        Item[] items = {
            new Item("potion", "heals you for 10% of your health.", 10, "restore", 20),
            new Item("small meal", "restores 10% of your food.", 3, "food", 6),
            new Item("small gems", "absorb 40 mana from them!", 30, "mana", 40)
            
        };
        ItemStack[] inventory = new ItemStack[30];
        // define initial
        int townCount = 0;
        bool gameEnable = false;
        int currentMonster;
        Entities combatMonster = new Entities(0,0,0,0,0,0,0,"none", new List<Skill>());
        int monsterHealth;
        char[] vowels = "aeiou".ToCharArray();
        Random random = new Random();
        string[] consoleSave = new String[20];
        string lastInput;
        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string nameInput)
        {
            InitializeComponent();
            this.WindowState = WindowState.Normal;
            actionOutput.AppendText("Say /help for a list of commands." + Environment.NewLine);
            if (nameInput != null)
            {
                playerName = nameInput;
            }
            else
            {
                playerName = "Player";
            }
            for (int x = 0; x < droppedItems.GetLength(0); x++)
            {
                for (int y = 0; y < droppedItems.GetLength(1); y++)
                {
                    droppedItems[y, x] = new List<Item>();
                }
            }
            this.Title = appName + " - " + ver;
            FreezeButton(false);
            buttonStart.IsEnabled = true;
            this.PreviewKeyDown += new KeyEventHandler(MainWindow_PreviewKeyDown);
            PlayerCoordinates();
            UpdateInfo();
            UpdateBars();
        }

        private void LearnSkill(Skill newSkill)
        {
            learnedSkills.Add(newSkill);
        }

        void MainWindow_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (!actionInput.IsKeyboardFocused & gameEnable)
            {
                string controls;
                switch (e.Key)
                {
                    case Key.D:
                        ButtonDown(buttonRight);
                        controls = "default";
                        Controls(controls);
                        break;
                    case Key.Right:
                        ButtonDown(buttonRight);
                        controls = "secondary";
                        Controls(controls);
                        break;
                    case Key.S:
                        ButtonDown(buttonDown);
                        controls = "default";
                        Controls(controls);
                        break;
                    case Key.Down:
                        ButtonDown(buttonDown);
                        controls = "secondary";
                        Controls(controls);
                        break;
                    case Key.A:
                        ButtonDown(buttonLeft);
                        controls = "default";
                        Controls(controls);
                        break;
                    case Key.Left:
                        ButtonDown(buttonLeft);
                        controls = "secondary";
                        Controls(controls);
                        break;
                    case Key.W:
                        ButtonDown(buttonUp);
                        controls = "default";
                        Controls(controls);
                        break;
                    case Key.Up:
                        ButtonDown(buttonUp);
                        controls = "secondary";
                        Controls(controls);
                        break;
                    case Key.Space:
                        ButtonDown(buttonAction);
                        break;
                    case Key.Enter:
                        ButtonDown(buttonAction);
                        break;
                    case Key.LeftCtrl:
                        ButtonDown(buttonBack);
                        break;
                    case Key.RightCtrl:
                        ButtonDown(buttonBack);
                        break;
                    case Key.LeftShift:
                        ButtonDown(buttonInventory);
                        break;
                    case Key.RightShift:
                        ButtonDown(buttonInventory);
                        break;
                    case Key.Y:
                        ButtonDown(buttonYes);
                        break;
                    case Key.N:
                        ButtonDown(buttonNo);
                        break;
                    case Key.R:
                        ButtonDown(buttonStart);
                        break;
                    case Key.Z:
                        ButtonDown(buttonWait);
                        break;
                    case Key.OemPeriod:
                        ButtonDown(buttonWait);
                        break;
                    default:
                        break;
                }
            }
            else if (e.Key == Key.Enter && actionInput.IsKeyboardFocused)
            {
                EnterCheck();
            }
        }
        private void MainWindow_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (!actionInput.IsKeyboardFocused & gameEnable)
            {
                string controls;
                switch (e.Key)
                {
                    case Key.D:
                        ButtonUp(buttonRight);
                        controls = "default";
                        Controls(controls);
                        MoveRight();
                        break;
                    case Key.Right:
                        ButtonUp(buttonRight);
                        controls = "secondary";
                        Controls(controls);
                        MoveRight();
                        break;
                    case Key.S:
                        ButtonUp(buttonDown);
                        controls = "default";
                        Controls(controls);
                        MoveDown();
                        break;
                    case Key.Down:
                        ButtonUp(buttonDown);
                        controls = "secondary";
                        Controls(controls);
                        MoveDown();
                        break;
                    case Key.A:
                        ButtonUp(buttonLeft);
                        controls = "default";
                        Controls(controls);
                        MoveLeft();
                        break;
                    case Key.Left:
                        ButtonUp(buttonLeft);
                        controls = "secondary";
                        Controls(controls);
                        MoveLeft();
                        break;
                    case Key.W:
                        ButtonUp(buttonUp);
                        controls = "default";
                        Controls(controls);
                        MoveUp();
                        break;
                    case Key.Up:
                        ButtonUp(buttonUp);
                        controls = "secondary";
                        Controls(controls);
                        MoveUp();
                        break;
                    case Key.Space:
                        ButtonUp(buttonAction);
                        DoAction();
                        break;
                    case Key.Enter:
                        ButtonUp(buttonAction);
                        EnterCheck();
                        break;
                    case Key.LeftCtrl:
                        ButtonUp(buttonBack);
                        Back();
                        break;
                    case Key.RightCtrl:
                        ButtonUp(buttonBack);
                        Back();
                        break;
                    case Key.LeftShift:
                        ButtonUp(buttonInventory);
                        OpenInventory();
                        break;
                    case Key.RightShift:
                        ButtonUp(buttonInventory);
                        OpenInventory();
                        break;
                    case Key.R:
                        ButtonUp(buttonStart);
                        if (buttonStart.IsEnabled)
                        {
                            GameStart();
                        }
                        break;
                    case Key.Z:
                        ButtonUp(buttonWait);
                        Wait();
                        break;
                    case Key.OemPeriod:
                        ButtonUp(buttonWait);
                        Wait();
                        break;
                    default:
                        break;
                }
            }
        }

        private void ButtonDown(Button current)
        {
            if (!actionInput.IsKeyboardFocused)
            {
                current.Background = Brushes.DarkGray;
            }
        }

        private void ButtonUp(Button current)
        {
            if (!actionInput.IsKeyboardFocused)
            {
                current.Background = Brushes.White;
            }
        }
        // Check for values within range
        private bool IsInBounds(int x, int y)
        {
            Point point = new Point(x, y);
            if (point.X >= terrainSize || point.X < 0)
            {
                return false;
            }
            else if (point.Y >= terrainSize || point.Y < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        // Add item to inventory
        private void AddToInventory(Item item)
        {

        }

        // Control Restraints
        private void ResponseToggle(bool enabled)
        {
            buttonYes.IsEnabled = enabled;
            buttonNo.IsEnabled = enabled;
        }


        private void MovementToggle(bool enabled)
        {
            buttonUp.IsEnabled = enabled;
            buttonRight.IsEnabled = enabled;
            buttonDown.IsEnabled = enabled;
            buttonLeft.IsEnabled = enabled;
            buttonAction.IsEnabled = enabled;
        }

        private void FreezeButton(bool enabled)
        {
            buttonUp.IsEnabled = enabled;
            buttonLeft.IsEnabled = enabled;
            buttonRight.IsEnabled = enabled;
            buttonDown.IsEnabled = enabled;
            buttonAction.IsEnabled = enabled;
            buttonStart.IsEnabled = enabled;
            buttonInventory.IsEnabled = enabled;
            buttonBack.IsEnabled = enabled;
            buttonYes.IsEnabled = enabled;
            buttonNo.IsEnabled = enabled;
            actionInput.IsEnabled = enabled;
            buttonWait.IsEnabled = enabled;
        }


        // Action Responses
        private void MoveRight()
        {
            bool reaction = false;
            if (playerX < 14 && monsterAlive[playerX, playerY] == false)
            {
                if (terrain[playerX + 1, playerY] == "ocean")
                {
                    actionOutput.AppendText(playerName + " cannot move this way" + Environment.NewLine);
                    actionOutput.ScrollToEnd();
                }
                else if (terrain[playerX + 1, playerY] == "town" || terrain[playerX + 1, playerY] == "camp")
                {
                    LandOnTown();
                    double x = Canvas.GetLeft(character);
                    Canvas.SetLeft(character, x + 30);
                    playerX++;
                    actionOutput.AppendText(playerName + " arrived in " + towns[playerX, playerY].name + Environment.NewLine);
                    PlayerCoordinates();
                    UpdateInfo();
                    actionOutput.ScrollToEnd();
                    reaction = true;
                    TownUI(playerX, playerY);
                }
                else
                {
                    actionOutput.AppendText(playerName + " moved East" + Environment.NewLine);
                    double x = Canvas.GetLeft(character);
                    Canvas.SetLeft(character, x + 30);
                    playerX++;
                    PlayerCoordinates();
                    UpdateInfo();
                    StaminaDrain(TerrainType());
                    actionOutput.ScrollToEnd();
                    reaction = true;
                }
                if (monsterAlive[playerX, playerY] == true)
                {
                    StartCombat();
                }
            }
            else
            {

                string output = playerName + " cannot move this way" + Environment.NewLine;
                if (monsterAlive[playerX, playerY])
                {
                    output = playerName + " must kill the " + monsters[currentMonster].objName + " first!" + Environment.NewLine;
                }
                actionOutput.AppendText(output);
            }

            if (reaction)
            {
                pExp++;
                Check();
                NextTurn();
            }
        }

        private void MoveDown()
        {
            bool reaction = false;
            if (playerY < 14 && monsterAlive[playerX, playerY] == false)
            {
                if (terrain[playerX, playerY + 1] == "ocean")
                {
                    actionOutput.AppendText(playerName + " cannot move this way" + Environment.NewLine);
                    actionOutput.ScrollToEnd();
                }
                else if (terrain[playerX, playerY + 1] == "town" || terrain[playerX, playerY + 1] == "camp")
                {
                    LandOnTown();
                    double y = Canvas.GetTop(character);
                    Canvas.SetTop(character, y + 30);
                    playerY++;
                    actionOutput.AppendText(playerName + " arrived in " + towns[playerX, playerY].name + Environment.NewLine);
                    PlayerCoordinates();
                    UpdateInfo();
                    actionOutput.ScrollToEnd();
                    reaction = true;
                    TownUI(playerX, playerY);
                }
                else
                {
                    actionOutput.AppendText(playerName + " moved South" + Environment.NewLine);
                    double y = Canvas.GetTop(character);
                    Canvas.SetTop(character, y + 30);
                    playerY++;
                    PlayerCoordinates();
                    UpdateInfo();
                    StaminaDrain(TerrainType());
                    actionOutput.ScrollToEnd();
                    reaction = true;
                }
                if (monsterAlive[playerX, playerY] == true)
                {
                    StartCombat();
                }
            }
            else
            {
                string output = playerName + " cannot move this way" + Environment.NewLine;
                if (monsterAlive[playerX, playerY])
                {
                    output = playerName + " must kill the " + monsters[currentMonster].objName + " first!" + Environment.NewLine;
                }
                actionOutput.AppendText(output);
            }
            if (reaction)
            {
                pExp++;
                Check();
                NextTurn();
            }
        }

        private void MoveLeft()
        {
            bool reaction = false;
            if (playerX > 0 && monsterAlive[playerX, playerY] == false)
            {
                if (terrain[playerX - 1, playerY] == "ocean")
                {
                    actionOutput.AppendText(playerName + " cannot move this way" + Environment.NewLine);
                    actionOutput.ScrollToEnd();
                }
                else if (terrain[playerX - 1, playerY] == "town" || terrain[playerX - 1, playerY] == "camp")
                {
                    LandOnTown();
                    double x = Canvas.GetLeft(character);
                    Canvas.SetLeft(character, x - 30);
                    playerX--;
                    actionOutput.AppendText(playerName + " arrived in " + towns[playerX, playerY].name + Environment.NewLine);
                    PlayerCoordinates();
                    UpdateInfo();
                    actionOutput.ScrollToEnd();
                    reaction = true;
                    TownUI(playerX, playerY);
                }
                else
                {
                    actionOutput.AppendText(playerName + " moved West" + Environment.NewLine);
                    double x = Canvas.GetLeft(character);
                    Canvas.SetLeft(character, x - 30);
                    playerX--;
                    PlayerCoordinates();
                    UpdateInfo();
                    StaminaDrain(TerrainType());
                    actionOutput.ScrollToEnd();
                    reaction = true;
                }
                if (monsterAlive[playerX, playerY] == true)
                {
                    StartCombat();
                }
            }
            else
            {
                string output = playerName + " cannot move this way" + Environment.NewLine;
                if (monsterAlive[playerX, playerY])
                { 
                    output = playerName + " must kill the " + monsters[currentMonster].objName + " first!" + Environment.NewLine;
                }
                actionOutput.AppendText(output);
            }
            if (reaction)
            {
                pExp++;
                Check();
                NextTurn();
            }
        }

        private void MoveUp()
        {
            bool reaction = false;
            if (playerY > 0 && monsterAlive[playerX, playerY] == false)
            {
                if (terrain[playerX, playerY - 1] == "ocean")
                {
                    actionOutput.AppendText(playerName + " cannot move this way" + Environment.NewLine);
                    actionOutput.ScrollToEnd();
                }
                else if (terrain[playerX, playerY - 1] == "town" || terrain[playerX, playerY - 1] == "camp")
                {
                    LandOnTown();
                    double y = Canvas.GetTop(character);
                    Canvas.SetTop(character, y - 30);
                    playerY--;
                    actionOutput.AppendText(playerName + " arrived in " + towns[playerX, playerY].name + Environment.NewLine);
                    PlayerCoordinates();
                    UpdateInfo();
                    actionOutput.ScrollToEnd();
                    reaction = true;
                    TownUI(playerX, playerY);
                }
                else
                {
                    actionOutput.AppendText(playerName + " moved North" + Environment.NewLine);
                    double y = Canvas.GetTop(character);
                    Canvas.SetTop(character, y - 30);
                    playerY--;
                    PlayerCoordinates();
                    UpdateInfo();
                    StaminaDrain(TerrainType());
                    actionOutput.ScrollToEnd();
                    reaction = true;
                }
                if (monsterAlive[playerX, playerY] == true)
                {
                    StartCombat();
                }
            }
            else
            {
                string output = playerName + " cannot move this way" + Environment.NewLine;
                if (monsterAlive[playerX, playerY])
                {
                    output = playerName + " must kill the " + monsters[currentMonster].objName + " first!" + Environment.NewLine;
                }
                actionOutput.AppendText(output);
            }
            if (reaction)
            {
                pExp++;
                Check();
                NextTurn();
            }
        }

        private int TerrainType()
        {
            if (terrain[playerX, playerY] == "mountain")
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }

        private void DoAction()
        {
            if (monsterAlive[playerX, playerY] == true)
            {
                MonsterTurn();
            }
            else
            {
                actionOutput.AppendText(playerName + " did something!" + Environment.NewLine);
            }
            actionOutput.ScrollToEnd();
        }

        private void Wait()
        {
            if (pHealth < (pHealthMax - 2))
            {
                StaminaDrain(1);
                pHealth += 2;
            }
            else
            {
                StaminaDrain(1);
                pHealth = pHealthMax;
            }
            actionOutput.ScrollToEnd();
            UpdateBars();
            UpdateInfo();
        }

        private void Back()
        {
            actionOutput.ScrollToEnd();
        }

        private void OpenInventory()
        {
            actionOutput.AppendText(playerName + " has " + gold + " gold." + Environment.NewLine);
            actionOutput.ScrollToEnd();
        }

        private void ShowStats()
        {
            actionOutput.AppendText("Stats" + Environment.NewLine + "============" + Environment.NewLine + playerName + "'s Vitality: " + pCon + Environment.NewLine + playerName + "'s Strength: " + pStr + Environment.NewLine + playerName + "'s Agility: " + pAgi + Environment.NewLine + playerName + "'s Intelligence: " + pInt + Environment.NewLine + playerName + "'s Defense: " + pDef + Environment.NewLine + playerName + "'s Charisma: " + pCha + Environment.NewLine + playerName + "'s Experience: " + pExp + Environment.NewLine + playerName + "'s Level: " + pLvl + Environment.NewLine);
            actionOutput.ScrollToEnd();
        }

        // Click Action
        private void buttonRight_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            MoveRight();
        }

        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            MoveDown();
        }

        private void buttonLeft_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            MoveLeft();
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            MoveUp();
        }

        private void buttonAction_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            DoAction();
        }

        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            string controls = "tertiary";
            Controls(controls);
            Back();
        }

        private void buttonStart_Click(object sender, RoutedEventArgs e)
        {
            GameStart();
        }

        private void buttonStart_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonStart_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonWait_Click(object sender, RoutedEventArgs e)
        {
            Wait();
        }

        // Config
        private void TownUI(int x, int y)
        {

        }

        private void StartCombat()
        {
            int monster = random.Next(monsters.Length);
            currentMonster = monster;
            combatMonster = monsters[currentMonster];
            monsterHealth = (combatMonster.objCon * 10);
            AddConsoleQuick("A " + monsters[monster].objName + " appeared!");
        }

        private void MonsterTurn()
        {
            int diceRoll = random.Next(0, 21);
            int bonus = 0;
            AddConsoleQuick(playerName + " rolled a " + diceRoll);
            if (diceRoll > 15 && diceRoll != 20)
            {
                bonus = pStr;
            }
            else if (diceRoll == 20)
            {
                bonus = pStr * 2;
            }
            else if (diceRoll < 3)
            {
                bonus = -2;
            }
            bool pHit = true;
            if (random.Next(0, 21) < combatMonster.objAgi && diceRoll != 20)
            {
                pHit = false;
            }
            // succesful hit
            if (pHit)
            {
                monsterHealth -= ((pStr * 5) - combatMonster.objDef + bonus);
                if (bonus == pStr)
                {
                    AddConsoleQuick(playerName + " dealt double damage to " + combatMonster.objName + " for " + ((pStr * 5) - combatMonster.objDef + bonus) + " damage");
                }
                else if (bonus == pStr * 2)
                {
                    AddConsoleQuick(playerName + " dealt critical damage to " + combatMonster.objName + " for " + ((pStr * 5) - combatMonster.objDef + bonus) + " damage");
                }
                else if (bonus == -2)
                {
                    AddConsoleQuick(playerName + " grazed the " + combatMonster.objName + " for " + ((pStr * 5) - combatMonster.objDef + bonus) + " damage");
                }
                else
                {
                    AddConsoleQuick(playerName + " struck the " + combatMonster.objName + " for " + ((pStr * 5) - combatMonster.objDef) + " damage");
                }
                if (monsterHealth <= 0)
                {
                    monsterAlive[playerX, playerY] = false;
                    AddConsoleQuick(monsters[currentMonster].objName + " was slain by " + playerName);
                    int exp = monsters[currentMonster].objLevel * monsters[currentMonster].objCon;
                    AddConsoleQuick(playerName + " gained " + exp + " experience!");
                    pExp += exp;
                    Check();
                    Reward loot = MonsterDeath();
                    if (loot.gold != 0)
                    {
                        AddConsoleQuick(monsters[currentMonster].objName + " dropped " + loot.gold + " gold!");
                        gold += loot.gold;
                    }
                    else if (loot.HasItem)
                    {
                        AddConsoleQuick(monsters[currentMonster].objName + " dropped " + loot.item.name + "!" + Environment.NewLine + "'/get' and then '" + loot.item.name + "' to pick it up!");
                        droppedItems[playerX, playerY].Add(loot.item);
                    }
                }
                else
                {
                    AddConsoleQuick(monsters[currentMonster].objName + " has " + monsterHealth + " left!");
                }
            }
            // miss
            else
            {
                AddConsoleQuick(playerName + " missed the " + monsters[currentMonster].objName);
            }
            if (monsterAlive[playerX,playerY])
            {
                diceRoll = random.Next(0, 21);
                int damageDealt;
                if (diceRoll == 20)
                {
                    damageDealt = combatMonster.objStr * 2;
                }
                else if (diceRoll >= 12 && diceRoll <= 19)
                {
                    damageDealt = combatMonster.objStr * (int)1.5;
                }
                else if (diceRoll < 4)
                {
                    damageDealt = 1;
                }
                else
                {
                    damageDealt = combatMonster.objStr;
                }
                damageDealt = damageDealt - pDef;
                if (damageDealt <= 0)
                {
                    damageDealt = 1;
                }
                bool hit = true;
                if (random.Next(0, 21) < pAgi)
                {
                    hit = false;
                }
                AddConsoleQuick(monsters[currentMonster].objName + " rolled a " + diceRoll);
                if (hit)
                {
                    AddConsoleQuick(monsters[currentMonster].objName + " dealt " + damageDealt + " damage to " + playerName);
                    pHealth = pHealth - damageDealt;
                }
                else
                {
                    AddConsoleQuick(monsters[currentMonster].objName + " missed " + playerName + "!");
                }
                if (pHealth <= 0)
                {
                    DeathConfiguration();
                }
            }
            UpdateBars();
            UpdateInfo();
        }

        private Reward MonsterDeath()
        {
            Item reward = null;
            string rewardtype = "";
            string returnname = "";
            string enchant = "";
            string description = "";
            bool HasItem = false;
            int value = 0;
            int cReward = 0;
            if (random.Next(0, 2) == 0)
            {
                cReward = (random.Next(0, 11) * monsters[currentMonster].objLevel) + random.Next(0, 9);
            }
            else
            {
                 switch(random.Next(0,3))
                 {
                    case 0:
                        // weapons
                        int r = random.Next(0, 4);
                        switch (r)
                        {
                            case 0:
                                rewardtype = "sword";
                                int q = random.Next(0, (bladeCondition.Length - 1));
                                if (random.Next(0, 4) >= 2)
                                {
                                    enchant = " of " + enchantments[random.Next(0, enchantments.Length - 1)];
                                }
                                returnname = bladeCondition[q] + " sword" + enchant;
                                break;
                            case 1:
                                rewardtype = "spear";
                                q = random.Next(0, (bladeCondition.Length - 1));
                                if (random.Next(0, 4) >= 2)
                                {
                                    enchant = " of " + enchantments[random.Next(0, enchantments.Length - 1)];
                                }
                                returnname = bladeCondition[q] + " spear" + enchant;
                                break;
                            case 2:
                                rewardtype = "axe";
                                q = random.Next(0, (bladeCondition.Length - 1));
                                if (random.Next(0, 4) >= 2)
                                {
                                    enchant = " of " + enchantments[random.Next(0, enchantments.Length - 1)];
                                }
                                returnname = bladeCondition[q] + " axe" + enchant;
                                break;
                            case 3:
                                rewardtype = "bow";
                                q = random.Next(0, (rangedCondition.Length - 1));
                                if (random.Next(0, 4) >= 2)
                                {
                                    enchant = " of " + enchantments[random.Next(0, enchantments.Length - 1)];
                                }
                                returnname = rangedCondition[q] + " bow" + enchant;
                                break;
                        }
                        value = random.Next(0, 9) + (10 * monsters[currentMonster].objLevel);
                        char[] word = qualityList[random.Next(0, qualityList.Length - 1)].ToCharArray();
                        string ana = "a";
                        for (int i = 0; i < vowels.Length; i++)
                        {
                            if (word[0] == vowels[i])
                            {
                                ana = "an";
                            }
                        }

                        description = ana + " " + qualityList[monsters[currentMonster].objLevel] + " " + rewardtype + ".";
                        reward = new Item(returnname, description, value, rewardtype, monsters[currentMonster].objLevel);
                        HasItem = true;
                        break;
                    case 1:
                        // items
                        int check = random.Next(0, 9) + (10 * monsters[currentMonster].objLevel);
                        if (check >= items.Length)
                        {
                            check = random.Next(0, items.Length - 1);
                        }
                        reward = items[check];
                        HasItem = true;
                        break;
                    case 2:
                        // armor
                        int a = random.Next(0, 4);
                        switch (a)
                        {
                            case 0:
                                rewardtype = "head";
                                break;
                            case 1:
                                rewardtype = "torso";
                                break;
                            case 2:
                                rewardtype = "legs";
                                break;
                            case 3:
                                rewardtype = "feet";
                                break;
                        }
                        value = random.Next(0, 9) + (10 * monsters[currentMonster].objLevel);
                        word = qualityList[random.Next(0, qualityList.Length - 1)].ToCharArray();
                        ana = "a";
                        for (int i = 0; i < vowels.Length; i++)
                        {
                            if (word[0] == vowels[0])
                            {
                                ana = "an";
                            }
                        }
                        description = ana + " " + qualityList[monsters[currentMonster].objLevel] + " " + rewardtype + " protective piece.";
                        reward = new Item(returnname, description, value, rewardtype, monsters[currentMonster].objLevel);
                        HasItem = true;
                        break;
                 }
            }
            return new Reward(reward, cReward, HasItem);
        }

        private void GameStart()
        {
            buttonStart.IsEnabled = false;
            if (gameStarted)
            {
                for (int x = 0; x < 15; x++)
                {
                    for (int y = 0; y < 15; y++)
                    {
                        terrain[x, y] = "plains";
                        monsterDeath[x, y] = 0;
                    }
                }
                FreezeButton(true);
                ResponseToggle(false);
                buttonStart.IsEnabled = false;
                buttonStart.Content = "Restart [R]";
                gameStarted = true;
                gameEnable = true;
                pHealthMax = 100;
                pMagicMax = 100;
                pStaminaMax = 100;
                pMagic = 100;
                pExp = 0;
                pStamina = 100;
                pAgi = 1;
                pStr = 1;
                pCon = 1;
                pInt = 1;
                pDef = 1;
                pCha = 1;
                Canvas.SetTop(character, 210);
                Canvas.SetLeft(character, 210);
                pClass = "Adventurer";
                inventoryUsed = 0;
                inventoryMax = 100;
                playerX = 7;
                playerY = 7;
                MapGeneration();
                MapCreation();
                buttonStart.IsEnabled = true;
            }
            else
            {
                FreezeButton(true);
                ResponseToggle(false);
                buttonStart.Content = "Restart [R]";
                gameStarted = true;
                gameEnable = true;
                pHealth = 100;
                pMagic = 100;
                pExp = 0;
                pStamina = 100;
                pAgi = 1;
                pStr = 1;
                pCon = 1;
                pInt = 1;
                pDef = 1;
                pCha = 1;
                pClass = "Adventurer";
                inventoryUsed = 0;
                inventoryMax = 100;
                playerX = 7;
                playerY = 7;
                MapGeneration();
                MapCreation();
                new Player(canvasDisplay, iconset[0], playerX, playerY, character);
                Canvas.SetZIndex(character, 2);
                for (int x = 0; x < (terrainSize - 1); x++)
                {
                    for (int y = 0; y < (terrainSize - 1); y++)
                    {
                        monsterDeath[x, y] = 0;
                    }
                }
            }
            UpdateStats();
            UpdateInfo();
            UpdateBars();
        }
        private void Controls(string controls)
        {
            if (controls == "default")
            {
                buttonRight.Content = "D";
                buttonDown.Content = "S";
                buttonLeft.Content = "A";
                buttonUp.Content = "W";
                buttonYes.Content = "Yes [Y]";
                buttonNo.Content = "No [N]";
                buttonInventory.Content = "Inventory [Shift]";
                buttonStart.Content = "Restart [R]";
                buttonBack.Content = returnType + " [Ctrl]";
                buttonWait.Content = "Wait [Z]";
            }
            if (controls == "secondary")
            {
                buttonRight.Content = "→";
                buttonDown.Content = "↓";
                buttonLeft.Content = "←";
                buttonUp.Content = "↑";
                buttonYes.Content = "Yes [Y]";
                buttonNo.Content = "No [N]";
                buttonInventory.Content = "Inventory [Shift]";
                buttonStart.Content = "Restart [R]";
                buttonBack.Content = returnType + " [Ctrl]";
                buttonWait.Content = "Wait [.]";
            }
            if (controls == "tertiary")
            {
                buttonRight.Content = "";
                buttonDown.Content = "";
                buttonLeft.Content = "";
                buttonUp.Content = "";
                buttonYes.Content = "Yes";
                buttonNo.Content = "No";
                buttonInventory.Content = "Inventory";
                buttonStart.Content = "Restart";
                buttonBack.Content = returnType;
                buttonWait.Content = "Wait";
            }
        }

        private void EnterCheck()
        {
            if (actionInput.IsFocused)
            {
                lastInput = (actionInput.Text.ToLower());
                if (nameInput && (actionInput.Text != ""))
                {
                    actionOutput.AppendText("> " + actionInput.Text + Environment.NewLine);
                    actionOutput.AppendText(actionInput.Text + " is now set as player name" + Environment.NewLine);
                    playerName = actionInput.Text;
                    actionInput.Text = "";
                    nameInput = false;
                }
                else
                {
                    if (actionInput.Text != "")
                    {
                        actionOutput.AppendText("> " + actionInput.Text + Environment.NewLine);
                        actionInput.Text = "";
                        commandCheck();
                    }
                }
            }
            else
            {
                if (gameStarted)
                {
                    DoAction();
                }
            }
        }

        private void commandCheck()
        {
            string input = lastInput;
            actionOutput.ScrollToEnd();
            if (nameInput == false)
            {
                switch (input)
                {
                    case ("/help"):
                        if (devCheats)
                        {
                            actionOutput.AppendText("Commands" + Environment.NewLine);
                            actionOutput.AppendText("============" + Environment.NewLine);
                            actionOutput.AppendText("/inv - Opens your Inventory" + Environment.NewLine);
                            actionOutput.AppendText("/name - Sets a player name" + Environment.NewLine);
                            actionOutput.AppendText("/n or /s or /w or /e or Move a player in the inputted direction" + Environment.NewLine);
                            actionOutput.AppendText("/cls - Clears this object of all text history" + Environment.NewLine);
                            actionOutput.AppendText("/stats - Shows" + playerName + "'s character stats" + Environment.NewLine);
                            actionOutput.AppendText("/close - Returns user to the main menu" + Environment.NewLine);
                            actionOutput.AppendText("/cheats - Disables testing cheats" + Environment.NewLine + Environment.NewLine);
                            actionOutput.AppendText("Cheats" + Environment.NewLine);
                            actionOutput.AppendText("============" + Environment.NewLine);
                            actionOutput.AppendText("/stamina - Maxes out the players stamina stat" + Environment.NewLine);
                            actionOutput.AppendText("/health - Maxes out the players health stat" + Environment.NewLine);
                            actionOutput.AppendText("/mana - Maxes out the players magic stat" + Environment.NewLine);
                            actionOutput.AppendText("/level - Initiates level up script" + Environment.NewLine);

                        }
                        else
                        {
                            actionOutput.AppendText("Commands" + Environment.NewLine);
                            actionOutput.AppendText("============" + Environment.NewLine);
                            actionOutput.AppendText("/inv - Opens your Inventory" + Environment.NewLine);
                            actionOutput.AppendText("/name - Sets a player name" + Environment.NewLine);
                            actionOutput.AppendText("/n or /s or /w or /e or Move a player in the inputted direction" + Environment.NewLine);
                            actionOutput.AppendText("/cls - Clears this object of all text history" + Environment.NewLine);
                            actionOutput.AppendText("/stats - Shows" + playerName + "'s character stats" + Environment.NewLine);
                            actionOutput.AppendText("/close - Returns user to the main menu" + Environment.NewLine);
                            actionOutput.AppendText("/cheats - Enables testing cheats" + Environment.NewLine);
                        }
                        break;
                    case ("/inv"):
                        OpenInventory();
                        break;
                    case ("/stats"):
                        ShowStats();
                        break;
                    case ("/name"):
                        actionOutput.AppendText("Type your character name" + Environment.NewLine);
                        nameInput = true;
                        break;
                    case ("/n"):
                        MoveUp();
                        break;
                    case ("/s"):
                        MoveDown();
                        break;
                    case ("/e"):
                        MoveRight();
                        break;
                    case ("/w"):
                        MoveLeft();
                        break;
                    case ("/close"):
                        Close();
                        break;
                    case ("/cls"):
                        actionOutput.Text = "Say /help for a list of commands." + Environment.NewLine;
                        break;
                    case ("/cheats"):
                        if (devCheats)
                        {
                            devCheats = false;
                            actionOutput.AppendText("Cheats Disabled" + Environment.NewLine);
                        }
                        else
                        {
                            devCheats = true;
                            actionOutput.AppendText("Cheats Enabled" + Environment.NewLine);
                        }
                        break;
                    case ("/stamina"):
                        if (devCheats)
                        {
                            actionOutput.AppendText(playerName + "'s stamina maxed" + Environment.NewLine);
                            pStamina = pStaminaMax;
                            UpdateBars();
                            break;
                        }
                        else
                        {
                            actionOutput.AppendText("Unrecognized Command" + Environment.NewLine);
                            break;
                        }
                    case ("/health"):
                        if (devCheats)
                        {
                            actionOutput.AppendText(playerName + "'s health maxed" + Environment.NewLine);
                            pHealth = pHealthMax;
                            UpdateBars();
                            break;
                        }
                        else
                        {
                            actionOutput.AppendText("Unrecognized Command" + Environment.NewLine);
                            break;
                        }
                    case ("/mana"):
                        if (devCheats)
                        {
                            actionOutput.AppendText(playerName + "'s mana maxed" + Environment.NewLine);
                            pMagic = pMagicMax;
                            UpdateBars();
                            break;
                        }
                        else
                        {
                            actionOutput.AppendText("Unrecognized Command" + Environment.NewLine);
                            break;
                        }
                    case ("/level"):
                        if (devCheats)
                        {
                            LevelUp();
                        }
                        else
                        {
                            actionOutput.AppendText("Unrecognized Command" + Environment.NewLine);
                            break;
                        }
                        break;
                    default:
                        actionOutput.AppendText("Unrecognized Command" + Environment.NewLine);
                        break;
                }
                actionOutput.ScrollToEnd();
            }
        }

        private void mainScreen_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!actionInput.IsMouseOver)
            {
                actionOutput.Focus();
            }
        }

        private void actionInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (actionInput.Text.Contains(" "))
            {
                actionInput.Text = actionInput.Text.Remove(actionInput.Text.Length - 1);
                actionInput.AppendText("_");
                actionInput.SelectionStart = actionInput.Text.Length;
            }
        }

        private void AddConsoleQuick(string message)
        {
            actionOutput.AppendText(message + Environment.NewLine);
        }

        private void UpdateBars()
        {
            barHealth.Value = pHealth;
            barHealth.ToolTip = "(" + pHealth + "/" + pHealthMax + ") Health";
            barHealth.Maximum = pHealthMax;
            barStamina.Value = pStamina;
            barStamina.ToolTip = "(" + pStamina + "/" + pStaminaMax + ") Stamina";
            barStamina.Maximum = pStaminaMax;
            barMagic.Value = pMagic;
            barMagic.ToolTip = "(" + pMagic + "/" + pMagicMax + ") Magic";
            barMagic.Maximum = pMagicMax;
            barExperience.Value = pExp;
            barExperience.ToolTip = "(" + pExp + "/" + nextExp + ") Experience until Level " + (pLvl + 1);
            barExperience.Maximum = nextExp;
        }

        private void UpdateInfo()
        {
            characterInfo.Text = playerName + ", a level " + pLvl + " " + pClass + Environment.NewLine + "Inventory: (" + inventoryUsed + "/" + inventoryMax + ")" + Environment.NewLine + "Location: (" + (playerX + 1) + ", " + (playerY + 1) + ")";
        }

        // Checks and Number Setting
        private void StaminaDrain(int intensity)
        {
            if (pStamina > 0)
            {
                int stamDrain = ((intensity * 3) - pAgi);
                if (stamDrain < 0)
                {
                    stamDrain = 0;
                    UpdateBars();
                }

                if (pHealth - stamDrain <= 0)
                {
                    pStamina = 0;
                    UpdateBars();
                    AddConsoleQuick(playerName + " is out of stamina!");
                }
                else
                {
                    pStamina = pStamina - stamDrain;
                    UpdateBars();
                }
            }
            else
            {
                pStamina = 0;
                HealthDrain(intensity);
                UpdateBars();
            }
        }

        private void HealthDrain(int intensity)
        {
            int healthDrain = ((intensity * 2) - pDef);
            if (healthDrain < 0)
            {
                healthDrain = 1;
                UpdateInfo();
            }
            if (pHealth - healthDrain > 0)
            {
                pHealth = pHealth - healthDrain;
                UpdateInfo();
                AddConsoleQuick(playerName + " is hurt by exhaustion for " + healthDrain + " points of damage!");
            }
            else
            {
                pHealth = 0;
                characterInfo.Text = "You have died.";
                DeathConfiguration();
            }
        }

        private void LandOnTown()
        {

        }

        private void LandOnMonster()
        {

        }

        private void NextTurn()
        {
            for (int x = 0; x < (terrainSize - 1); x++)
            {
                for (int y = 0; y < (terrainSize - 1); y++)
                {
                    if (monsterAlive[x,y] == false && monsterSpawn[x,y] == true)
                    {
                        if (monsterDeath[x, y] == 5)
                        {
                            monsterDeath[x, y] = 0;
                            monsterAlive[x, y] = true;
                        }
                        monsterDeath[x, y]++;
                    }
                }
            }
        }

        private void DeathConfiguration()
        {
            canvasDisplay.Children.Clear();
            canvasDisplay.Children.Add(character);
            FreezeButton(false);
            buttonStart.Content = "Start";
            gameEnable = false;
            gameStarted = false;
            actionOutput.AppendText(playerName + " has died." + Environment.NewLine);
            buttonStart.IsEnabled = true;
            actionOutput.ScrollToEnd();
        }

        private bool ExperienceCheck()
        {
            if (pExp >= nextExp)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void LevelUp()
        {
            pLvl++;
            AddConsoleQuick(playerName + " has leveled to level " + pLvl);
            LevelUp win = new LevelUp(pCon, pStr, pAgi, pInt, pDef, pCha, statPoints, pLvl);
            win.ShowDialog();
            pCon = win.con;
            pStr = win.str;
            pAgi = win.agi;
            pInt = win.inte;
            pDef = win.def;
            pCha = win.cha;
            statPoints = win.pRem;
            UpdateStats();
            UpdateBars();
            UpdateInfo();
        }

        private void UpdateStats()
        {
            SetExperience();
            SetHealth();
            SetStamina();
            SetMagic();
        }

        private void SetExperience()
        {
            pExp = 0;
            nextExp = (pLvl * 100) + 100;
        }

        private void SetHealth()
        {
            pHealthMax = (10 * pCon) + pHealthMax - 10;
            pHealth = pHealthMax;
            UpdateInfo();
            UpdateBars();
        }

        private void SetStamina()
        {
            pStaminaMax = (10 * pAgi) + pStaminaMax - 10;
            pStamina = pStaminaMax;
            UpdateInfo();
            UpdateBars();
        }

        private void SetMagic()
        {
            pMagicMax = (10 * pInt) + pMagicMax - 10;
            pMagic = pMagicMax;
            UpdateInfo();
            UpdateBars();
        }

        private void PlayerCoordinates()
        {
            character.ToolTip = playerName + " (" + (playerX + 1) + ", " + (playerY + 1) + ")";
        }

        private void Check()
        {
            if (ExperienceCheck())
            {
                LevelUp();
            }

            UpdateBars();
            UpdateInfo();
        }

        // Generation
        private void MapGeneration()
        {
            int waterType = (random.Next(0, 5));
            Point waterCursor = new Point(0, 0);
            string OceanSeed = "";
            switch (waterType)
            {
                case 0:
                    int random1 = random.Next(-3, 3);
                    int random2 = random.Next(-3, 3);
                    waterCursor.X = 7 + random1;
                    waterCursor.Y = 7 + random2;
                    while (waterCursor.X == playerX && waterCursor.Y == playerY)
                    {
                        random1 = random.Next(-3, 3);
                        random2 = random.Next(-3, 3);
                        waterCursor.X = 7 + random1;
                        waterCursor.Y = 7 + random2;
                        OceanSeed += waterCursor.X + waterCursor.Y;
                    }
                    break;
                case 1:
                    waterCursor.X = 0;
                    waterCursor.Y = 7 + random.Next(-3, 4);
                    break;
                case 2:
                    waterCursor.X = 7 + random.Next(-3, 4);
                    waterCursor.Y = 0;
                    break;
                case 3:
                    waterCursor.X = 14;
                    waterCursor.Y = 7 + random.Next(-3, 4);
                    break;
                case 4:
                    waterCursor.X = 7 + random.Next(-3, 4);
                    waterCursor.Y = 14;
                    break;
            }
            OceanSeed += waterCursor.X + waterCursor.Y;
            int waterMax = 45 + random.Next(-5, 5);
            Point waterSeed = new Point(waterCursor.X, waterCursor.Y);
            terrain[(int)waterCursor.X, (int)waterCursor.Y] = "ocean";
            for (int i = 0; i < waterMax; i++)
            {
                waterCursor.X = waterSeed.X + random.Next(-5, 5);
                waterCursor.Y = waterSeed.Y + random.Next(-5, 5);
                while ((waterCursor.X == waterSeed.X && waterCursor.Y == waterSeed.Y) || (waterCursor.X == playerX && waterCursor.Y == playerY) || (waterCursor.Y < 0) || (waterCursor.Y > 14) || (waterCursor.X < 0) || (waterCursor.X > 14))
                {
                    waterCursor.X = waterSeed.X + random.Next(-7, 7);
                    waterCursor.Y = waterSeed.Y + random.Next(-7, 7);
                    OceanSeed += waterCursor.X + waterCursor.Y;
                }

                for (int a = -1; a < 1; a++)
                {
                    for (int b = 0; b < 1; b++)
                    {
                        int c = playerX + a;
                        int d = playerY + b;
                        if (terrain[c, d] == "ocean")
                        {
                            terrain[c, d] = "plains";
                        }
                    }
                }
                terrain[(int)waterCursor.X, (int)waterCursor.Y] = "ocean";
            }
            if (devCheats)
            {
                AddConsoleQuick("Ocean generated with " + OceanSeed);
            }
            for (int i = 0; i < random.Next(1, 9); i++)
            {
                if (devCheats)
                {
                    AddConsoleQuick("Mountain " + i + " generated with " + MountainGeneration());
                }
                else
                {
                    MountainGeneration();
                }
            }
            if (devCheats)
            {
                AddConsoleQuick("Vallies generated with " + ValleyGeneration());
            }
            else
            {
                ValleyGeneration();
            }

            // Beach
            for (int x = 0; x < 14; x++)
            {
                for (int y = 0; y < 14; y++)
                {
                    if ((terrain[x, y] == "plains"))
                    {
                        int quantity = 0;
                        // a is x min/max
                        // b is y min/max
                        int a = -1;
                        int a2 = 1;
                        int b = -1;
                        int b2 = 1;
                        if (x == 0)
                        {
                            a = 0;
                        }
                        if (y == 0)
                        {
                            b = 0;
                        }
                        if (x == 14)
                        {
                            a2 = 0;
                        }
                        if (y == 14)
                        {
                            b2 = 0;
                        }
                        for (int x2 = a; x2 < a2; x2++)
                        {
                            for (int y2 = b; y2 < b2; y2++)
                            {
                                if (terrain[x + x2, y + y2] == "ocean")
                                {
                                    quantity++;
                                }
                            }
                        }
                        if (quantity > 3)
                        {
                            terrain[x, y] = "desert";
                        }
                    }
                }
            }

            // Towns
            townCount = random.Next(1, 5);
            TownGeneration(townCount);
            RoadGeneration();
            // Monsters
            MonsterGeneration();
        }

        private string MountainGeneration()
        {
            string mountSeed = "";
            Point mountainSeed = new Point(random.Next(0, 15), random.Next(0, 15));
            while ((terrain[(int)mountainSeed.X, (int)mountainSeed.Y] == "ocean") || (mountainSeed.X == 7 && mountainSeed.Y == 7))
            {
                mountainSeed.X = random.Next(0, terrainSize);
                mountainSeed.Y = random.Next(0, terrainSize);
            }
            mountSeed += mountainSeed.X + mountainSeed.Y;
            terrain[(int)mountainSeed.X, (int)mountainSeed.Y] = "mountain";
            Point mountainCursor = new Point(mountainSeed.X, mountainSeed.Y);
            bool mountainSpread = true;
            while (mountainSpread)
            {
                int directionChoice = random.Next(0, 8);
                switch (directionChoice)
                {
                    case 0:
                        mountainCursor.X += 1;
                        int x = (int)mountainCursor.X;
                        int y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 1:
                        mountainCursor.Y += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 2:
                        mountainCursor.Y += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 3:
                        mountainCursor.Y += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 4:
                        mountainCursor.Y -= 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 5:
                        mountainCursor.X -= 1;
                        mountainCursor.Y += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 6:
                        mountainCursor.X += 1;
                        mountainCursor.Y += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 0;
                            mountainCursor.Y = 0;
                        }
                        break;
                    case 7:
                        mountainCursor.X += 1;
                        x = (int)mountainCursor.X;
                        y = (int)mountainCursor.Y;
                        if (IsInBounds(x, y))
                        {
                            mountainSpread = true;
                        }
                        else
                        {
                            mountainSpread = false;
                            mountainCursor.X = 1;
                            mountainCursor.Y = 1;
                        }
                        break;
                }
                if (terrain[(int)mountainCursor.X, (int)mountainCursor.Y] != "plains")
                {
                    if (terrain[(int)mountainCursor.X, (int)mountainCursor.Y] != "mountain")
                    {

                    }
                    else
                    {
                        mountainSpread = false;
                    }
                }
                if (mountainSpread)
                {
                    terrain[(int)mountainCursor.X, (int)mountainCursor.Y] = "mountain";
                    mountSeed += mountainCursor.X + mountainCursor.Y;
                }
            }
            return mountSeed;
        }

        private string ValleyGeneration()
        {
            string valleyString = "";
            for (int x = 1; x < terrainSize - 1; x++)
            {
                for (int y = 1; y < terrainSize - 1; y++)
                {
                    int mountTiles = 0;
                    if (terrain[x, y] == "plains")
                    {

                        for (int x2 = -1; x2 <= 1; x2++)
                        {
                            for (int y2 = -1; y2 <= 1; y2++)
                            {
                                if (terrain[x + x2, y + y2] == "mountain")
                                {
                                    mountTiles++;
                                }
                            }
                        }
                    }
                    if (mountTiles >= 3)
                    {
                        terrain[x, y] = "valley";
                        valleyString += x + y;
                    }
                }
            }
            if (valleyString == "")
            {
                valleyString = "0";
            }
            return valleyString;
        }

        private void TownGeneration(int quantity)
        {
            
            string townSeed = "";
            for (int i = 0; i < quantity; i++)
            {
                townSeed = "";
                Point townCursor = new Point(random.Next(0, terrainSize), random.Next(0, terrainSize));
                while ((terrain[(int)townCursor.X, (int)townCursor.Y] == "ocean") || (townCursor.X == 7 && townCursor.Y == 7))
                {
                    townCursor.X = random.Next(0, terrainSize);
                    townCursor.Y = random.Next(0, terrainSize);
                }
                townSeed += townCursor.X + townCursor.Y;
                if (terrain[(int)townCursor.X, (int)townCursor.Y] == "plains")
                {
                    terrain[(int)townCursor.X, (int)townCursor.Y] = "town";
                    towns[(int)townCursor.X, (int)townCursor.Y] = new Town();
                }
                else
                {
                    terrain[(int)townCursor.X, (int)townCursor.Y] = "camp";
                    towns[(int)townCursor.X, (int)townCursor.Y] = new Town();
                }
                if (devCheats)
                {
                    townSeed +=Environment.NewLine + towns[(int)townCursor.X, (int)townCursor.Y].name + " generated at (" + townCursor.X + ", " + townCursor.Y + ") "; 
                    AddConsoleQuick(towns[(int)townCursor.X, (int)townCursor.Y].name + " generated with " + townSeed);
                    if (towns[(int)townCursor.X, (int)townCursor.Y].buildings.Contains("shop"))
                    {
                        AddConsoleQuick(towns[(int)townCursor.X, (int)townCursor.Y].name + " generated with shop");
                    }
                    if (towns[(int)townCursor.X, (int)townCursor.Y].buildings.Contains("hall"))
                    {
                        AddConsoleQuick(towns[(int)townCursor.X, (int)townCursor.Y].name + " generated with hall");
                    }
                    if (towns[(int)townCursor.X, (int)townCursor.Y].buildings.Contains("tavern"))
                    {
                        AddConsoleQuick(towns[(int)townCursor.X, (int)townCursor.Y].name + " generated with tavern");
                    }
                }
            }
        }

        private void RoadGeneration()
        {
            Line myLine = new Line();
            for (int i = 0; i < townCount; i++)
            {
                if (townCount == 1)
                {
                    break;
                }
            }
        }

        private void MonsterGeneration()
        {
            for (int x = 0; x < (terrainSize - 1); x++)
            {
                for (int y = 0; y < (terrainSize - 1); y++)
                {
                    if (random.Next(0, 4) == 3 && terrain[x, y] != "town" && terrain[x, y] != "camp" && x != 7 && y != 7)
                    {
                        monsterSpawn[x,y] = true;
                        monsterAlive[x, y] = true;
                    }
                }
            }
        }
        private int AmountCheck(int x, int y, string type)
        {
            int quantity = 0;
            // a is x min/max
            // b is y min/max
            int a = -1;
            int a2 = 1;
            int b = -1;
            int b2 = 1;
            if (x == 0)
            {
                a = 0;
            }
            if (y == 0)
            {
                b = 0;
            }
            if (x == 14)
            {
                a2 = 0;
            }
            if (y == 14)
            {
                b2 = 0;
            }
            for (int x2 = a; x2 < a2; x2++)
            {
                for (int y2 = b; y2 < b2; y2++)
                {
                    if (terrain[x + x2, y + y2] == "plains")
                    {
                        quantity++;
                    }
                }
            }
            return quantity;
        }

        private void MapCreation()
        {
            for (int y = 0; y < terrainSize; y++)
            {
                for (int x = 0; x < terrainSize; x++)
                {
                    int i = 1;
                    switch (terrain[x, y])
                    {
                        case "ocean":
                            i = 2;
                            break;
                        case "mountain":
                            i = 3;
                            break;
                        case "desert":
                            i = 4;
                            break;
                        case "valley":
                            i = 5;
                            break;
                        case "forest":
                            i = 6;
                            break;
                        case "hill":
                            i = 7;
                            break;
                        case "island":
                            i = 8;
                            break;
                        case "camp":
                            i = 10;
                            break;
                        case "town":
                            i = 10;
                            break;
                    }
                    new Terrain(canvasDisplay, iconset[i], terrain[x, y], x, y);
                }
            }

        }
    }
}
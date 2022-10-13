using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace HealthSystem
{
    internal class Program
    {
        static int health;
        static int shield;
        static int lives;
        static int enemydamage;
        static int enemyhealth;
        static int enemyxpvalue;
        static int experience;
        static int playerlevel;
        static int levelboost;
        static bool gameover;
        static string currentweapon;
        static bool testmode;

        const int healthmax = 999;
        const int healthmin = 0;
        const int shieldmax = 50;
        const int shieldmin = -1;
        const int livesmax = 99;
        const int experiencemin = -1;
        
        static void Main(string[] args)
        {
            BaseStats(); //method to initialize all variables to base levels

            while (gameover == false && testmode == false)
            {
                ShowHud();
                PlayerChoice();
                while (testmode == true)
                {
                    ShowHud();
                    PlayerChoice();
                    DeathCheck();
                    LivesCheck();                
                    Console.WriteLine();
                    Thread.Sleep(3000);
                    Console.Clear();
                }                                
                TakeDamage();
                DeathCheck();
                LivesCheck();
                ShieldRegen();                
                Console.WriteLine();
                Thread.Sleep(3000);
                Console.Clear();
            }

            Console.ReadKey();
        }
        static void ShowHud()
        {
            Console.WriteLine("-------------------");
            Console.WriteLine("Ancient Combat.");
            Console.WriteLine("By: Sword of Creation Studios");
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Shield: " + shield + " Health: " + health);
            Console.WriteLine("Lives: " + lives);
            Console.WriteLine("Current Weapon: " + currentweapon);
            Console.WriteLine("EXP: " + experience + " Level: " + playerlevel);
            Console.WriteLine();
        }

        static void PlayerChoice()
        {
            if (testmode == false)
            {
                Console.WriteLine("Press 'F' to shoot, 'T' to switch weapons, 'H' to attempt to heal, or 'Y' to toggle TESTMODE");
                if (Console.ReadKey().Key == ConsoleKey.H)
                {
                    Console.WriteLine();
                    Console.WriteLine("You attempt to heal!");
                    PlayerHeal();
                }
                else if (Console.ReadKey().Key == ConsoleKey.T)
                {
                    Console.WriteLine();
                    Console.WriteLine("You grab for another weapon!");
                    WeaponSwitch();
                }
                else if (Console.ReadKey().Key == ConsoleKey.F)
                {
                    Console.WriteLine();
                    Console.WriteLine("You squeeze off a shot!");
                    PlayerShoot();
                }
                else if (Console.ReadKey().Key == ConsoleKey.Y)
                {
                    Console.WriteLine();
                    Console.WriteLine("Toggling TESTMODE");
                    TestMode();
                }

            }
            else if (testmode == true)
            {
                Console.WriteLine("Press 'F' to deal damage to the player, Press 'H' to heal, Press 'S' to regenerate shield, Press 'L' to modify lives, Press 'E' to modify experience, Press 'R' to reset stats, or 'Y' to toggle TESTMODE");
                if (Console.ReadKey().Key == ConsoleKey.F)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter how much damage you would like the player to take.");
                    int playerinput = Convert.ToInt32(Console.ReadLine());
                    if (playerinput > healthmax) 
                    {
                        Console.WriteLine("You are trying to apply an amount of damage that is not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else if (playerinput < healthmin)
                    {
                        Console.WriteLine("You are trying to apply an amount of damage that is not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else
                    {
                        Console.WriteLine("You are about to apply " + playerinput + " damage.");
                        TakeDamageTest(playerinput);
                    }
                }
                else if (Console.ReadKey().Key == ConsoleKey.H)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter how much healing you would like the player to receive.");
                    int playerinput = Convert.ToInt32(Console.ReadLine());
                    if (playerinput > healthmax)
                    {
                        Console.WriteLine("You are trying to apply an amount of healing that is not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else if (playerinput < healthmin)
                    {
                        Console.WriteLine("You are trying to apply an amount of healing that is not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else
                    {
                        Console.WriteLine("You are about to apply " + playerinput + " health.");
                        PlayerHealTest(playerinput);
                    }
                }
                else if (Console.ReadKey().Key == ConsoleKey.S)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter how many shields you would like the player to receive.");
                    int playerinput = Convert.ToInt32(Console.ReadLine());
                    if (playerinput > shieldmax)
                    {
                        Console.WriteLine("You are trying to apply an amount of shields that are not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else if (playerinput < shieldmin)
                    {
                        Console.WriteLine("You are trying to apply an amount of shields that are not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else
                    {
                        Console.WriteLine("You are about to apply " + playerinput + " shields.");
                        ShieldRegenTest(playerinput);
                    }
                }
                else if (Console.ReadKey().Key == ConsoleKey.L)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the number of lives you would like.");
                    int playerinput = Convert.ToInt32(Console.ReadLine());
                    if (playerinput > livesmax)
                    {
                        Console.WriteLine("You are trying to apply an amount of lives that are not possible, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }                    
                    else
                    {
                        Console.WriteLine("You are about to apply " + playerinput + " lives.");
                        lives = lives + playerinput;
                        LivesCheck();
                    }

                }
                else if (Console.ReadKey().Key == ConsoleKey.E)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the amount of experience you would like.");
                    int playerinput = Convert.ToInt32(Console.ReadLine());
                    if (playerinput < experiencemin)
                    {
                        Console.WriteLine("You are trying to apply a negative amount of experience, please try again.");
                        Thread.Sleep(3000);
                        Console.Clear();
                        ShowHud();
                        PlayerChoice();
                    }
                    else
                    {
                        Console.WriteLine("You are about to apply " + playerinput + " experience.");
                        experience = experience + playerinput;
                        LevelUpCheck();
                    }

                }
                else if (Console.ReadKey().Key == ConsoleKey.R)
                {
                    BaseStats();
                    Console.WriteLine("Stats have now been reset.");
                    Thread.Sleep(3000);
                    Console.Clear();
                    ShowHud();
                    PlayerChoice();

                }
                else if (Console.ReadKey().Key == ConsoleKey.Y) //removes player from testmode
                {
                    TestMode();
                }
            }
        }
        static void TakeDamage()
        {
            Random rnd = new Random();
            enemydamage = rnd.Next(3, 21);

            Console.WriteLine();
            Console.WriteLine("You're about to take " + enemydamage + " damage!");
            SpillOver(enemydamage);
        }
        static void TakeDamageTest(int playerinput)
        {
            int testdamage = playerinput;

            Console.WriteLine();
            SpillOver(testdamage);
        }
        static void ShieldRegen() //regens shields and keeps them from going past maximum (complete)
        {
            if (shield != shieldmax && shield < 45)
            {
                Random rnd = new Random();
                int shieldregen = rnd.Next(1, 6);
                Console.WriteLine("Your shield regenerates for " + shieldregen + " points.");
                shield = shield + shieldregen;
            }
            else if (shield != shieldmax && shield >= 45 && shield < 50)
            {
                Console.WriteLine("Your shield regenerates to full!");
                shield = 50;
            }
            else
            {
                Console.WriteLine("Shields are at full!");
            }

        }
        static void ShieldRegenTest(int playerinput)
        {
            int testshieldregen = playerinput;
            if (shield != shieldmax && shield < 45)
            {
                Console.WriteLine("Your shield regenerates for " + testshieldregen + " points.");
                shield = shield + testshieldregen;
            }
            else if (shield != shieldmax && shield >= 45 && shield < 50)
            {
                Console.WriteLine("Your shield regenerates to full!");
                shield = 50;
            }
            else
            {
                Console.WriteLine("Shields are at full!");
            }
        }
        static void PlayerHeal()
        {
            Random rnd = new Random();
            int heal = rnd.Next(0, 6);

            if (heal == 0)
            {
                Console.WriteLine("The enemy lunges and you're forced to dodge. No healing this turn!");
            }
            else if (heal > 0 && heal < 5)
            {
                int healamount;
                healamount = rnd.Next(1, 20);
                Console.WriteLine("You're about to heal for " + healamount + ".");
                health = health + healamount;
            }
            else
            {
                int healamount;
                healamount = rnd.Next(5, 16) * 2;
                Console.WriteLine("A critical heal!");
                Console.WriteLine("You're about to heal for " + healamount + ".");
                health = health + healamount;
            }
        }
        static void PlayerHealTest(int playerinput)
        {
            int playerheal = playerinput;
            Console.WriteLine("You're about to heal for " + playerheal + ".");
            health = health + playerheal;
        }
        static void WeaponSwitch()
        {
            Console.WriteLine("Press 'P' to choose your Pistol, or 'J' to choose your Infinity Cannon");
            if (Console.ReadKey().Key == ConsoleKey.P)
            {
                Console.WriteLine();
                Console.WriteLine("You grab your pistol!");
                currentweapon = "Pistol";
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("You haul out your Infinity Cannon!");
                currentweapon = "Infinity Cannon";
            }

        }

        static void PlayerShoot()
        {
            Random rnd = new Random();
            int playershot = rnd.Next(5, 21) + levelboost;

            Console.WriteLine();
            Console.WriteLine("You shoot the enemy for " + playershot + " damage!");
            enemyhealth = enemyhealth - playershot;
            EnemyDeathCheck();
            Console.WriteLine();

        }
        static void BaseStats() //initializes starting variables. can also be used to reset game to default parameters.
        {
            health = 150 + (levelboost * 2);
            shield = 50;
            lives = 3;
            enemydamage = 0;
            enemyhealth = 25;
            experience = 0;
            playerlevel = 1;
            levelboost = playerlevel * 2;
            enemyxpvalue = 25;
            currentweapon = "Pistol";
            gameover = false;
            if (testmode != true)
            {
            testmode = false;
            }
        }
        static void DeathCheck() //checks player health and, if 0 or lower, decrements lives available and resets health and shields to default.
        { 
        if (health <= 0)
            {
                lives--;
                health = 150 + (levelboost * 2);
                shield = 50;
            }
        }
        static void LivesCheck() //checks for lives left and, if none, toggles the end of game key
        {
            if (lives <= 0 && testmode == false)
            {
                gameover = true;
            }
            else if (lives <= 0 && testmode == true)
            {
                Console.WriteLine();
                Console.WriteLine("You have died.");
                Console.WriteLine("Lives will now be reset.");
                lives = 3;
            }
        }
        static void EnemyDeathCheck() //checks to see if the current enemy has died or not
        {
            if (enemyhealth <= 0) //increments the players experience if the enemy has died and prepares a new enemy
            {
                enemyhealth = 25 + (levelboost * 2);
                Console.WriteLine();
                Console.WriteLine("You've killed the enemy!");
                experience = experience + enemyxpvalue;
                LevelUpCheck();
                enemyxpvalue = enemyxpvalue * 2;
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("The enemy still stands with " + enemyhealth + " health remaining!");
            }
        }
        static void LevelUpCheck() //checks player experience to see if they've earned a level and applies a levelup if they have
        {
            if (testmode == false)
            {
                if (experience >= ((playerlevel * levelboost) + 100))
                {
                    Console.WriteLine();
                    Console.WriteLine("You gained " + enemyxpvalue + " experience!");
                    Console.WriteLine("Congratulations, you've gained a level!");
                    playerlevel++;                
                }
                else
                {
                    Console.WriteLine();
                    Console.WriteLine("You gained " + enemyxpvalue + " experience!");
                }
            }
            else
            {
                while (experience >= (playerlevel * levelboost) + 100)
                {
                    Console.WriteLine();
                    Console.WriteLine("Congratulations, you've gained a level!");
                    playerlevel++;
                }
            }
        }
        static void SpillOver(int enemydamage) //calculates damage spillover from shields into health.
        {
            int spillovercheck;
            int healthspill;
            spillovercheck = enemydamage;

            if (shield >= spillovercheck)
            {
                shield = shield - spillovercheck;
                Console.WriteLine("Shields took " + spillovercheck + " damage!");
            }
            else if (shield < spillovercheck)
            {
                shield = shield - spillovercheck;
                healthspill = shield;
                Console.WriteLine("Your shields are gone!");
                shield = 0;
                Console.WriteLine("You're about to lose " + healthspill + " health!");
                health = health + healthspill;
                Console.WriteLine();
            }
        }
        static void TestMode() //toggles TestMode on or off
        {
            if (testmode == false)
            {
            testmode = true;
            Console.WriteLine("Welcome to TESTMODE");
            Console.WriteLine();
            Thread.Sleep(3000);
            Console.Clear();
            }
            else
            {
                testmode = false;
                Console.WriteLine();
                Console.WriteLine("You are now leaving TESTMODE");
            }            
        }
    }
}
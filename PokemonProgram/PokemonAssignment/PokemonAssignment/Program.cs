using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Program
    {
        public static Pokemon player = null;
        public static Pokemon enemy = null;
        static void Main(string[] args)
        {
          
            List<Pokemon> roster = new List<Pokemon>();
            List<Move> cMoves = new List<Move>()
            {
                new Move("Ember"),
                new Move("Fire Blast")
            };
            List<Move> sMoves = new List<Move>()
            {
                new Move("Bubble"),
                new Move("Bite")
            };
            List<Move> bMoves = new List<Move>()
            {
                new Move("Cut"),
                new Move("Mega Drain"),
                new Move("Razor Leaf")
            };

            // INITIALIZE YOUR THREE POKEMONS HERE
            Pokemon Charmander = new Pokemon("Charmander", 3, 52f, 43f, 39f, Elements.Fire, cMoves);
            Pokemon Squirtle = new Pokemon("Squirtle", 2, 48f, 65f, 44f, Elements.Water, sMoves);
            Pokemon Bulbasaur = new Pokemon("Bulbasaur", 3, 49f, 49f, 45f, Elements.Grass, bMoves);

            roster.Add(Charmander);
            roster.Add(Squirtle);
            roster.Add(Bulbasaur);

            void PrintNames()
            {
                for (int i = 0; i < roster.Count; i++)
                {
                    Console.Write(roster[i].Name + " ");
                }
            }

            Console.WriteLine("Welcome to the world of Pokemon!\nThe available commands are list/fight/heal/quit");

            while (true)
            {
                    Console.WriteLine("\nPlese enter a command");
                    switch (Console.ReadLine())
                    {
                        case "list":
                        // PRINT THE POKEMONS IN THE ROSTER HERE
                        PrintNames();
                            break;

                        case "fight":
                            //PRINT INSTRUCTIONS AND POSSIBLE POKEMONS (SEE SLIDES FOR EXAMPLE OF EXECUTION)
                            while(player==null && enemy == null)
                                    { 
                                    Console.Write("Choose who should fight: ");
                                    PrintNames();
                                    Console.Write("-> (Your Pokemon)  (Enemy Pokemon): ");

                                    //READ INPUT, REMEMBER IT SHOULD BE TWO POKEMON NAMES
                                    string input = Console.ReadLine();
                                    string[] inputs = input.Split(' ');
                                    //Pokemon player = null;
                                    //Pokemon enemy = null;
                                if (inputs.Length>1 && inputs[0] != inputs[1])
                                    {
                                        bool containsPokemon1 = roster.Exists(x => x.Name == inputs[0]);
                                        bool containsPokemon2 = roster.Exists(x => x.Name == inputs[1]);
                                        if(containsPokemon1==true && containsPokemon2 == true)
                                        {
                                            for(int i=0; i < roster.Count; i++)
                                            {
                                               if( inputs[0] == roster[i].Name && roster[i].Hp>0)
                                                {
                                                    player = roster[i];
                                                }
                                                if (inputs[1] == roster[i].Name && roster[i].Hp > 0)
                                                {
                                                    enemy = roster[i];
                                                }
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Remember to write the name of two different Pokemons");
                                        }
                                }
                                else
                                {
                                    Console.WriteLine("Remember to write the name of two different Pokemons");
                                }
                            
                            }
                        //BE SURE TO CHECK THE POKEMON NAMES THE USER WROTE ARE VALID (IN THE ROSTER) AND IF THEY ARE IN FACT 2!
                        //if everything is fine and we have 2 pokemons let's make them fight
                        if (player != null && enemy != null && player != enemy)
                            {
                                Console.WriteLine("A wild " + enemy.Name + " appears!");
                                Console.Write(player.Name + " I choose you! ");

                                //BEGIN FIGHT LOOP
                                while (player.Hp > 0 && enemy.Hp > 0)
                                {
                                int moveToInt = -1;
                                string move = null;
                                //PRINT POSSIBLE MOVES
                                while (moveToInt > player.Moves.Count - 1 || moveToInt < 0 || move == null)
                                    {


                                        Console.Write("What move should we use?\n");
                                        for (int i = 0; i < player.Moves.Count; i++)
                                        {
                                            Console.Write(" " + player.Moves[i].Name + "[" + i + "]");
                                        }
                                        Console.WriteLine();
                                        move = Console.ReadLine();
                                        
                                        while (string.IsNullOrEmpty(move))
                                        {
                                            Console.WriteLine("Remeber to enter a move");
                                            move = Console.ReadLine();
                                        }
                                        bool isItANummber = move.All(char.IsDigit);


                                        //GET USER ANSWER, BE SURE TO CHECK IF IT'S A VALID MOVE, OTHERWISE ASK AGAIN
                                        if (isItANummber == true)
                                        {
                                            moveToInt = Convert.ToInt32(move);

                                            if (moveToInt <= player.Moves.Count - 1 && moveToInt >= 0)
                                            {
                                                string choosenMove = player.Moves[moveToInt].Name;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Please write the number of the move you would like to do");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Please write the number of the move you would like to do");
                                        }
                                    }

                                    //CALCULATE AND APPLY DAMAGE
                                    //I know that this is very ugly to look at, but it works, and that meant i could focus on our other assignments.
                                    //If we didn't have the other assignments, I would re-write the functions, so it was possible to just call "Attack" and then calculate the dmg inside the pokemon class
                                    float typeFact = 0f;
                                    float dmgFact = 0f;
                                    float totalAttack = 0f;
                                    float curHP = 0f;

                                    typeFact = player.CalculateElementalEffects(player.Element, enemy.Element);
                                    dmgFact = enemy.CalculateDamageFactor(player.BaseAttack, enemy.BaseDefence, player.Level, enemy.Level);
                                    totalAttack = player.Attack(dmgFact, typeFact);
                                    curHP = player.ApplyDamage(enemy.Hp, totalAttack);
                                    enemy.Hp = curHP;
                                

                                    //print the move and damage
                                    Console.WriteLine(player.Name + " uses " + player.Moves[moveToInt].Name + ". " + enemy.Name + " loses " + totalAttack + " HP");

                                    //if the enemy is not dead yet, it attacks
                                    if (enemy.Hp > 0)
                                    {
                                            //CHOOSE A RANDOM MOVE BETWEEN THE ENEMY MOVES AND USE IT TO ATTACK THE PLAYER
                                            Random rand = new Random();
                                            int enMove = rand.Next(0, enemy.Moves.Count);

                                            typeFact = enemy.CalculateElementalEffects(enemy.Element, player.Element);
                                            dmgFact = player.CalculateDamageFactor(enemy.BaseAttack, player.BaseDefence, enemy.Level, player.Level);
                                            totalAttack = enemy.Attack(dmgFact, typeFact);
                                            curHP = enemy.ApplyDamage(player.Hp, totalAttack);
                                            player.Hp = curHP;

                                            //print the move and damage
                                            Console.WriteLine(enemy.Name + " uses " + enemy.Moves[enMove].Name + ". " + player.Name + " loses " + totalAttack + " HP");
                                    }
                                }
                                //The loop is over, so either we won or lost
                                if (enemy.Hp <= 0)
                                {
                                    Console.WriteLine(enemy.Name + " faints, you won!");
                                    player = null;
                                    enemy = null;
                                }
                                else
                                {
                                    Console.WriteLine(player.Name + " faints, you lost...");
                                    player = null;
                                    enemy = null;
                            }
                            }
                            //otherwise let's print an error message
                            else
                            {
                                Console.WriteLine("Invalid pokemons");
                            }
                            break;

                        case "heal":
                            //RESTORE ALL POKEMONS IN THE ROSTER
                            for(int i = 0; i < roster.Count; i++)
                            {
                                roster[i].Restore();
                            }
                            //player.Hp = player.MaxHP;
                            //enemy.Hp = enemy.MaxHP;

                            Console.WriteLine("All pokemons have been healed");
                            break;

                        case "quit":
                            Environment.Exit(0);
                            break;

                        default:
                            Console.WriteLine("Unknown command");
                            break;
                    }
                }
        }
    }
}

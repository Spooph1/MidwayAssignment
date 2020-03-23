using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    /// <summary>
    /// The possible elemental types
    /// </summary>
    public enum Elements
    {
        Fire,
        Water,
        Grass
    }

    public class Pokemon
    {
        //fields
        int level;
        float baseAttack;
        float baseDefence;
        float hp;
        float maxHp;
        Elements element;

        //properties, imagine them as private fields with a possible get/set property (accessors)
        //in this case used to allow other objects to read (get) but not write (no set) these variables
        public string Name { get; }

        public float BaseAttack { get => baseAttack; }

        public float BaseDefence { get => baseDefence; }

        public int Level { get => level; }

        public Elements Element { get => element; }
        //example of how to make the string Name readable AND writable  
        //  public string Name { get; set; }
        public List<Move> Moves { get; }
        //can also be used to get/set other private fields
        public float Hp { get; set; }
        public float MaxHP { get => hp; }

        /// <summary>
        /// Constructor for a Pokemon, the arguments are fairly self-explanatory
        /// </summary>
        /// <param name="name"></param>
        /// <param name="level"></param>
        /// <param name="baseAttack"></param>
        /// <param name="baseDefence"></param>
        /// <param name="hp"></param>
        /// <param name="element"></param>
        /// <param name="moves">This needs to be a List of Move objects</param>
        public Pokemon(string name, int level, float baseAttack,
            float baseDefence, float hp, Elements element,
            List<Move> moves)
        {
            this.level = level;
            this.baseAttack = baseAttack;
            this.baseDefence = baseDefence;
            this.Name = name;
            this.hp = hp;
            this.maxHp = hp;
            this.element = element;
            this.Moves = moves;
            Hp = hp;
        }

        /// <summary>
        /// performs an attack and returns total damage, check the slides for how to calculate the damage
        /// IMPORTANT: should also apply the damage to the enemy pokemon
        /// </summary>
        /// <param name="enemy">This is the enemy pokemon that we are attacking</param>
        /// <returns>The amount of damage that was applied so we can print it for the user</returns>
        public float Attack(float dmgFac, float typeFac)
        {
            //With the calculation for the dmg you gave us, it was a one-shot fiesta, so i made it more similar to the actual calculation i Pokemon. 
            //Which means that it's still the strongest element that wins, but atleast it takes a couple of turns.
            float totalAttack = (float)Math.Round(10f * dmgFac * typeFac);
            return totalAttack;
        }

        /// <summary>
        /// calculate the current amount of defence points
        /// </summary>
        /// <returns> returns the amount of defence points considering the level as well</returns>
        public float CalculateDamageFactor(float baseAttack, float baseDefence, int attackingLevel, int defendingLevel)
        {
            float dmgFactor = (attackingLevel * baseAttack) / (defendingLevel * baseDefence);
            return dmgFactor;
        }

        /// <summary>
        /// Calculates elemental effect, check table at https://bulbapedia.bulbagarden.net/wiki/Type#Type_chart for a reference
        /// </summary>
        /// <param name="damage">The amount of pre elemental-effect damage</param>
        /// <param name="enemyType">The elemental type of the enemy</param>
        /// <returns>The damage post elemental-effect</returns>
        public float CalculateElementalEffects(Elements attackingType, Elements defendingType)
        {
            float calDamage;
            if(attackingType == Elements.Fire && defendingType == Elements.Grass ||
               attackingType == Elements.Grass && defendingType == Elements.Water ||
               attackingType == Elements.Water && defendingType == Elements.Fire)
            {
                calDamage = 2f;
                return calDamage;
            }
            else
            {
                calDamage = 0.5f;
                return calDamage;
            }
        }

        /// <summary>
        /// Applies damage to the pokemon
        /// </summary>
        /// <param name="damage"></param>
        public float ApplyDamage(float hp, float damage)
        {
            float currentHP = hp - damage;
            return currentHP;
        }

        /// <summary>
        /// Heals the pokemon by resetting the HP to the max
        /// </summary>
        public void Restore()
        {
            Hp = maxHp;
        }
    }
}

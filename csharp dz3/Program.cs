
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetLessons
{
    public enum TipBonusa
    {
        Add,
        Multiply
    }

    public struct Bonus
    {
        public string Tag;
        public TipBonusa Type;
        public double Value;
    }

    public class Attribute
    {
        public int BaseValue { get; set; }
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }

        private List<Bonus> bonuses = new List<Bonus>();

        public Attribute(int baseValue)
        {
            BaseValue = baseValue;
        }

        public int Value
        {
            get
            {
                double result = BaseValue;

                foreach (Bonus bonus in bonuses)
                {
                    if (bonus.Type == TipBonusa.Add)
                    {
                        result += bonus.Value;
                    }
                }

                foreach (Bonus bonus in bonuses)
                {
                    if (bonus.Type == TipBonusa.Multiply)
                    {
                        result *= bonus.Value;
                    }
                }

                int value = (int)Math.Round(result);

                if (MinValue.HasValue && value < MinValue.Value)
                {
                    value = MinValue.Value;
                }

                if (MaxValue.HasValue && value > MaxValue.Value)
                {
                    value = MaxValue.Value;
                }

                return value;
            }
        }

        public void AddBonus(string tag, TipBonusa type, double value)
        {
            Bonus bonus = new Bonus();
            bonus.Tag = tag;
            bonus.Type = type;
            bonus.Value = value;

            bonuses.Add(bonus);
        }

        public void AddBonus(string tag, double value)
        {
            AddBonus(tag, TipBonusa.Add, value);
        }

        public void RemoveBonus(string tag)
        {
            bonuses.RemoveAll(b => b.Tag == tag);
        }
    }

    public class Character
    {
        public Attribute Health { get; private set; }
        public Attribute Damage { get; private set; }

        public Character(int health, int damage)
        {
            Health = new Attribute(health);
            Damage = new Attribute(damage);
        }

        public override string ToString()
        {
            return "Health:" + Health.Value + "; Damage:" + Damage.Value;
        }

        public void TakeDamage(int value)
        {
            Health.BaseValue -= value;
        }

        public void Heal(int value)
        {
            Health.BaseValue += value;
        }
    }
}

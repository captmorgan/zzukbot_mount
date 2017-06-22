using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZzukBot.Engines.CustomClass;

namespace something
{
    public class SimpleWarrior : CustomClass
    {

        public override byte DesignedForClass
        {
            get
            {
                return PlayerClass.Warrior;
            }
        }

        public override string CustomClassName
        {
            get
            {
                return "EmuWarrior (Simple)";
            }
        }
		
		public string mountname = "Swift Horse";
		
		public override void PreFight() 
		{
			this.SetCombatDistance(4);
			this.Player.Attack();	
			
			if (this.Player.GotBuff(mountname) && this.Target.DistanceToPlayer <= 30)
			{
				this.Player.UseItem(mountname);
		
			}		
			else if (!this.Player.GotBuff("Battle Stance"))
			{
				this.Player.Cast("Battle Stance");
			}
			else if (this.Player.GetSpellRank("Charge") != 0 && this.Target.DistanceToPlayer < 25 && this.Target.DistanceToPlayer > 8) 
			{
				this.Player.Cast("Charge");			
			}
		}	
		
        public override void Fight()
        {
			if (this.Player.GotBuff(mountname))
			{
				this.Player.UseItem(mountname);
			}
			
			this.SetCombatDistance(4);
			this.Player.Attack();			

            if (this.Player.GetSpellRank("Battle Shout") != 0)
            {
                if (this.Player.Rage >= 10 && !this.Player.GotBuff("Battle Shout"))
                {
                    this.Player.Cast("Battle Shout");
                }
            }

            if (this.Player.Rage <= 40)
            {
                if (this.Player.GetSpellRank("Blood Rage") != 0 && this.Player.CanUse("Blood Rage"))
                {
                    this.Player.Cast("Blood Rage");
                }
                if (this.Player.GetSpellRank("Berserker Rage") != 0 && this.Player.CanUse("Berserker Rage") && this.Player.GotBuff("Berserker Stance"))
                {
                    this.Player.Cast("Berserker Rage");
                }
            }

            if (this.Player.GetSpellRank("Execute") != 0 && !this.Player.GotBuff("Defensive Stance"))
            {
                if (this.Target.HealthPercent <= 20 && Player.Rage >= 15)
                {
                    this.Player.Cast("Execute");
                    return;
                }
            }

            if (this.Player.GetSpellRank("Bloodthirst") != 0)
            {
                if (this.Player.CanUse("Bloodthirst") && this.Player.Rage >= 20)
                {
                    this.Player.Cast("Bloodthirst");
                    return;
                }
            }
            else if (this.Player.GetSpellRank("Mortal Strike") != 0)
            {
                if (this.Player.CanUse("Mortal Strike") && this.Player.Rage >= 20)
                {
                    this.Player.Cast("Mortal Strike");
                    return;
                }
            }
            else //Super low level 
            {


                if (this.Player.GetSpellRank("Rend") != 0 && this.Player.GotBuff("Battle Stance"))
                {
                    if (!this.Target.GotDebuff("Rend") && this.Player.Rage >= 10 & this.Target.HealthPercent > 25)
                    {
                        this.Player.Cast("Rend");
                        return;
                    }
                }
                if (this.Player.Rage >= 15)
                {
                    this.Player.Cast("Heroic Strike");
                }
            }



            if (this.Player.GetSpellRank("Overpower") != 0)
            {
                if (this.Player.CanOverpower)
                {
                    if (this.Player.Rage >= 5)
                        Player.Cast("Overpower");

                }
            }

            if (this.Player.Rage >= 30)
            {
                if (this.Attackers.Count >= 2 && this.Player.GetSpellRank("Cleave") != 0)
                {
                    this.Player.Cast("Cleave");
                }
                else
                {
                    this.Player.Cast("Heroic Strike");
                }
            }

        }

        public override bool Buff()
        {
			if (Player.NeedToLoot()) return true;
			if (this.Player.IsCasting == mountname) return false;
			if (this.Player.Rage == 0 && !this.Player.GotBuff(mountname) && (this.Target.DistanceToPlayer >= 60 || this.Target.HealthPercent <= 0))
			{
				this.Player.UseItem(mountname);
				return false;
			}			
            //True means we are done buffing, or cannot buff
            return true;
        }
		
		public override void Rest() 
		{
			if (this.Player.GotBuff(mountname))
			{
				this.Player.UseItem(mountname);
			}
			if (this.Player.NeedToEat == true && !this.Player.GotBuff("Food") && this.Target.HealthPercent <= 0) 
			{
				this.Player.DoString("DoEmote('Sit')");
				this.Player.Eat();
				return;
			}
		}
    }
}

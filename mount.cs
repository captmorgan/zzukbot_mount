public override void PreFight() 
{
	//remove the mount buff when approaching target
	if (this.Player.GotBuff("Swift Palomino") && this.Target.DistanceToPlayer <= 25)
	{
	  this.Player.UseItem("Swift Palomino");
	}
  else this.Player.Cast("Frostbolt");
}

public override void Fight() 
{
	// remove mount buff if in combat
  if (this.Player.GotBuff("Swift Palomino"))
	{
    this.Player.UseItem("Swift Palomino");
  }
}

public override bool Buff() 
{
	// Dont mount until looting is complete (must have looting turned on)
	if (Player.NeedToLoot()) return true;
	// Dont break casting
	if (this.Player.IsCasting == "Swift Palomino") return false;
	// use mount if target is far away, or have not gotten a new target. testing other meathods.
	if (!this.Player.GotBuff("Swift Palomino") && (this.Target.DistanceToPlayer >= 45 || this.Target.HealthPercent <= 0))
  {
		this.Player.UseItem("Swift Palomino");
		return false;
	}
	return true;
}

public override void PreFight() 
{
	if (this.Player.GotBuff("Swift Palomino") && this.Target.DistanceToPlayer <= 25)
	{
	  this.Player.UseItem("Swift Palomino");
	}
  else this.Player.Cast("Frostbolt");
}

public override void Fight() 
{
  if (this.Player.GotBuff("Swift Palomino"))
	{
    this.Player.UseItem("Swift Palomino");
  }
}

public override bool Buff() 
{
	if (Player.NeedToLoot()) return true;
	if (this.Player.IsCasting == "Swift Palomino") return false;
	if (!this.Player.GotBuff("Swift Palomino") && this.Target.DistanceToPlayer >= 40)
  {
		this.Player.UseItem("Swift Palomino");
		return false;
	}
	return true;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateBaseHPCommand : Command {

    public Player player;
    private int totalHP;
    private int avaliableHP;

    public UpdateBaseHPCommand(Player player, int totalHP, int avaliableHP)
    {
        this.player = player;
        this.totalHP = totalHP;
        this.avaliableHP = avaliableHP;
    }

    public override void StartCommandExecution()
    {
        player.PArea.baseHPVisual.TotalHP = totalHP;
        player.PArea.baseHPVisual.AvailableHP = avaliableHP;
        Command.CommandExecutionComplete();
    }
}

﻿namespace Persistence.DataTransferObject;

public class GameRoundGhostGuessDTO
{
    public Guid ID { get; set; }

    public Guid GameSessionID { get; set; }

    public Guid GameRoundID { get; set; }

    /// <summary>
    ///     Gets or sets the degrees of offset from the origin where the team placed their guess.
    /// </summary>
    public int TargetOffset { get; set; }

    public Guid UserID { get; set; }
}
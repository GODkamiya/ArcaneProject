class PieceInformation
{
    public string pieceName;
    public string uprightEffectDescription;
    public string reverseEffectDescription;

    public int[,] movementDefinitions;
    public int[,] reverseMovementDefinitions;

    public PieceInformation(string pieceName, string uprightEffectDescription, string reverseEffectDescription, int[,] movementDefinitions, int[,] reverseMovementDefinitions = null)
    {
        this.pieceName = pieceName;
        this.uprightEffectDescription = uprightEffectDescription;
        this.reverseEffectDescription = reverseEffectDescription;
        this.movementDefinitions = movementDefinitions;
        this.reverseMovementDefinitions = reverseMovementDefinitions;
    }
}
namespace CsCat
{
    public enum PathResult
    {
        /// <summary>
        ///   the path does not cross this cell
        /// </summary>
        NO_RELATIONSHIP,

        /// <summary>
        ///   the path ends in this cell
        /// </summary>
        ENDING_CELL,
        EXITING_CELL
    }
}
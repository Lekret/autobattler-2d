namespace Logic
{
    public enum Team
    {
        Left, Right
    }

    public static class TeamExtensions
    {
        public static Team Opposite(this Team team)
        {
            if (team == Team.Left)
                return Team.Right;
            return Team.Left;
        }
    }
}
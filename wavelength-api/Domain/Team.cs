namespace Domain;

public enum Team : byte
{
    NONE = 0,
    ONE = 1,
    TWO = 2
}

public static class TeamHelper
{

    public static Team? GetTeamFromString(string value)
    {
        if (Int32.TryParse(value, out int j))
        {
            switch (j)
            {
                case 0:
                    return Team.NONE;
                case 1:
                    return Team.ONE;
                case 2:
                    return Team.TWO;
            }
        }

        return null;
    }
}
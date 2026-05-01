namespace helpdesk.Helper;

public class UniqueID
{
    public static string GeneratChatID(string s1, string s2)
    {
        string[] strings = [s1, s2]; Array.Sort(strings);
        string ID = string.Join("-", strings);
        return ID;
    }
    public static string GenerateUID()
    {
        string uid = Guid.NewGuid().ToString("N");
        return uid;
    }
}
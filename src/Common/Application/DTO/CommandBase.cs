namespace Application.DTO
{
    /// <summary>
    /// Base command that contains the fields common to all commands
    /// </summary>
    public class CommandBase
    {
        private string? UserName;

        public void SetUsername(string username)
        {
            UserName = username;
        }

        public string GetUserName() => UserName ?? string.Empty;
    }
}

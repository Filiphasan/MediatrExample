namespace MediatrExample.Shared.OptionModel
{
    public class MyTokenOptions
    {
        public string SecretKey { get; set; } = string.Empty;
        public int TokenExpireTimeMinute { get; set; }
    }
}

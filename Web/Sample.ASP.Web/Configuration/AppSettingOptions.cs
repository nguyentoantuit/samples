namespace Sample.ASP.Web.Configuration
{
    public class AppSettingOptions
    {
        public Redis Redis { get; set; }
        public ConnectionStrings ConnectionStrings { get; set; }
    }

    public class Redis
    {
        public string Configuration { get; set; }
    }

    public class ConnectionStrings
    {
        public string SampleWeb { get; set; }
        public string SampleAdmin { get; set; }
    }
}

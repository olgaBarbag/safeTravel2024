namespace SafeTravelApp.Services
{
    public interface IApplicationService
    {
        UserService UserService { get; }
        AgentService AgentService { get; }
        CitizenService CitizenService { get; }
    }
}

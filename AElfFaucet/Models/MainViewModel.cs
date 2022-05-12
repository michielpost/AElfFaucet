using AElfBlazor.Models;

namespace AElfFaucet.Models
{
    public class MainViewModel
    {
        public bool HasExtension { get; set; }
        public bool IsConnected { get; set; }
        public string? Address { get; set; }
        public BalanceResult? Balance { get; set; }

    }
}

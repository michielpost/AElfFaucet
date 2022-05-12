using AElfBlazor;
using AElfFaucet.Models;
using Microsoft.AspNetCore.Components;
using System.Dynamic;

namespace AElfFaucet.Pages
{
    public partial class Index
    {
        [Inject]
        public AElfService AElfService { get; set; } = default!;

        public MainViewModel Model { get; set; } = new();

        public bool IsLoading { get; set; }

        public string? TxId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Model.HasExtension = await AElfService.HasNightElfAsync();

            if (Model.HasExtension)
            {
                var testnetUrl = "https://explorer-test.aelf.io/chain";

                await AElfService.InitializeNightElfAsync("aelf Testnet Faucet", testnetUrl);

                Model.IsConnected = await AElfService.IsConnectedAsync();
            }

        }

        public async Task LoginAsync()
        {
            IsLoading = true;

            await AElfService.LoginAsync();
            Model.IsConnected = await AElfService.IsConnectedAsync();

            await GetSelectedAddress();
            await GetBalance();

            IsLoading = false;

        }

        public async Task GetSelectedAddress()
        {
            Model.Address = await AElfService.GetAddressAsync();
        }

        public async Task GetBalance()
        {
            IsLoading = true;
            Model.Balance = await AElfService.GetBalanceAsync();
            IsLoading = false;

        }

        public async Task TakeFromFaucetAsync()
        {
            var address = "2M24EKAecggCnttZ9DUUMCXi4xC67rozA87kFgid9qEwRUMHTs";
            string functionName = "Take";
            dynamic payload = new ExpandoObject();
            payload.symbol = "ELF";
            payload.amount = "10000000000";

            TxId = await AElfService.ExecuteSmartContractAsync(address, functionName, payload);
        }

    }
}

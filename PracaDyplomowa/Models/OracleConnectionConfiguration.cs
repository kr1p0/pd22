using Microsoft.Extensions.Hosting;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PracaDyplomowa.Models
{
    public class OracleConnectionConfiguration : IHostedService
    {
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            // Set TnsAdmin value to directory location of tnsnames.ora and sqlnet.ora files
            OracleConfiguration.TnsAdmin = @"Wallet_pd";

            // Set WalletLocation value to directory location of the ADB wallet (i.e. cwallet.sso)
            OracleConfiguration.WalletLocation = @"Wallet_pd";

        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            //Cleanup logic here
            Console.WriteLine("OracleConnectionConfiguration service stop.");
        }
    }
}

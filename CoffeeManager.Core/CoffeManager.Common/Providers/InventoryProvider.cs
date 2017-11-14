using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoffeeManager.Models;
using RestSharp.Portable;

namespace CoffeManager.Common
{
    public class InventoryProvider : ServiceBase, IInventoryProvider
    {
        public async Task<IEnumerable<SupliedProduct>> GetInventoryItems()
        {
            var request = CreateGetRequest(RoutesConstants.GetInventoryItems);
            return await ExecuteRequestAsync<SupliedProduct[]>(request);
        }

        public async Task<IEnumerable<InventoryItem>> GetInventoryReportDetails(int reportId)
        {
            var request = CreateGetRequest(RoutesConstants.GetInventoryReportDetails);
            request.Parameters.Add(new Parameter() { Name = nameof(reportId), Value = reportId });
            return await ExecuteRequestAsync<IEnumerable<InventoryItem>>(request);
        }

        public async Task<IEnumerable<InventoryReport>> GetInventoryReports()
        {
            var request = CreateGetRequest(RoutesConstants.GetInventoryReports);
            return await ExecuteRequestAsync<IEnumerable<InventoryReport>>(request);
        }

        public async Task SentInventoryInfo(IEnumerable<InventoryItem> items)
        {
            var request = CreatePostRequest(RoutesConstants.SentInventoryInfo);
            request.AddBody(items);
            await ExecuteRequestAsync<IEnumerable<InventoryItem>>(request);
        }

        public async Task ToggleItemInventoryEnabled(int suplyProductId)
        {
            var request = CreatePostRequest(RoutesConstants.ToggleItemInventoryEnabled);
            request.Parameters.Add(new Parameter() { Name = nameof(suplyProductId), Value = suplyProductId });
            await ExecuteRequestAsync(request);
        }
    }
}

using EPiServer.Commerce.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinstonTraining.Core.Utilities.CartExtensions
{
    public static class CartExtensions
    {
        public static void AddValidationIssues(this Dictionary<ILineItem, List<ValidationIssue>> issues, ILineItem lineItem, ValidationIssue issue)
        {
            if (!issues.ContainsKey(lineItem))
                issues.Add(lineItem, new List<ValidationIssue>());

            if (!issues[lineItem].Contains(issue))
                issues[lineItem].Add(issue);
        }
    }
}

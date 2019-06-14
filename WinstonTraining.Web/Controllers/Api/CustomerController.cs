using Mediachase.Commerce.Customers;
using System;
using System.Web.Http;

namespace WinstonTraining.Web.Controllers.Api
{
    [RoutePrefix("api/customer")]
    public class CustomerController : ApiController
    {
        class CustomerApiModel
        {
            public string CustomerId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }

            public CustomerApiModel(CustomerContact epiCustomerContact, Guid epiCustomerId  )
            {
                CustomerId = epiCustomerId.ToString();
                FirstName = epiCustomerContact.FirstName;
                LastName = epiCustomerContact.LastName;
            }
        }

        [HttpGet]
        [Route("")]
        public IHttpActionResult GetCurrentCustomer()
        {
            var customerContact = CustomerContext.Current.CurrentContact;
            var customerId = CustomerContext.Current.CurrentContactId;

            if (customerContact == null)
                return NotFound();

            return Ok(new CustomerApiModel(customerContact, customerId));
        }
    }
}


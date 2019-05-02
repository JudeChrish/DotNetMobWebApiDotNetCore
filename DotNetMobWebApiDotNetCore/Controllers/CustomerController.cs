﻿using DotnetMobDomain.Abstract;
using DotnetMobDomain.Concrete;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DotnetMobAPI.Controllers
{
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private ICustomer customerRepo;
        public CustomerController(ICustomer repo)
        {
            customerRepo = repo;
        }

        [HttpGet]
        [Route("getAllTheCustomers")]
        public IEnumerable<Customer> GetAllCustomers()
        {
            return customerRepo.GetAllCustomers();
        }

        [HttpGet]
        [Route("getSpecificCustomer/{customerId}")]
        public IQueryable<Customer> GetCustomerById(int customerId)
        {
            return customerRepo.GetCustomerById(customerId);
        }

        [HttpPost]
        [Route("SaveNewCustomer")]
        public HttpResponseMessage SaveCustomer(Customer customer)
        {
            HttpResponseMessage response = new HttpResponseMessage();
            if (customer.CustId == 0)
            {
                customerRepo.SaveCustomer(customer);
                response.ReasonPhrase = "Insert success";
                response.StatusCode = HttpStatusCode.OK;
            }
            else
            {
                response.ReasonPhrase = "Employee Id should not be there";
                response.StatusCode = HttpStatusCode.NotAcceptable;
            }
            return response;

        }

        [HttpPut]
        [Route("UpdateSelectedCustomer/{id}")]
        public IActionResult UpdateSelectedCustomer(int id, Customer cm)
        {
            try
            {
                customerRepo.UpdateCustomer(cm);
            }
            catch (Exception)
            {
                if (customerRepo.GetCustomerById(id).Count() == 0)
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteSelectedCustomer/{cusID}")]
        public void DeleteCustomer(int cusID)
        {
            customerRepo.DeleteCustomer(cusID);
        }
    }
}

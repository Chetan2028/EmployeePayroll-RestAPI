using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using EmployeePayroll_RestAPI;
using Newtonsoft.Json;
using System;

namespace MSTest
{
    [TestClass]
    public class RestSharpTestCase
    {
        RestClient client;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            //This path is used by all api's and urls to access Json Rest Api's
            client = new RestClient("http://localhost:4000");
        }

        /// <summary>
        /// Gets the employee list.
        /// </summary>
        /// <returns></returns>
        private IRestResponse GetEmployeeList()
        {
            //arrange
            RestRequest request = new RestRequest("/employees", Method.GET);

            //act
            IRestResponse response = client.Execute(request);
            return response;
        }

        /// <summary>
        /// Retrieves the employee list
        /// </summary>
        [TestMethod]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            int dataCount = 4;
            Assert.AreEqual(dataCount, dataResponse.Count);

            foreach (Employee emp in dataResponse)
            {
                Console.WriteLine("id : " + emp.id + " name : " + emp.name + " salary : " + emp.salary);
            }
        }
    }
}


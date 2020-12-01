using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestSharp;
using System.Collections.Generic;
using System.Net;
using EmployeePayroll_RestAPI;
using Newtonsoft.Json;
using System;
using Newtonsoft.Json.Linq;

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
        /// T.C -> 1
        /// Retrieves the employee list
        /// </summary>
        [TestMethod]
        public void OnCallingList_ReturnEmployeeList()
        {
            IRestResponse response = GetEmployeeList();

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.OK);
            List<Employee> dataResponse = JsonConvert.DeserializeObject<List<Employee>>(response.Content);
            int dataCount = 5;
            Assert.AreEqual(dataCount, dataResponse.Count);

            foreach (Employee emp in dataResponse)
            {
                Console.WriteLine("id : " + emp.id + " name : " + emp.name + " salary : " + emp.salary);
            }
        }

        [TestMethod]
        public void GivenEmployee_OnPost_ShouldReturnAddEmployee()
        {
            //Arrange
            RestRequest request = new RestRequest("/employees", Method.POST);
            JObject jObjectBody = new JObject();
            jObjectBody.Add("name", "SRK");
            jObjectBody.Add("salary", "78526");
            //id not added but addded in json file

            request.AddParameter("application/json", jObjectBody, ParameterType.RequestBody);

            //Act
            IRestResponse response = client.Execute(request);

            //Assert
            Assert.AreEqual(response.StatusCode, HttpStatusCode.Created);
            Employee dataResponse = JsonConvert.DeserializeObject<Employee>(response.Content);
            Assert.AreEqual("SRK", dataResponse.name);
            Assert.AreEqual("78526", dataResponse.salary);
            Console.WriteLine(response.Content);
        }
    }
}


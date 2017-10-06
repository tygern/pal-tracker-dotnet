using System;
using System.Net;
using Newtonsoft.Json.Linq;
using PalTracker;
using Xunit;

namespace PalTrackerTests
{
    [Collection("Integration")]
    public class TimeEntryIntegrationTest : IntegrationTest
    {
        [Fact]
        public void Read()
        {
            var id = CreateTimeEntry(new TimeEntry(999, 1010, Convert.ToDateTime("10/10/2015"), 9));

            var response = TestHttpClient.GetAsync($"/time-entries/{id}").Result;
            var responseBody = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(id, responseBody["Id"].ToObject<long>());
            Assert.Equal(999, responseBody["ProjectId"].ToObject<long>());
            Assert.Equal(1010, responseBody["UserId"].ToObject<long>());
            Assert.Equal("10/10/2015 00:00:00", responseBody["Date"].ToObject<string>());
            Assert.Equal(9, responseBody["Hours"].ToObject<int>());
        }

        [Fact]
        public void Create()
        {
            var timeEntry = new TimeEntry(222, 333, Convert.ToDateTime("01/08/2008"), 24);

            var response = TestHttpClient.PostAsync("/time-entries", SerializePayload(timeEntry)).Result;
            var responseBody = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            Assert.True(responseBody["Id"].ToObject<long>() > 0);
            Assert.Equal(222, responseBody["ProjectId"].ToObject<long>());
            Assert.Equal(333, responseBody["UserId"].ToObject<long>());
            Assert.Equal("01/08/2008 00:00:00", responseBody["Date"].ToObject<string>());
            Assert.Equal(24, responseBody["Hours"].ToObject<int>());
        }

        [Fact]
        public void List()
        {
            var id1 = CreateTimeEntry(new TimeEntry(222, 333, Convert.ToDateTime("01/08/2008"), 24));
            var id2 = CreateTimeEntry(new TimeEntry(444, 555, Convert.ToDateTime("02/10/2008"), 6));

            var response = TestHttpClient.GetAsync("/time-entries").Result;
            var responseBody = JArray.Parse(response.Content.ReadAsStringAsync().Result);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            Assert.Equal(id1, responseBody[0]["Id"].ToObject<int>());
            Assert.Equal(222, responseBody[0]["ProjectId"].ToObject<long>());
            Assert.Equal(333, responseBody[0]["UserId"].ToObject<long>());
            Assert.Equal("01/08/2008 00:00:00", responseBody[0]["Date"].ToObject<string>());
            Assert.Equal(24, responseBody[0]["Hours"].ToObject<int>());

            Assert.Equal(id2, responseBody[1]["Id"].ToObject<int>());
            Assert.Equal(444, responseBody[1]["ProjectId"].ToObject<long>());
            Assert.Equal(555, responseBody[1]["UserId"].ToObject<long>());
            Assert.Equal("02/10/2008 00:00:00", responseBody[1]["Date"].ToObject<string>());
            Assert.Equal(6, responseBody[1]["Hours"].ToObject<int>());
        }

        [Fact]
        public void Update()
        {
            var id = CreateTimeEntry(new TimeEntry(222, 333, Convert.ToDateTime("01/08/2008"), 24));
            var updated = new TimeEntry(999, 888, Convert.ToDateTime("08/12/2012"), 2);

            var putResponse = TestHttpClient.PutAsync($"/time-entries/{id}", SerializePayload(updated)).Result;            
            var getResponse = TestHttpClient.GetAsync($"/time-entries/{id}").Result;
            var getAllResponse = TestHttpClient.GetAsync("/time-entries").Result;
            
            Assert.Equal(HttpStatusCode.OK, putResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, getResponse.StatusCode);
            Assert.Equal(HttpStatusCode.OK, getAllResponse.StatusCode);
            
            var getAllResponseBody = JArray.Parse(getAllResponse.Content.ReadAsStringAsync().Result);
            
            Assert.Equal(1, getAllResponseBody.Count);
            Assert.Equal(id, getAllResponseBody[0]["Id"].ToObject<int>());
            Assert.Equal(999, getAllResponseBody[0]["ProjectId"].ToObject<long>());
            Assert.Equal(888, getAllResponseBody[0]["UserId"].ToObject<long>());
            Assert.Equal("08/12/2012 00:00:00", getAllResponseBody[0]["Date"].ToObject<string>());
            Assert.Equal(2, getAllResponseBody[0]["Hours"].ToObject<int>());

            var getResponseBody = JObject.Parse(getResponse.Content.ReadAsStringAsync().Result);
            
            Assert.Equal(id, getResponseBody["Id"].ToObject<int>());
            Assert.Equal(999, getResponseBody["ProjectId"].ToObject<long>());
            Assert.Equal(888, getResponseBody["UserId"].ToObject<long>());
            Assert.Equal("08/12/2012 00:00:00", getResponseBody["Date"].ToObject<string>());
            Assert.Equal(2, getResponseBody["Hours"].ToObject<int>());
        }

        [Fact]
        public void Delete()
        {
            var id = CreateTimeEntry(new TimeEntry(222, 333, Convert.ToDateTime("01/08/2008"), 24));

            var deleteResponse = TestHttpClient.DeleteAsync($"/time-entries/{id}").Result;
            var getResponse = TestHttpClient.GetAsync($"/time-entries/{id}").Result;
            var getAllResponse = TestHttpClient.GetAsync("/time-entries").Result;

            Assert.Equal(HttpStatusCode.NoContent, deleteResponse.StatusCode);
            Assert.Equal(HttpStatusCode.NotFound, getResponse.StatusCode);
            Assert.Equal("[]", getAllResponse.Content.ReadAsStringAsync().Result);
        }

        private long CreateTimeEntry(TimeEntry timeEntry)
        {
            var response = TestHttpClient.PostAsync("/time-entries", SerializePayload(timeEntry)).Result;

            Assert.Equal(HttpStatusCode.Created, response.StatusCode);

            var responseBody = JObject.Parse(response.Content.ReadAsStringAsync().Result);

            return responseBody["Id"].ToObject<long>();
        }

    }
}
using System.Net.Http;
using CRMIntegration.ConnectionMaster;
using System.Threading.Tasks;
using System.Net;
using CRMIntegration.Models;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Linq;

namespace CRMIntegration.CRMRepository
{
    public  class ApiQuery
    {
        private readonly HttpClient _crmClient;
        public ApiQuery()
        {
            _crmClient = CrmHttpClient.GetClient();
        }




        public object getContact()
        {

            try {
                //string filter = $"&$filter=description eq 'SA'";
                string urlPath = $"/api/data/v9.1/contacts(8F508B50-F086-EF11-AC20-7C1E525D7168)";
                urlPath += $"?$select=contactid,firstname,lastname,mobilephone,emailaddress1,middlename,createdon,_parentcustomerid_value";
                HttpResponseMessage retrieveResponse = _crmClient.GetAsync(urlPath).Result;


                if (retrieveResponse.IsSuccessStatusCode)
                {
                    string jRetrieveResponse = retrieveResponse.Content.ReadAsStringAsync().Result;
                    contactEntity contactDetails = JsonConvert.DeserializeObject<contactEntity>(jRetrieveResponse);

                    return contactDetails;
                }
            }
            catch (Exception ex){
                throw new Exception(ex.Message);
            }


            return null;


        }

        public List<AppointmentReqEntity> getAppointmentReqByEmail(string customerEmail)
        {

            try
            {
                string urlPath = $"api/data/v9.2/sm_appointmentrequests";
                string select = "?$select=sm_appointmentrequestid,sm_name,createdon,statuscode,sm_preferredtime,sm_preferreddays,_ownerid_value,sm_description,_sm_customer_value";
                string filter = $"&$filter=(sm_Customer/emailaddress1 eq '{customerEmail}' and statecode eq 0)&$orderby=createdon desc";
                string expand = "&$expand=sm_Customer($select=emailaddress1)";                 
                urlPath += select + expand + filter;

                HttpResponseMessage retrieveResponse = _crmClient.GetAsync(urlPath).Result;

                List<AppointmentReqEntity> entityList = new List<AppointmentReqEntity>();
                if (retrieveResponse.IsSuccessStatusCode)
                {
                    string jRetrieveResponse = retrieveResponse.Content.ReadAsStringAsync().Result;
                    appResponse p = JsonConvert.DeserializeObject<appResponse>(jRetrieveResponse);
                    entityList = (List<AppointmentReqEntity>)p.Value;

                    return entityList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return null;


        }

        public List<CompletedAppointmentsDto> getCompletedAppointments(string customerId)
        {

            try
            {
                string urlPath = $"api/data/v9.2/appointments";
                string select = "?$select=subject,scheduledstart,scheduledend,statuscode,_regardingobjectid_value";
                string expand = "&$expand=regardingobjectid_sm_appointmentrequest_appointment($select=_sm_relationshipmanager_value,_sm_customer_value)";
                string filter = $"&$filter=(statecode eq 1) and (regardingobjectid_sm_appointmentrequest_appointment/_sm_customer_value eq {customerId})&$orderby=scheduledstart asc";
               
                urlPath += select + expand + filter;


                HttpResponseMessage retrieveResponse = _crmClient.GetAsync(urlPath).Result;

                List<CompletedAppointmentsDto> entityList = new List<CompletedAppointmentsDto>();
                if (retrieveResponse.IsSuccessStatusCode)
                {
                    string jRetrieveResponse = retrieveResponse.Content.ReadAsStringAsync().Result;
                    compAppResponse p = JsonConvert.DeserializeObject<compAppResponse>(jRetrieveResponse);


                    foreach (var app in p.Value)
                    {
                        CompletedAppointmentsDto Appointments = new CompletedAppointmentsDto
                        {
                            subject = app.subject,
                            startTime = app.startTime,
                            endTime = app.endTime,
                            status = app.Status,
                            executiveName = app.relatedAppReq.ExecutiveName,
                            relMgerName = app.relatedAppReq.relMgrName
                        };

                        entityList.Add(Appointments);
                    }
                    
                    return entityList;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            return null;


        }

        public object CreateContact(CreateUser createContact)
        {
            contactCreationPayload contactpayload = new contactCreationPayload();

            contactpayload.emailAddress = createContact.emailAddress;

            contactpayload.lastName = createContact.lastName;

            if (createContact.firstName != null && !String.IsNullOrEmpty(createContact.firstName))
                contactpayload.firstName = createContact.firstName;

            if (createContact.middleName != null && !String.IsNullOrEmpty(createContact.middleName))
                contactpayload.middleName = createContact.middleName;

            if (createContact.mobileNumber != null && !String.IsNullOrEmpty(createContact.mobileNumber))
                contactpayload.mobileNumber = createContact.mobileNumber;

            if (createContact.address != null && !String.IsNullOrEmpty(createContact.address))
                contactpayload.address = createContact.address;

            string requestBody = JsonConvert.SerializeObject(contactpayload);

            var response = CreateEntityRecord("contacts", requestBody, "false");

            if(response != null)
            {
                return response;
            }

            return null;
        }


        public object CreateAppointmentReq(CustomerAppointment appdata, string customerid)
        {
            AppointmentCreationReqEntity AppReqParam = new AppointmentCreationReqEntity();

            AppReqParam.DiscussionSubject = appdata.DiscussionSubject;

            if (!String.IsNullOrEmpty(appdata.Description))
                AppReqParam.Description = appdata.Description;

            if (!String.IsNullOrEmpty(customerid))
                AppReqParam.contactId = $"/contacts({customerid})";

            if (appdata.PreferredDays != null && appdata.PreferredDays.Count > 0)
            {

                string selectedDays = String.Join(",", appdata.PreferredDays);

                AppReqParam.PreferredDays = selectedDays;

            }

            if (appdata.PreferredDays != null && appdata.PreferredDays.Count > 0)
            {
                string selectedTimeslots = String.Join(",", appdata.PreferredTime);

                AppReqParam.PreferredTime = selectedTimeslots;
            }



            string requestBody = JsonConvert.SerializeObject(AppReqParam);

            var response = CreateEntityRecord("sm_appointmentrequests", requestBody, "false");

            if (response != null)
            {
                return response;
            }

            return null;
        }





        private object CreateEntityRecord(string entity, string requestBody, string allowDuplicate)
        {
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, $"/api/data/v9.1/{entity}");
            request.Content = new StringContent(requestBody);
            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");

            request.Headers.Add("MSCRM.SuppressDuplicateDetection", allowDuplicate);
            HttpResponseMessage response = _crmClient.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result;

            Guid entityId = new Guid();
            if (response.IsSuccessStatusCode)
            {
                string accountUri = response.Headers.GetValues("OData-EntityId").FirstOrDefault();
                if (accountUri != null)
                    entityId = Guid.Parse(accountUri.Split('(', ')')[1]);

                return entityId.ToString();
            }

            return null;
        }


    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web;
using System.Web.Helpers;

namespace SchoolApp.API.Models
{
    public enum ResponceStatus
    {
        error = 404,
        success = 200
    }

    public class ReturnResponce
    {
        //For success responce
        public ReturnResponce(dynamic _data, string[] _jsonproperties = null)
        {
            status = ResponceStatus.success;
            Orgdata = _data;
            Message = "success";

            if (_jsonproperties != null)
            {
                jsonIngnoreProperties = _jsonproperties;
            }

            getData();
        }

        public ReturnResponce(dynamic _data, string SuccessMessage,  string[] _jsonproperties = null)
        {
            status = ResponceStatus.success;
            Orgdata = _data;
            Message = "success : " + SuccessMessage;

            if (_jsonproperties != null)
            {
                jsonIngnoreProperties = _jsonproperties;
            }

            getData();
        }

        //For Error responce
        public ReturnResponce(string _error)
        {
            status = ResponceStatus.error;
            Message = "error";
            error = _error;
        }

        public ResponceStatus status { get; set; }

      
        protected string[] jsonIngnoreProperties { get; set; }

        public string error { get; set; }

        public string Message { get; set; }

        protected dynamic Orgdata { get; set; }

        public dynamic data { get; set; }

        public void getData()
        {
            if (Orgdata != null)
            {
                if (jsonIngnoreProperties != null && jsonIngnoreProperties.Length > 0)
                {
                    data = Json.Decode(JsonConvert.SerializeObject(Orgdata, new JsonSerializerSettings()
                    { ContractResolver = new IgnorePropertiesResolver(jsonIngnoreProperties) }));
                }
                else
                {
                    data = Orgdata;
                }
            }
            else
            {
                data = null;
            }
        }
    }

    //short helper class to ignore some properties from serialization
    public class IgnorePropertiesResolver : DefaultContractResolver
    {
        private readonly HashSet<string> ignoreProps;
        public IgnorePropertiesResolver(IEnumerable<string> propNamesToIgnore)
        {
            this.ignoreProps = new HashSet<string>(propNamesToIgnore);
        }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            if (this.ignoreProps.Contains(property.PropertyName))
            {
                property.ShouldSerialize = _ => false;
            }
            return property;
        }
    }


    public class ReturnResponceWithTokem
    {
        //For success responce
        public ReturnResponceWithTokem(dynamic _data, string _Token, string[] _jsonproperties = null)
        {
            status = ResponceStatus.success;
            Orgdata = _data;
            Message = "success";
            AccessToken = _Token;

            if (_jsonproperties != null)
            {
                jsonIngnoreProperties = _jsonproperties;
            }

            getData();
        }

        //For Error responce
        public ReturnResponceWithTokem(string _error)
        {
            status = ResponceStatus.error;
            Message = "error";
            error = _error;
            AccessToken = "unauthorized";
        }

        public ResponceStatus status { get; set; }


        protected string[] jsonIngnoreProperties { get; set; }

        public string error { get; set; }

        public string Message { get; set; }

        protected dynamic Orgdata { get; set; }

        public dynamic data { get; set; }

        public void getData()
        {
            if (Orgdata != null)
            {
                if (jsonIngnoreProperties != null && jsonIngnoreProperties.Length > 0)
                {
                    data = Json.Decode(JsonConvert.SerializeObject(Orgdata, new JsonSerializerSettings()
                    { ContractResolver = new IgnorePropertiesResolver(jsonIngnoreProperties) }));
                }
                else
                {
                    data = Orgdata;
                }
            }
            else
            {
                data = null;
            }
        }

        public string AccessToken { get; set; }
    }

  

}
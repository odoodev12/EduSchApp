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
            message = "success";

            if (_jsonproperties != null)
            {
                jsonIngnoreProperties = _jsonproperties;
            }

            getData();
        }

        //For success responce
        public ReturnResponce(string _error)
        {
            status = ResponceStatus.error;
            message = "error";
            error = _error;
        }

        public ResponceStatus status { get; set; }

      
        private string[] jsonIngnoreProperties { get; set; }

        public string error { get; set; }

        public string message { get; set; }

        private dynamic Orgdata { get; set; }

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
}
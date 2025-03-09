﻿namespace BTM.Account.Shared.Common
{
    public static class GlobalConstants
    {
        public static class ApiConstants
        {
            //API project specific constants that are not to be
            //shared with the API project or other projects, go here.
            public const string Root = "api";
            public const string BaseApiUrl = "https://localhost:44300";

            public const string AdminPanel = Root + "/" + "AdminPanel";
        }
        public static class Roles
        {
            public const string Admin = "admin";
            public const string Registered = "registered";
        }

        public static class Links
        {
            public const string AccountClient = "https://localhost:7237";
            public const string AccountAPI = "https://localhost:7150/";
            public const string IdentityService = "https://localhost:44301";
        }

        public static class Methods
        {
            public const string Get = "GET";
            public const string Post = "POST";
            public const string Put = "PUT";
            public const string Delete = "DELETE";

            public const string AdminPanel = "AdminPanel";
        }
    }
}

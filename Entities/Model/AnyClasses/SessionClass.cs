using Entities.DTO;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Model.AnyClasses
{
    public static class SessionClass
    {


        private static string token1 = "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9." +
            "eyJuYW1laWQiOiJhbGwiLCJyb2xlIjoiQW55IiwibmJmIjoxNzExNjk3MDUxLCJleHAi" +
            "OjE3MTE2OTcwNTIsImlhdCI6MTcxMTY5NzA1MX0.o64p8lC1PMQDjPsmH6dWR1N-tuyTEG9-v0NHfJVHdlk";


        public static int id { get; set; }

        public static string tokenCheck = token1;
        public static string token { get; set; } = token1;

        public static string fileUploadsUrl = @"";

        public static readonly string[] allowedExtensions = { ".jpg", ".jpeg", ".mp4", ".avi", ".png", ".gif", ".doc", ".docx", ".xlsx", ".pdf", ".ppt", ".pptx" };

        public static readonly string[] allowedExtensionsImg = { ".jpg", ".jpeg", ".png", ".gif" };

        public static readonly string[] allowedExtensionsDoc = { ".doc", ".docx", ".pdf", ".ppt", ".pptx", ".xlsx" };

        public static readonly string[] allowedExtensionsVideo = { ".mp4", ".avi" };

        public static readonly string[] allowedExtensionsFile = { ".mp4", ".avi", ".doc", "docx", ".xlsx", ".docx", ".pdf", ".ppt", ".pptx" };


    }

}



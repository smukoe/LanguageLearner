using System;
using Xunit;
using LanguageLearning.Models.JWT;
using Moq;
using Microsoft.Extensions.Configuration;
using LanguageLearning.Interfaces;
using System.Collections.Generic;
using LanguageLearning.Models.UserAccount;
using LanguageLearning.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json.Linq;

namespace LanguageLearningTests
{
    public class JwtValidation_Test
    {       
        [Fact]
        public void WhenValidatingJwt_GivenValidToken_ThenTokenShouldBeValidated()
        {
            //Arrange
            string secretKey = "supersecretkeythatneedstobemuchmoresecure";
            string issuer = "https://localhost:44313/";
            string audience = "https://localhost:44313/";

            //token has an expiry time of 10 years            
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhIiwianRpIjoiNzcwYWE0NDktN2QxOC00NTY4LTlkNWYtMzc2NTg1NTM2NzQyIiwiZXhwIjoxODUwODUyNzk5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxMy8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxMy8ifQ.aKAgfAsiV5--SIZI95z6yw6hwttSxMmJgzpbGDwQPAE";           

            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig
                .SetupGet(c => c["Jwt:SecretKey"]).Returns(secretKey);
            mockConfig
                .SetupGet(c => c["Jwt:Issuer"]).Returns(issuer);
            mockConfig
                .SetupGet(c => c["Jwt:Audience"]).Returns(audience);

            JwtValidation jwtValidationTest = new JwtValidation(mockConfig.Object);

            //Act
            bool result = jwtValidationTest.ValidateToken(token);

            //Assert
            Assert.True(result);
        }

        [Fact]
        public void WhenParsingTokenToJSON_GivenValidToken_ThenJSONObjectShouldNotBeNull()
        {
            //Arrange
            string secretKey = "supersecretkeythatneedstobemuchmoresecure";
            string issuer = "https://localhost:44313/";
            string audience = "https://localhost:44313/";

            //token has an expiry time of 10 years            
            string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJhIiwianRpIjoiNzcwYWE0NDktN2QxOC00NTY4LTlkNWYtMzc2NTg1NTM2NzQyIiwiZXhwIjoxODUwODUyNzk5LCJpc3MiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxMy8iLCJhdWQiOiJodHRwczovL2xvY2FsaG9zdDo0NDMxMy8ifQ.aKAgfAsiV5--SIZI95z6yw6hwttSxMmJgzpbGDwQPAE";

            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig
                .SetupGet(c => c["Jwt:SecretKey"]).Returns(secretKey);
            mockConfig
                .SetupGet(c => c["Jwt:Issuer"]).Returns(issuer);
            mockConfig
                .SetupGet(c => c["Jwt:Audience"]).Returns(audience);

            JwtValidation jwtValidationTest = new JwtValidation(mockConfig.Object);

            //Act

            JObject jsonResult = jwtValidationTest.ParseTokenToJSON(token);

            //Assert
            Assert.NotNull(jsonResult);
        }
    }
}

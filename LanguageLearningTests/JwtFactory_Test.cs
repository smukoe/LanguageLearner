using LanguageLearning.Interfaces;
using LanguageLearning.Models;
using LanguageLearning.Models.JWT;
using LanguageLearning.Models.UserAccount;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LanguageLearningTests
{
    public class JwtFactory_Test
    {
        [Fact]
        public void WhenCreatingNewJwt_GivenValidUserDataOrString_TokenShouldNotBeNull()
        {
            //Arrange
            string secretKey = "supersecretkeythatneedstobemuchmoresecure";
            string issuer = "https://localhost:44313/";
            string audience = "https://localhost:44313/";

            UserData userData = new UserData
            {
                ID = 100,
                UserName = "username",
                Password = "asPodiaSjsdoDajda",
                StringifiedSalt = "aidEasuwenUapScaiwmq"
            };

            string anyString = "testsubject";

            Mock<IConfiguration> mockConfig = new Mock<IConfiguration>();
            mockConfig
                .SetupGet(c => c["Jwt:SecretKey"]).Returns(secretKey);
            mockConfig
                .SetupGet(c => c["Jwt:Issuer"]).Returns(issuer);
            mockConfig
                .SetupGet(c => c["Jwt:Audience"]).Returns(audience);

            Mock<ILoginManager> mockLoginManager = new Mock<ILoginManager>();

            JwtFactory jwtFactoryTest = new JwtFactory(mockConfig.Object, mockLoginManager.Object);

            //Act
            JwToken tokenFromUserData = jwtFactoryTest.GenerateToken(userData);
            JwToken tokenFromString = jwtFactoryTest.GenerateToken(anyString);

            //Assert
            Assert.NotNull(tokenFromUserData);
            Assert.NotNull(tokenFromString);
        }
    }
}

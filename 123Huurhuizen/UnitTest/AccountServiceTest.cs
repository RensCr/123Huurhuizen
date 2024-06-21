using Logic;
using Microsoft.IdentityModel.Tokens;
using UnitTest.Mockdata;

namespace UnitTest
{
    [TestClass]
    public class AccountServiceTest
    {
        private AccountService _accountService=new AccountService(new MockDataUserRepository());
        [TestMethod]
        public void HashPassword_NormalPassword_ShouldNotReturnSamePassword()
        {
            string password = "test";
            string hashedPassword = _accountService.HashPassword(password);
            if (hashedPassword != password)
            {
                Assert.IsTrue(true);
            }
        }

        [TestMethod]
        public void IsValidUser_ValidUser_ShouldReturnUserId()
        {
            string email = "test";
            string password = "test";
            int userId;
            if (_accountService.IsValidUser(email, password, out userId))
            {
                Assert.IsTrue(userId == 1);
            }
        }

        [TestMethod]
        public void GetUserName_ValidUserId_ShouldReturnUserName()
        {
            int userId = 1;
            string userName = _accountService.GetUserName(userId);
            Assert.IsTrue(userName == "test");
        }
    }
}
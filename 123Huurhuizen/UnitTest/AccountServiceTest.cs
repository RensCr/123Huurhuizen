using UnitTest.Mockdata;

namespace UnitTest
{
    [TestClass]
    public class AccountServiceTest
    {
        [TestMethod]
        public void TestHashPassword()
        {
            if (new MockAccountService().HashPassword("test") != "test")
            {
                Assert.Fail();
            }
        }
    }
}
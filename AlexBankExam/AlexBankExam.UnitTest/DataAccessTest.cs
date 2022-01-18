using NUnit.Framework;
using AlexBankExam.Persistence;

namespace AlexBankExam.UnitTest
{
    [TestFixture]
    public class DataAccessTest
    {
        //private SQLHelper sqlHelper;
        //private DataContext context;

        [SetUp]
        public void Setup()
        {
            //context = new DataContext();
            //sqlHelper = new SQLHelper(context);
        }

        [Test]
        public void GetData_Customers_Test()
        {
            Assert.Pass();
        }
    }
}
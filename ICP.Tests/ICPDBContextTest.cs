using ICP.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ICP.Tests
{
    [TestClass]
    public class ICPDBContextTest
    {
        [TestMethod]
        public void DBContaxt_created_successfully()
        {
            var db = new ICPTestDbContext();

            Assert.IsNotNull(db.ContextId);
        }

    }
}

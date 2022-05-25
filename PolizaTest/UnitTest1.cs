using Microsoft.VisualStudio.TestTools.UnitTesting;
using PolizaDB.Context;
using PolizaDB.DTOs;

namespace PolizaTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginAutentificarTrue()
        {
            AutosContext context = new AutosContext();
            UserDto use = new UserDto();
            use.UserName = "admin";
            use.Password = "admin";
            var login = context.Login(use);

            Assert.AreEqual("Bienvenido al sistema", login.Mensaje);
        }

        [TestMethod]
        public void LoginAutentificarFalse()
        {
            AutosContext context = new AutosContext();
            UserDto use = new UserDto();
            use.UserName = "admin";
            use.Password = "admin1";
            var login = context.Login(use);

            Assert.AreEqual("Credenciales invalidas", login.Mensaje);
        }

        [TestMethod]
        public void ObtenerPolizaPorPlaca()
        {
            AutosContext context = new AutosContext();
            var poliza = context.ObtenerPoliza(3400, "");

            Assert.IsTrue(poliza.EsError);
        }

        [TestMethod]
        public void ObtenerPolizaPorPoliza()
        {
            AutosContext context = new AutosContext();
            var poliza = context.ObtenerPoliza(0, "2233B");

            Assert.IsTrue(!poliza.EsError);
        }
    }
}

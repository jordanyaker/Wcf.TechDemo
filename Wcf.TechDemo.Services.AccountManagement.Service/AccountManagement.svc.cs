
namespace TechDemo.Services.AccountManagement {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.Web;
    using System.Text;
    using TechDemo.Services.AccountManagement.Contracts;

    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    public class AccountManagement : IAccountManagement {

        public bool Authenticate(string username, string password) {
            /*
             * user = Attempt to retrieve user from cache.
             *  
             * IF (user == null) THEN
             *      Get user from repository.
             * 
             *      IF (user == null) THEN
             *          RETURN false
             *      END
             *      
             *      Store user in cache
             * END
             * 
             * RETURN user.password == password
             */
            throw new NotImplementedException();
        }
    }
}

namespace TechDemo.Services.AccountManagement.Contracts {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.ServiceModel;

    [ServiceContract]
    public interface IAccountManagement {
        [OperationContract]
        bool Authenticate(string username, string password);
    }
}

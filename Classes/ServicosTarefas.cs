using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;

namespace GestaoDeTarefas.Classes
{
    public class ServicosTarefas
    {
        public static string SenhaFireBase = "qBee02kbr48SqETNmnKEgS72NQiEE95LSsyECiFn";

        FirebaseClient Cliente = new FirebaseClient("https://gestaodetarefas-5c5fd-default-rtdb.europe-west1.firebasedatabase.app/", new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(SenhaFireBase)
    });
    }
}

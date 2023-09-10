using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Text.Json;
namespace Boss.Az
{
    internal class Worker:Person
    {
        public override Notfication Notfications { get; set; }
        public Worker(string name, string surName, string gmail,string Paswword)
            : base(name, surName, gmail,Paswword) { }


       static  public void Regstration(Worker wk)
        {
            int VerifyCode = Random.Shared.Next(111111, 999999);
            int result = SmtpServerConnection.GmailVerify(wk.Gmail, VerifyCode);
            if (result != VerifyCode)
                throw new Exception("incorret verify code");
            var JsonForm = JsonConvert.SerializeObject(wk);
            File.WriteAllText("Workers.json", JsonForm);
        }
        static public bool LogIn(string name,string password)
        {
            var jsonfile = File.ReadAllText("workers.json");
            var workers = JsonConvert.DeserializeObject<Worker[]>(jsonfile);
            foreach (var worker in workers)
            {
                if (worker.Name == name && worker.Paswword == password)
                    return true;
            }
            return false;
        }

    }
}

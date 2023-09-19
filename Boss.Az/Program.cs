using Boss.Az.ManageDataBase;
using System.Text.Json;
using System;
using System.IO;

namespace Boss.Az
{
    internal class Program
    {
        static bool ColorTemp = true;
        static void ColorSettings(string[] choices, int start)
        {
            for (int i = 0; i < choices.Length; i++)
            {
                if (i == start - 1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.WriteLine(choices[i]);
                    Console.BackgroundColor = default;
                }
                else
                    Console.WriteLine(choices[i]);
            }
        }
        static void Main(string[] args)
        {
            FillData();
            StartUp();
        }

        static void FillData()
        {
            string?[] jsonContentNot = default;
            string?[] jsonContentW = default;
            string?[] jsonContentE = default;
            if (File.Exists("Workers.json"))
            {
                jsonContentW = File.ReadAllLines("Workers.json");
                foreach (string line in jsonContentW)
                    Database.Workers.Add(JsonSerializer.Serialize<Worker>(line))
            }
            if (File.Exists("Ewmployers.json"))
            {
                jsonContentE = File.ReadAllLines("Employers.json");
                foreach (string line in jsonContentWE
                    Database.Employers.Add(JsonSerializer.Serialize<Employer>(line))
            }
            if (File.Exists("Notfications.json"))
            {
                jsonContentNotfications = File.ReadAllLines("Notfications.json");
                foreach (string line in jsonContentNotfications)
                {
                    var data = JsonSerializer.de
                }
            }

        }
        static public void LogInWorker()
        {
            Console.Write("enter your name: ");
            var name = Console.ReadLine();
            Console.Write("enter your password: ");
            var password = Console.ReadLine();
            var jsonfile = File.ReadAllText("workers.json");
            var workers = JsonConvert.DeserializeObject<Worker[]>(jsonfile);
            foreach (var worker in workers)
            {
                if (worker.Name == name && worker.Paswword == password)
                {
                    int temp = 1;
                    while (true)
                    {
                        string[] choices = { "Notfications", "All Job Postings", "Filter Jobs", "Exit" };
                        ColorSettings(choices, temp);
                        var key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (temp == 4)
                                    temp = 1;
                                else
                                    temp++;
                                break;
                            case ConsoleKey.UpArrow:
                                if (temp == 1)
                                    temp = 4;
                                else
                                    temp--;
                                break;
                            case ConsoleKey.Enter:
                                if (temp == 1)
                                {
                                    foreach (var notfic in worker.Notfications)
                                        notfic.ShowNotfics();
                                }
                                else if (temp == 2)
                                    AllJobPostings(worker.Id);
                                else if (temp == 3)
                                    FilteredJobs(worker.Id);
                                else
                                    StartUp();
                                break;

                        }

                    }
                }
            }
        }
        static void FilteredJobs(int ById)
        {
            string profession = CommonJobs();
            foreach (var em in Database.Employers)
            {
                if (em.CvEmployer.WorkArea == profession)
                    em.CvEmployer.ShowInfo();
            }
            Console.Write("1 - request\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of Posting");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var em in Database.Employers)
                {
                    if (em.CvEmployer.Id == IdPosting)
                    {
                        em.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id), em.Id);
                        var JsonForm = JsonConvert.SerializeObject(em.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("Notfications.json", JsonForm); return;
                    }
                }
            }
            else
                StartUp();
        }
        static void AllJobPostings(int ById)
        {
            foreach (var em in Database.Employers)
                em.CvEmployer.ShowInfo();
            Console.Write("1 - request\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of Posting");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var em in Database.Employers)
                {
                    if (em.CvEmployer.Id == IdPosting)
                    {
                        em.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id), em.Id);
                        var JsonForm = JsonConvert.SerializeObject(em.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("Notfications.json", JsonForm); return;
                    }
                }
            }
            else
                StartUp();

        }

        static void GuestFunc()
        {


        }
        static void WorkerFunc()
        {
            int start = 1;
            while (true)
            {
                Console.Clear();
                string[] choices = { "LogIn", "Registration" };
                ColorSettings(choices, start);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (start == 1)
                            start = 2;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (start == 1)
                            start = 2;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                            LogInWorker();
                        else if (start == 2)
                            RegstrationWorker();
                        break;
                }
            }
        }
        static string CommonJobs()
        {
            int temp = 1;
            while (true)
            {
                Console.Clear();
                string[] arr = new[] { "prgramming", "IT", "Design" };
                ColorSettings(arr, temp);
                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (temp == 3)
                            temp = 1;
                        else
                            temp++;
                        break;
                    case ConsoleKey.UpArrow:
                        if (temp == 1)
                            temp = 3;
                        else
                            temp--;
                        break;
                    case ConsoleKey.Enter:
                        return arr[temp - 1];
                }
            }
        }
        static public void RegstrationWorker()
        {
            Worker wk = new();
            Console.Write("enter Name: ");
            wk.Name = Console.ReadLine();
            Console.Write("enter Surname: ");
            wk.Surname = Console.ReadLine();
            Console.Write("enter gmail: ");
            wk.Gmail = Console.ReadLine();
            Console.Write("work area\n: ");
            wk.CvWorker.ProfessionKind = CommonJobs();
            Console.Write("enter your work experince: ");
            wk.CvWorker.WorkExperince = int.Parse(Console.ReadLine());
            Console.WriteLine("wants amount: ");
            wk.CvWorker.WantsAmount = int.Parse(Console.ReadLine());
            Console.WriteLine("birthday: ");
            wk.CvWorker.Birthdate = Convert.ToDateTime((Console.ReadLine()));
            Console.WriteLine("marital status 1/2: ");
            wk.CvWorker.MaritalStatus = int.Parse(Console.ReadLine());
            Console.WriteLine("Gender 1/2: ");
            wk.CvWorker.Gender = int.Parse(Console.ReadLine());
            Console.WriteLine("enter password: ");
            wk.Paswword = Console.ReadLine();
            int VerifyCode = default;
            int result = default;
            do
            {
                VerifyCode = Random.Shared.Next(111111, 999999);
                result = SmtpServerConnection.GmailVerify(wk.Gmail, VerifyCode);
                Console.Clear();
            } while (VerifyCode != result);
            var JsonForm = JsonConvert.SerializeObject(wk);
            File.AppendAllText("Workers.json", JsonForm);
        }
        public static void RegstrationEmployer()
        {
            Employer em = new();
            Console.Write("enter Company Name: ");
            em.CompanyName = Console.ReadLine();
            Console.Write("enter gmail: ");
            em.Gmail = Console.ReadLine();
            Console.Write("work area\n: ");
            em.CvEmployer.WorkArea = CommonJobs();
            Console.Write("enter period of experince: ");
            em.CvEmployer.RequiredWorkExperience = int.Parse(Console.ReadLine());
            Console.WriteLine("enter languages: ");
            em.CvEmployer.RequiredLanguage = Console.ReadLine();
            Console.WriteLine("enter password: ");
            em.password = Console.ReadLine();

            int VerifyCode = Random.Shared.Next(111111, 999999);
            int result = SmtpServerConnection.GmailVerify(em.Gmail, VerifyCode);
            if (result != VerifyCode)
                throw new Exception("incorret verify code");
            var JsonForm = JsonConvert.SerializeObject(em);
            File.AppendAllText("Employers.json", JsonForm);
        }
        static void EmployerFunc()
        {
            int start = 1;
            while (true)
            {
                Console.Clear();
                string[] choices = { "LogIn", "Registration" };
                ColorSettings(choices, start);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (start == 1)
                            start = 2;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.DownArrow:
                        if (start == 1)
                            start = 2;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                            LogInEmployer();
                        else if (start == 2)
                            RegstrationEmployer();
                        break;
                }
            }
        }
        static void AllJobSeekers(int ById)
        {
            foreach (var worker in Database.Workers)
                em.CvEmployer.ShowInfo();
            Console.Write("1 - invited\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of cv");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var worker in Database.Workers)
                {
                    if (worker.CvEmployer.Id == IdPosting)
                    {
                        worker.Notfications.Add(new Notfication("invited", ById, worker.Id);
                        var JsonForm = JsonConvert.SerializeObject(worker.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("Notfications.json", JsonForm); return;
                    }
                }
            }
            else
                StartUp();
        }
        static void LogInEmploer()
        {
            Console.Write("enter your name: ");
            var name = Console.ReadLine();
            Console.Write("enter your password: ");
            var password = Console.ReadLine();
            foreach (var em in Database.Employers)
            {
                if (em.CompanyName == name && em.password == password)
                {
                    int temp = 1;
                    while (true)
                    {
                        string[] choices = { "Notfications", "All Job Seekers", "Filter Job Seekers", "Exit" };
                        ColorSettings(choices, temp);
                        var key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (temp == 4)
                                    temp = 1;
                                else
                                    temp++;
                                break;
                            case ConsoleKey.UpArrow:
                                if (temp == 1)
                                    temp = 4;
                                else
                                    temp--;
                                break;
                            case ConsoleKey.Enter:
                                if (temp == 1)
                                {
                                    foreach (var notfic in em.Notfications)
                                        notfic.ShowNotfics();
                                }
                                else if (temp == 2)
                                    AllJobSeekers(em.Id);
                                else if (temp == 3)
                                    FilteredJobSeekers(em.Id);
                                else
                                    StartUp();
                                break;

                        }

                    }
                }
            }

        }
        static void FilteredJobSeekers(int ById)
        {
            string profession = CommonJobs();
            foreach (var worker in Database.Workers)
            {
                if (worker.CvWorker.ProfessionKind == profession)
                    worker.CvWorker.showInfo();
            }
            Console.Write("1 - invited\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of cv");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var worker in Database.Workers)
                {
                    if (worker.CvEmployer.Id == IdPosting)
                    {
                        worker.Notfications.Add(new Notfication("invited", ById, worker.Id);
                        var JsonForm = JsonConvert.SerializeObject(worker.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("Notfications.json", JsonForm); return;
                    }
                }
            }
            else
                StartUp();
        }
        static void StartUp()
        {
            int start = 1;
            while (true)
            {
                string[] choices = { "Guest", "Worker", "Employer" };
                ColorSettings(choices, start);
                var key = Console.ReadKey();
                switch (key.Key)
                {

                    case ConsoleKey.DownArrow:
                        if (start < 3)
                            start++;
                        else
                            start = 1;
                        break;
                    case ConsoleKey.UpArrow:
                        if (start > 1)
                            start--;
                        else
                            start = 3;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                            GuestFunc();
                        else if (start == 2)
                            WorkerFunc();
                        else if (start == 3)
                            EmployerFunc();
                        break;
                }
                Console.Clear();
            }
        }
    }
}

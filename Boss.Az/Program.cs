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
            string?[] jsonContentNotE = default;
            string?[] jsonContentNotW = default;
            string?[] jsonContentW = default;
            string?[] jsonContentE = default;
            if (File.Exists("Workers.json"))
            {
                jsonContentW = File.ReadAllLines("Workers.json");
                foreach (string line in jsonContentW)
                    Database.Workers.Add(JsonSerializer.Deserialize<Worker>(line));
            }
            if (File.Exists("Employers.json"))
            {
                jsonContentE = File.ReadAllLines("Employers.json");
                foreach (string line in jsonContentE)
                    Database.Employers.Add(JsonSerializer.Deserialize<Employer>(line));
            }
            if (File.Exists("NotficationsEmployer.json"))
            {
                jsonContentNotE = File.ReadAllLines("NotficationsEmployer.json");
                foreach (string line in jsonContentNotE)
                {
                    var notfic = JsonSerializer.Deserialize<Notfication>(line);
                    foreach (var em in Database.Employers)
                    {
                        if (em.Id == notfic.Id)
                            em.Notfications.Add(notfic);
                    }
                }
            }
            if (File.Exists("NotficationsWorker.json"))
            {
                jsonContentNotW = File.ReadAllLines("NotficationsWorker.json");
                foreach (string line in jsonContentNotW)
                {
                    var notfic = JsonSerializer.Deserialize<Notfication>(line);
                    foreach (var worker in Database.Workers)
                    {
                        if (worker.Id == notfic.Id)
                            worker.Notfications.Add(notfic);
                    }
                }
            }
        }
        static public void LogInWorker()
        {
            Console.Write("enter your name: ");
            var name = Console.ReadLine();
            Console.Write("enter your password: ");
            var password = Console.ReadLine();
            foreach (var worker in Database.Workers)
            {
                if (worker.Name == name && worker.Paswword == password)
                {
                    int temp = 1;
                    while (true)
                    {
                        Console.Clear();
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
                                        notfic.ShowNotfics(1);
                                Console.ReadLine();
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
                        em.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id, em.Id));
                        var JsonForm = JsonSerializer.Serialize(em.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("NotficationsEmployer.json", JsonForm); return;
                        FillData();
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
                        em.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id, em.Id));
                        var JsonForm = JsonSerializer.Serialize(em.Notfications[em.Notfications.Count - 1]);
                        File.AppendAllText("NotficationsEmployer.json", JsonForm); return;
                        FillData();
                    }
                }
            }
            else
                StartUp();

        }

        static void GuestFunc()
        {
            int start = 1;
            while (true)
            {
                Console.Clear();
                string[] choices = { "LokkEmployers", "LookWorkers", "Exit" };
                ColorSettings(choices, start);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (start == 1)
                            start = 3;
                        else
                            start--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (start == 3)
                            start = 1;
                        else
                            start++;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                        {
                            string JobKind = CommonJobs();
                            foreach (var em in Database.Employers)
                            {
                                if (em.CvEmployer.WorkArea == JobKind)
                                    em.CvEmployer.ShowInfo();
                            }
                            Console.ReadLine();
                        }
                        else if (start == 2)
                        {
                            string JobKind = CommonJobs();
                            foreach (var em in Database.Workers)
                            {
                                if (em.CvWorker.ProfessionKind == JobKind)
                                    em.CvWorker.showInfo();
                            }
                            Console.ReadLine();
                        }
                        else
                            StartUp();
                        break;
                }
            }

        }
        static void WorkerFunc()
        {
            int start = 1;
            while (true)
            {
                Console.Clear();
                string[] choices = { "LogIn", "Registration", "Exit" };
                ColorSettings(choices, start);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (start == 1)
                            start = 3;
                        else
                            start--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (start == 3)
                            start = 1;
                        else
                            start++;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                            LogInWorker();
                        else if (start == 2)
                            RegstrationWorker();
                        else
                            StartUp();
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
                string[] arr = new[] { "Programming", "IT", "Design" };
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
        again:
            try
            {

                Console.Write("enter Name: ");
                wk.Name = Console.ReadLine();
                Console.Write("enter Surname: ");
                wk.Surname = Console.ReadLine();
                Console.Write("work area\n: ");
                wk.CvWorker.ProfessionKind = CommonJobs();
                Console.Write("enter your work experince: ");
                wk.CvWorker.WorkExperince = int.Parse(Console.ReadLine());
                Console.WriteLine("wants amount: ");
                wk.CvWorker.WantsAmount = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Clear();
                goto again;
            }
        IncorrectGmail:
            Console.Write("enter gmail: ");
            wk.Gmail = Console.ReadLine();
            if (!wk.Gmail.Contains("@gmail.com"))
                goto IncorrectGmail;
            IncorrectDate:
            Console.WriteLine("birthday: ");
            try
            {
                wk.CvWorker.Birthdate = Convert.ToDateTime((Console.ReadLine()));
            }
            catch (Exception)
            {
                goto IncorrectDate;
            }
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
                try
                {
                    result = SmtpServerConnection.GmailVerify(wk.Gmail, VerifyCode);
                }
                catch (Exception)
                {
                    goto IncorrectGmail;
                }
                Console.Clear();
            } while (VerifyCode != result);
            var JsonForm = JsonSerializer.Serialize(wk);
            File.AppendAllText("Workers.json", JsonForm);
            FillData();
        }
        public static void RegstrationEmployer()
        {
            Employer em = new();
        again:
            try
            {

                Console.Write("enter Company Name: ");
                em.CompanyName = Console.ReadLine();
            IncorrectGmail:
                Console.Write("enter gmail: ");
                em.Gmail = Console.ReadLine();
                if (!em.Gmail.Contains("@gmail.com"))
                    goto IncorrectGmail;
                Console.Write("work area\n: ");
                em.CvEmployer.WorkArea = CommonJobs();
                Console.Write("enter period of experince: ");
                em.CvEmployer.RequiredWorkExperience = int.Parse(Console.ReadLine());
                Console.WriteLine("enter languages: ");
                em.CvEmployer.RequiredLanguage = Console.ReadLine();
                Console.WriteLine("enter password: ");
                em.password = Console.ReadLine();
                Console.WriteLine("enter salary: ");
                em.CvEmployer.Salary = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.Clear();
                goto again;
            }

            int VerifyCode = Random.Shared.Next(111111, 999999);
            int result = 0;
            try
            {
                result = SmtpServerConnection.GmailVerify(em.Gmail, VerifyCode);
            }
            catch (Exception)
            {
                goto again;
            }
            if (result != VerifyCode)
                throw new Exception("incorret verify code");
            var JsonForm = JsonSerializer.Serialize(em);
            File.AppendAllText("Employers.json", JsonForm);
            FillData();
        }
        static void EmployerFunc()
        {
            int start = 1;
            while (true)
            {
                Console.Clear();
                string[] choices = { "LogIn", "Registration", "Exit" };
                ColorSettings(choices, start);

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        if (start == 1)
                            start = 3;
                        else
                            start--;
                        break;
                    case ConsoleKey.DownArrow:
                        if (start == 3)
                            start = 1;
                        else
                            start++;
                        break;
                    case ConsoleKey.Enter:
                        if (start == 1)
                            LogInEmployer();
                        else if (start == 2)
                            RegstrationEmployer();
                        else
                            StartUp();
                        break;
                }
            }
        }
        static void AllJobSeekers(int ById)
        {
            foreach (var worker in Database.Workers)
                worker.CvWorker.showInfo();
            Console.Write("1 - invited\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of cv");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var worker in Database.Workers)
                {
                    if (worker.CvWorker.Id == IdPosting)
                    {
                        worker.Notfications.Add(new Notfication("invited", ById, worker.Id));
                        var JsonForm = JsonSerializer.Serialize(worker.Notfications[worker.Notfications.Count - 1]);
                        File.AppendAllText("NotficationsWorker.json", JsonForm); return;
                        FillData();
                    }
                }
            }
            else
                StartUp();
        }
        static void LogInEmployer()
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
                        Console.Clear();
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
                                        notfic.ShowNotfics(2);
                                    Console.ReadLine();
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
                    if (worker.CvWorker.Id == IdPosting)
                    {
                        worker.Notfications.Add(new Notfication("invited", ById, worker.Id));
                        var JsonForm = JsonSerializer.Serialize(worker.Notfications[worker.Notfications.Count - 1]);
                        File.AppendAllText("NotficationsWorker.json", JsonForm); return;
                        FillData();
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
                Console.Clear();
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

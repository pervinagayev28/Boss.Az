using Boss.Az.ManageDataBase;
using System.Text.Json;
using System;
using System.IO;
using System.Xml;

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
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.WriteLine(choices[i]);
                    Console.BackgroundColor = ConsoleColor.Black;

                    Console.ForegroundColor = ConsoleColor.White;
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
        static void Design()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            string bossAzAsciiArt = @"
███████╗██╗   ██╗██╗ ██████╗███████╗    ██╗███████╗      ██╗    ██╗
██╔════╝██║   ██║██║██╔════╝██╔════╝    ██║██╔════╝     ██╔╝   ██╔╝
███████╗██║   ██║██║██║     █████╗      ██║███████╗    ██╔╝   ██╔╝ 
╚════██║██║   ██║██║██║     ██╔══╝      ██║╚════██║   ██╔╝   ██╔╝  
███████║╚██████╔╝██║╚██████╗███████╗    ██║███████║  ██╔╝   ██╔╝   
╚══════╝ ╚═════╝ ╚═╝ ╚═════╝╚══════╝    ╚═╝╚══════╝  ╚═╝   ╚═╝    
";
            Console.WriteLine(bossAzAsciiArt);
            Console.ForegroundColor = ConsoleColor.White;

        }
        static void FillData()
        {
            string? jsonContentNotE = default;
            string? jsonContentNotW = default;
            string? jsonContentW = default;
            string? jsonContentE = default;

            if (File.Exists("Workers.json"))
            {
                jsonContentW = File.ReadAllText("Workers.json");
                Database.Workers = JsonSerializer.Deserialize<List<Worker>>(jsonContentW);
            }

            if (File.Exists("Employers.json"))
            {
                jsonContentE = File.ReadAllText("Employers.json");
                Database.Employers = (JsonSerializer.Deserialize<List<Employer>>(jsonContentE));
            }
            if (Database.Workers.Count != 0)
            {
                Worker.StaticId = Database.Workers.Count;
                if (File.Exists("WorkerCvId.txt"))
                    CvWorker.StaticId = int.Parse(File.ReadAllText("WorkerCvId.txt"));
            }

            if (Database.Employers.Count != 0)
            {
                Employer.StaticId = Database.Employers.Count;
                if (File.Exists("EmployerCvId.txt"))
                    CvEmployer.StaticIdCv = int.Parse(File.ReadAllText("EmployerCvId.txt"));
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
                        Console.Clear(); Design();
                        string[] choices = { "Notfications", "All Job Postings", "Filter Jobs", "Creat a new CV", "look all my CV", "Exit" };
                        ColorSettings(choices, temp);
                        var key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (temp == 6)
                                    temp = 1;
                                else
                                    temp++;
                                break;
                            case ConsoleKey.UpArrow:
                                if (temp == 1)
                                    temp = 6;
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
                                else if (temp == 4)
                                    CreatCvWorker(worker.Id);
                                else if (temp == 5)
                                {
                                    foreach (var item in worker.CvWorker)
                                        item.showInfo();
                                    Console.ReadLine();
                                }
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
            foreach (var emcv in Database.Employers)
            {
                foreach (var em in emcv.CvEmployer)
                {
                    if (em.WorkArea == profession)
                    {
                        emcv.showInfo();
                        em.ShowInfo();
                    }
                }
            }
            Console.Write("1 - request\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of Posting");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var emcv in Database.Employers)
                {
                    foreach (var em in emcv.CvEmployer)
                    {

                        if (em.Id == IdPosting)
                        {
                            emcv.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id, em.Id));
                            var JsonForm = JsonSerializer.Serialize(Database.Employers);
                            File.WriteAllText("Employers.json", JsonSerializer.Serialize(Database.Employers));
                            FillData();
                        }
                    }
                }
            }
            else
                StartUp();
        }
        static void AllJobPostings(int ById)
        {
            foreach (var emcv in Database.Employers)
            {
                foreach (var em in emcv.CvEmployer)
                {
                    emcv.showInfo();
                    em.ShowInfo();
                }
            }
            Console.Write("1 - request\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of Posting");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var emcv in Database.Employers)
                {
                    foreach (var em in emcv.CvEmployer)
                    {
                        emcv.Notfications.Add(new Notfication("request", Database.Workers.FirstOrDefault(w => w.Id == ById).Id, em.Id));
                        var JsonForm = JsonSerializer.Serialize(Database.Employers);
                        File.WriteAllText("Employers.json", JsonSerializer.Serialize(Database.Employers));
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
                Console.Clear(); Design();
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
                            foreach (var emcv in Database.Employers)
                            {
                                foreach (var em in emcv.CvEmployer)
                                {
                                    if (em.WorkArea == JobKind)
                                    {
                                        emcv.showInfo();
                                        em.ShowInfo();
                                    }
                                }
                            }
                            Console.ReadLine();
                        }
                        else if (start == 2)
                        {
                            string JobKind = CommonJobs();
                            foreach (var workers in Database.Workers)
                            {
                                foreach (var worker in workers.CvWorker)
                                {
                                    if (worker.ProfessionKind == JobKind)
                                    {
                                        workers.showInfo();
                                        worker.showInfo();
                                    }
                                }
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
                Console.Clear(); Design();
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
                Design();
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
        static void CreatCvWorker(int Id)
        {
            CvWorker cv = new CvWorker();
            Console.Write("work area\n: ");
            cv.ProfessionKind = CommonJobs();
        IncorrectInt:
            Console.Write("enter your work experince: ");
            try
            {
                cv.WorkExperince = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto IncorrectInt;
            }
        incorrectAmount:
            Console.Write("wants amount: ");
            try
            {
                cv.WantsAmount = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto incorrectAmount;
            }
        IncorrectDate:
            Console.WriteLine("birthday: ");
            try
            {
                cv.Birthdate = Convert.ToDateTime((Console.ReadLine()));
            }
            catch (Exception)
            {
                goto IncorrectDate;
            }
        IncorrectType:
            try
            {

                Console.WriteLine("marital status 1/2: ");
                cv.MaritalStatus = int.Parse(Console.ReadLine());
                Console.WriteLine("Gender 1/2: ");
                cv.Gender = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto IncorrectType;
            }
            using (StreamWriter st = new StreamWriter("WorkerCvId.txt", false))
            {
                st.WriteLine(cv.Id.ToString());
            }
            Database.Workers.FirstOrDefault(e => e.Id == Id).CvWorker.Add(cv);
            File.WriteAllText("Workers.json", JsonSerializer.Serialize(Database.Workers));
            FillData();
        }
        static public void RegstrationWorker()
        {
            Worker wk = new();

            Console.Write("enter Name: ");
            wk.Name = Console.ReadLine();
            Console.Write("enter Surname: ");
            wk.Surname = Console.ReadLine();
        IncorrectGmail:
            Console.Write("enter gmail: ");
            wk.Gmail = Console.ReadLine();
            if (!wk.Gmail.Contains("@gmail.com"))
                goto IncorrectGmail;
            int VerifyCode = Random.Shared.Next(111111, 999999); ;
            try
            {
                SmtpServerConnection.GmailVerify(wk.Gmail, VerifyCode);
            }
            catch (Exception)
            {
                goto IncorrectGmail;
            }
            Console.Clear();
        resendCode:
            Console.WriteLine("we have sent a verify code to your gmail");
            Console.Write("enter 6 number code (press 0 to exit): ");
            int result = 1;
            try
            {
                result = int.Parse(Console.ReadLine());

            }
            catch (Exception)
            {
                Console.WriteLine("incorrect verify code");
                Console.Clear();
                goto resendCode;
            }
            if (result == 0)
                WorkerFunc();
            if (VerifyCode != result)
            {
                Console.WriteLine("incorrect verify code");
                SmtpServerConnection.GmailVerify(wk.Gmail, VerifyCode);
                Console.Clear();
                goto resendCode;
            }
            Console.WriteLine("enter password: ");
            wk.Paswword = Console.ReadLine();
            Database.Workers.Add(wk);
            var JsonForm = JsonSerializer.Serialize(Database.Workers);
            File.WriteAllText("Workers.json", JsonForm);
            FillData();
        }
        static void CreatCvEmployer(int Id)
        {
            CvEmployer cv = new CvEmployer();
            Console.Write("work area\n: ");
            cv.WorkArea = CommonJobs();
        IncorrectExperience:
            Console.Write("enter period of experince: ");
            try
            {
                cv.RequiredWorkExperience = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto IncorrectExperience;
            }
            Console.WriteLine("enter languages: ");
            cv.RequiredLanguage = Console.ReadLine();
        IncorrectSalary:
            Console.WriteLine("enter salary: ");
            try
            {
                cv.Salary = int.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                goto IncorrectSalary;
            }
            using (StreamWriter st = new StreamWriter("EmployerCvId.txt", false))
            {
                st.WriteLine(cv.Id.ToString());
            }
            Database.Employers.FirstOrDefault(e => e.Id == Id).CvEmployer.Add(cv);
            File.WriteAllText("Employers.json", JsonSerializer.Serialize(Database.Employers));
            FillData();
        }

        public static void RegstrationEmployer()
        {
            Employer em = new();
            Console.Write("enter Company Name: ");
            em.CompanyName = Console.ReadLine();
        IncorrectGmail:
            Console.Write("enter gmail: ");
            em.Gmail = Console.ReadLine();
            if (!em.Gmail.Contains("@gmail.com"))
                goto IncorrectGmail;
            int VerifyCode = Random.Shared.Next(111111, 999999);
            try
            {
                SmtpServerConnection.GmailVerify(em.Gmail, VerifyCode);
            }
            catch (Exception)
            {
                Console.WriteLine("incorrect gmail");
                goto IncorrectGmail;
            }

        resendCode:
            Console.WriteLine("we have sent a verify code to your gmail");
            Console.Write("enter 6 number code: ");
            int result = int.Parse(Console.ReadLine());
            if (VerifyCode != result)
            {
                Console.WriteLine("incorrect verify code");
                SmtpServerConnection.GmailVerify(em.Gmail, VerifyCode);
                goto resendCode;
            }
            Console.WriteLine("enter password: ");
            em.password = Console.ReadLine();
            Database.Employers.Add(em);
            var JsonForm = JsonSerializer.Serialize(Database.Employers);
            File.WriteAllText("Employers.json", JsonForm);
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
            if (Database.Workers.Count == 0)
                StartUp();
            foreach (var workers in Database.Workers)
            {
                foreach (var worker in workers.CvWorker)
                {

                    workers.showInfo();
                    worker.showInfo();
                }
            }
            Console.Write("1 - invited\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of cv");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var workers in Database.Workers)
                {
                    foreach (var worker in workers.CvWorker)
                    {
                        if (worker.Id == IdPosting)
                        {
                            workers.Notfications.Add(new Notfication("invited", ById, worker.Id));
                            var JsonForm = JsonSerializer.Serialize(Database.Workers);
                            File.WriteAllText("Workers.json", JsonForm);
                            FillData();
                        }
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
                        Console.Clear(); Design();
                        string[] choices = { "Notfications", "All Job Seekers", "Filter Job Seekers", "Creat a New Posting", "look all my postings", "Exit" };
                        ColorSettings(choices, temp);
                        var key = Console.ReadKey();
                        switch (key.Key)
                        {
                            case ConsoleKey.DownArrow:
                                if (temp == 6)
                                    temp = 1;
                                else
                                    temp++;
                                break;
                            case ConsoleKey.UpArrow:
                                if (temp == 1)
                                    temp = 6;
                                else
                                    temp--;
                                break;
                            case ConsoleKey.Enter:
                                if (temp == 1)
                                {
                                    if (em.Notfications.Count != 0)
                                    {
                                        foreach (var notfic in em.Notfications)
                                            notfic.ShowNotfics(2);
                                        Console.ReadLine();
                                    }
                                }
                                else if (temp == 2)
                                    AllJobSeekers(em.Id);
                                else if (temp == 3)
                                    FilteredJobSeekers(em.Id);
                                else if (temp == 4)
                                    CreatCvEmployer(em.Id);
                                else if (temp == 5)
                                {
                                    em.showInfo();
                                    if (em.CvEmployer.Count != 0)
                                    {
                                        foreach (var cv in em.CvEmployer)
                                            cv.ShowInfo();
                                        Console.ReadLine();
                                    }
                                }
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
            if (Database.Workers.Count == 0)
                StartUp();
            string profession = CommonJobs();
            foreach (var workers in Database.Workers)
            {
                foreach (var worker in workers.CvWorker)
                {

                    if (worker.ProfessionKind == profession)
                    {
                        workers.showInfo();
                        worker.showInfo();
                    }
                }
            }
            Console.Write("1 - invited\n2 - to go back");
            if (int.Parse(Console.ReadLine()) == 1)
            {
                Console.WriteLine("enter Id of cv");
                int IdPosting = int.Parse(Console.ReadLine());
                foreach (var workers in Database.Workers)
                {
                    foreach (var worker in workers.CvWorker)
                    {
                        if (worker.Id == IdPosting)
                        {
                            workers.Notfications.Add(new Notfication("invited", ById, worker.Id));
                            var JsonForm = JsonSerializer.Serialize(Database.Workers);
                            File.WriteAllText("Workers.json", JsonForm);
                            FillData();
                        }
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
                Design();
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



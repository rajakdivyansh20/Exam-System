using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ProjectHandler Handler = new ProjectHandler();
            List<SubjectModel> SubjectList = Handler.GetSubjects();
            UserModel user = Session["user"] as UserModel;
            decimal totalScore = Handler.GetTS(user.Email);
            Session["totalscore"] = totalScore;
            return View(SubjectList);
        }
        public ActionResult Logout()
        {
            Session["user"] = null;
            return RedirectToAction("Login","User");
        }

        public ActionResult Quiz(int id=1)
        {
            ProjectHandler Handler = new ProjectHandler();
            List<QuestionModel> QuestionList = Handler.GetQuestions(id);
            AnswerModel ans = new AnswerModel();
            Session["list"] = QuestionList;

            return View(ans);
        }
        [HttpPost]
        public ActionResult Quiz(AnswerModel answers)
        {
            if (answers != null)
            {
                ProjectHandler Handler = new ProjectHandler();
                UserModel user = Session["user"] as UserModel;
                bool result = Handler.NewAnswers(user.Email, answers);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Calculate(int id , string choice)
        {
            ProjectHandler Handler = new ProjectHandler();
            bool ans = Handler.Validate(id,choice);
            if (ans)
            {
                UserModel user = Session["user"] as UserModel;
                ans = Handler.IncEnglish(user.Email);
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Logout");
            }
        }
        public ActionResult CheckScore()
        {
            ProjectHandler Handler = new ProjectHandler();
            UserModel user = Session["user"] as UserModel;
            ScoreModel myscore = Handler.CheckScore(user.Email);
            Session["myscore"] = myscore;
            //myscore.Name = user.Name;
            return View();
            //=-----------------------------------------------------------------------------------------------
        }
        public ActionResult Addmarks()
        {
            ProjectHandler Handler = new ProjectHandler();
            UserModel user = Session["user"] as UserModel;
            bool ans = Handler.IncEnglish(user.Email);
            return RedirectToAction("Index");
        }

   

    }
}
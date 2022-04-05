using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;

namespace Project.Models
{
    public class ProjectHandler
    {
        private SqlConnection connection;

        public void connect()
        {
            connection = new SqlConnection("Server=WGIN-DSK-053\\SQL19;Database=MyScore;Trusted_Connection=True;");
        }

        public UserModel Registration(UserModel user)
        {
            
            connect();
            SqlCommand command = new SqlCommand("Registration", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Name", user.Name);
            command.Parameters.AddWithValue("@Contact", user.Contact);
            command.Parameters.AddWithValue("@Password", user.Password);
            connection.Open();
            int RowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if (RowsAffected >= 1)
                return user;
            else
                return null;
        }

        public bool Validate(int id,string choice)
        {
            connect();
            string entered = "";
            SqlCommand command = new SqlCommand("GetAnswer", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@id", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                entered = reader.GetString(0);

            }
            connection.Close();
            if (entered == choice)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        internal ScoreModel CheckScore(string email)
        {
            ScoreModel myscore = new ScoreModel();
            connect();
            SqlCommand command = new SqlCommand("GetUserScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                myscore.English_Score = reader.GetInt32(0);
                myscore.Apti_Score = reader.GetInt32(1);
                myscore.Prog_Score = reader.GetInt32(2);
                myscore.Total_Score = reader.GetDecimal(3);

            }
            return myscore;
        }

        public bool IncEnglish(string email)
        {
            connect();
            SqlCommand command = new SqlCommand("SetEnglishScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            connection.Open();
            int RowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if (RowsAffected >= 1)
                return true;
            else
                return false;
        }

        public bool IncApti(string email)
        {
            connect();
            SqlCommand command = new SqlCommand("SetAptitudeScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            connection.Open();
            int RowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if (RowsAffected >= 1)
                return true;
            else
                return false;
        }

        public bool IncProg(string email)
        {
            connect();
            SqlCommand command = new SqlCommand("SetProgrammingScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            connection.Open();
            int RowsAffected = command.ExecuteNonQuery();
            connection.Close();
            if (RowsAffected >= 1)
                return true;
            else
                return false;
        }

        internal decimal GetTS(string email)
        {
            connect();
            decimal score = 0;
            SqlCommand command = new SqlCommand("CalcTotalScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);
            connection.Open();
            int i = command.ExecuteNonQuery();
            connection.Close();
            command = new SqlCommand("GetScore", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                score = reader.GetDecimal(0);

            }
            connection.Close();
            return score;
        }

        public UserModel Verify(UserModel user)
        {
            connect();
            SqlCommand command = new SqlCommand("StudentLogin", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", user.Email);
            command.Parameters.AddWithValue("@Password", user.Password);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                user.Name = reader.GetString(1);
                user.Contact = reader.GetString(2);
                
            }
            connection.Close();
            if (user.Name == null)
                return null;
            else
                return user;
        }
        //-------------------------------------------------Subject ------------------------------------------------
        public List<SubjectModel> GetSubjects()
        {
            List<SubjectModel> SubjectList = new List<SubjectModel>();
            connect();
            SqlCommand command = new SqlCommand("GetSubject", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                SubjectModel s = new SubjectModel();
                s.SubjectID = reader.GetInt32(0);
                s.SubjectName = reader.GetString(1);
                SubjectList.Add(s);
            }
            return SubjectList;

        }
        //------------------------------------------------- Questions ------------------------------------------------
        public List<QuestionModel> GetQuestions(int id)
        {
            List<QuestionModel> QuestionList = new List<QuestionModel>();
            connect();

            string subname = "";
            if (id == 1)
                subname = "English";

            else if (id == 2)
                subname = "Aptitude";
            else
                subname = "Programming";

            SqlCommand command = new SqlCommand("GetQuestion", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@SubName", subname);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                QuestionModel Q = new QuestionModel();
                Q.QID = reader.GetInt32(0);
                Q.QSubject = reader.GetString(1);
                Q.Question = reader.GetString(2);
                Q.A = reader.GetString(3);
                Q.B = reader.GetString(4);
                Q.C = reader.GetString(5);
                Q.D = reader.GetString(6);
                QuestionList.Add(Q);
            }
            return QuestionList;
        }

        //---------------------------------------------------- Answers -------------------------------------------------
        public bool NewAnswers(string email ,AnswerModel Ans)
        {
            AnswerModel ActualAnswers = GetActualAnswers(Ans.subject);
            connect();
            SqlCommand command = new SqlCommand("AddAnswers", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@email", email);
            command.Parameters.AddWithValue("@QSubject", Ans.subject);
            command.Parameters.AddWithValue("@Ans1", Ans.AnswerofQuestion1);
            command.Parameters.AddWithValue("@Ans2", Ans.AnswerofQuestion2);
            command.Parameters.AddWithValue("@Ans3", Ans.AnswerofQuestion3);
            command.Parameters.AddWithValue("@Ans4", Ans.AnswerofQuestion4);
            connection.Open();
            int RowsAffected = command.ExecuteNonQuery();
            connection.Close();
            
            if (RowsAffected >= 1)
            {
                MatchAnswers(Ans, ActualAnswers,email);
                return true;
            }
                
            else
                return false;
        }
        public void MatchAnswers(AnswerModel UserAnswer,AnswerModel ActualAnswer,string email)
        {
            if (UserAnswer.subject == "English")
            {
                if (UserAnswer.AnswerofQuestion1 == ActualAnswer.AnswerofQuestion1)
                {
                    bool res=IncEnglish(email);
                }
                if (UserAnswer.AnswerofQuestion2 == ActualAnswer.AnswerofQuestion2)
                {
                    bool res = IncEnglish(email);
                }
                if (UserAnswer.AnswerofQuestion3 == ActualAnswer.AnswerofQuestion3)
                {
                    bool res = IncEnglish(email);
                }
                if (UserAnswer.AnswerofQuestion4 == ActualAnswer.AnswerofQuestion4)
                {
                    bool res = IncEnglish(email);
                }
            }
            else if (UserAnswer.subject == "Aptitude")
            {
                if (UserAnswer.AnswerofQuestion1 == ActualAnswer.AnswerofQuestion1)
                {
                    bool res = IncApti(email);
                }
                if (UserAnswer.AnswerofQuestion2 == ActualAnswer.AnswerofQuestion2)
                {
                    bool res = IncApti(email);
                }
                if (UserAnswer.AnswerofQuestion3 == ActualAnswer.AnswerofQuestion3)
                {
                    bool res = IncApti(email);
                }
                if (UserAnswer.AnswerofQuestion4 == ActualAnswer.AnswerofQuestion4)
                {
                    bool res = IncApti(email);
                }
            }
            else
            {
                if (UserAnswer.AnswerofQuestion1 == ActualAnswer.AnswerofQuestion1)
                {
                    bool res = IncProg(email);
                }
                if (UserAnswer.AnswerofQuestion2 == ActualAnswer.AnswerofQuestion2)
                {
                    bool res = IncProg(email);
                }
                if (UserAnswer.AnswerofQuestion3 == ActualAnswer.AnswerofQuestion3)
                {
                    bool res = IncProg(email);
                }
                if (UserAnswer.AnswerofQuestion4 == ActualAnswer.AnswerofQuestion4)
                {
                    bool res = IncProg(email);
                }
            }
        }
        public List<AnswerModel> GetAnswers(string email)
        {
            List<AnswerModel> ansList = new List<AnswerModel>();
            connect();
            SqlCommand command = new SqlCommand("GetAnswers", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                AnswerModel ans = new AnswerModel();
                ans.subject = reader.GetString(0);
                ans.AnswerofQuestion1 = reader.GetString(1);
                ans.AnswerofQuestion2 = reader.GetString(2);
                ans.AnswerofQuestion3 = reader.GetString(3);
                ans.AnswerofQuestion4 = reader.GetString(4);
                ansList.Add(ans);
                AnswerModel ActAns = GetActualAnswers(ans.subject);
                ActAns.subject += " (Correct)";
                ansList.Add(ActAns);
            }

            return ansList;
        }

        public AnswerModel GetActualAnswers(string subject)
        {
            AnswerModel ans = new AnswerModel();
            ans.subject = subject;
            connect();
            SqlCommand command = new SqlCommand("GetAnswertoMatch", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Subject", subject);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                ans.AnswerofQuestion1 = reader.GetString(0);
            }
            if (reader.Read())
            {
                ans.AnswerofQuestion2 = reader.GetString(0);
            }
            if (reader.Read())
            {
                ans.AnswerofQuestion3 = reader.GetString(0);
            }
            if (reader.Read())
            {
                ans.AnswerofQuestion4 = reader.GetString(0);
            }
            return ans;
        }
    }



}
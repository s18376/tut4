using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Tutorial4.Models;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{
    private readonly string con = "Data Source = db - mssql; Initial Catalog = s18376; Integrated Security = True";

    [HttpGet("{id}")]
    public IActionResult getStudent(string id)
    {
        using (SqlConnection client = new SqlConnection(con))
        using (SqlCommand com = new SqlCommand())
        {
            com.Connection = client;
            com.CommandText = "Select Semester from Student left join Enrollment E on Student.IdEnrollment = E.IdEnrollment left join Studies S on E.IdStudy = S.IdStudy where Student.IndexNumber=@id;";
            com.Parameters.AddWithValue("id", id);
            client.Open();
            SqlDataReader dr = com.ExecuteReader();
            while (dr.Read())
            {
                Student st = new Student
                {
                    Semester = (int)dr["Semester"]
                };
                return Ok(st.Semester);
            }
            return NotFound("Not found");
        }
    }

    [HttpPost]
    public IActionResult CreateStudent()
    {
        Student s = new Student
        {
            IdStudent = 1,
            FirstName = "A",
            LastName = "B"
        };
        return Ok(s);
    }

    [HttpPut]
    public IActionResult putStudent()
    {
        Student s = new Student
        {
            IdStudent = 1,
            FirstName = "A",
            LastName = "B"
        };
        return Ok(s);
    }

    [HttpDelete]
    public IActionResult removeStudent(int id)
    {
        return Ok(id);
    }
}

using Lincoln.OnlineExam.Request;
using Lincoln.OnlineExam.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lincoln.OnlineExam.Repository
{
    public interface IUserRepository
    {
        LogInResponseDTO ValidateUser(LogInRequestDTO request, string Operation);
        int SaveStudent(StudentRequestDTO recordAttributer, string Operation);
        List<StudentResponseDTO> GetAllStudent();
        StudentResponseDTO SelectStudent(StudentRequestDTO recordAttributer);

        int SaveEmployee(EmployeeRequestDTO recordAttributer, string Operation);
        List<EmployeeResponseDTO> GetAllEmployee();
        EmployeeResponseDTO SelectEmployee(EmployeeRequestDTO recordAttributer);


    }
}

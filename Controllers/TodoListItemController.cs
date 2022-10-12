using TodoListApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Threading.Tasks;


namespace ApiTodoList.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        string data_source = AppConfig.getAppSettingKey("ConnectionStrings:CNS");

        [HttpPost]

        public ActionResult AddTask(TodoItem items)
        {

            SqlConnection connection = new SqlConnection(data_source);
            connection.Open();
            SqlCommand command = new SqlCommand("insert into Todo(Name,isComplete) values (@p1,@p2) ", connection);
            command.Parameters.AddWithValue("@p1", items.Name);
            command.Parameters.AddWithValue("@p2", items.IsCompleted);
            command.ExecuteNonQuery();
            connection.Close();

            return Ok("Task Add Succesfuly");

        }

        [HttpGet]
        public ActionResult ListTask()
        {
            try
            {
                SqlConnection connection = new SqlConnection(data_source);
                connection.Open();
                SqlCommand command = new SqlCommand("Select * from Todo", connection);

                SqlDataReader reader = command.ExecuteReader();
                List<TodoItem> todolist = new List<TodoItem>();

                while (reader.Read())
                {
                    TodoItem todo = new TodoItem();
                    todo.Id = Convert.ToInt32(reader["Id"]);
                    todo.Name = reader["Name"].ToString();
                    todo.IsCompleted = Convert.ToBoolean(reader["isComplete"]);
                    todolist.Add(todo);
                }
                connection.Close();
                return Ok(todolist);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



        [HttpGet("{id}")]
        public ActionResult DeleteTask(string Id)
        {


            try
            {
                SqlConnection connection = new SqlConnection(data_source);
                connection.Open();
                SqlCommand command = new SqlCommand("DELETE from Todo WHERE @p1=Id", connection);
                command.Parameters.AddWithValue("@p1", Id);
                command.ExecuteNonQuery();
                connection.Close();
                return Ok("Task Deleted Id:" + Id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPost]

        public ActionResult PutTask(int id, string name, bool isComplete)
        {

            SqlConnection connection = new SqlConnection(data_source);
            connection.Open();
            SqlCommand command = new SqlCommand("UPDATE Todo SET Name=@p1,isComplete=@p2 WHERE Id=@p3 ", connection);
            command.Parameters.AddWithValue("@p1", name);
            command.Parameters.AddWithValue("@p2", isComplete);
            command.Parameters.AddWithValue("@p3", id);
            command.ExecuteNonQuery();
            connection.Close();

            return Ok();

        }
    }


}




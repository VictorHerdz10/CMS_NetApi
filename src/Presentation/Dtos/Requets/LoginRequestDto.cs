using System.ComponentModel.DataAnnotations;

 namespace CMS_NetApi.Presentation.Dtos.Request
{

public class LoginRequetsDto
{
       public string Email { get; set; } = null!;

   
    public string Password { get; set; } = null!;
}

}
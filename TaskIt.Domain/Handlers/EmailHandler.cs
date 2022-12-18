using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using TaskItApi.Entities;
using TaskItApi.Handlers.Interfaces;

namespace TaskItApi.Handlers
{
    /// <summary>
    /// Handler for sending and creating emails
    /// </summary>
    public class EmailHandler: IEmailHandler
    {
    //    private readonly SmtpClient smtpClient;
    //    private readonly IConfiguration _configuration;
    //    private readonly IResourcesHelper _resourcesHelper;
    //    private readonly ITokenHandler _tokenHandler;

    //    private readonly string hostEmail;
    //    private readonly string portEmail;
    //    private readonly string userNameEmail;
    //    private readonly string passwordEmail;

    //    private readonly string noReplyEmail;

    //    public EmailHandler(IConfiguration config, IResourcesHelper resourcesHelper, ITokenHandler tokenHandler)
    //    {
    //        _configuration = config;
    //        _resourcesHelper = resourcesHelper;
    //        _tokenHandler = tokenHandler;

    //        hostEmail = _configuration["AppSettings:HostEmail"];
    //        portEmail = _configuration["AppSettings:PortEmail"];
    //        userNameEmail = _configuration["AppSettings:UsernameEmail"];
    //        passwordEmail = _configuration["AppSettings:PasswordEmail"];

    //        noReplyEmail = _configuration["AppSettings:NoReplayEmail"];

    //        smtpClient = new SmtpClient(this.hostEmail, Convert.ToInt32(this.portEmail))
    //        {
    //            Credentials = new NetworkCredential(this.userNameEmail, this.passwordEmail),
    //            EnableSsl = true ,
                
    //        };
    //    }

    //    /// <summary>
    //    /// Send invite email to user
    //    /// </summary>
    //    /// <param name="email">the email data</param>
    //    public void SendInviteEmail(EmailDTO email)
    //    {
    //       MailMessage mailMessage = new MailMessage(email.SendingAdrdress, email.RecievingAdrress, email.Subject, email.Message);
    //       mailMessage.IsBodyHtml = true;
    //       smtpClient.Send(mailMessage);
    //    }

    //    /// <summary>
    //    /// Create invite email
    //    /// </summary>
    //    /// <param name="recievingUser">User that recieves the invite</param>
    //    /// <param name="sendingUser">User that send the invite</param>
    //    /// <param name="group">The group where the reciever is invited for</param>
    //    /// <returns>The email data</returns>
    //    public EmailDTO CreateInviteEmail(User recievingUser, User sendingUser, Group group)
    //    {           
    //        string filePath = _resourcesHelper.GetInviteEmailTemplatePath();

    //        string emailDescription;
    //        using (StreamReader streamReader= new StreamReader(filePath))
    //        {
    //            emailDescription = streamReader.ReadToEnd();
    //        }

    //        string inviteUrl = BuildInviteURL(recievingUser, group);

    //        emailDescription = emailDescription.Replace("[usernameReciever]", recievingUser.Name);
    //        emailDescription = emailDescription.Replace("[usernameInviter]", sendingUser.Name);
    //        emailDescription = emailDescription.Replace("[groupName]", group.Name);
    //        emailDescription = emailDescription.Replace("[inviteLink]", inviteUrl);

    //        EmailDTO emailData = new EmailDTO()
    //        {
    //            RecievingAdrress = recievingUser.Email,
    //            SendingAdrdress = noReplyEmail,
    //            Subject = "TaskIt uitnodiging",
    //            Message = emailDescription
    //        };

    //        return emailData;
    //    }

    //    /// <summary>
    //    /// Build the invite url
    //    /// </summary>
    //    /// <param name="user">The user that recieves the invite</param>
    //    /// <param name="group">The group where the user is invited user</param>
    //    /// <returns>The invitation URL</returns>
    //    private string BuildInviteURL(User user, Group group)
    //    {
    //        string jwtToken = _tokenHandler.CreateInviteToken(user, group);            

    //        string apiBaseUrl = _configuration.GetSection("InviteEmailUrl").Value;
    //        UriBuilder uriBuilder = new UriBuilder(apiBaseUrl);

    //        uriBuilder.Path = Path.Combine(uriBuilder.Path, jwtToken);

    //        return uriBuilder.ToString();
    //    }
    }
}

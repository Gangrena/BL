using System.Web.Mvc;
using BetterLife.Domain.Entities;
using BetterLife.WebUi.ControllersLogic.MessageController;

namespace BetterLife.WebUi.Controllers
{
    public class MessageController : Controller
    {
        private MessageRepository repository = new MessageRepository();
        [Authorize]
        public ActionResult SendMessage(int personProfileId)
        {
            ViewBag.personProfileId = personProfileId;
            return View("SendMessage", new Message { SecPersonProfileId = personProfileId });
        }

        [HttpPost]
        [Authorize]
        public ActionResult SendMessage(Message message)
        {

            ViewBag.personProfileId = message.SecPersonProfileId;
            bool result = repository.SendMessage(message, User.Identity.Name);
            if (!result)
                ViewBag.Message = "Send failed";
            ViewBag.Message = "Send successful";
            return View("SendMessage", new Message { SecPersonProfileId = message.SecPersonProfileId });
        }
        [Authorize]
        public ActionResult AllNews()
        {           
            return View(repository.GetAll(User.Identity.Name));
        }
    }
}

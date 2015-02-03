using System.Web.Mvc;

namespace HBS.TimeTracking.Controllers
{
    [OutputCache(NoStore = true, Duration = 0, VaryByParam = "*")]
    public class NoCacheController : Controller
    {
        //[OutputCache(NoStore = true, Duration = 0, VaryByParam = "None")]

    }
}

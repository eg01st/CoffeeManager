using System.Collections.Generic;
using System.Linq;

namespace CoffeeManager.Api
{
    public class UserSessions
    {
        private static List<string> Sessions = new List<string>(); 

        public static void AddSession(int userId, string guid)
        {
            Sessions.Add(guid);
            using (var ctx = new CoffeeRoomEntities())
            {
                var session = new Session { Guid = guid, UserId = userId };
                ctx.Sessions.Add(session);
                ctx.SaveChanges();
            }
        }

        public static bool Contains(string guid)
        {
            if(!Sessions.Contains(guid))
            {
                using (var ctx = new CoffeeRoomEntities())
                {
                    return ctx.Sessions.Any(s => s.Guid == guid);
                }
            }
            return true;
        }
    }
}
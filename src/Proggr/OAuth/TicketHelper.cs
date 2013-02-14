using Proggr.Models;

namespace Proggr.OAuth
{
    public interface TicketHelper
    {
        void SetCookie( string data, string name, bool createPersistentCookie );
        void SetUserCookie( WebsiteUser user, bool createPersistentCookie );
        WebsiteUser GetUserFromCookie();
        void RemoveUserCookie();
        void SetAuthCookie( WebsiteUser user, bool createPersistentCookie );
    }
}
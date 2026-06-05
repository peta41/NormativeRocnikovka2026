using Normative.Models.Config;

namespace Normative.Extensions;

public static class UserExtensions
{
    public static IQueryable<User> ByUsernameOrEmail(this IQueryable<User> query, string username, string email)
    {
        if (string.IsNullOrWhiteSpace(username) && string.IsNullOrEmpty(email))
        {
            return query;
        }

        // todo: case sensitive
        // petr.docekal@chart industries.com != Petr.Docekal@chartindustries.com

        return query.Where(f => f.UserName == username || f.Email == email);
    }
}
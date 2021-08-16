using Microsoft.AspNetCore.Mvc;

namespace IdentityServerApi.Utils.Authorization
{
    public class MultiplePoliciesAuthorizeAttribute : TypeFilterAttribute
    {
        /// <summary>
        /// Usage
        /// 
        /// only require one of the policy from the given list
        /// [MultiplePoliciesAuthorize("AOSupportUser;AOAdminUser")]
        /// 
        /// require all the policies
        /// [MultiplePoliciesAuthorize("AOSupportUser;AOAdminUser", true)]
        /// 
        /// </summary>
        /// <param name="policies"></param>
        /// <param name="isAnd"></param>

        public MultiplePoliciesAuthorizeAttribute(string policies, bool isAnd = false) : base(typeof(MultiplePoliciesAuthorizeFilter))
        {
            Arguments = new object[] { policies, isAnd };
        }
    }
}

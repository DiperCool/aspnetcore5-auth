namespace Web.Models.Models
{
    public class ReturnTokens
    {
        public ReturnTokens(string _token, string _refreshtoken)
        {
            Token=_token;
            RefreshToken=_refreshtoken;
        }

        public ReturnTokens(){}

        public string Token{get;set;}
        public string RefreshToken{get;set;}
    }
}